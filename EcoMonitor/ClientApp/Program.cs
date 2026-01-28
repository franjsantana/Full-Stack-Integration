using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ClientApp;
using Blazored.LocalStorage;
using ClientApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configurar HttpClient para el servidor de EcoMonitor
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5011")
});

// Registrar servicios adicionales
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<StateService>();

await builder.Build().RunAsync();
