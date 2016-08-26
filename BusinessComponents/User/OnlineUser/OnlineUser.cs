using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;
using ShareNet.Infrastructure.Utilities;
using WusNet.Infrastructure.Caching;
using WusNet.Infrastructure.WusNet;

namespace ShareNet.Common.User.OnlineUser
{
    /// <summary>
    /// 在线用户实体类
    /// </summary>
    [TableName("ws_OnlineUsers")]
    [PrimaryKey("Id", AutoIncrement = true)]
    [CacheSetting(true)]
    [Serializable]
    public class OnlineUser : IEntity
    {
        #region 构造函数



        /// <summary>
        /// 依据登录用户创建OnlineUser
        /// </summary>
        /// <param name="user"></param>
        public static OnlineUser New(IUser user)
        {
            OnlineUser onlineUser = new OnlineUser()
            {
                UserId = user.UserId,
                UserName = user.UserName,
                DisplayName = user.DisplayName,
                LastActivityTime = DateTime.UtcNow,
                LastAction = user.LastAction,
                Ip = WebUtility.GetIP(),
                DateCreated = DateTime.UtcNow

            };
            return onlineUser;
        }

        /// <summary>
        /// 依据匿名登录用户创建OnlineUser
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static OnlineUser NewAnonymous(string userName)
        {
            OnlineUser onlineUser = new OnlineUser()
            {
                UserId = 0,
                UserName = userName,
                DisplayName = userName,
                LastActivityTime = DateTime.UtcNow,
                LastAction = string.Empty,
                Ip = WebUtility.GetIP(),
                DateCreated = DateTime.UtcNow
            };
            return onlineUser;
        }

        #endregion

        /// <summary>
        /// 新建实体时使用
        /// </summary>

        public static OnlineUser New()
        {
            OnlineUser onlineUser = new OnlineUser()
            {
                UserName = string.Empty,
                DisplayName = string.Empty,
                LastActivityTime = DateTime.UtcNow,
                LastAction = string.Empty,
                Ip = string.Empty,
                DateCreated = DateTime.UtcNow

            };
            return onlineUser;
        }

        #region 需持久化属性

        /// <summary>
        ///Id
        /// </summary>
        public long Id { get; protected set; }

        /// <summary>
        ///用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///对外显示的名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        ///上次活动时间
        /// </summary>
        public DateTime LastActivityTime { get; set; }

        /// <summary>
        ///上次操作
        /// </summary>
        public string LastAction { get; set; }

        /// <summary>
        ///IP
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        ///创建时间
        /// </summary>
        public DateTime DateCreated { get; set; }

        #endregion

        #region IEntity 成员
        [Ignore]
        object IEntity.EntityId { get { return this.Id; } }
         [Ignore]
        bool IEntity.IsDeletedInDatabase { get; set; }

        #endregion
    }
}
