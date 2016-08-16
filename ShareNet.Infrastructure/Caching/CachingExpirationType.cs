namespace WusNet.Infrastructure.Caching
{
    /// <summary>
    /// 缓存过期类型
    /// </summary>
    public enum CachingExpirationType
    {
        /// <summary>
        /// 不变的
        /// </summary>
        Invariable,
        /// <summary>
        /// 稳定
        /// </summary>
        Stable,
        /// <summary>
        /// 相对稳定
        /// </summary>
        RelativelyStable,
        /// <summary>
        /// 一般
        /// </summary>
        UsualSingleObject,
        UsualObjectCollection,
        /// <summary>
        /// 单个对象
        /// </summary>
        SingleObject,
        /// <summary>
        /// 对象集合
        /// </summary>
        ObjectCollection

    }
}
