using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareNet.Common.User
{
  
   /// <summary>
   /// 生日类型
   /// </summary>
    public enum BirthdayType
    {
        /// <summary>
        /// 公历生日
        /// </summary>
        Birthday = 1,
        /// <summary>
        /// 阴历生日
        /// </summary>
        LunarBirthday = 2
    
    }

    /// <summary>
    /// 证件类型
    /// </summary>
    public enum CertificateType
    {
        /// <summary>
        /// 居民身份证
        /// </summary>
        Residentcard = 0,
        /// <summary>
        /// 军官证
        /// </summary>
        SergeantsCard = 1,
        /// <summary>
        /// 学生证
        /// </summary>
        StudentCard = 2,
        /// <summary>
        /// 驾驶证
        /// </summary>
        DriverCard = 3,
        /// <summary>
        /// 护照
        /// </summary>
        passport = 4,
        /// <summary>
        /// 港澳通行证
        /// </summary>
        HongKongPermit = 5
    }

    /// <summary>
    /// 性别类型
    /// </summary>
    public enum GenderType
    {
        /// <summary>
        /// 未设置
        /// </summary>
        NotSet = 0,
        /// <summary>
        /// 男
        /// </summary>
        Male = 1,
        /// <summary>
        /// 女
        /// </summary>
        FeMale = 2
    }

    /// <summary>
    /// 学历类型
    /// </summary>
    public enum DegreeType
    {
        /// <summary>
        /// 小学
        /// </summary>
        [Display(Name = "小学")]
        PrimarySchool = 7,
        /// <summary>
        /// 初中
        /// </summary>
        [Display(Name = "初中")]
        MiddleSchool = 6,
        /// <summary>
        /// 中专/技校
        /// </summary>
        [Display(Name = "中专/技校")]
        VocationalSchool = 5,
        /// <summary>
        /// 高中
        /// </summary>
        [Display(Name = "高中")]
        HighSchool = 4,
        /// <summary>
        /// 大专
        /// </summary>
        [Display(Name = "大专")]
        CommunityCollege = 3,
        /// <summary>
        /// 本科
        /// </summary>
        [Display(Name = "本科")]
        Undergraduate = 2,
        /// <summary>
        /// 硕士
        /// </summary>
        [Display(Name = "硕士")]
        Master = 1,
        /// <summary>
        /// 博士
        /// </summary>
        [Display(Name = "博士")]
        Doctor = 0

    }

    /// <summary>
    /// 用户激活、管制、封禁数
    /// </summary>
    public enum UserManageableCountType
    {
        /// <summary>
        /// 激活
        /// </summary>
        IsActivated = 1,

        /// <summary>
        /// 封禁
        /// </summary>
        IsBanned = 2,

        /// <summary>
        /// 管制
        /// </summary>
        IsModerated = 3,

        /// <summary>
        /// 总用户数
        /// </summary>
        IsAll = 4,

        /// <summary>
        /// 24小时新增数
        /// </summary>
        IsLast24 = 5

    }

    /// <summary>
    /// 用户排序字段
    /// </summary>
    public enum SortBy_User
    {
        FollowerCount,
        ReputationPoints,
        PreWeekReputationPoints,
        HitTimes,
        TradePoints,
        PreWeekHitTimes,
        Rank,
        DateCreated
    }

}
