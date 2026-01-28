using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using ServerApp.Services;
using Shared.DTOs;
using ServerApp.Hubs;

var builder = WebApplication.CreateBuilder(args);

// ============================================
// CONFIGURACIÓN DE SERVICIOS (Contenedor de Dependencias)
// ============================================

// 1. SignalR: Motor para comunicación en tiempo real
builder.Services.AddSignalR();

// 2. Caché e Inyección de Dependencias
builder.Services.AddMemoryCache(); // Requerido por CacheService
builder.Services.AddSingleton<CacheService>();

// 3. Background Services: El simulador que genera datos en segundo plano
builder.Services.AddHostedService<TelemetrySimulator>();

// 4. Output Caching: Caché de respuesta HTTP a nivel de middleware
builder.Services.AddOutputCache(options =>
{
    // Política global de 60 segundos
    options.AddBasePolicy(builder => builder.Expire(TimeSpan.FromSeconds(60)));
    
    // Política específica para telemetría (se invalida cada 30s o varía por dispositivo)
    options.AddPolicy("TelemetryCache", builder => 
        builder.Expire(TimeSpan.FromSeconds(30))
               .SetVaryByQuery("deviceId"));
});

// 5. Compresión de Respuestas: Para optimizar el ancho de banda
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    // Añadimos compatibilidad con flujos de SignalR
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(["application/octet-stream"]);
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);

// 6. Configuración JSON: Estándares de la industria
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; // camelCase para JS
    options.SerializerOptions.WriteIndented = true; // Legible en desarrollo
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()); // Enums como strings
});

// 7. CORS Actualizado para SignalR
// IMPORTANTE: SignalR requiere AllowCredentials() y no permite "*" en Origins si se usa.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); 
    });
});

builder.Services.AddOpenApi();

var app = builder.Build();

// ============================================
// MIDDLEWARE PIPELINE (Orden de ejecución de peticiones)
// ============================================

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowBlazorClient");
app.UseResponseCompression();
app.UseOutputCache();

// ============================================
// ENDPOINTS (Definición de rutas)
// ============================================

// Mapear la ruta del Hub de SignalR
app.MapHub<NotificationHub>("/hubs/notifications");

// Endpoint de salud
app.MapGet("/", () => Results.Ok(new { status = "EcoMonitor Online", version = "1.0.0" }))
   .RequireCors("AllowBlazorClient");

// GET /api/devices: Lista de dispositivos usando caché en memoria (IMemoryCache)
app.MapGet("/api/devices", async (CacheService cache) =>
{
    return await cache.GetOrCreateAsync("devices_list", async () =>
    {
        // Simulamos un retraso como si fuera una base de datos
        await Task.Delay(50);
        return new List<DeviceDto>
        {
            new DeviceDto { Id = 1, Name = "Panel Solar Principal", Type = "Appliance", IsActive = true, Location = "Techo" },
            new DeviceDto { Id = 2, Name = "Aire Acondicionado Sala", Type = "HVAC", IsActive = true, Location = "Sala" },
            new DeviceDto { Id = 3, Name = "Iluminación Exterior", Type = "Lighting", IsActive = false, Location = "Jardín" }
        };
    });
})
.RequireCors("AllowBlazorClient")
.WithTags("Devices");

// GET /api/telemetry: Lectura actual por dispositivo usando OutputCache
app.MapGet("/api/telemetry", (int deviceId) =>
{
    // Generamos un dato aleatorio si no está en la caché de salida
    var reading = new EnergyReadingDto
    {
        DeviceId = deviceId,
        Watts = Random.Shared.Next(100, 1500),
        Amps = Random.Shared.Next(1, 10),
        EstimatedCost = Random.Shared.Next(5, 50) / 100m
    };

    return Results.Ok(reading);
})
.CacheOutput("TelemetryCache")
.RequireCors("AllowBlazorClient")
.WithTags("Telemetry");

// POST /api/cache/clear: Fuerza la limpieza de la caché de dispositivos
app.MapPost("/api/cache/clear", (CacheService cache) =>
{
    cache.Remove("devices_list");
    return Results.Ok(new { message = "Caché de dispositivos limpiada" });
})
.RequireCors("AllowBlazorClient")
.WithTags("Cache");

app.Run();
