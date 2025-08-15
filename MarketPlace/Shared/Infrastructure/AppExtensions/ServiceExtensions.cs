using System.Text;
using System.Data.Common;
using JwtSettings.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore; // Data Base
using Microsoft.AspNetCore.Identity; // Security
using Microsoft.IdentityModel.Tokens; // JWT
// using WebApplication.Server.Controllers;
using Microsoft.Extensions.Configuration;
// using Microsoft.AspNetCore.Mvc.Versioning; // set Version For Controller
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Persistence; // JWT

namespace Infrastructure.Extensions;

public static class ServiceExtensions
{
	public static void ConfigureCors(this IServiceCollection service)
		=>
		service.AddCors(option =>
		{
			option.AddPolicy("CorsPolicy", builder =>
			builder.AllowAnyOrigin()    // WithOrgin("https://shahsevaniehsan.ir")
				   .AllowAnyMethod()    // WithMethodes("get;", Post)
				   .AllowAnyHeader()    // WithHeaders("accept", "contenttype")
				   );
		});

	public static void ConfigureIISIntegration(this IServiceCollection services)
		=>
		services.Configure<IISOptions>(option =>
		{
		});

	// public static void ConfigureLoggerService(this IServiceCollection services)
	// {
	// 	//services.AddScoped
	// 	//	(serviceType: typeof(Logging.ILogger<>),
	// 	//	implementationType: typeof(Persistence.Log));
	//
	// 	services.AddScoped
	// 		(serviceType: typeof(Logging.ILogger<>),
	// 		implementationType: typeof(Logging.NLogAdapter<>));
	// }

	// configure Unit Of Work
	public static void ConfigureUnitOfWork(this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddScoped<Persistence.IUnitOfWork, UnitOfWork>(sp =>
		{
			string connectionString =
				configuration.GetSection(key: "ConnectionStrings")
				.GetSection(key: "connection").Value!;

			Persistence.Tools.Options options =
				new Persistence.Tools.Options(connectionString);
				
			Persistence.UnitOfWork unitOfWork =
				new Persistence.UnitOfWork(options: options);

			return unitOfWork;
		});
	}

	public static void ConfigureJWt(this IServiceCollection services, IConfiguration configuration)
	{
		var settings = new Jwt();
		configuration.GetSection(nameof(Jwt)).Bind(settings);

		byte[] key =
			Encoding.ASCII.GetBytes(s: settings.SecretKey!);

		services.AddAuthentication(option =>
		{
			option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
		.AddJwtBearer(option =>
		{
			option.RequireHttpsMetadata = true;
			option.SaveToken = true;
			option.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = false,
				ValidateAudience = false,
				ValidateIssuerSigningKey = true,

				IssuerSigningKey =
						new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key: key),

				ClockSkew = System.TimeSpan.Zero,

				//ValidateIssuer = false,
				//ValidateAudience = false,
				//ValidateLifetime = true,
				//ValidateIssuerSigningKey = true,
				//// ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
				//// ValidAudience = jwtSettings.GetSection("validAudience").Value,
			};
		});
	}

	// Microsoft.AspNetCore.Versioning Packege (Get Version)
	// NOTE: for use the versioning add to controller:
	/*
		 [ApiVersion("1.0")]
		 [Route("api/v{version:apiVersion}/[controller]")]

		 for off version : [ApiVersion("2.0", Deprecated = true)]
		 */

	//public static void ConfigureVersioning(this IServiceCollection services)
	//{
	//    services.AddApiVersioning(option =>
	//    {
	//        option.ReportApiVersions = true;
	//        option.AssumeDefaultVersionWhenUnspecified = true;

	//        option.DefaultApiVersion =
	//            new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);

	//        //option.ApiVersionReader = new HeaderApiVersionReader("api-version");

	//        //option.Conventions.Controller<CompanyController>().HasApiVersion(new ApiVersion(1, 0));

	//        //option.Conventions.Controller<Company2Controller>().HasDeprecatedApiVersion(new ApiVersion(2, 0));

	//    });
	//}

	// Formatter:
	//public static IMvcBuilder AddCustomCSVFormatter(this IMvcBuilder builder)
	//{
	//	var result =
	//		builder
	//		.AddMvcOptions(config => config.OutputFormatters.Add(new CsvOutputFormatter()));

	//	return result;
	//}
}
