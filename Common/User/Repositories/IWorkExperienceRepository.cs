//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System.Collections.Generic;
using WusNet.Infrastructure.Repositories;

namespace ShareNet.Common.User.Repositories
{
    /// <summary>
    /// 用户工作信息借口
    /// </summary>
    public interface IWorkExperienceRepository : IRepository<WorkExperience>
    {
        /// <summary>
        /// 获取所有用户工作信息
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        IEnumerable<long> GetWorkExperienceOfUser(long userId);

        /// <summary>
        /// 根据用户ID列表获取工作经历ID列表，本方法现用于用户搜索功能的索引生成
        /// </summary>
        /// <param name="userIds">用户ID列表</param>
        /// <returns>工作经历ID列表</returns>
        IEnumerable<long> GetEntityIdsByUserIds(IEnumerable<long> userIds);
    }
}