using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;
using WusNet.Infrastructure.Caching;
using WusNet.Infrastructure.Repositories;

namespace ShareNet.Common.User.OnlineUser.Repositories
{
    public class OnlineUserRepository:Repository<OnlineUser>,IOnlineUserRepository
    {
        public void Offline(string userName)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取在线登录用户列表
        /// </summary>
        /// <remarks>key=UserName,value=OnlineUser</remarks>
        public Dictionary<string, OnlineUser> GetLoggedUsers()
        {
            //设计要点：
            //1、缓存期限：集合，无需即时，使用一级缓存
            string cacheKey = GetCacheKey_LoggedUsers();
            //done:liuz,by zhengw:使用一级缓存，应调用GetFromFirstLevel方法,而且应该用泛型类型的
            //已修改
            Dictionary<string, OnlineUser> dictionary = cacheService.GetFromFirstLevel<Dictionary<string, OnlineUser>>(cacheKey);
            if (dictionary==null)
            {
                Sql sql = Sql.Builder;
                sql.Select("*")
                    .From("ws_OnlineUsers")
                    .Where("UserId!=0")
                    .OrderBy("LastActivityTime desc");
                List<OnlineUser> list = CreateDao().Fetch<OnlineUser>(sql);
                dictionary = new Dictionary<string, OnlineUser>();
                foreach (var onlineUser in list)
                {
                    //dictionary.Add(onlineUser.UserName,onlineUser);
                    dictionary[onlineUser.UserName] = onlineUser;
                }
                cacheService.Set(cacheKey, dictionary, CachingExpirationType.ObjectCollection);

            }
            return dictionary;
        }

        public IList<OnlineUser> GetAnonymousUsers()
        {
            throw new NotImplementedException();
        }

        public void Refresh(ConcurrentDictionary<string, OnlineUser> OnlineUsersForProcess)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取在线登录用户CacheKey
        /// </summary>
        /// <returns></returns>
        private string GetCacheKey_LoggedUsers()
        {
            return "OnlineUser_LoggedUsers";
        }
    }
}
