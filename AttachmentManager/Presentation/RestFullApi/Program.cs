using System.Reflection;
using System.Text.Json.Serialization;
using AutoMapper;
using DecoyabServices;
using Domain.Base;
using Enums.SharedService;
using HttpServices.ProjectManager;
using Infrastructure.Extensions;
using Infrastructure.Middelwares;
using Infrastructure.Profiles;
using InfrastructureSeedworks;
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

// Identity Settings
// builder.Services.AddIdentityMicrosoft();

// Log Server
builder.Services.AddLogManagerServices<DatabaseContext>(connectionString);

// auto mapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// builder.Services.AddScoped<CheckEntityUserIdFilterAction>();

builder.Services.AddScoped<IMessageService, MessageService>();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "مستندات API v1");
        c.DocumentTitle = "AttachmentManager API V01";
        c.RoutePrefix = "swagger"; // برای اینکه با /swagger باز بشه
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
    var domains =
        BaseEntity.DoaminFinder(nameof(Domain));

    // insert to db if not exist ...
    await unitOfWork!.SubSystemLocalRepository.AddByNamesAsync(domains);

    await unitOfWork.SaveAsync();

    var subSystemService =
        new SubSystemService();

    var subSystemLocals =
        await unitOfWork.SubSystemLocalRepository.GetAllAsync();

    var subSystemViewModels =
        mapper!.Map<List<SubSystemResponseViewModel>>(subSystemLocals);

    // send to project manager : please run the project manager
    var result =
        await subSystemService
            .AddAsync(subSystemViewModels, ProjectType.AttachmentManager,
                ServerKeyConstant.Key);

    if (result.IsFailed)
    {
        // has error ...
        throw new Exception(string.Join(',', result.Errors));
    }
}
// **********

app.Run();