using System.Web.Mvc;
using Autofac;
using Autofac.Core;

namespace WusNet.Infrastructure.WusNet
{

    /// <summary>
    /// DI容器对象
    /// </summary>
    public class DIContainer
    {
        // Fields
        private static IContainer _container;

        // Methods
        public static void RegisterContainer(IContainer container)
        {
            _container = container;
        }

        public static TService Resolve<TService>()
        {
            return _container.Resolve<TService>();
        }
        public static TService Resolve<TService>(params Parameter[] parameters)
        {
            return _container.Resolve<TService>(parameters);
        }

        public static TService ResolveKeyed<TService>(object serviceKey)
        {
            return _container.ResolveKeyed<TService>(serviceKey);
        }

        public static TService ResolveNamed<TService>(string serviceName)
        {
            return _container.ResolveNamed<TService>(serviceName);
        }

        public static TService ResolvePerHttpRequest<TService>()
        {
            IDependencyResolver current = DependencyResolver.Current;
            if (current != null)
            {
                TService service = (TService)current.GetService(typeof(TService));
                if (service != null)
                {
                    return service;
                }
            }
            return _container.Resolve<TService>();
        }

    }
}
