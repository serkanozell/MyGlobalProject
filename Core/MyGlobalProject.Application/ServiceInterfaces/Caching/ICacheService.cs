﻿namespace MyGlobalProject.Application.ServiceInterfaces.Caching
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key) where T : class;
        Task<T> GetAsync<T>(string key, Func<Task<T>> factory) where T : class;
        Task SetAsync<T>(string key, T value) where T : class;
        Task RemoveAsync(string key);
        Task RemoveByPrefixAsync(string prefixKey);
        Task RemoveAllKeysAsync();

    }
}
