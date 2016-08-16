using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareNet.Common.User.Repositories;
using Spacebuilder.Common;

namespace ShareNet.Common.User
{
   public class UserProfileService
    {
        private IProfileRepository profileRepository;
        private IEducationExperienceRepository educationExperienceRepository;
        private IWorkExperienceRepository workExperienceRepository;

        public UserProfileService()
            : this(new ProfileRepository(), new EducationExperienceRepository(), new WorkExperienceRepository())
        { }

        public UserProfileService(IProfileRepository profileRepository, IEducationExperienceRepository educationExperienceRepository, IWorkExperienceRepository workExperienceRepository)
        {
            this.profileRepository = profileRepository;
            this.educationExperienceRepository = educationExperienceRepository;
            this.workExperienceRepository = workExperienceRepository;
        }

        #region Create & Update & Get
          

        #endregion Create & Update & Get


        #region Get & Gets
        /// <summary>
        /// 获取用户的所有教育经历
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <returns></returns>
        public IEnumerable<EducationExperience> GetEducationExperiences(long userId)
        {
            return educationExperienceRepository.PopulateEntitiesByEntityIds<long>(educationExperienceRepository.GetEducationExperienceOfUser(userId));
        }

        /// <summary>
        /// 获取用户的所有工作经历
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <returns></returns>
        public IEnumerable<WorkExperience> GetWorkExperiences(long userId)
        {
            return workExperienceRepository.PopulateEntitiesByEntityIds<long>(workExperienceRepository.GetWorkExperienceOfUser(userId));
        }
        #endregion  Get & Gets


    }
}
