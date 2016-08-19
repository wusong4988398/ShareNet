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
        /// <summary>
        /// 添加或更新缓存
        /// </summary>
        /// <param name="cacheKey">缓存项标识</param>
        /// <param name="value">缓存项</param>
        /// <param name="cachingExpirationType">缓存期限类型</param>
        void Set(string cacheKey, object value, CachingExpirationType cachingExpirationType);

        // Properties
        bool EnableDistributedCache { get; }

    }
}
