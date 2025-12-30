using System;
using Microsoft.Extensions.Caching.Memory;

public class CacheService
{
    private readonly IMemoryCache _cache; // Interface for caching

    public CacheService(IMemoryCache cache) // Constructor injection
    {
        _cache = cache;
    }

    public T GetOrCreate<T>(string key, Func<T> createItem, TimeSpan expiration)
    {
        if (!_cache.TryGetValue(key, out T cachedEntry))
        {
            cachedEntry = createItem();
            _cache.Set(key, cachedEntry, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = expiration });
        }
        return cachedEntry;
    }
}

/*
Implement a service for managing cached data:
- Define a caching service class using IMemoryCache to store and retrieve frequently accessed data.
*/