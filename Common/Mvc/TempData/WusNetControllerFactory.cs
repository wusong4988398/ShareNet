using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShareNet.Common.Mvc.TempData
{
    /// <summary>
    /// 替换创建Controller工厂类
    /// </summary>
    public class WusNetControllerFactory : DefaultControllerFactory
    {
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            IController iController = base.CreateController(requestContext, controllerName);
            Controller controller = iController as Controller;
            if (iController != null)
                controller.TempDataProvider = new CookieTempDataProvider(requestContext.HttpContext);
            return controller;
        }
    }
}
