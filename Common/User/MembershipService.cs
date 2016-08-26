using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareNet.Common.User.Account;
using Spacebuilder.Common;

namespace ShareNet.Common.User
{
    /// <summary>
    /// 用户账户业务逻辑
    /// </summary>
    public class MembershipService:IMembershipService
    {
        public IUserRepository userRepository { get; set; }

        /// <summary>
        /// 验证提供的用户名和密码是否匹配
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>返回<see cref="UserLoginStatus"/></returns>
        public UserLoginStatus ValidateUser(string username, string password)
        {
            long userId=UserIdToUserNameDictionary.GetUserId(username);
            User user = userRepository.Get(userId);
            if (user == null) return UserLoginStatus.InvalidCredentials;
            if (!UserPasswordHelper.CheckPassword(password, user.Password, (UserPasswordFormat)user.PasswordFormat))
                return UserLoginStatus.InvalidCredentials;
            if (!user.IsActivated)
                return UserLoginStatus.NotActivated;
            if (user.IsBanned)
            {
                if (user.BanDeadline >= DateTime.UtcNow)
                    return UserLoginStatus.Banned;
                else
                {
                    user.IsBanned = false;
                    user.BanDeadline = DateTime.UtcNow;
                    userRepository.Update(user);
                }
            }
            return UserLoginStatus.Success;
         

            
        }
    }
}
