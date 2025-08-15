using BlazorComponents.Services;
using InfrastructureSeedworks;
using Microsoft.Extensions.Logging;

namespace MauiAdmin;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

		builder.Services.AddMauiBlazorWebView();


		// Service that depends on the environment
		// var environment = "Development";
		// Configure environment-specific settings
		// if (environment == "Development")
		// {
		// 	ServerSettings.DomainApiProjectManager = "https://localhost:6061";
		// 	ServerSettings.DomainApiAttachmentManager = "https://localhost:9091";
		// 	ServerSettings.DomainApiMarketPlace = "https://localhost:5051";
		// 	ServerSettings.DomainAdmin = "https://admin.decoyab.com";
		// 	ServerSettings.DomainWeb = "https://decoyab.com";
		// }
		// else if (environment == "Production")
		// {
		// 	ServerSettings.DomainApiProjectManager = "https://ToolsA.decoyab.com";
		// 	ServerSettings.DomainApiAttachmentManager = "https://ToolsB.decoyab.com";
		// 	ServerSettings.DomainApiMarketPlace = "https://MarketplaceApi.decoyab.com";
		// 	ServerSettings.DomainAdmin = "https://admin.decoyab.com";
		// 	ServerSettings.DomainWeb = "https://decoyab.com";
		// }

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		// New
		builder.Services.AddScoped
			<Providers.CustomAuthenticationStateProvider>();

		builder.Services.AddScoped
			<Microsoft.AspNetCore.Components.Authorization.AuthenticationStateProvider>
			(current => current.GetRequiredService<Providers.CustomAuthenticationStateProvider>());


		builder.Services.AddScoped<RequestResultService>();

		builder.Services.AddScoped<BlazorComponents.Services.UrlQueryService>();

		builder.Services.AddScoped<Infrastructure.IntropClasses.CKEditorService>();
		builder.Services.AddScoped<Infrastructure.IntropClasses.BootstrapService>();

		builder.Services.AddScoped<HttpServices.Marketplace.ShopService>();
		builder.Services.AddScoped<HttpServices.Marketplace.ProductService>();
		builder.Services.AddScoped<HttpServices.Marketplace.CategoryService>();
		builder.Services.AddScoped<HttpServices.Marketplace.PageSettingService>();
		builder.Services.AddScoped<HttpServices.Marketplace.ProductTitleService>();
		builder.Services.AddScoped<HttpServices.Marketplace.SubSystemLocalService>();

		builder.Services.AddScoped<Infrastructure.Marketplace.PageState>();

		return builder.Build();
	}
}