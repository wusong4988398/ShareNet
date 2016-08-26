using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ShareNet.Common.User.Account;
using Spacebuilder.Common;
using WusNet.Infrastructure.WusNet;

namespace ShareNet.Common.User
{
    public class UserService:IUserService
    {
        private IUserRepository userRepository = DIContainer.Resolve<IUserRepository>();

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userId">用户ID</param>
       
        public IUser GetUser(long userId)
        {
            if (userId<=0)return null;
           
            return userRepository.GetUser(userId);
        }

        public IUser GetUser(string userName)
        {
            long userId = UserIdToUserNameDictionary.GetUserId(userName);
            return GetUser(userId);
        }

        public IUser FindUserByEmail(string accountEmail)
        {
            if (string.IsNullOrEmpty(accountEmail)) return null;
            long userid = userRepository.GetUserIdByEmail(accountEmail);
            return GetUser(userid);

        }
    }
}
