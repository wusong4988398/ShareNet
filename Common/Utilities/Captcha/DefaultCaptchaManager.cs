using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using ShareNet.Common.Utilities.Captcha;

namespace ShareNet.Common
{
    public class DefaultCaptchaManager:ICaptchaManager
    {
        private string _captchaInputName = "CaptchaInputName";
        /// <summary>
        /// 产生验证码
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="showCaptchaImage">默认是否显示验证码图片</param>
        /// <returns></returns>
        public MvcHtmlString GenerateCaptcha<TModel>(HtmlHelper<TModel> htmlHelper, bool showCaptchaImage = false)
        {
            return htmlHelper.EditorForModel("DefaultCaptchaInput", new { InputName = _captchaInputName, ShowCaptchaImage = showCaptchaImage });
        }

        public bool IsCaptchaValid(ActionExecutingContext filterContext)
        {
            throw new NotImplementedException();
        }
    }
}
