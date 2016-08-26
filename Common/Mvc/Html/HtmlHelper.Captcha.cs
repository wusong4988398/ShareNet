using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using WusNet.Infrastructure.WusNet;

namespace ShareNet.Common
{
    /// <summary>
    /// 验证码帮助
    /// </summary>
    public static class HtmlHelperCaptchaExtensions
    {
        /// <summary>
        /// 输出验证码
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="scenarios">使用场景</param>
        /// <param name="showCaptchaImage">默认是否显示验证码图片(仅针对验证码图片可以不立即显示的情况)</param>
        /// <param name="templateName">模板名称</param>
        /// <returns></returns>
        public static MvcHtmlString Captcha<TModel>(this HtmlHelper<TModel> htmlHelper,
            VerifyScenarios scenarios = VerifyScenarios.Post, bool showCaptchaImage = false,
            string templateName = "Captcha")
        {
            if (!CaptchaUtility.UseCaptcha(scenarios))
                return MvcHtmlString.Empty;
            ICaptchaManager captchaManager = DIContainer.Resolve<ICaptchaManager>();
            MvcHtmlString captchaText = captchaManager.GenerateCaptcha(htmlHelper, showCaptchaImage);

            return htmlHelper.EditorForModel(templateName, new { CaptchaText = captchaText });
        }
    }
}
