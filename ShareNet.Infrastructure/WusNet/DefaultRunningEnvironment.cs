using System;
using System.IO;
using System.Web;
using ShareNet.Infrastructure.Utilities;

namespace WusNet.Infrastructure.WusNet
{
    public class DefaultRunningEnvironment:IRunningEnvironment
    {
        // Fields
        private const string BinPath = "~/bin";
        private const string RefreshHtmlPath = "~/refresh.html";
        private const string WebConfigPath = "~/web.config";
        /// <summary>
        /// 重启AppDomain
        /// </summary>
        public void RestartAppDomain()
        {
            if (this.IsFullTrust)
            {
                HttpRuntime.UnloadAppDomain();

            }else if (!(this.TryWriteBinFolder()||this.TryWriteWebConfig()))
            {
                throw new ApplicationException(string.Format("需要启动站点，在非FullTrust环境下必须给\"{0}\"或者\"~/{1}\"写入的权限", "~/bin", "~/web.config"));
            }
            HttpContext current = HttpContext.Current;
            if (current != null)
            {
                if (current.Request.RequestType == "GET")
                {
                    current.Response.Redirect(current.Request.RawUrl, true);
                }
                else
                {
                    current.Response.ContentType = "text/html";
                    current.Response.WriteFile("~/refresh.html");
                    current.Response.End();
                }
            }

        }

        private bool TryWriteBinFolder()
        {
            try
            {
                string physicalFilePath = WebUtility.GetPhysicalFilePath("~/binHostRestart");
                Directory.CreateDirectory(physicalFilePath);
                using (StreamWriter writer = File.CreateText(Path.Combine(physicalFilePath, "log.txt")))
                {
                    writer.WriteLine("Restart on '{0}'", DateTime.UtcNow);
                    writer.Flush();
                }
                return true;
            }
            catch 
            {
                
                return false;
            }


        }

        private bool TryWriteWebConfig()
        {
            try
            {
                File.SetLastWriteTimeUtc(WebUtility.GetPhysicalFilePath("~/web.config"), DateTime.UtcNow);
                return true;
            }
            catch
            {
                return false;
            }
        }




        public bool IsFullTrust
        {
            get { return (AppDomain.CurrentDomain.IsHomogenous && AppDomain.CurrentDomain.IsFullyTrusted); }
        }
    }
}
