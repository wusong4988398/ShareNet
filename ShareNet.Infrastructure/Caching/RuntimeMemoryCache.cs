﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace WusNet.Infrastructure.Caching
{
    /// <summary>
    /// 运行内存缓存
    /// </summary>
    public class RuntimeMemoryCache:ICache
    {
        // Fields
        private readonly MemoryCache _cache = MemoryCache.Default;
        /// <summary>
        /// 新增缓存(带)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="timeSpan"></param>
        public void Add(string key, object value, TimeSpan timeSpan)
        {
            if (!string.IsNullOrEmpty(key) && (value != null))
            {
                this._cache.Add(key, value, DateTimeOffset.Now.Add(timeSpan), null);
            }

        }

        public void AddWithFileDependency(string key, object value, string fullFileNameOfFileDependency)
        {
            if (!string.IsNullOrEmpty(key) && (value != null))
            {
                CacheItemPolicy policy = new CacheItemPolicy
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddMonths(1)
                };
                policy.ChangeMonitors.Add(new HostFileChangeMonitor(new List<string> { fullFileNameOfFileDependency }));
                this._cache.Add(key, value, policy, null);
            }

        }
        /// <summary>
        /// 清空缓存
        /// </summary>
        public void Clear()
        {
            foreach (string str in this._cache.AsParallel<KeyValuePair<string, object>>().ToDictionary<KeyValuePair<string, object>, string>(a => a.Key).Keys)
            {
                this._cache.Remove(str, null);
            }

        }

        public object Get(string cacheKey)
        {
            return this._cache[cacheKey];

        }

        public void MarkDeletion(string key, object value, TimeSpan timeSpan)
        {
            this.Remove(key);

        }

        public void Remove(string cacheKey)
        {
            this._cache.Remove(cacheKey, null);

        }

        public void Set(string key, object value, TimeSpan timeSpan)
        {
            this._cache.Set(key, value, DateTimeOffset.Now.Add(timeSpan), null);

        }
    }
}
