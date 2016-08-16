//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------


using PetaPoco;
using ShareNet.Common.Common.Settings;
using WusNet.Infrastructure.Repositories;
using WusNet.Infrastructure.WusNet;

namespace ShareNet.Common.User.Repositories
{
    /// <summary>
    /// 用户资料的数据访问
    /// </summary>
    public class ProfileRepository : Repository<UserProfile>, IProfileRepository
    {
        /// <summary>
        /// 检查用户是否存在用户资料
        /// </summary>
        /// <param name="userId">用户Id</param>
        public bool UserIdIsExist(long userId)
        {
            var sql = Sql.Builder;
            sql.Select("Count(*)")
               .From("spb_Profiles")
               .Where("UserId = @0", userId);
            int count = CreateDao().ExecuteScalar<int>(sql);
            return count > 0;
        }

        /// <summary>
        /// 更新完成度
        /// </summary>
        /// <param name="userId">用户Id</param>
        public void UpdateIntegrity(long userId)
        {
            //ISettingsManager<UserProfileSettings> userProfileSettingsManager = DIContainer.Resolve<ISettingsManager<UserProfileSettings>>();
            //UserProfileSettings userProfileSettings = userProfileSettingsManager.Get();
            //int[] integrityItems = userProfileSettings.IntegrityProportions;
            //int integrity = integrityItems[(int)ProfileIntegrityItems.Birthday];

            //Database dao = CreateDao();
            //dao.OpenSharedConnection();
            //var sql = Sql.Builder;
            //sql.Select("Count(*)")
            //   .From("spb_EducationExperiences")
            //   .Where("UserId = @0", userId);

            //int countEducation = dao.ExecuteScalar<int>(sql);

            //if (countEducation > 0)
            //{
            //    integrity += integrityItems[(int)ProfileIntegrityItems.EducationExperience];
            //}

            //sql = Sql.Builder;
            //sql.Select("count(*)")
            //   .From("spb_WorkExperiences")
            //   .Where("UserId = @0", userId);
            //int countWork = dao.ExecuteScalar<int>(sql);

            //if (countWork > 0)
            //{
            //    integrity += integrityItems[(int)ProfileIntegrityItems.WorkExperience];
            //}

            //sql = Sql.Builder;
            //sql.Where("userId = @0", userId);
            //UserProfile userProfile = dao.FirstOrDefault<UserProfile>(sql);
            //if (userProfile != null)
            //{
            //    IUser user = DIContainer.Resolve<UserService>().GetUser(userProfile.UserId);

            //    integrity += (user.HasAvatar ? integrityItems[(int)ProfileIntegrityItems.Avatar] : 0);
            //    integrity += (userProfile.HasHomeAreaCode ? integrityItems[(int)ProfileIntegrityItems.HomeArea] : 0);
            //    integrity += (userProfile.HasIM ? integrityItems[(int)ProfileIntegrityItems.IM] : 0);
            //    integrity += (userProfile.HasIntroduction ? integrityItems[(int)ProfileIntegrityItems.Introduction] : 0);
            //    integrity += (userProfile.HasMobile ? integrityItems[(int)ProfileIntegrityItems.Mobile] : 0);
            //    integrity += (userProfile.HasNowAreaCode ? integrityItems[(int)ProfileIntegrityItems.NowArea] : 0);

            //    userProfile.Integrity = integrity;
            //    Update(userProfile);
            //}

            //dao.CloseSharedConnection();
        }

    }
}