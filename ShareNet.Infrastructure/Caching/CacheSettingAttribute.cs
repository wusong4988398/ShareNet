using System;

namespace WusNet.Infrastructure.Caching
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CacheSettingAttribute:Attribute
    {
        // Fields
        private EntityCacheExpirationPolicies expirationPolicy = EntityCacheExpirationPolicies.Normal;

      
        public CacheSettingAttribute(bool enableCache)
        {
            this.EnableCache = enableCache;
        }
        public bool EnableCache { get; private set; }
        /// <summary>
        /// 过期策略
        /// </summary>
        public EntityCacheExpirationPolicies ExpirationPolicy
        {
            get
            {
                return this.expirationPolicy;
            }
            set
            {
                this.expirationPolicy = value;
            }
        }
        public string PropertyNameOfBody { get; set; }

        public string PropertyNamesOfArea { get; set; }



    }
}
