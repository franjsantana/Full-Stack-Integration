using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using ServerApp.Services;

var builder = WebApplication.CreateBuilder(args);

// ============================================
// CONFIGURACIÓN DE CACHÉ - Estrategia Multi-Nivel
// ============================================

// 1. Caché en Memoria (IMemoryCache)
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<CacheService>();

// 2. Response Caching Middleware
builder.Services.AddResponseCaching();

// 3. Output Caching con políticas personalizadas
builder.Services.AddOutputCache(options =>
{
    // Política por defecto: 60 segundos
    options.AddBasePolicy(builder => builder.Expire(TimeSpan.FromSeconds(60)));

    // Política específica para productos: 5 minutos
    options.AddPolicy("ProductsCache", builder =>
        builder.Expire(TimeSpan.FromMinutes(5))
               .SetVaryByQuery("category", "search")
               .SetVaryByHeader("Origin"));
});

// 4. Compresión de Respuestas (Brotli + Gzip)
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});

// Configurar niveles de compresión
builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.SmallestSize;
});

// ============================================
// CONFIGURACIÓN JSON
// ============================================

// Configurar JSON con estándares de la industria
builder.Services.ConfigureHttpJsonOptions(options =>
{
    // Usar camelCase para nombres de propiedades (estándar JSON)
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

    // Formato legible con indentación
    options.SerializerOptions.WriteIndented = true;

    // Ignorar propiedades nulas en la respuesta
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

    // Permitir lectura de números desde strings
    options.SerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;

    // Configuración de conversión de enums
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});


// ============================================
// CONFIGURACIÓN CORS
// ============================================

// Agregar CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.WithOrigins("http://localhost:5250", "https://localhost:7016", "http://127.0.0.1:5250", "https://127.0.0.1:7016")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .WithExposedHeaders("Cache-Control", "ETag");
    });
});

var app = builder.Build();

// ============================================
// MIDDLEWARE PIPELINE
// ============================================

// 1. Enrutamiento (explícito para CORS)
app.UseRouting();

// 2. CORS (antes de compresión para asegurar headers correctos)
app.UseCors("AllowBlazorClient");

// 3. Compresión
app.UseResponseCompression();

// 4. Response Caching
app.UseResponseCaching();

// 5. Output Caching
app.UseOutputCache();

// ============================================
// ENDPOINTS
// ============================================

// Health check endpoint
app.MapGet("/", () => Results.Ok(new { status = "Online", version = "1.0.0" }))
   .RequireCors("AllowBlazorClient");

// Endpoint con caché multi-nivel
app.MapGet("/api/productlist", async (CacheService cache, HttpContext context) =>
{
    const string cacheKey = "products_list";

    // Usar CacheService para caché en memoria (5 minutos)
    var products = await cache.GetOrCreateAsync(
        cacheKey,
        async () =>
        {
            // Simular obtención de datos (en producción vendría de base de datos)
            await Task.Delay(50); // Simular latencia de BD

            return new List<ProductDto>
            {
                new ProductDto
                {
                    Id = 1,
                    Name = "Laptop",
                    Price = 1200.50m,
                    Stock = 25,
                    Category = new CategoryDto { Id = 101, Name = "Electronics" }
                },
                new ProductDto
                {
                    Id = 2,
                    Name = "Headphones",
                    Price = 50.00m,
                    Stock = 100,
                    Category = new CategoryDto { Id = 102, Name = "Accessories" }
                }
            };
        },
        TimeSpan.FromMinutes(5)
    );

    // Agregar headers de caché HTTP
    context.Response.Headers.CacheControl = "public, max-age=300"; // 5 minutos
    context.Response.Headers.Vary = "Accept-Encoding";

    return Results.Ok(products);
})
.CacheOutput("ProductsCache") // Reactivado y configurado con VaryByHeader("Origin")
.RequireCors("AllowBlazorClient")
.WithName("GetProducts")
.WithTags("Products")
.Produces<List<ProductDto>>(StatusCodes.Status200OK)
.WithOpenApi();

// Endpoint para invalidar caché (útil para administración)
app.MapPost("/api/cache/clear", (CacheService cache) =>
{
    cache.Remove("products_list");
    return Results.Ok(new { message = "Caché invalidada exitosamente" });
})
.RequireCors("AllowBlazorClient")
.WithName("ClearCache")
.WithTags("Cache")
.Produces(StatusCodes.Status200OK)
.WithOpenApi();

app.Run();

// DTOs (Data Transfer Objects) - Modelos fuertemente tipados
public record ProductDto
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public decimal Price { get; init; }
    public int Stock { get; init; }
    public CategoryDto? Category { get; init; }
}

public record CategoryDto
{
    public int Id { get; init; }
    public required string Name { get; init; }
}


