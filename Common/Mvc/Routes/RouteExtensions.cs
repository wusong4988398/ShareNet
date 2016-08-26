using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace ShareNet.Common.Mvc.Routes
{
    /// <summary>
    /// RouteCollection扩展
    /// </summary>
    public static class RouteExtensions
    {
        /// <summary>
        /// 添加使用HttpHandler的Route
        /// </summary>
        /// <typeparam name="THandler"></typeparam>
        /// <param name="routes"></param>
        /// <param name="name"></param>
        /// <param name="url"></param>
        public static Route MapHttpHandler<THandler>(this RouteCollection routes, string name, string url) where THandler : IHttpHandler, new()
        {
            return routes.MapHttpHandler<THandler>(name, url, defaults: null, constraints: null, handlerFactory: r => new THandler());
        }

        /// <summary>
        /// 添加使用HttpHandler的Route
        /// </summary>
        /// <typeparam name="THandler"></typeparam>
        /// <param name="routes"></param>
        /// <param name="name"></param>
        /// <param name="url"></param>
        /// <param name="defaults"></param>
        /// <param name="constraints"></param>
        /// <param name="handlerFactory"></param>
        public static Route MapHttpHandler<THandler>(this RouteCollection routes, string name, string url, object defaults, object constraints, Func<RequestContext, THandler> handlerFactory) where THandler : IHttpHandler
        {
            var route = new Route(url, new HttpHandlerRouteHandler<THandler>(handlerFactory));
            route.Defaults = new RouteValueDictionary(defaults);
            route.Constraints = new RouteValueDictionary(constraints);

            routes.Add(name, route);
            return route;
        }

    }
}
