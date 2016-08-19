using System.Web.Mvc;
using ShareNet.Common.PresentAreas;
using ShareNet.Common.UI.Themes;

namespace ShareNet.Web.Controllers
{
    [Themed(PresentAreaKeysOfBuiltIn.Channel, IsApplication = false)]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}