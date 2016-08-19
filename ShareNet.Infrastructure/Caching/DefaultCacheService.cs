using System;
using System.Collections.Generic;
using WusNet.Infrastructure.WusNet;

namespace WusNet.Infrastructure.Caching
{
    /// <summary>
    /// 默认缓存服务
    /// </summary>
    [Serializable]
    public class DefaultCacheService:ICacheService
    {
        // Fields
        private ICache cache;
        private readonly Dictionary<CachingExpirationType, TimeSpan> cachingExpirationDictionary;
        /// <summary>
        /// 是否启用分布式缓存
        /// </summary>
        private bool enableDistributedCache;
        /// <summary>
        /// 本地缓存
        /// </summary>
        private ICache localCache;


        public DefaultCacheService(ICache cache, float cacheExpirationFactor)
            : this(cache, cache, cacheExpirationFactor, false)

        {
            
        }
        public DefaultCacheService(ICache cache,ICache localCache,float cacheExpirationFactor, bool enableDistributedCache)
        {
            this.cache = cache;
            this.localCache = localCache;
            this.enableDistributedCache = enableDistributedCache;
            this.cachingExpirationDictionary=new Dictionary<CachingExpirationType, TimeSpan>();
            this.cachingExpirationDictionary.Add(CachingExpirationType.Invariable, new TimeSpan(0, 0, (int)(86400f * cacheExpirationFactor)));
            this.cachingExpirationDictionary.Add(CachingExpirationType.Stable, new TimeSpan(0, 0, (int)(28800f * cacheExpirationFactor)));
            this.cachingExpirationDictionary.Add(CachingExpirationType.RelativelyStable, new TimeSpan(0, 0, (int)(7200f * cacheExpirationFactor)));
            this.cachingExpirationDictionary.Add(CachingExpirationType.UsualSingleObject, new TimeSpan(0, 0, (int)(600f * cacheExpirationFactor)));
            this.cachingExpirationDictionary.Add(CachingExpirationType.UsualObjectCollection, new TimeSpan(0, 0, (int)(300f * cacheExpirationFactor)));
            this.cachingExpirationDictionary.Add(CachingExpirationType.SingleObject, new TimeSpan(0, 0, (int)(180f * cacheExpirationFactor)));
            this.cachingExpirationDictionary.Add(CachingExpirationType.ObjectCollection, new TimeSpan(0, 0, (int)(180f * cacheExpirationFactor)));
        }

        public void Add(string cacheKey, object value, TimeSpan timeSpan)
        {
            this.cache.Add(cacheKey, value, timeSpan);

        }

        public void Add(string cacheKey, object value, CachingExpirationType cachingExpirationType)
        {
            this.Add(cacheKey, value, this.cachingExpirationDictionary[cachingExpirationType]);

        }

        public void Clear()
        {
            this.cache.Clear();
        }

        public object Get(string cacheKey)
        {
            object obj2 = null;
            if (this.enableDistributedCache)
            {
                obj2 = this.localCache.Get(cacheKey);
            }
            if (obj2 == null)
            {
                obj2 = this.cache.Get(cacheKey);
                if (this.enableDistributedCache)
                {
                    this.localCache.Add(cacheKey, obj2, this.cachingExpirationDictionary[CachingExpirationType.SingleObject]);
                }
            }
            return obj2;

        }

        public T Get<T>(string cacheKey) where T : class
        {
            object obj2 = this.Get(cacheKey);
            return obj2 as T;
        }

        public object GetFromFirstLevel(string cacheKey)
        {
            return this.cache.Get(cacheKey);

        }

        public T GetFromFirstLevel<T>(string cacheKey) where T : class
        {
            object fromFirstLevel = this.GetFromFirstLevel(cacheKey);
            if (fromFirstLevel != null)
            {
                return (fromFirstLevel as T);
            }
            return default(T);

        }

        public void MarkDeletion(string cacheKey, IEntity entity, CachingExpirationType cachingExpirationType)
        {
            entity.IsDeletedInDatabase = true;
            this.cache.MarkDeletion(cacheKey, entity, this.cachingExpirationDictionary[cachingExpirationType]);

        }

        public void Remove(string cacheKey)
        {
            this.cache.Remove(cacheKey);

        }

        public void Set(string cacheKey, object value, TimeSpan timeSpan)
        {
            this.cache.Set(cacheKey, value, timeSpan);

        }
        /// <summary>
        /// 添加或更新缓存
        /// </summary>
        /// <param name="cacheKey">缓存项标识</param>
        /// <param name="value">缓存项</param>
        /// <param name="cachingExpirationType">缓存期限类型</param>
        public void Set(string cacheKey, object value, CachingExpirationType cachingExpirationType)
        {
            this.Set(cacheKey, value, this.cachingExpirationDictionary[cachingExpirationType]);

        }

        // Properties
        public bool EnableDistributedCache
        {
            get
            {
                return this.enableDistributedCache;
            }
        }

    }
}
