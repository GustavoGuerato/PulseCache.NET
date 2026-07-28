using System;
using System.Collections.Concurrent;
namespace PulseCache.Server.Storage
{
    public class StorageEngine
    {
        ConcurrentDictionary<string, CacheEntry> cacheEntries = new ConcurrentDictionary<string, CacheEntry>();
        public void addEntry(string key, CacheEntry entry)
        {
            cacheEntries[key] = entry;
        }

    }
}