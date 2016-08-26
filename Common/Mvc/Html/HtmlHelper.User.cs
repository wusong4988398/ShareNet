using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareNet.Common.Mvc.Html
{
    /// <summary>
    /// 扩展对User的HtmlHelper输出方法
    /// </summary>
    public static class HtmlHelperUserExtensions
    {
    }

    /// <summary>
    /// 超级链接Target
    /// </summary>
    public enum HyperLinkTarget
    {
        /// <summary>
        /// 将内容呈现在一个没有框架的新窗口中
        /// </summary>
        _blank,

        /// <summary>
        /// 将内容呈现在含焦点的框架中
        /// </summary>
        _self,

        /// <summary>
        /// 将内容呈现在上一个框架集父级中
        /// </summary>
        _parent,

        /// <summary>
        /// 将内容呈现在没有框架的全窗口中
        /// </summary>
        _top

    }
}
