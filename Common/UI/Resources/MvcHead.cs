using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ShareNet.Common.Common.Settings;
using ShareNet.Common.Common.SiteSetting;
using WusNet.Infrastructure.WusNet;

namespace ShareNet.Common
{
    /// <summary>
    /// 输出html的head
    /// </summary>
    /// <remarks>
    /// 在head中自动呈现Title、Meta、ShortcutIcon、引入的js/css、注册的js/css代码块
    /// </remarks>
    public class MvcHead : IDisposable
    {
        private bool _disposed;
        private readonly ViewContext _viewContext;
        private readonly TextWriter _writer;
        private readonly IPageResourceManager pageResourceManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="viewContext"></param>
        /// <param name="title">标题</param>
        /// <param name="disableClientCache">是否启用客户端缓存</param>
        /// <param name="charset">编码格式</param>
        public MvcHead(ViewContext viewContext, string title = null, bool disableClientCache = false, string charset = "UTF-8")
        {
            if (viewContext == null)
                throw new ArgumentNullException("viewContext");

            _viewContext = viewContext;
            _writer = viewContext.Writer;
            pageResourceManager = DIContainer.ResolvePerHttpRequest<IPageResourceManager>();
            TagBuilder tagBuilder = new TagBuilder("head");
            _writer.WriteLine(tagBuilder.ToString(TagRenderMode.StartTag));
            _writer.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset={0}\" />", charset);
            if (string.IsNullOrEmpty(title))
            {
                if (pageResourceManager.IsAppendSiteName)
                {
                    pageResourceManager.AppendTitleParts(DIContainer.Resolve<ISettingsManager<SiteSettings>>().Get().SiteName);
                }
                title = pageResourceManager.GenerateTitle();
            }
            if (!string.IsNullOrEmpty(title))
            {
                _writer.WriteLine("<title>{0}</title>", title);
                _writer.WriteLine("<link rel=\"shortcut icon\" type=\"image/ico\" href=\"{0}\" />", pageResourceManager.ShortcutIcon);
            }
            if (disableClientCache)
            {
                _writer.WriteLine("<meta http-equiv=\"Pragma\" content=\"no-cache\" />\n");
                _writer.WriteLine("<meta http-equiv=\"no-cache\" />\n");
                _writer.WriteLine("<meta http-equiv=\"Expires\" content=\"-1\" />\n");
                _writer.WriteLine("<meta http-equiv=\"Cache-Control\" content=\"no-cache\" />\n");
            }
            IList<MetaEntry> metas = pageResourceManager.GetRegisteredMetas();
            foreach (var meta in metas)
            {
                _writer.WriteLine(meta.GetRenderingTag());
            }

        }


        public void Dispose()
        {
            Dispose(true /* disposing */);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;

                //呈现引入的css
                IList<string> includeStyles = pageResourceManager.GetIncludedStyleHtmls();
                foreach (var includeStyle in includeStyles)
                {
                    _writer.WriteLine(includeStyle);
                }
                pageResourceManager.ClearIncludedStyles();

                //呈现注册的css代码块
                IList<string> styleBlocks = pageResourceManager.GetRegisteredStyleBlocks();
                _writer.WriteLine("<style type=\"text/css\">");
                foreach (var styleBlock in styleBlocks)
                {
                    _writer.WriteLine(styleBlock);
                }
                _writer.WriteLine("</style>");
                pageResourceManager.ClearRegisteredStyleBlocks();

                _writer.WriteLine("</head>");
            }
        }
    }
}
