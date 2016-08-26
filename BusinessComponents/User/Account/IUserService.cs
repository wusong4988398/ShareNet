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
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userName">用户名</param>
        IUser GetUser(string userName);

        /// <summary>
        /// 根据账号邮箱获取用户
        /// </summary>
        /// <param name="accountEmail">账号邮箱</param>
        IUser FindUserByEmail(string accountEmail);
    }
}
