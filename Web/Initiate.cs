using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using ShareNet.Common.Common.Settings;
using ShareNet.Common.Common.Settings.Repositories;
using ShareNet.Common.Mvc.ModelBinder;
using ShareNet.Common.Mvc.TempData;
using ShareNet.Common.UI;
using ShareNet.Common.UI.Themes;
using WusNet.Infrastructure.Caching;
using WusNet.Infrastructure.Globalization;
using WusNet.Infrastructure.Logging;
using WusNet.Infrastructure.Logging.Log4Net;
using WusNet.Infrastructure.Repositories;
using WusNet.Infrastructure.WusNet;

namespace ShareNet.Web
{
    /// <summary>
    /// 初始化并启动
    /// </summary>
    public class Initiate
    {
        /// <summary>
        /// 启动
        /// </summary>
        public static void Start()
        {
            //初始化ResourceAccessor(资源存储器)
           // ResourceAccessor.Initialize("Spacebuilder.Web.Resources.Resource", typeof(Spacebuilder.Web.Resources.Resource).Assembly);
           // ResourceAccessor.Initialize("Spacebuilder.Web.Resources.Resource", null);
            InitializeDiContainer();
            InitializeMVC();
            InitializeApplication();
        }

      

        private static void InitializeDiContainer()
        {
            var containerBuilder = new ContainerBuilder();
            #region 运行环境及全局设置
             //获取web引用的所有Tunynet开头的程序集
            
            AssemblyName[] assemblyNames1 = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            AssemblyName[] assemblyNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Where(n => n.Name.StartsWith("WusNet")).ToArray();
            List<Assembly> assemblyList=assemblyNames.Select(n => Assembly.Load(n)).ToList();
            //获取web\bin下的所有Spacebuilder开头的的程序集
             IEnumerable<string> files=Directory.EnumerateFiles(HttpRuntime.BinDirectory, "ShareNet.*.dll");
             assemblyList.AddRange(files.Select(n=>Assembly.Load(AssemblyName.GetAssemblyName(n))));
            Assembly[] assemblies = assemblyList.ToArray();
            //批量注入所有Service
            containerBuilder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.Name.EndsWith("Service"))
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            //批量注入所有的Repository
            containerBuilder.RegisterAssemblyTypes(assemblies).Where(t => t.Name.EndsWith("Repository")).AsSelf().AsImplementedInterfaces().SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            //批量注入所有的EventMoudle
            //containerBuilder.RegisterAssemblyTypes(assemblies).Where(t => typeof(IEventMoudle).IsAssignableFrom(t)).As<IEventMoudle>().SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            //批量注入所有的UrlGetter
            containerBuilder.RegisterAssemblyTypes(assemblies).Where(t => t.Name.EndsWith("UrlGetter")).AsSelf().AsImplementedInterfaces().SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            //批量注入所有的DataGetter
            containerBuilder.RegisterAssemblyTypes(assemblies).Where(t => t.Name.EndsWith("DataGetter")).AsSelf().AsImplementedInterfaces().SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            //批量注入所有的Handler
            containerBuilder.RegisterAssemblyTypes(assemblies).Where(t => t.Name.EndsWith("Handler")).AsSelf().AsImplementedInterfaces().SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            //解决SearchHistoryService的单实例问题
            //containerBuilder.Register(c => new SearchHistoryService()).AsSelf().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            //用户身份认证
            //containerBuilder.Register(c => new FormsAuthenticationService()).As<IAuthenticationService>().PropertiesAutowired().InstancePerHttpRequest();

            //权限认证
            //containerBuilder.Register(c => new Authorizer()).AsSelf().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies).SingleInstance();

            //注册运行环境
            containerBuilder.Register(c => new DefaultRunningEnvironment()).As<IRunningEnvironment>().SingleInstance();

            //注册系统日志
            containerBuilder.Register(c => new Log4NetLoggerFactoryAdapter()).As<ILoggerFactoryAdapter>().SingleInstance();

            #endregion

            #region 范型注入

            containerBuilder.RegisterGeneric(typeof(SettingsManager<>)).As(typeof(ISettingsManager<>)).SingleInstance().PropertiesAutowired();
            containerBuilder.RegisterGeneric(typeof(SettingsRepository<>)).As(typeof(ISettingsRepository<>)).SingleInstance().PropertiesAutowired();
            containerBuilder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).SingleInstance().PropertiesAutowired();


            #endregion

            #region 业务逻辑实现

            //注册缓存
            if (false)
            {
                //containerBuilder.Register(c => new DefaultCacheService(new MemcachedCache(), new RuntimeMemoryCache(), 1.0F, true)).As<ICacheService>().SingleInstance();
            }
            else
            {
                containerBuilder.Register(c => new DefaultCacheService(new RuntimeMemoryCache(), 1.0F)).As<ICacheService>().SingleInstance();
            }

            //操作日志
            //containerBuilder.Register(c => new OperatorInfoGetter()).As<IOperatorInfoGetter>().SingleInstance();
            containerBuilder.Register(c => new OperationLogService()).As<OperationLogService>().SingleInstance();
            #endregion


            //注册各应用模块的组件
            //ApplicationConfig.InitializeAll(containerBuilder);

            //MobileClientApplicationConfig.InitializeAll(containerBuilder);

            containerBuilder.RegisterControllers(assemblies).PropertiesAutowired();
            containerBuilder.RegisterSource(new ViewRegistrationSource());
            containerBuilder.RegisterModelBinders(assemblies);
            containerBuilder.RegisterModelBinderProvider();
            containerBuilder.RegisterFilterProvider();
            containerBuilder.RegisterModule(new AutofacWebTypesModule());

            IContainer container = containerBuilder.Build();

            //将Autofac容器中的实例注册到mvc自带DI容器中（这样才获取到每请求缓存的实例）
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            DIContainer.RegisterContainer(container);

            //将webapi的返回值设为Json格式
            //var jsonFormatter = new JsonMediaTypeFormatter();
            //GlobalConfiguration.Configuration.Services.Replace(typeof(IContentNegotiator), new JsonContentNegotiator(jsonFormatter));
        }

        private static void InitializeMVC()
        {
            //扩展控制器(tempdata数据存储使用cookie存储代替session)
            ControllerBuilder.Current.SetControllerFactory(typeof(WusNetControllerFactory));
            //自定义模型绑定
            ModelBinders.Binders.DefaultBinder = new CustomModelBinder();

            //增加对Cookie的模型绑定
            ValueProviderFactories.Factories.Add(new CookieValueProviderFactory());

            //使MVC支持皮肤机制的视图引擎
           ViewEngines.Engines.Clear();
           ViewEngines.Engines.Add(new ThemedViewEngine());

           //注册区域
           AreaRegistration.RegisterAllAreas();
        }

        /// <summary>
        /// 初始化应用程序，加载基础数据
        /// </summary>
        private static void InitializeApplication()
        {

            //注册皮肤选择器
            ThemeService.RegisterThemeResolver("Channel", new ChannelThemeResolver());
            //ThemeService.RegisterThemeResolver("UserSpace", new UserSpaceThemeResolver());
            ThemeService.RegisterThemeResolver("ControlPanel", new ControlPanelThemeResolver());
        }
    }
}