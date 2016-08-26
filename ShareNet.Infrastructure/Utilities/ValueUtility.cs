using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WusNet.Infrastructure.Utilities
{
    public static class ValueUtility
    {
        public static T ChangeType<T>(object value, T defalutValue)
        {
            if (value == null)
            {
                return defalutValue;
            }
            Type nullableType = typeof(T);
            if (nullableType.IsInterface || (nullableType.IsClass && (nullableType != typeof(string))))
            {
                if (value is T)
                {
                    return (T)value;
                }
                return defalutValue;
            }
            if (nullableType.IsGenericType && (nullableType.GetGenericTypeDefinition() == typeof(Nullable<>)))
            {
                return (T)Convert.ChangeType(value, Nullable.GetUnderlyingType(nullableType));
            }
            if (nullableType.IsEnum)
            {
                return (T)Enum.Parse(nullableType, value.ToString());
            }
            return (T)Convert.ChangeType(value, nullableType);
        }

    }
}
