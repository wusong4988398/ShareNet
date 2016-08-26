using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace ShareNet.Common.Mvc.Routes
{
    public class HttpHandlerRouteHandler<THttpHandler>:IRouteHandler where THttpHandler:IHttpHandler
    {
        Func<RequestContext, THttpHandler> _handlerFactory = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="handlerFactory"></param>
        public HttpHandlerRouteHandler(Func<RequestContext, THttpHandler> handlerFactory)
        {
            _handlerFactory = handlerFactory;
        }
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return _handlerFactory(requestContext);
        }
    }
}
