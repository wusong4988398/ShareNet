using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ShareNet.Common.User;
using ShareNet.Common.Utilities;
using WusNet.Infrastructure.Caching;
using WusNet.Infrastructure.WusNet;

namespace ShareNet.Common
{
    /// <summary>
    /// 验证码管理类
    /// </summary>
    public static class CaptchaUtility
    {
        private static ICacheService cacheService = DIContainer.Resolve<ICacheService>();

        /// <summary>
        /// 是否开始使用验证码
        /// </summary>
        /// <param name="scenarios">验证码使用场景</param>
        /// <param name="isLimitCount"></param>
        /// <returns></returns>
        public static bool UseCaptcha(VerifyScenarios scenarios = VerifyScenarios.Post, bool isLimitCount = false)
        {
            CaptchaSettings verificationCodeSettings = CaptchaSettings.Instance();
            if (!verificationCodeSettings.EnableCaptcha)
                return false;
            IUser currentUser = UserContext.CurrentUser;
            if (scenarios == VerifyScenarios.Register || currentUser == null && scenarios==VerifyScenarios.Post)
            return true;
            //后台登陆
            if (scenarios == VerifyScenarios.Login && currentUser != null)
                return true;
            if (currentUser == null && scenarios == VerifyScenarios.Post && verificationCodeSettings.EnableAnonymousCaptcha)
                return true;
            string userName = GetUserName();
            if (scenarios == VerifyScenarios.Login && UserIdToUserNameDictionary.GetUserId(userName) == 0)
                return false;
            string cacheKey = GetCacheKey_LimitTryCount(userName, scenarios);

            int? limitTryCount = cacheService.Get(cacheKey) as int?;
            if (limitTryCount.HasValue
                && ((scenarios == VerifyScenarios.Login && limitTryCount >= verificationCodeSettings.CaptchaLoginCount)
                || (scenarios == VerifyScenarios.Post && limitTryCount >= verificationCodeSettings.CaptchaPostCount)))
                return true;
            if (isLimitCount)
            {
                if (limitTryCount.HasValue)
                {
                    limitTryCount++;
                }
                else
                {
                    limitTryCount = 1;
                }
                cacheService.Set(cacheKey, limitTryCount, CachingExpirationType.SingleObject);
            }
            return false;

        }

       


        private static string GetUserName()
        {
            HttpContext httpContext = HttpContext.Current;
            string userName = string.Empty;
            IUser currentUser = UserContext.CurrentUser;
            if (currentUser != null)
                userName = currentUser.UserName;
            else if (!string.IsNullOrEmpty(httpContext.Request.Form["UserName"]))
                userName = httpContext.Request.Form["UserName"];
            return userName;
        }
        /// <summary>
        /// 尝试次数统计CacheKey
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="scenarios"></param>
        /// <returns></returns>
        private static string GetCacheKey_LimitTryCount(string userName, VerifyScenarios scenarios)
        {
            return string.Format("LimitTryCount::UserName-{0}:Scenarios-{1}", userName, scenarios);
        }
    }
}
