using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ShareNet.Common.Common.Settings;

using ShareNet.Common.PresentAreas;

using ShareNet.Common.UI.Themes;
using ShareNet.Common.User;
using ShareNet.Common.User.Account;

using ShareNet.Common.Utilities;
using ShareNet.Common.Utilities.Extensions;
using ShareNet.Common;
using ShareNet.Infrastructure.Utilities;
using WusNet.Infrastructure.WusNet;

namespace ShareNet.Common.Controllers
{
     [Themed(PresentAreaKeysOfBuiltIn.Channel, IsApplication = false)]
    public class AccountController:Controller
    {
        public IPageResourceManager pageResourceManager { get; set; }

        public IMembershipService membershipService { get; set; }
        public IUserService userService { get; set; }
        public IAuthenticationService authenticationService { get; set; }
        private UserSettings userSettings = DIContainer.Resolve<ISettingsManager<UserSettings>>().Get();
        #region 注册&登录

        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
       [HttpGet]
        public ActionResult Login()
        {
            string returnUrl = Request.QueryString.Get<string>("returnUrl", null);
            if (!string.IsNullOrEmpty(returnUrl) && returnUrl.ToLower().StartsWith("http://"))
            {
                if (!returnUrl.ToLower().StartsWith(WebUtility.HostPath(Request.Url)))
                {
                    Console.WriteLine();
                }
            }
            //如果用户已经登陆则直接返回
                if (UserContext.CurrentUser != null && !string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    Response.Cookies.Remove("returnUrl");
                    HttpCookie cookie=new HttpCookie("returnUrl",WebUtility.UrlEncode(returnUrl));
                    Response.Cookies.Add(cookie);
                }
                else
                {
                    Response.Cookies["returnUrl"].Expires=DateTime.Now;
                }

                //pageResourceManager.InsertTitlePart("登录");
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    ViewData["PresetMessage"] = "您访问的页面需要登录才能查看";
                }

                if (TempData["PromptState"] != null)
                    ViewData["PresetMessage"] = TempData["PromptState"];
                //ViewData["CanRegister"] = userSettings.RegistrationMode == RegistrationMode.All;
                return View(new LoginEditModel { loginInModal = false, ReturnUrl = returnUrl });
            
            
        }
        /// <summary>
        /// 处理登录
        /// </summary>
        /// <param name="model">用户输入的内容</param>
        /// <returns></returns>
         [HttpPost]
       public ActionResult Login(LoginEditModel model)
        {
            pageResourceManager.InsertTitlePart("登录");
            if (!ModelState.IsValid)
            {
                model.Password = string.Empty;
                return View(model);
            }
             //尝试登陆
            User.User user = model.AsUser();
             //使用用户名作为用户名和邮件分别尝试登陆
            UserLoginStatus userLoginStatus = membershipService.ValidateUser(user.UserName, user.Password);
            if (userLoginStatus == UserLoginStatus.InvalidCredentials)
            {
                //尝试使用邮件登录
                IUser userByEmail = userService.FindUserByEmail(user.UserName);
                if (userByEmail!=null)
                {
                    user = userByEmail as User.User;
                    userLoginStatus = membershipService.ValidateUser(userByEmail.UserName, model.Password);
                }
                if (userLoginStatus != UserLoginStatus.InvalidCredentials && !userByEmail.IsEmailVerified)
                {
                    ViewData["StatusMessage"] = new StatusMessageData(StatusMessageType.Error, "您的邮箱没有激活， ");
                    model.Password = string.Empty;
                    return View(model);
                }
            }
            else
            {
                user = userService.GetUser(model.UserName) as User.User;
            }
            if (userLoginStatus == UserLoginStatus.Success || (userLoginStatus == UserLoginStatus.NotActivated && userSettings.EnableNotActivatedUsersToLogin))
            {
                //让用户登录
                user.UserId = UserIdToUserNameDictionary.GetUserId(user.UserName);
                authenticationService.SignIn(user, model.RememberPassword);

            }
            if (userLoginStatus == UserLoginStatus.Success)
            {
                if (Request.Cookies["invite"] != null)
                {
                    
                }
                if (Request.Cookies.Get("returnUrl") != null)
                {

                    Response.Cookies["returnUrl"].Expires = DateTime.Now;
                }
                if (!string.IsNullOrEmpty(model.ReturnUrl))
                {
                    return Redirect(WebUtility.UrlDecode(model.ReturnUrl.Replace("[%]", "%")));
                }
                if (Request.Cookies != null)
                {
                    string returnUrl = Request.Cookies.Get("returnUrl") != null ? Request.Cookies.Get("returnUrl").Value : string.Empty;
                    if (!string.IsNullOrEmpty(returnUrl))
                        return Redirect(WebUtility.UrlDecode(returnUrl));
                }
                if (model.loginInModal && Request.UrlReferrer != null)
                    return Redirect(Request.UrlReferrer.AbsoluteUri);
                if (userSettings.MyHomePageAsSiteEntry)
                    return Redirect(SiteUrls.Instance().MyHome(user.UserId));
                return Redirect(SiteUrls.Instance().SiteHome());

            }
            else if (userLoginStatus == UserLoginStatus.InvalidCredentials)
            {
                ViewData["StatusMessage"] = new StatusMessageData(StatusMessageType.Error, "帐号密码不匹配，请检查您的帐号密码");
                model.Password = string.Empty;
                return View(model);
            }


            ViewData["StatusMessage"] = new StatusMessageData(StatusMessageType.Error, "系统发生未知错误");
            model.Password = string.Empty;
            return View(model);
        }

        #endregion
    }
}
