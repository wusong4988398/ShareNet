using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using ShareNet.Infrastructure.Utilities;
using WusNet.Infrastructure.Caching;

namespace WusNet.Infrastructure.WusNet
{
    [Serializable]
    public class EntityData
    {
        // Fields
        private static ConcurrentDictionary<Type, EntityData> entityDatas = new ConcurrentDictionary<Type, EntityData>();
        private static readonly object lockObject = new object();
        private RealTimeCacheHelper realTimeCacheHelper;

        public EntityData(Type t)
        {
            this.Type = t;
            this.TypeHashID = EncryptionUtility.MD5_16(t.FullName);
            ICacheService service = DIContainer.Resolve<ICacheService>();

            RealTimeCacheHelper helper = this.ParseCacheTimelinessHelper(t);
            if (service.EnableDistributedCache)
            {
                service.Set(RealTimeCacheHelper.GetCacheKeyOfTimelinessHelper(this.TypeHashID), helper, CachingExpirationType.Invariable);

            }
            else
            {
                this.realTimeCacheHelper = helper;

            }
        }

        public static EntityData ForType(Type t)
        {
            EntityData data;
            if (!entityDatas.TryGetValue(t,out data)&&(data==null))
            {
                data=new EntityData(t);
                entityDatas[t] = data;

            }
            return data;
        }

        //属性
        public RealTimeCacheHelper RealTimeCacheHelper
        {
            get
            {
                ICacheService service = DIContainer.Resolve<ICacheService>();
                if (!service.EnableDistributedCache)
                {
                    return this.RealTimeCacheHelper;
                }
                string cacheKeyOfTimelinessHelper = RealTimeCacheHelper.GetCacheKeyOfTimelinessHelper(this.TypeHashID);

                RealTimeCacheHelper fromFirstLevel =
                    service.GetFromFirstLevel<RealTimeCacheHelper>(cacheKeyOfTimelinessHelper);
                if (fromFirstLevel==null)
                {
                     fromFirstLevel = this.ParseCacheTimelinessHelper(this.Type);
                     service.Set(cacheKeyOfTimelinessHelper,fromFirstLevel,CachingExpirationType.Invariable);
                }
                return fromFirstLevel;

            }
        }
        
        private RealTimeCacheHelper ParseCacheTimelinessHelper(Type type)
        {
            RealTimeCacheHelper helper = null;
            object[] customAttributes=  type.GetCustomAttributes(typeof (CacheSettingAttribute), true);
            if (customAttributes.Length>0)
            {
                CacheSettingAttribute attribute=customAttributes[0] as CacheSettingAttribute;
                if (attribute!=null)
                {
                    helper=new RealTimeCacheHelper(attribute.EnableCache,this.TypeHashID);
                    if (attribute.EnableCache)
                    {
                        switch (attribute.ExpirationPolicy)
                        {
                                case EntityCacheExpirationPolicies.Stable:
                                helper.CachingExpirationType=CachingExpirationType.Stable;
                                break;
                                case EntityCacheExpirationPolicies.Usual:
                                helper.CachingExpirationType = CachingExpirationType.UsualSingleObject;
                                break;
                                default:
                                helper.CachingExpirationType = CachingExpirationType.SingleObject;
                                break;
                        }
                        List<PropertyInfo> list=new List<PropertyInfo>();
                        if (!string.IsNullOrEmpty(attribute.PropertyNamesOfArea))
                        {
                            foreach (var str in attribute.PropertyNamesOfArea.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries))
                            {
                                PropertyInfo property = type.GetProperty(str);
                                if (property!=null)
                                {
                                    list.Add(property);
                                }
                            }
                       
                        }
                        helper.PropertiesOfArea = list;
                        if (!string.IsNullOrEmpty(attribute.PropertyNameOfBody))
                        {
                            PropertyInfo info2 = type.GetProperty(attribute.PropertyNameOfBody);
                            helper.PropertyNameOfBody = info2;

                        }

                    }
                }
            }
            if (helper==null)
            {
                helper = new RealTimeCacheHelper(true, this.TypeHashID);

            }
            return helper;

        }

        public Type Type { get; private set; }
        public string TypeHashID { get; private set; }
    }
}
