using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Optimization;
using ShareNet.Infrastructure.Utilities;

namespace Web
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            //基础样式
            bundles.Add(new StyleBundle("~/Bundle/Styles/Site")
                .Include("~/Themes/Shared/Styles/tn.core_src.css", new FixCssRewrite())
                .Include("~/Themes/Shared/Styles/tn.widgets_src.css", new FixCssRewrite())
                .Include("~/Themes/Shared/Styles/tn.theme_src.css", new FixCssRewrite())
                .Include("~/Themes/Shared/Styles/common.css", new FixCssRewrite())
                .Include("~/Scripts/jquery/artDialog/skins/default.css", new FixCssRewrite())
                );


            //站点基础脚本
            bundles.Add(new ScriptBundle("~/Bundle/Scripts/Site")
                .Include("~/Scripts/jquery/jquery.cookie.js")
                .Include("~/Scripts/jquery/jquery.hoverIntent.js")
                .Include("~/Scripts/jquery/jquery.metaData.js")
                .Include("~/Scripts/jquery/jquery.livequery.js")
                .Include("~/Scripts/jquery/jquery.dcmegamenu.1.3.3.js")
                .Include("~/Scripts/jquery/tipsy/jquery.tipsy-1.0.0a.js")
                .Include("~/Scripts/jquery/tipsy/jquery.tipsy.hovercard.js")
                .Include("~/Scripts/jquery/template/jquery.tmpl.js")
                .Include("~/Scripts/jquery/artDialog/jquery.artDialog-4.1.4.js")
                .Include("~/Scripts/jquery/artDialog/plugins/jquery.artDialog.iframeTools.js")
                .Include("~/Scripts/jquery/watermark/jquery.watermark-3.1.4.js")
                .Include("~/Scripts/jquery/scrollTo/jquery.scrollTo-1.4.3.1.js")
                .Include("~/Scripts/jquery/ajaxForm/jquery.blockUI.js")
                .Include("~/Scripts/jquery/ajaxForm/jquery.form.js")
                .Include("~/Scripts/jquery/validate/jquery.validate.js")
                .Include("~/Scripts/jquery/jquery.validate.password.js")
                .Include("~/Scripts/jquery/validate/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/tunynet/plugins/jquery.validate.additional-methods.js")
                .Include("~/Scripts/tunynet/plugins/jquery.validate.messages-zh-CN.js")
                .Include("~/Scripts/tunynet/plugins/jquery.tn.textarea.js")
                .Include("~/Scripts/tunynet/plugins/jquery.tn.menuButton.js")
                .Include("~/Scripts/tunynet/site.js")
                .Include("~/Scripts/tunynet/form.js")
                .Include("~/Scripts/tunynet/list.js")
                .Include("~/Scripts/tunynet/dialog.js")
                .Include("~/Scripts/tunynet/image.js")
                .Include("~/Scripts/tunynet/quickSearch.js")
                .Include("~/Scripts/tunynet/pointMessage.js")
                .Include("~/Scripts/UEditor/ueditor.parse.min.js")
                .Include("~/Scripts/UMditor/ueditor.parse.min.js")
                .Include("~/Scripts/tunynet/uploadify.js")
                .Include("~/Scripts/tunynet/emotionSelector.js")
                //.Include("~/Scripts/tunynet/jqueryUI.js")
                .Include("~/Scripts/tunynet/linkageDropDownList.js")
                //.Include("~/Scripts/tunynet/plugins/jquery.spb.collapsibleBox.js")
                //.Include("~/Scripts/tunynet/plugins/jquery.spb.sideMenu.js")
                );



        }
    }

    public class FixCssRewrite : IItemTransform
    {
        public string Process(string includedVirtualPath, string input)
        {
            if (includedVirtualPath == null)
            {
                throw new ArgumentNullException("includedVirtualPath");
            }
            return ConvertUrlsToAbsolute(VirtualPathUtility.GetDirectory(WebUtility.ResolveUrl(includedVirtualPath)), input);
        }

        private string ConvertUrlsToAbsolute(string baseUrl, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return content;
            }
            Regex regex = new Regex("url\\(['\"]?(?<url>[^)]+?)['\"]?\\)");
            return regex.Replace(content, (MatchEvaluator)(match => ("url(" + RebaseUrlToAbsolute(baseUrl, match.Groups["url"].Value) + ")")));
        }


        private string RebaseUrlToAbsolute(string baseUrl, string url)
        {
            if ((string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(baseUrl)) || url.StartsWith("/", StringComparison.OrdinalIgnoreCase))
            {
                return url;
            }
            if (!baseUrl.EndsWith("/", StringComparison.OrdinalIgnoreCase))
            {
                baseUrl = baseUrl + "/";
            }
            return WebUtility.ResolveUrl(VirtualPathUtility.ToAbsolute(baseUrl + url));
        }
    }
}
