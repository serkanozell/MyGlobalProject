using Microsoft.Extensions.Caching.Distributed;
using MyGlobalProject.Application.ServiceInterfaces.Caching;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace MyGlobalProject.Infrastructure.Caching
{
    public class CacheService : ICacheService
    {
        private static ConcurrentDictionary<string, bool> CacheKeys = new();
        private readonly IDistributedCache _distributedCache;

        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T?> GetAsync<T>(string key) where T : class
        {
            string? cachedValue = await _distributedCache.GetStringAsync(key);

            if (cachedValue == null)
                return null;

            T? value = JsonConvert.DeserializeObject<T>(cachedValue);

            return value;
        }

        public async Task<T> GetAsync<T>(string key, Func<Task<T>> factory) where T : class
        {
            T? cachedValue = await GetAsync<T>(key);

            if (cachedValue is not null)
                return cachedValue;

            cachedValue = await factory();

            await SetAsync(key, cachedValue);

            return cachedValue;
        }

        public async Task RemoveAsync(string key)
        {
            await _distributedCache.RemoveAsync(key);

            CacheKeys.TryRemove(key, out bool _);
        }

        public async Task RemoveByPrefixAsync(string prefixKey)
        {
            //foreach (var key in CacheKeys.Keys)
            //{
            //    if (key.StartsWith(prefixKey))
            //    {
            //        await RemoveAsync(key);
            //    }
            //}
            IEnumerable<Task> tasks = CacheKeys
                                        .Keys
                                        .Where(k => k.StartsWith(prefixKey))
                                        .Select(k => RemoveAsync(k));

            await Task.WhenAll(tasks);
        }

        public async Task RemoveAllKeysAsync()
        {
            IEnumerable<Task> tasks = CacheKeys
                            .Keys
                            .Where(k => k.Any())
                            .Select(k => RemoveAsync(k));

            await Task.WhenAll(tasks);
        }

        public async Task SetAsync<T>(string key, T value) where T : class
        {
            string cacheValue = JsonConvert.SerializeObject(value);

            await _distributedCache.SetStringAsync(key, cacheValue);

            CacheKeys.TryAdd(key, false);
        }
    }
}
