using Microsoft.Extensions.Caching.Memory;

namespace ServerApp.Services;

/// <summary>
/// Servicio de caché reutilizable que encapsula la lógica de IMemoryCache
/// </summary>
public class CacheService
{
    private readonly IMemoryCache _cache;
    private readonly ILogger<CacheService> _logger;

    public CacheService(IMemoryCache cache, ILogger<CacheService> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene un valor de la caché o lo crea si no existe
    /// </summary>
    /// <typeparam name="T">Tipo del valor a cachear</typeparam>
    /// <param name="key">Clave única para el valor en caché</param>
    /// <param name="factory">Función para crear el valor si no existe en caché</param>
    /// <param name="expiration">Tiempo de expiración (por defecto 5 minutos)</param>
    /// <returns>El valor cacheado o recién creado</returns>
    public async Task<T> GetOrCreateAsync<T>(
        string key,
        Func<Task<T>> factory,
        TimeSpan? expiration = null)
    {
        if (_cache.TryGetValue(key, out T? cachedValue) && cachedValue != null)
        {
            _logger.LogInformation("Cache HIT para la clave: {Key}", key);
            return cachedValue;
        }

        _logger.LogInformation("Cache MISS para la clave: {Key}. Creando nuevo valor...", key);

        var value = await factory();

        var cacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(5),
            Priority = CacheItemPriority.Normal
        };

        _cache.Set(key, value, cacheOptions);

        _logger.LogInformation("Valor cacheado con clave: {Key}, expiración: {Expiration}", 
            key, cacheOptions.AbsoluteExpirationRelativeToNow);

        return value;
    }

    /// <summary>
    /// Elimina un valor específico de la caché
    /// </summary>
    /// <param name="key">Clave del valor a eliminar</param>
    public void Remove(string key)
    {
        _cache.Remove(key);
        _logger.LogInformation("Valor eliminado de la caché: {Key}", key);
    }

    /// <summary>
    /// Verifica si una clave existe en la caché
    /// </summary>
    /// <param name="key">Clave a verificar</param>
    /// <returns>True si existe, false si no</returns>
    public bool Exists(string key)
    {
        return _cache.TryGetValue(key, out _);
    }
}
