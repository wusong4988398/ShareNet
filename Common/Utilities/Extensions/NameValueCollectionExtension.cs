using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareNet.Common.Utilities.Extensions
{
    /// <summary>
    /// 获取Request.QueryString[key],Request.Form[key],Request.Params[key]并进行类型转换
    /// </summary>
    public static class NameValueCollectionExtension
    {
        /// <summary>
        /// 获取请求的参数 
        /// </summary>
        /// <typeparam name="T">必须是基本类型</typeparam>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T Get<T>(this NameValueCollection collection, string key, T defaultValue)
        {
            T returnValue = defaultValue;
            if (collection[key] != null)
            {
                Type tType = typeof(T);
                if (tType.IsGenericType && tType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    if (string.IsNullOrEmpty(collection[key].ToString()))
                        return defaultValue;
                    return (T)TypeDescriptor.GetConverter(Nullable.GetUnderlyingType(tType)).ConvertFrom(collection[key]);
                }
                else if (tType.IsEnum)
                {

                    return (T)Enum.Parse(tType, collection[key]);
                }
                else
                {
                    try
                    {
                        return (T)Convert.ChangeType(collection[key], tType);
                    }
                    catch
                    {
                        return returnValue;
                    }
                }
            }
            else
                return returnValue;
        }
    }
}
