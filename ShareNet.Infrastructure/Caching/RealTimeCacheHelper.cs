using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using WusNet.Infrastructure.WusNet;

namespace WusNet.Infrastructure.Caching
{
    [Serializable]
    public class RealTimeCacheHelper
    {
        // Fields
        private ConcurrentDictionary<string, ConcurrentDictionary<int, int>> areaVersionDictionary = new ConcurrentDictionary<string, ConcurrentDictionary<int, int>>();
        private ConcurrentDictionary<object, int> entityVersionDictionary = new ConcurrentDictionary<object, int>();
        private int globalVersion;

        // Methods
        public RealTimeCacheHelper(bool enableCache, string typeHashID)
        {
            this.EnableCache = enableCache;
            this.TypeHashID = typeHashID;
        }

        public int GetAreaVersion(string propertyName, object propertyValue)
        {
            int num = 0;
            if (!string.IsNullOrEmpty(propertyName))
            {
                ConcurrentDictionary<int, int> dictionary;
                propertyName = propertyName.ToLower();
                if (this.areaVersionDictionary.TryGetValue(propertyName, out dictionary))
                {
                    dictionary.TryGetValue(propertyValue.GetHashCode(), out num);
                }
            }
            return num;
        }
        /// <summary>
        /// 根据实体获取缓存key
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public string GetCacheKeyOfEntity(object primaryKey)
        {
            if (DIContainer.Resolve<ICacheService>().EnableDistributedCache)
            {
                return string.Concat(new object[] { this.TypeHashID, ":", primaryKey, ":", this.GetEntityVersion(primaryKey) });
            }
            return (this.TypeHashID + ":" + primaryKey);
        }

        public string GetCacheKeyOfEntityBody(object primaryKey)
        {
            if (DIContainer.Resolve<ICacheService>().EnableDistributedCache)
            {
                return string.Concat(new object[] { this.TypeHashID, ":B-", primaryKey, ":", this.GetEntityVersion(primaryKey) });
            }
            return (this.TypeHashID + ":B-" + primaryKey);
        }
        /// <summary>
        /// 及时缓存key
        /// </summary>
        /// <param name="typeHashID"></param>
        /// <returns></returns>
        internal static string GetCacheKeyOfTimelinessHelper(string typeHashID)
        {
            return ("CacheTimelinessHelper:" + typeHashID);
        }

        public int GetEntityVersion(object primaryKey)
        {
            int num = 0;
            this.entityVersionDictionary.TryGetValue(primaryKey, out num);
            return num;
        }

        public int GetGlobalVersion()
        {
            return this.globalVersion;
        }

        public string GetListCacheKeyPrefix(CacheVersionType cacheVersionType)
        {
            return this.GetListCacheKeyPrefix(cacheVersionType, null, null);
        }

        public string GetListCacheKeyPrefix(IListCacheSetting cacheVersionSetting)
        {
            StringBuilder builder = new StringBuilder(this.TypeHashID);
            builder.Append("-L:");
            switch (cacheVersionSetting.CacheVersionType)
            {
                case CacheVersionType.GlobalVersion:
                    builder.AppendFormat("{0}:", this.GetGlobalVersion());
                    break;

                case CacheVersionType.AreaVersion:
                    builder.AppendFormat("{0}-{1}-{2}:", cacheVersionSetting.AreaCachePropertyName, cacheVersionSetting.AreaCachePropertyValue.ToString(), this.GetAreaVersion(cacheVersionSetting.AreaCachePropertyName, cacheVersionSetting.AreaCachePropertyValue));
                    break;
            }
            return builder.ToString();
        }

        public string GetListCacheKeyPrefix(CacheVersionType cacheVersionType, string areaCachePropertyName, object areaCachePropertyValue)
        {
            StringBuilder builder = new StringBuilder(this.TypeHashID);
            builder.Append("-L:");
            switch (cacheVersionType)
            {
                case CacheVersionType.GlobalVersion:
                    builder.AppendFormat("{0}:", this.GetGlobalVersion());
                    break;

                case CacheVersionType.AreaVersion:
                    builder.AppendFormat("{0}-{1}-{2}:", areaCachePropertyName, areaCachePropertyValue, this.GetAreaVersion(areaCachePropertyName, areaCachePropertyValue));
                    break;
            }
            return builder.ToString();
        }

        public void IncreaseAreaVersion(string propertyName, IEnumerable<object> propertyValues)
        {
            this.IncreaseAreaVersion(propertyName, propertyValues, true);
        }

        public void IncreaseAreaVersion(string propertyName, object propertyValue)
        {
            if (propertyValue != null)
            {
                this.IncreaseAreaVersion(propertyName, new object[] { propertyValue }, true);
            }
        }
        /// <summary>
        /// 新增区域版本
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="propertyValues"></param>
        /// <param name="raiseChangeEvent"></param>
        private void IncreaseAreaVersion(string propertyName, IEnumerable<object> propertyValues, bool raiseChangeEvent)
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                ConcurrentDictionary<int, int> dictionary;
                propertyName = propertyName.ToLower();
                int num = 0;
                if (!this.areaVersionDictionary.TryGetValue(propertyName, out dictionary))
                {
                    this.areaVersionDictionary[propertyName] = new ConcurrentDictionary<int, int>();
                    dictionary = this.areaVersionDictionary[propertyName];
                }
                foreach (object obj2 in propertyValues)
                {
                    int hashCode = obj2.GetHashCode();
                    if (dictionary.TryGetValue(hashCode, out num))
                    {
                        num++;
                    }
                    else
                    {
                        num = 1;
                    }
                    dictionary[hashCode] = num;
                }
                if (raiseChangeEvent)
                {
                    this.OnChanged();
                }
            }
        }
        /// <summary>
        /// 增加实体缓存版本
        /// </summary>
        /// <param name="entityId"></param>
        public void IncreaseEntityCacheVersion(object entityId)
        {
            if (DIContainer.Resolve<ICacheService>().EnableDistributedCache)
            {
                int num;
                if (this.entityVersionDictionary.TryGetValue(entityId, out num))
                {
                    num++;
                }
                else
                {
                    num = 1;
                }
                this.entityVersionDictionary[entityId] = num;
                this.OnChanged();
            }
        }

        public void IncreaseGlobalVersion()
        {
            this.globalVersion++;
        }
        /// <summary>
        /// 新增列表缓存版本
        /// </summary>
        /// <param name="entity"></param>
        public void IncreaseListCacheVersion(IEntity entity)
        {
            if (this.PropertiesOfArea != null)
            {
                foreach (PropertyInfo info in this.PropertiesOfArea)
                {
                    object obj2 = info.GetValue(entity, null);
                    if (obj2 != null)
                    {
                        this.IncreaseAreaVersion(info.Name.ToLower(), new object[] { obj2 }, false);
                    }
                }
            }
            this.IncreaseGlobalVersion();
            this.OnChanged();
        }

        public void MarkDeletion(IEntity entity)
        {
            ICacheService service = DIContainer.Resolve<ICacheService>();
            if (this.EnableCache)
            {
                service.MarkDeletion(this.GetCacheKeyOfEntity(entity.EntityId), entity, CachingExpirationType.SingleObject);
            }
        }

        private void OnChanged()
        {
            ICacheService service = DIContainer.Resolve<ICacheService>();
            if (service.EnableDistributedCache)
            {
                service.Set(GetCacheKeyOfTimelinessHelper(this.TypeHashID), this, CachingExpirationType.Invariable);
            }
        }

        // Properties
        public CachingExpirationType CachingExpirationType { get; set; }

        public bool EnableCache { get; private set; }

        public IEnumerable<PropertyInfo> PropertiesOfArea { get; set; }

        public PropertyInfo PropertyNameOfBody { get; set; }

        public string TypeHashID { get; private set; }



    }
}
