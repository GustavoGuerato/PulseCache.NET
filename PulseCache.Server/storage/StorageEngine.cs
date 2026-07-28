using System.Collections.Concurrent;

namespace PulseCache.Server.Storage;

public class StorageEngine
{
    private readonly ConcurrentDictionary<string, CacheEntry> cacheEntries = new();

    public void Set(string key, byte[] value, TimeSpan? ttl = null)
    {
        DateTime? expiration = ttl.HasValue
            ? DateTime.UtcNow.Add(ttl.Value)
            : null;

        var entry = new CacheEntry(value, expiration);

        cacheEntries[key] = entry;
    }
    public bool TryGet(string key, out byte[]? value)
    {
        value = null;

        if (!cacheEntries.TryGetValue(key, out var entry))
            return false;


        if (entry.Expiration.HasValue &&
            entry.Expiration <= DateTime.UtcNow)
        {
            cacheEntries.TryRemove(key, out _);
            return false;
        }


        value = entry.Value;
        return true;
    }

    public bool Delete(string key)
    {
        return cacheEntries.TryRemove(key, out _);
    }
}
