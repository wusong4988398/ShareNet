using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareNet.Common.Utilities.WordFilter
{
    /// <summary>
    /// 单个过滤词字符串实体
    /// </summary>
    public class WordEntity
    {
        /// <summary>
        /// 需要替换的敏感词
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        /// 占位字符
        /// </summary>
        public string Replacement { get; set; }

        /// <summary>
        /// 敏感词处理状态
        /// </summary>
        public WordFilterStatus WordFilterStatus { get; set; }
    }
}
