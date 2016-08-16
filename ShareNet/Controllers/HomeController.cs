using System.Collections.Generic;
using System.Web.Mvc;
using BusinessComponents.Common.User;

namespace ShareNet.Web.Controllers
{
    public class HomeController : Controller
    {
        private UserServices userService =new UserServices();
        public ActionResult Index()
        {

           List<Users> userlist=userService.GetAll();
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