using System;
using System.Security.Cryptography;
using System.Text;
using ShareNet.Infrastructure.Utilities;

namespace WusNet.Infrastructure.Utilities
{
    /// <summary>
    /// 加密工具类
    /// </summary>
    public class EncryptionUtility
    {
        // Methods
        public static string Base64_Decode(string str)
        {
            byte[] bytes = Convert.FromBase64String(str);
            return Encoding.UTF8.GetString(bytes);
        }

        public static string Base64_Encode(string str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }

        public static string MD5(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            bytes = new MD5CryptoServiceProvider().ComputeHash(bytes);
            string str2 = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                str2 = str2 + bytes[i].ToString("x").PadLeft(2, '0');
            }
            return str2;
        }

        public static string MD5_16(string str)
        {
            return MD5(str).Substring(8, 0x10);
        }

        public static string SymmetricDncrypt(SymmetricEncryptType encryptType, string str, string ivString, string keyString)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            SymmetricEncrypt encrypt = new SymmetricEncrypt(encryptType)
            {
                IVString = ivString,
                KeyString = keyString
            };
            return encrypt.Decrypt(str);
        }

        public static string SymmetricEncrypt(SymmetricEncryptType encryptType, string str, string ivString, string keyString)
        {
            if ((string.IsNullOrEmpty(str) || string.IsNullOrEmpty(ivString)) || string.IsNullOrEmpty(keyString))
            {
                return str;
            }
            SymmetricEncrypt encrypt = new SymmetricEncrypt(encryptType)
            {
                IVString = ivString,
                KeyString = keyString
            };
            return encrypt.Encrypt(str);
        }

    }
}
