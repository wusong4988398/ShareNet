//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using WusNet.Infrastructure.Repositories;

namespace ShareNet.Common.User.Repositories
{
    /// <summary>
    /// 用户基本资料的数据访问接口
    /// </summary>
    public interface IProfileRepository : IRepository<UserProfile>
    {
        /// <summary>
        /// 检查用户是否存在用户资料
        /// </summary>
        /// <param name="userId">用户Id</param>
        
        //好了
        bool UserIdIsExist(long userId);

        /// <summary>
        /// 更新完成度
        /// </summary>
        /// <param name="UserId">用户Id</param>
        void UpdateIntegrity(long userId);

    }
}