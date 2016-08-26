using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WusNet.Infrastructure.Events;

namespace ShareNet.Common.User
{
    public static class EventOperationTypeExtension
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="eventOperationType"></param>      
        /// <returns></returns>
        public static string SignIn(this EventOperationType eventOperationType)
        {
            return "SignIn";
        }
    }
}
