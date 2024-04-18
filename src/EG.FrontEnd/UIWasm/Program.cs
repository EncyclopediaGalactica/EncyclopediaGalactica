using Fluxor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;
using UIWasm;
using UIWasm.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Logging.AddConfiguration(
    builder.Configuration.GetSection("Logging"));

builder.Services
    .AddScoped<IModuleService, ModuleService>()
    .AddScoped<IScreenService, ScreenService>()
    .AddScoped<IUserService, UserService>()
    .AddScoped<IRoleService, RoleService>()
    .AddScoped<IDocumentService, DocumentService>()
    .AddScoped<IRelationTypeService, RelationTypeService>()
    .AddScoped<IApplicationService, ApplicationService>()
    ;

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddFluentUIComponents();
builder.Services.AddFluxor(options => options.ScanAssemblies(typeof(Program).Assembly));


await builder.Build().RunAsync();