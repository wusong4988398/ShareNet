using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using ShareNet.Common.User.Account;
using WusNet.Infrastructure.WusNet;

namespace ShareNet.Common.User
{
    /// <summary>
    /// 基于Form的身份认证服务实现
    /// </summary>
    public class FormsAuthenticationService:IAuthenticationService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user">登录的用户</param>
        /// <param name="rememberPassword">是否记住密码</param>
        public void SignIn(IUser user, bool rememberPassword)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 注销
        /// </summary>
        public void SignOut()
        {
            throw new NotImplementedException();
        }
        private IUser _signedInUser;
        /// <summary>
        /// 获取当前认证的用户
        /// </summary>
        /// <returns>
        /// 当前用户未通过认证则返回null
        /// </returns>
        public IUser GetAuthenticatedUser()
        {
            HttpContext httpContext = HttpContext.Current;
            if (httpContext == null || !httpContext.Request.IsAuthenticated || !(httpContext.User.Identity is FormsIdentity))
            {
                return null;
            }
            if (_signedInUser != null)
                return _signedInUser;
            IUserService userService = DIContainer.Resolve<IUserService>();
            _signedInUser = userService.GetFullUser(httpContext.User.Identity.Name);

            return _signedInUser;
        }
    }
}
