using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareNet.Common.User.Account
{
    public interface IUserService
    {
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userId">用户ID</param>
        IUser GetUser(long userId);
    }
}
