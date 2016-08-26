using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ShareNet.Common.PresentAreas;
using ShareNet.Common.UI.Themes;

namespace ShareNet.Common.Controllers
{

    /// <summary>
    /// 后台控制面板Controller
    /// </summary>
    [Themed(PresentAreaKeysOfBuiltIn.ControlPanel, IsApplication = false)]
    public class ControlPanelController : Controller
    {
        /// <summary>
        /// 后台首页
        /// </summary>
        /// <returns></returns>
       
       
        public ActionResult Home()
        {
            return View();
        }
    }
}
