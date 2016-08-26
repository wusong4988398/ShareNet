using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareNet.Common.User.Account
{
    /// <summary>
    /// 用户账户业务逻辑接口（使用单点登录时需替换具体实现）
    /// </summary>
    public interface IMembershipService
    {
        /// <summary>
        /// 验证提供的用户名和密码是否匹配
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>返回<see cref="UserLoginStatus"/></returns>
        UserLoginStatus ValidateUser(string username, string password);
    }
}
