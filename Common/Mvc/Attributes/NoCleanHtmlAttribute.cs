using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareNet.Common.Mvc.Attributes
{
    /// <summary>
    /// 表示不清理Html
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class NoCleanHtmlAttribute : Attribute
    {

    }
}
