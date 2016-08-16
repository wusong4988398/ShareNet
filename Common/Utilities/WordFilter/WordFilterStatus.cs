using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareNet.Common.Utilities.WordFilter
{
    /// <summary>
    /// 字符处理状态
    /// </summary>
    
    public enum WordFilterStatus
    {
        /// <summary>
        /// 匹配到需要替代的关键字
        /// </summary>
        [Display(Name = "替换")]
        Replace,

        /// <summary>
        /// 匹配到禁止提交的关键字
        /// </summary>
        [Display(Name = "禁用")]
        Banned

    }
}
