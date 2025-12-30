// ========================================
// CONFIGURACIÓN DEL FRONTEND (Blazor WebAssembly)
// ========================================

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using frontend;

// WebAssembly permite ejecutar código C# directamente en el navegador
var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient
{
    // BaseAddress: URL base para peticiones relativas
    // En este caso, apunta al mismo servidor donde está alojado el frontend
    // Nota: En este proyecto usamos URLs absolutas (http://localhost:5074/products)
    // por lo que BaseAddress no se usa, pero es buena práctica configurarlo
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

await builder.Build().RunAsync();
