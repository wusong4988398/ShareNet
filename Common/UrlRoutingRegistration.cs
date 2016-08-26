using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ShareNet.Common.Handlers;
using ShareNet.Common.Mvc.Routes;

namespace ShareNet.Common
{
    public class UrlRoutingRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Common"; }
        }
        public override void RegisterArea(AreaRegistrationContext context)
        {
            //对于IIS6.0默认配置不支持无扩展名的url
            string extensionForOldIIS = ".aspx";
            int iisVersion = 0;

            if (!int.TryParse(ConfigurationManager.AppSettings["IISVersion"], out iisVersion))
                iisVersion = 7;
            if (iisVersion >= 7)
                extensionForOldIIS = string.Empty;

            #region Channel

            context.MapRoute(
                  "Channel_SiteHome", // Route name
                  "", // URL with parameters
                  new { controller = "Channel", action = "SimpleHome" } // Parameter defaults
              );

            context.MapRoute(
              "Channel_Account_Comon", // Route name
              "Account/{action}" + extensionForOldIIS, // URL with parameters
              new { controller = "Account", action = "Login" } // Parameter defaults
          );
            #endregion


            #region Handler
            context.Routes.MapHttpHandler<CaptchaHttpHandler>("Captcha", "Handlers/CaptchaImage.ashx");
            #endregion

        }

     
    }
}
