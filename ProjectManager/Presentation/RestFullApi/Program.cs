using DecoyabServices;
using Persistence.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Infrastructure.Extensions;
using Infrastructure.Middelwares;
using System.Text.Json.Serialization;
using PersistenceSeedworks.Extensions;
using Infrastructure.Filters.FilterActions;
using InfrastructureSeedworks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddControllers()
    .AddJsonOptions(option =>
    {
        // جلوگیری از حلقه های بی نهایت در مدل های Json
        option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// infrastructure services
builder.Services.ConfigureCors();

var connectionString =
    builder.Configuration.GetConnectionString("connection")!;

// DatabaseContext
builder.Services.AddDatabaseContextEntityFrameworkCore(connectionString);

// UnitOfWork
builder.Services.ConfigureUnitOfWork(builder.Configuration);

// Identity Settings
builder.Services.AddIdentityMicrosoft();

// Log Server
builder.Services
    .AddLogManagerServices<Persistence.DatabaseContext>(connectionString);

// auto mapper
builder.Services.AddAutoMapper(typeof(Infrastructure.Profiles.MappingProfile));

builder.Services.AddScoped<CheckEntityUserIdFilterAction>();
builder.Services.AddScoped<LoginFilterAction>();
builder.Services.AddScoped<CheckPhoneNumberFilterAction>();

builder.Services.AddScoped<IMessageService, MessageService>();

// builder.Services.AddScoped<ValidationRoleFilter>();

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
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);    
});

var app = builder.Build();

app.ConfigureExceptionHandler();

if (app.Environment.IsDevelopment())
{
    ServerSettings.DomainApiProjectManager = "";
    ServerSettings.DomainApiAttachmentManager = "";
    ServerSettings.DomainApiMarketPlace = "";
    ServerSettings.DomainAdmin = "";
    ServerSettings.DomainWeb = "";
}
else if (app.Environment.IsProduction())
{
    ServerSettings.DomainApiProjectManager = "";
    ServerSettings.DomainApiAttachmentManager = "";
    ServerSettings.DomainApiMarketPlace = "";
    ServerSettings.DomainAdmin = "";
    ServerSettings.DomainWeb = "";
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "مستندات API v1");
        c.DocumentTitle = "ProjectManager API V01";
        c.RoutePrefix = "swagger"; // برای اینکه با /swagger باز بشه
    });
}

// app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

// **********
using (var scope = app.Services.CreateScope())
{
    // var unitOfWork =
    //     scope.ServiceProvider.GetService(typeof(Persistence.IUnitOfWork)) as Persistence.IUnitOfWork;

    // await unitOfWork!.MigrateAsync();

    //Resolve ASP .NET Core Identity with DI help
    // var userManager =
    //     scope.ServiceProvider.GetService(typeof(UserManager<User>)) as UserManager<User>;
    //
    // var roleManager =
    //     scope.ServiceProvider.GetService(typeof(RoleManager<Role>)) as RoleManager<Role>;
    //
    // var logger =
    //     scope.ServiceProvider.GetService(typeof(Logging.ILogger<Program>)) as Logging.ILogger<Program>;
    //
    // var initialData =
    //     new Infrastructure.InitialData<Program>(    await initialData.CreateCustomerAndUserSystem();
    //         userManager, roleManager, builder.Configuration, unitOfWork, logger);
    //
    // await initialData.CreatePagesAsync();
    // await initialData.CreateRolesAsync();
    // await initialData.CreateTicketTypeAsync();
    // await initialData.CreateUserPriotryAsync();
    // await initialData.CreateStatusTicketsAsync();
    // await initialData.CreateReferenceTypesAsync();
    // await initialData.CreateReferenceTypesAsync();
    // await initialData.CreatePermissionTitleAsync();
    //
    // await initialData.CreatePermmisions();
    //
    // await initialData.RunQuery();
}
// **********

app.Run();