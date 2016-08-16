using System;
using WusNet.Infrastructure.WusNet;

namespace WusNet.Infrastructure.Caching
{
    public interface ICacheService
    {
        // Methods
        void Add(string cacheKey, object value, TimeSpan timeSpan);
        void Add(string cacheKey, object value, CachingExpirationType cachingExpirationType);
        void Clear();
        object Get(string cacheKey);
        T Get<T>(string cacheKey) where T : class;
        object GetFromFirstLevel(string cacheKey);
        T GetFromFirstLevel<T>(string cacheKey) where T : class;
        void MarkDeletion(string cacheKey, IEntity entity, CachingExpirationType cachingExpirationType);
        void Remove(string cacheKey);
        void Set(string cacheKey, object value, TimeSpan timeSpan);
        void Set(string cacheKey, object value, CachingExpirationType cachingExpirationType);

        // Properties
        bool EnableDistributedCache { get; }

    }
}
