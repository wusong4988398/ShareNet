using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using ShareNet.Common.Mvc.Routes;
using ShareNet.Common.User;
using ShareNet.Infrastructure.Utilities;

namespace ShareNet.Common
{
    /// <summary>
    /// 站点Url配置
    /// </summary>
    public class SiteUrls
    {
        //平台的AreaName
        private readonly string CommonAreaName = "Common";

        private static volatile SiteUrls _instance = null;
        private static readonly object lockObject = new object();
        public static SiteUrls Instance()
        {
            if (_instance==null)
            {
                lock (lockObject)
                {
                    if (_instance==null)
                    {
                        _instance=new SiteUrls();
                    }
                }
            }
            return _instance;
        }

        #region 系统页面

        /// <summary>
        /// 站点首页
        /// </summary>
        public string SiteHome()
        {
            return CachedUrlHelper.Action("Home", "Channel", CommonAreaName);
        }


        #endregion 系统页面


        #region User 用户相关

        /// <summary>
        /// 注册用户的链接
        /// </summary>
        /// <returns></returns>
        public string Register(string returnUrl = null, bool includeReturnUrl = false)
        {
            if (includeReturnUrl)
            {
                HttpContext httpContext = HttpContext.Current;
                string currentPath = httpContext.Request.Url.PathAndQuery;

                returnUrl = SiteUrls.ExtractQueryParams(currentPath)["ReturnUrl"];

                if (string.IsNullOrEmpty(returnUrl))
                    returnUrl = WebUtility.UrlEncode(HttpContext.Current.Request.RawUrl);
            }
            RouteValueDictionary routeValueDictionary = new RouteValueDictionary();
            if (!string.IsNullOrEmpty(returnUrl))
                routeValueDictionary.Add("returnUrl", WebUtility.UrlEncode(returnUrl));
            return CachedUrlHelper.Action("Register", "Account", CommonAreaName, routeValueDictionary);
        }

        /// <summary>
        /// 我的首页
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public string MyHome(long userId)
        {
            string spaceKey = UserIdToUserNameDictionary.GetUserName(userId);
            if (string.IsNullOrEmpty(spaceKey))
                return string.Empty;

            return MyHome(spaceKey);
        }

        /// <summary>
        /// 我的首页
        /// </summary>
        /// <param name="spaceKey">用户空间标识</param>
        /// <returns></returns>
        public string MyHome(string spaceKey, long? groupId = null, long? applicationId = null)
        {
            RouteValueDictionary routeValueDictionary = new RouteValueDictionary();
            if (!string.IsNullOrEmpty(spaceKey))
                routeValueDictionary.Add("spaceKey", spaceKey);
            if (groupId != null)
                routeValueDictionary.Add("groupId", groupId);
            if (applicationId != null)
                routeValueDictionary.Add("applicationId", applicationId);
            return CachedUrlHelper.Action("MyHome", "UserSpace", CommonAreaName, routeValueDictionary);

        }

        /// <summary>
        /// 跳转至忘记密码的页面
        /// </summary>
        /// <returns></returns>
        public string FindPassword()
        {
            return CachedUrlHelper.Action("FindPassword", "Account", CommonAreaName);
        }

        #endregion 用户相关

        #region 公共控件
        /// <summary>
        /// 验证码地址
        /// </summary>
        /// <returns></returns>
        public string CaptchaImage()
        {
            return CachedUrlHelper.RouteUrl("Captcha");
        } 
        #endregion

        #region Help Methods
        /// <summary>
        /// 获取url中的查询字符串参数
        /// </summary>
        public static NameValueCollection ExtractQueryParams(string url)
        {
            int startIndex = url.IndexOf("?");
            NameValueCollection values = new NameValueCollection();

            if (startIndex <= 0)
                return values;

            string[] nameValues = url.Substring(startIndex + 1).Split('&');

            foreach (string s in nameValues)
            {
                string[] pair = s.Split('=');

                string name = pair[0];
                string value = string.Empty;

                if (pair.Length > 1)
                    value = pair[1];

                values.Add(name, value);
            }

            return values;
        }
        #endregion Help Methods

       
    }
}
