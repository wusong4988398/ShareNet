using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using ShareNet.Common.Common.Settings;
using ShareNet.Common.Common.SiteSetting;
using ShareNet.Common.PresentAreas;

using ShareNet.Common.UI.Themes;
using ShareNet.Common.User;
using ShareNet.Common.Utilities;
using WusNet.Infrastructure.WusNet;

namespace ShareNet.Common.UI
{
    /// <summary>
    /// 当前皮肤获取器接口
    /// </summary>
    public class ChannelThemeResolver : IThemeResolver
    {
        #region IThemeSelector 成员
        public ThemeAppearance GetRequestTheme(RequestContext controllerContext)
        {
            string themeKey = null;
            string appearanceKey = null;
            SiteSettings siteSettings = DIContainer.Resolve<ISettingsManager<SiteSettings>>().Get();
            if (!string.IsNullOrEmpty(siteSettings.SiteTheme) && !string.IsNullOrEmpty(siteSettings.SiteThemeAppearance))
            {
                themeKey = siteSettings.SiteTheme;
                appearanceKey = siteSettings.SiteThemeAppearance;
            }
            else
            {
                PresentArea pa = new PresentAreaService().Get(PresentAreaKeysOfBuiltIn.Channel);
                if (pa != null)
                {
                    themeKey = pa.DefaultThemeKey;
                    appearanceKey = pa.DefaultAppearanceKey;
                }
            }
            return new ThemeService().GetThemeAppearance(PresentAreaKeysOfBuiltIn.Channel, themeKey, appearanceKey);


        }
        /// <summary>
        /// 加载样式
        /// </summary>
        /// <param name="controllerContext"></param>
        public void IncludeStyle(RequestContext controllerContext)
        {
            ThemeAppearance themeAppearance = GetRequestTheme(controllerContext);
            if (themeAppearance == null)
                return;
            PresentArea presentArea = new PresentAreaService().Get(themeAppearance.PresentAreaKey);
            if (presentArea == null)
                return;
            string themeCssPath = string.Format("{0}/{1}/theme.css", presentArea.ThemeLocation, themeAppearance.ThemeKey);
            string appearanceCssPath = string.Format("{0}/{1}/Appearances/{2}/appearance.css", presentArea.ThemeLocation, themeAppearance.ThemeKey, themeAppearance.AppearanceKey);
            IPageResourceManager resourceManager = DIContainer.ResolvePerHttpRequest<IPageResourceManager>();
            resourceManager.IncludeStyle(themeCssPath);
            resourceManager.IncludeStyle(appearanceCssPath);
        } 
        #endregion
        /// <summary>
        /// 验证当前用户是否修改皮肤的权限
        /// </summary>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        public bool Validate(long ownerId)
        {
            //获取当前用户
            IUser currentUser = UserContext.CurrentUser;
            if (currentUser == null)
                return false;
            //if (currentUser.IsSuperAdministrator() || currentUser.IsContentAdministrator())
            //    return true;
            //return false;
            return true;
        }

        public string GetThemeAppearance(long ownerId)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 更新皮肤
        /// </summary>
        /// <param name="ownerId">拥有者Id（如：用户Id、群组Id）</param>
        /// <param name="isUseCustomStyle">是否使用自定义皮肤</param>
        /// <param name="themeAppearance">themeKey与appearanceKey用逗号关联</param>
        public void ChangeThemeAppearance(long ownerId, bool isUseCustomStyle, string themeAppearance)
        {
            SiteSettings siteSettings = DIContainer.Resolve<ISettingsManager<SiteSettings>>().Get();
            string themeKey = null;
            string appearanceKey = null;
            string[] themeAppearanceArray = themeAppearance.Split(',');

            throw new NotImplementedException();
        }
    }
}
