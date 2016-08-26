using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareNet.Common.User.OnlineUser.Repositories;

namespace ShareNet.Common.User.OnlineUser
{
    /// <summary>
    /// 在线用户业务逻辑类
    /// </summary>
    public class OnlineUserService
    {
        private IOnlineUserRepository onlineUserRepository;

      
        /// <summary>
        /// 判断用户是否在线
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsOnline(string userName)
        {
            Dictionary<string, OnlineUser> loggedUsers = GetLoggedUsers();
            return loggedUsers.ContainsKey(userName);
        }

        /// <summary>
        /// 获取在线登录用户列表
        /// </summary>
        /// <remarks>key=UserName,value=OnlineUser</remarks>
        public Dictionary<string, OnlineUser> GetLoggedUsers()
        {
            //设计要点：
            //1、缓存期限：集合，无需即时，使用一级缓存
            return onlineUserRepository.GetLoggedUsers();
        }
    }
}
