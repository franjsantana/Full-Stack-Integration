// ========================================
// CONFIGURACIÓN DEL BACKEND (ASP.NET Core Web API)
// ========================================

// Este objeto se usa para configurar servicios y middleware
var builder = WebApplication.CreateBuilder(args);

// ========================================
// CONFIGURACIÓN DE CORS (Cross-Origin Resource Sharing)
// ========================================
// CORS es necesario porque el frontend (puerto 5269) y el backend (puerto 5074)
// están en diferentes puertos, lo que el navegador considera "orígenes diferentes"
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin()    // ⚠️ ADVERTENCIA: En producción, especifica los orígenes permitidos
                                 // Ejemplo: .WithOrigins("https://tu-dominio.com")                                 
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Construye la aplicación con la configuración definida
var app = builder.Build();

// Activa el middleware de CORS para que las políticas definidas arriba se apliquen
app.UseCors();

// Define un endpoint HTTP GET en la ruta "/products"
// Este endpoint devuelve una lista de productos en formato JSON
app.MapGet(
    "/products",  // Ruta del endpoint: http://localhost:5074/products
    () => Results.Ok(new[]
    {
        new { Id = 1, Name = "Laptop" },  // Producto 1
        new { Id = 2, Name = "Phone" }    // Producto 2
    })
    // Results.Ok() devuelve un HTTP 200 (éxito) con el contenido en JSON
    // En una aplicación real, estos datos vendrían de una base de datos
);

app.Run();
