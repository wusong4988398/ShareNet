using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WusNet.Infrastructure.Utilities
{
    public static class DictionaryExtension
    {
        // Methods
        public static T Get<T>(this IDictionary<string, object> dictionary, string key, T defaultValue)
        {
            if (dictionary.ContainsKey(key))
            {
                object obj2;
                dictionary.TryGetValue(key, out obj2);
                return ValueUtility.ChangeType<T>(obj2, defaultValue);
            }
            return defaultValue;
        }

    }
}
