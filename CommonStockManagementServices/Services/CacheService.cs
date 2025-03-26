using Microsoft.Extensions.Caching.Memory;

namespace CommonStockManagementServices.Services
{
    public class CacheService(IMemoryCache cache)
    {
        private readonly IMemoryCache _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        private readonly HashSet<string> _keys = new HashSet<string>(); // Persistent across requests due to singleton lifetime

        public bool Get<T>(string key, out T value)
        {
            if (_cache.TryGetValue(key, out var cachedValue))
            {
                value = (T)cachedValue;
                return true;
            }

            value = default;
            return false;
        }

        public void Set<T>(string key, T value, TimeSpan duration)
        {
            _cache.Set(key, value, duration);
            _keys.Add(key);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
            _keys.Remove(key);
        }


        public List<KeyValuePair<string, object>> GetAllCache()
        {
            var cacheList = new List<KeyValuePair<string, object>>();
            foreach (var key in _keys)
            {
                if (_cache.TryGetValue(key, out var value))
                {
                    cacheList.Add(new KeyValuePair<string, object>(key, value));
                }
            }
            return cacheList;
        }

        public void RemoveAll()
        {
            foreach (var key in _keys.ToList()) // Iterate over a copy of the keys to avoid modification issues
            {
                _cache.Remove(key); // Remove from the memory cache
            }
            _keys.Clear(); // Clear the tracking collection
        }
    }
}