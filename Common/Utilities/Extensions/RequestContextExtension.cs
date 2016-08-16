using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace ShareNet.Common.Utilities.Extensions
{
    public static class RequestContextExtension
    {
        /// <summary>
        /// 从RouteData或QueryString中获取参数 
        /// </summary>
        /// <param name="requestContext">RequestContext</param>
        /// <param name="key">参数名称</param>
        /// <returns>字符串类型的参数值</returns>
        public static string GetParameterFromRouteDataOrQueryString(this RequestContext requestContext, string key)
        {
            if (requestContext.RouteData.Values != null && requestContext.RouteData.Values.ContainsKey(key))
            {
                object resultValue = null;
                if (requestContext.RouteData.Values.TryGetValue(key, out resultValue) && resultValue != null)
                {
                    return resultValue.ToString();//.Replace('<', ' ').Replace('>', ' ').Replace("%3C", "&lt;").Replace("%3E", "&gt;");
                }
            }

            return requestContext.HttpContext.Request.QueryString[key];
        }
    }
}
