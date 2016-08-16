namespace WusNet.Infrastructure.Caching
{
   public interface IListCacheSetting
    {
        // Properties
        string AreaCachePropertyName { get; set; }
        object AreaCachePropertyValue { get; set; }
        CacheVersionType CacheVersionType { get; }

    }
}
