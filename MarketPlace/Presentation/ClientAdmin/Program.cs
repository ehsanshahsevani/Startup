// using Microsoft.AspNetCore.Components.Web;
// using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
// using ClientAdmin;
//
// var builder = WebAssemblyHostBuilder.CreateDefault(args);
// builder.RootComponents.Add<App>("#app");
// builder.RootComponents.Add<HeadOutlet>("head::after");
//
// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//
// await builder.Build().RunAsync();

// New
using BlazorComponents.Services;
using Blazored.LocalStorage;

// New
using Blazored.SessionStorage;
using InfrastructureSeedworks;

var builder =
    Microsoft.AspNetCore.Components.WebAssembly
        .Hosting.WebAssemblyHostBuilder.CreateDefault(args: args);

builder.RootComponents.Add<ClientAdmin.App>(selector: "#app");

builder.RootComponents.Add<Microsoft.AspNetCore
    .Components.Web.HeadOutlet>(selector: "head::after");

// Service that depends on the environment
var environment = builder.HostEnvironment.Environment;

// Configure environment-specific settings
if (environment == "Development")
{
    ServerSettings.DomainApiProjectManager = "https://localhost:6061";
    ServerSettings.DomainApiAttachmentManager = "https://localhost:9091";
    ServerSettings.DomainApiMarketPlace = "https://localhost:5051";
    ServerSettings.DomainAdmin = "https://admin.decoyab.com";
    ServerSettings.DomainWeb = "https://decoyab.com";
}
else if (environment == "Production")
{
    ServerSettings.DomainApiProjectManager = "https://ToolsA.decoyab.com";
    ServerSettings.DomainApiAttachmentManager = "https://ToolsB.decoyab.com";
    ServerSettings.DomainApiMarketPlace = "https://MarketplaceApi.decoyab.com";
    ServerSettings.DomainAdmin = "https://admin.decoyab.com";
    ServerSettings.DomainWeb = "https://decoyab.com";
}


builder.Services.AddOptions();

// New
builder.Services.AddBlazoredLocalStorage();

// New
builder.Services.AddBlazoredSessionStorage();

builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy(name: "CanBuy",
        configurePolicy: policy => policy.RequireClaim(claimType: "Over21"));

    options.AddPolicy(name: "CanDelete",
        configurePolicy: policy => policy.RequireRole(roles: "Administrator"));
});

// New
//builder.Services.AddScoped
//	<Microsoft.AspNetCore.Components.Authorization.AuthenticationStateProvider,
//	Providers.CustomAuthenticationStateProvider>();

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

builder.Services.AddScoped<Infrastructure.Marketplace.PageState>();

// automatic injection services from HttpServices Dll!
// Scrutor lib!
builder.Services.Scan(scan => scan
    .FromAssemblyOf<HttpServices.Marketplace.ShopService>()
    .AddClasses(classes => classes
        .Where(type =>
            typeof(HttpServiceSeedworks.HttpService).IsAssignableFrom(type)))
    .AsSelf()
    .WithScopedLifetime());

builder.Services.AddScoped
    (implementationFactory: current => new System.Net.Http.HttpClient
{
    BaseAddress = new System.Uri
        (uriString: builder.HostEnvironment.BaseAddress),
});

await builder.Build().RunAsync();
