using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using ShareNet.Common.PresentAreas;

using ShareNet.Common.UI.Themes;
using WusNet.Infrastructure.WusNet;

namespace ShareNet.Common.UI
{
    public class ControlPanelThemeResolver : IThemeResolver
    {
        public ThemeAppearance GetRequestTheme(RequestContext controllerContext)
        {
            return new ThemeService().GetThemeAppearance("ControlPanel", "Default", "Default");
        }

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

        public bool Validate(long ownerId)
        {
            throw new NotImplementedException();
        }

        public string GetThemeAppearance(long ownerId)
        {
            return "Default,Default";
        }

        public void ChangeThemeAppearance(long ownerId, bool isUseCustomStyle, string themeAppearance)
        {
            throw new NotImplementedException();
        }
    }
}
