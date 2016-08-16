﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareNet.Common.User.Account
{
    /// <summary>
    /// 注册方式
    /// </summary>
    public enum RegistrationMode
    {
        /// <summary>
        /// 允许所有途径的注册
        /// </summary>
        [Display(Name = "允许注册")]
        All = 1,

        /// <summary>
        /// 仅允许通过邀请注册
        /// </summary>
        [Display(Name = "仅邀请注册")]
        Invitation = 2,

        /// <summary>
        /// 禁止注册
        /// </summary>
        [Display(Name = "禁止注册")]
        Disabled = 4
    }

    /// <summary>
    /// 帐号激活方式
    /// </summary>
    public enum AccountActivation
    {
        /// <summary>
        /// 用户注册时自动激活
        /// </summary>
        [Display(Name = "自动激活")]
        Automatic = 0,

        /// <summary>
        /// 通过验证Email激活
        /// </summary>
        [Display(Name = "Email激活")]
        Email = 1,

        /// <summary>
        /// 通过手机短信激活
        /// </summary>
        [Display(Name = "短信激活")]
        SMS = 2,

        /// <summary>
        /// 管理员激活
        /// </summary>
        [Display(Name = "管理员激活")]
        Administrator = 9
    }

    /// <summary>
    /// 用户密码存储格式
    /// </summary>
    public enum UserPasswordFormat
    {
        /// <summary>
        /// 密码未加密
        /// </summary>
        [Display(Name = "不加密")]
        Clear = 0,

        /// <summary>
        /// 标准MD5加密
        /// </summary>
        [Display(Name = "MD5加密")]
        MD5 = 1,
    }

    /// <summary>
    /// 用什么名称作为用户的DisplayName对外显示
    /// </summary>
    public enum DisplayNameType
    {
        /// <summary>
        /// 首先采用昵称作为DisplayName，如果昵称不存在则用真实姓名作为DisplayName，如果真实姓名也不存在则用UserName作为DisplayName
        /// </summary>
        NicknameFirst = 1,

        /// <summary>
        /// 首先采用真实姓名作为DisplayName，如果真实姓名不存在则用昵称作为DisplayName，如果昵称也不存在则用UserName作为DisplayName
        /// </summary>
        TrueNameFirst = 2
    }

    /// <summary>    
    /// 用于创建用户账号时的返回值
    /// </summary>
    public enum UserCreateStatus
    {
        /// <summary>
        /// 未知错误
        /// </summary>
        UnknownFailure = 0,

        /// <summary>
        /// 创建成功
        /// </summary>
        Created = 1,

        /// <summary>
        /// 用户名重复
        /// </summary>
        DuplicateUsername = 2,

        /// <summary>
        /// Email重复
        /// </summary>
        DuplicateEmailAddress = 3,

        /// <summary>
        /// 手机号重复
        /// </summary>
        DuplicateMobile = 4,


        /// <summary>
        /// 不允许的用户名
        /// </summary>
        DisallowedUsername = 5,

        ///// <summary>
        ///// 更新成功
        ///// </summary>
        //Updated = 6,

        /// <summary>
        /// 不合法的密码提示问题/答案
        /// </summary>
        InvalidQuestionAnswer = 7,

        /// <summary>
        /// 不合法的密码
        /// </summary>
        InvalidPassword = 8
    }

}