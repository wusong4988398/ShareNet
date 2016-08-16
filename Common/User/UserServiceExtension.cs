using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ShareNet.Common.User.Account;
using ShareNet.Common.User.Repositories;
using Spacebuilder.Common;
using WusNet.Infrastructure.WusNet;

namespace ShareNet.Common.User
{
    public static class UserServiceExtension
    {
        /// <summary>
        /// 获取完整的用户实体
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="userName">用户名</param>
        public static User GetFullUser(this IUserService userService, string userName)
        {
            IUserRepository userRepository = userService.GetUserRepository();
            long userId = UserIdToUserNameDictionary.GetUserId(userName);
            return userRepository.GetUser(userId);
        }

        /// <summary>
        /// 获取用户数据访问实例
        /// </summary>
        /// <param name="userService"></param>
        /// <returns></returns>
        private static IUserRepository GetUserRepository(this IUserService userService)
        {
            IUserRepository userRepository = DIContainer.Resolve<IUserRepository>();
            if (userRepository == null)
                userRepository = new UserRepository();
            return userRepository;
        }
    }
}
