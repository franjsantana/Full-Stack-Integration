using Microsoft.Extensions.Caching.Memory;

namespace ServerApp.Services;

/// <summary>
/// Servicio centralizado para gestionar la caché en memoria (IMemoryCache).
/// </summary>
public class CacheService(IMemoryCache cache)
{
    private readonly IMemoryCache _cache = cache;

    /// <summary>
    /// Obtiene un elemento de la caché o lo crea si no existe (Estrategia Get-or-Create).
    /// </summary>
    /// <typeparam name="T">Tipo de dato a almacenar.</typeparam>
    /// <param name="key">Clave única para identificar el recurso.</param>
    /// <param name="createItem">Función asíncrona que genera el dato si no está en caché.</param>
    /// <param name="expiration">Tiempo de vida opcional (por defecto 5 min).</param>
    /// <returns>El elemento solicitado.</returns>
    public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> createItem, TimeSpan? expiration = null)
    {
        // Intentar obtener el valor de la caché
        if (!_cache.TryGetValue(key, out T? cacheEntry))
        {
            // Si no existe, ejecutar la función para crear el item (ej: consulta a BD)
            cacheEntry = await createItem();

            // Configurar opciones de expiración
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(expiration ?? TimeSpan.FromMinutes(5));

            // Guardar en caché con la clave indicada
            _cache.Set(key, cacheEntry, cacheEntryOptions);
        }

        return cacheEntry!;
    }

    /// <summary>
    /// Elimina un elemento específico de la caché para forzar su actualización.
    /// </summary>
    /// <param name="key">Clave del elemento a eliminar.</param>
    public void Remove(string key)
    {
        _cache.Remove(key);
    }
}
