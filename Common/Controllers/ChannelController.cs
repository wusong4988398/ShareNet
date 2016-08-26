using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ShareNet.Common.Common.SiteSetting;
using ShareNet.Common.PresentAreas;
using ShareNet.Common.UI.Themes;
using ShareNet.Common.Utilities;

namespace ShareNet.Common.Controllers
{
    /// <summary>
    /// 频道Controller
    /// </summary>
    [Themed(PresentAreaKeysOfBuiltIn.Channel, IsApplication = false)]
    public class ChannelController : Controller
    {
        /// <summary>
        /// 简单首页
        /// </summary>
        /// <returns>简单首页</returns>
        [HttpGet]
        public ActionResult SimpleHome()
        {
           //SiteSettings siteSettings = siteSettingsManager.Get();

            //当前网页如果是作为站点首页。并且用户是登陆的情况下。再查看此页面是不合适的。
            //if (siteSettings.EnableSimpleHome && UserContext.CurrentUser != null)
            //    return Redirect(SiteUrls.Instance().SiteHome());
           // return Redirect(SiteUrls.Instance().SiteHome());
            //pageResourceManager.InsertTitlePart(siteSettings.SiteDescription);
            //pageResourceManager.InsertTitlePart(siteSettings.SiteName);
            //pageResourceManager.SetMetaOfDescription(siteSettings.SearchMetaDescription);
            //pageResourceManager.SetMetaOfKeywords(siteSettings.SearchMetaKeyWords);
            //Console.WriteLine("在这里显示主页");
            return View();
        }

        #region 公共区域

        /// <summary>
        /// 主页
        /// </summary>
        /// <returns>返回主页信息</returns>

        public ActionResult Home()
        {
            return View();
        } 
        #endregion
    }
}
