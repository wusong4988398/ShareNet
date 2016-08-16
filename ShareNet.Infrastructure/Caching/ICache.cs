using System;

namespace WusNet.Infrastructure.Caching
{
    public interface ICache
    {
        // Methods
        void Add(string key, object value, TimeSpan timeSpan);
        void AddWithFileDependency(string key, object value, string fullFileNameOfFileDependency);
        void Clear();
        object Get(string cacheKey);
        void MarkDeletion(string key, object value, TimeSpan timeSpan);
        void Remove(string cacheKey);
        void Set(string key, object value, TimeSpan timeSpan);

    }
}
