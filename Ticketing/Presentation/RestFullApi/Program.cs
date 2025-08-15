using System.Reflection;
using System.Text.Json.Serialization;
using AutoMapper;
using DecoyabServices;
using Domain.Base;
using Enums.SharedService;
using HttpServices.ProjectManager;
using Infrastructure;
using Infrastructure.AppExtensions;
using Infrastructure.Filters.FilterActions;
using Infrastructure.Middelwares;
using Infrastructure.Profiles;
using InfrastructureSeedworks;
using InfrastructureSeedworks.ActionFilter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Persistence;
using Persistence.Settings;
using PersistenceSeedworks.Extensions;
using ViewModels.ProjectManager;
using BaseEntity = DomainSeedworks.BaseEntity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

builder.Services.AddControllers()
    .AddJsonOptions(option =>
    {
        // جلوگیری از حلقه های بی نهایت در مدل های Json
        option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// infrastructure services
builder.Services.ConfigureCors();

builder.Services.AddHttpContextAccessor();

var connectionString =
    builder.Configuration.GetConnectionString("connection")!;

// DatabaseContext
builder.Services.AddDatabaseContextEntityFrameworkCore(connectionString);

// UnitOfWork
builder.Services.ConfigureUnitOfWork(builder.Configuration);

// Log Server
builder.Services
    .AddLogManagerServices<DatabaseContext>(connectionString);

// auto mapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// inject automatic Filter Actions !
// lib: Scrutor
builder.Services.Scan(scan => scan
    .FromAssemblyOf<CheckPageSettingIdActionFilter>()
    .AddClasses(classes => classes
        .Where(type =>
            typeof(IBaseAsyncActionFilter).IsAssignableFrom(type) &&
            (type.Name.EndsWith("ActionFilter") || type.Name.EndsWith("ViewModelFilterAction"))))
    .AsSelf()
    .WithScopedLifetime());

// inject automatic MessageService !
// lib: Scrutor
builder.Services.Scan(scan => scan
    .FromAssemblyOf<IMessageService>()
    .AddClasses(classes => classes.AssignableTo<IMessageService>())
    .AsImplementedInterfaces()
    .WithScopedLifetime());

// inject automatic all services in services dll !
// lib: Scrutor
// builder.Services.Scan(scan => scan
//     .FromAssemblyOf<DocumentService>()
//     .AddClasses(classes =>
//         classes.Where(type =>
//             typeof(BaseMarketplaceService).IsAssignableFrom(type) &&
//             type != typeof(BaseMarketplaceService)))
//     .AsSelf()
//     .WithScopedLifetime());

builder.Services.AddHttpClient();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "مستندات API",
        Version = "v1",
        Description = "این مستندات برای تیم فرانت‌اند جهت استفاده از API ارائه شده است.",
        Contact = new OpenApiContact
        {
            Name = "پشتیبانی",
            Email = "ShahsevaniEhsan@gmail.com"
        }
    });

    // اضافه کردن پشتیبانی از توضیحات XML
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath, true);
});

var app = builder.Build();

app.ConfigureExceptionHandler();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     
//     app.UseSwaggerUI(c =>
//     {
//         c.SwaggerEndpoint("/swagger/v1/swagger.json", "مستندات API v1");
//         c.DocumentTitle = "MarketPlace API V01";
//         c.RoutePrefix = "swagger"; // برای اینکه با /swagger باز بشه
//     });
// }

if (app.Environment.IsDevelopment())
{
    ServerSettings.DomainApiProjectManager = "https://localhost:6061";
    ServerSettings.DomainApiAttachmentManager = "https://localhost:9091";
    ServerSettings.DomainApiMarketPlace = "https://localhost:5051";
    ServerSettings.DomainAdmin = "https://admin.decoyab.com";
    ServerSettings.DomainWeb = "https://decoyab.com";
}
else if (app.Environment.IsProduction())
{
    ServerSettings.DomainApiProjectManager = "https://ToolsA.decoyab.com";
    ServerSettings.DomainApiAttachmentManager = "https://ToolsB.decoyab.com";
    ServerSettings.DomainApiMarketPlace = "https://MarketplaceApi.decoyab.com";
    ServerSettings.DomainAdmin = "https://admin.decoyab.com";
    ServerSettings.DomainWeb = "https://decoyab.com";
}

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "مستندات API v1");
        c.DocumentTitle = "Ticketing API V01";
        c.RoutePrefix = "swagger";
    });
}


app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

// **********
using (var scope = app.Services.CreateScope())
{
    // create services unit of work
    var unitOfWork =
        scope.ServiceProvider
            .GetService(typeof(IUnitOfWork)) as UnitOfWork;

    // create services mapper
    var mapper =
        scope.ServiceProvider
            .GetService(typeof(IMapper)) as Mapper;

    // find all domains in Domain
    var domians =
        BaseEntity.DoaminFinder(nameof(Domain));

    // insert to db if not exist ...
    await unitOfWork!.SubSystemLocalRepository.AddByNamesAsync(domians);

    await unitOfWork.SaveAsync();

    var subSystemService =
        new SubSystemService();

    var subSystemLocals =
        await unitOfWork!.SubSystemLocalRepository.GetAllAsync();

    var subSystemViewModels =
        mapper!.Map<List<SubSystemResponseViewModel>>(subSystemLocals);

    // send to project manager : please run the project manager
    var result =
        await subSystemService
            .AddAsync(subSystemViewModels, ProjectType.Marketplace,
                ServerKeyConstant.Key);

    if (result.IsFailed)
        // has error ...
        throw new Exception(string.Join(',', result.Errors));
    // *************************************************************************************************
    // *************************************************************************************************
    // *************************************************************************************************
    // *************************************************************************************************
    // *************************************************************************************************

    // await unitOfWork!.MigrateAsync();

    var initialData =
        new InitialData(builder.Configuration, unitOfWork);

    await initialData.CreateStatusAsync();
    await initialData.CreateTicketSubjectAsync();
}
// **********

app.Run();