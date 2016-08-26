using System.Collections.Concurrent;
using System.Reflection;
using System.Resources;

namespace WusNet.Infrastructure.Globalization
{
    /// <summary>
    /// 资源存储器
    /// </summary>
    public static class ResourceAccessor
    {

        // Fields
        private static ConcurrentDictionary<int, ResourceManager> _applicationResourceManagers = new ConcurrentDictionary<int, ResourceManager>();
        private static ResourceManager _commonResourceManager;

      


        

        public static string GetString(string resourcesKey)
        {
            try
            {
                string str = _commonResourceManager.GetString(resourcesKey);
                if (str != null)
                {
                    return str;
                }
            }
            catch
            {
            }
            return GetMissingResourcePrompt(resourcesKey);
        }

        public static string GetString(string resourcesKey, int applicationId)
        {
            string str;
            ResourceManager manager;
            if (_applicationResourceManagers.TryGetValue(applicationId, out manager))
            {
                try
                {
                    str = manager.GetString(resourcesKey);
                    if (str != null)
                    {
                        return str;
                    }
                }
                catch
                {
                }
            }
            try
            {
                str = _commonResourceManager.GetString(resourcesKey);
                if (str != null)
                {
                    return str;
                }
            }
            catch
            {
            }
            return GetMissingResourcePrompt(resourcesKey);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commonResourceFileBaseName"></param>
        /// <param name="commonResourceAssembly"></param>
        public static void Initialize(string commonResourceFileBaseName, Assembly commonResourceAssembly)
        {
            _commonResourceManager = new ResourceManager(commonResourceFileBaseName, commonResourceAssembly);
            _commonResourceManager.IgnoreCase = true;
        }



        private static string GetMissingResourcePrompt(string resourcesKey)
        {
            return string.Format("<span style=\"color:#ff0000; font-weight:bold\">missing resource: {0}</span>", resourcesKey);
        }


    }
}
