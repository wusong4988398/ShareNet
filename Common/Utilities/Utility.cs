using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareNet.Infrastructure.Utilities;
using WusNet.Infrastructure.Utilities;
using WusNet.Infrastructure.WusNet;

namespace ShareNet.Common.Utilities
{
    public class Utility
    {
        #region 加密与解密
        /// <summary>
        /// 解密上传附件的用户Id
        /// </summary>
        /// <param name="token">要解密的令牌</param>
        /// <param name="isTimeout">输出参数：令牌是否过期</param>
        /// <returns>解密后的用户Id</returns>
        public static long DecryptTokenForUploadfile(string token, out bool isTimeout)
        {
            string key = ConfigurationManager.AppSettings["TokenKeyForUploadfile"];
            string iv = System.Configuration.ConfigurationManager.AppSettings["TokenIvForUploadfile"];
            return DecryptToken(token, out isTimeout, key, iv);
        }

        /// <summary>
        /// 解密操作类
        /// </summary>
        /// <param name="token">网络令牌</param>
        /// <param name="isTimeout">是否失效</param>
        /// <param name="key">key</param>
        /// <param name="iv">向量</param>
        /// <returns></returns>
        private static long DecryptToken(string token, out bool isTimeout, string key, string iv)
        {
            long userId = 0;
            isTimeout = true;
            try
            {
                token = token.Replace(" ", "+");
                string tokenStr = EncryptionUtility.SymmetricDncrypt(SymmetricEncryptType.DES, token, iv, key);
                int partition = tokenStr.IndexOf(',');
                if (partition > 0)
                {
                    long.TryParse(tokenStr.Substring(0, tokenStr.IndexOf(',')), out userId);
                    DateTime dtTokenTime = new DateTime();
                    DateTime.TryParse(tokenStr.Substring(tokenStr.IndexOf(',')), out dtTokenTime);
                    isTimeout = DateTime.Compare(DateTime.Now, dtTokenTime) > 0;
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionFacade("解密操作的时候发生错误", ex);
            }
            return userId;
        } 
        #endregion

    }
}
