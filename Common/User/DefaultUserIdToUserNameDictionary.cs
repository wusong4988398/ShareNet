using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Spacebuilder.Common;

namespace ShareNet.Common.User
{
    public class DefaultUserIdToUserNameDictionary:UserIdToUserNameDictionary
    {
        public IUserRepository userRepository { get; set; }
        /// <summary>
        /// 根据用户id获取用户名
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        protected override string GetUserNameByUserId(long userId)
        {
            User user = userRepository.Get(userId);
            if (user!=null)
            {
                return user.UserName;
            }
            return null;

        }
        /// <summary>
        /// 根据用户名获取用户Id
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>
        /// 用户Id
        /// </returns>
        protected override long GetUserIdByUserName(string userName)
        {
            return userRepository.GetUserIdByUserName(userName);
        }
    }
}
