using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ShareNet.Common.Utilities.Captcha;

namespace ShareNet.Common.Handlers
{
    public class CaptchaHttpHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            bool isremove = false;
            HttpContextBase currentContext = new HttpContextWrapper(context);
            if (!string.IsNullOrEmpty(context.Request.QueryString["isremove"]))
                bool.TryParse(context.Request.QueryString["isremove"], out isremove);
            string cookieName = CaptchaSettings.Instance().CaptchaCookieName;
            bool enableLineNoise = CaptchaSettings.Instance().EnableLineNoise;
            CaptchaCharacterSet characterSet = CaptchaSettings.Instance().CharacterSet;
            int minCharacterCount = CaptchaSettings.Instance().MinCharacterCount;
            int maxCharacterCount = CaptchaSettings.Instance().MaxCharacterCount;
            string generatedKey = string.Empty;
            bool addCooikes = false;
            //创建或从缓存取验证码
        }

        public bool IsReusable { get; private set; }
    }
}
