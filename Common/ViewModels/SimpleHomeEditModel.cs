using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareNet.Common
{
    public class SimpleHomeEditModel : LoginEditModel
    {
        /// <summary>
        /// 是否属于模式窗口登录
        /// </summary>
        public bool loginInModal { get; set; }

        /// <summary>
        /// 回跳网页
        /// </summary>
        public string ReturnUrl { get; set; }


    }
}
