﻿using System;

using System.Web.Mvc;
namespace ShareNet.Common.UI.Themes
{
    public abstract class ThemedWebViewPage : WebViewPage
    {

        /// <summary>
        /// 从Controller/Action设置的Layout（已经经过视图引擎定位）
        /// </summary>
        internal string OverridenLayoutPath { get; set; }

        /// <summary>
        /// 利用视图引擎定位layout的委托
        /// </summary>
        internal Func<ThemeAppearance, string, string, string> FindLayoutPathOfThemeDelegate;

        /// <summary>
        /// 当前页面的ThemeAppearance
        /// </summary>
        public ThemeAppearance ThemeAppearance { get; set; }

        /// <summary>
        /// 是否局部视图
        /// </summary>
        public bool IsPartialView { get; set; }

        /// <summary>
        /// 应用模块标识
        /// </summary>
        public string ApplicationKey { get; internal set; }
       
     
        /// <summary>
        /// 重写基类的方法用于实现View中设置layout也可以使用视图引擎定位
        /// </summary>
        public override void ExecutePageHierarchy()
        {
            base.ExecutePageHierarchy();
            // 在View中设置的Layout也可以依据视图引擎进行定位
            if (!IsPartialView)
            {
                if (!string.IsNullOrEmpty(OverridenLayoutPath))
                    Layout = OverridenLayoutPath;
                else if (!string.IsNullOrEmpty(Layout) && FindLayoutPathOfThemeDelegate != null && ThemeAppearance != null)
                {
                    try
                    {
                        Layout = FindLayoutPathOfThemeDelegate(ThemeAppearance, Layout, ApplicationKey);
                    }
                    catch
                    {
                        Layout = string.Format("~/Applications/{1}/Layouts/{0}.cshtml", Layout, ApplicationKey);
                    }
                }
            }

        }
    }
}
