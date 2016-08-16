using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WusNet.Infrastructure.Utilities
{
    public static class StringUtility
    {
        // Methods
        public static string CleanInvalidCharsForXML(string rawXml)
        {
            if (string.IsNullOrEmpty(rawXml))
            {
                return rawXml;
            }
            StringBuilder builder = new StringBuilder();
            char[] chArray = rawXml.ToCharArray();
            for (int i = 0; i < chArray.Length; i++)
            {
                int num2 = Convert.ToInt32(chArray[i]);
                if ((((num2 < 0) || (num2 > 8)) && ((num2 < 11) || (num2 > 12))) && ((num2 < 14) || (num2 > 0x1f)))
                {
                    builder.Append(chArray[i]);
                }
            }
            return builder.ToString();
        }

        public static string StripSQLInjection(string sql)
        {
            if (!string.IsNullOrEmpty(sql))
            {
                string pattern = @"((\%27)|(\'))\s*((\%6F)|o|(\%4F))((\%72)|r|(\%52))";
                string str2 = @"(\%27)|(\')|(\-\-)";
                string str3 = @"\s+exec(\s|\+)+(s|x)p\w+";
                sql = Regex.Replace(sql, pattern, string.Empty, RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, str2, string.Empty, RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, str3, string.Empty, RegexOptions.IgnoreCase);
                sql = sql.Replace("%", "[%]");
            }
            return sql;
        }

        public static string Trim(string rawString, int charLimit)
        {
            return Trim(rawString, charLimit, "...");
        }

        public static string Trim(string rawString, int charLimit, string appendString)
        {
            if (string.IsNullOrEmpty(rawString) || (rawString.Length <= charLimit))
            {
                return rawString;
            }
            if (Encoding.UTF8.GetBytes(rawString).Length <= (charLimit * 2))
            {
                return rawString;
            }
            charLimit = (charLimit * 2) - Encoding.UTF8.GetBytes(appendString).Length;
            StringBuilder builder = new StringBuilder();
            int num2 = 0;
            for (int i = 0; i < rawString.Length; i++)
            {
                char ch = rawString[i];
                builder.Append(ch);
                num2 += (ch > '\x0080') ? 2 : 1;
                if (num2 >= charLimit)
                {
                    break;
                }
            }
            return builder.Append(appendString).ToString();
        }

        public static string UnicodeEncode(string rawString)
        {
            if ((rawString == null) || (rawString == string.Empty))
            {
                return rawString;
            }
            StringBuilder builder = new StringBuilder();
            string str2 = rawString;
            for (int i = 0; i < str2.Length; i++)
            {
                int num = str2[i];
                string str = "";
                if (num > 0x7e)
                {
                    builder.Append(@"\u");
                    str = num.ToString("x");
                    for (int j = 0; j < (4 - str.Length); j++)
                    {
                        builder.Append("0");
                    }
                }
                else
                {
                    str = ((char)num).ToString();
                }
                builder.Append(str);
            }
            return builder.ToString();
        }

    }
}
