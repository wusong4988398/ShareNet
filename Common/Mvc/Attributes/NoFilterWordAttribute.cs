using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareNet.Common.Mvc.Attributes
{
    /// <summary>
    /// 表示不过滤敏感词
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class NoFilterWordAttribute : Attribute
    {

    }
}
