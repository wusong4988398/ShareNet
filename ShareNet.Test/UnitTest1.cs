using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShareNet.Test
{
    [TestClass]
    public class UnitTest1
    {
        private string word = "{11}";
      
        [TestMethod]
        public void TestMethod2()
        {
            IgnoreCharacterProcess(word);
        }


        /// <summary>
        /// 处理需要跳过字符
        /// </summary>
        /// <param name="word">要处理敏感词</param>
        private string IgnoreCharacterProcess(string word)
        {
            if (!word.Contains("{"))
                return word;

            int startPos = 0, endPos = 0;

            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == '{')
                {
                    startPos = i;
                }
                else if (word[i] == '}')
                {
                    endPos = i;
                }

                if (endPos > startPos)
                {
                    string sss = word.Substring(startPos + 1, endPos - startPos - 1);
                    int ignoreCount = 0;
                    int.TryParse(word.Substring(startPos + 1, endPos - startPos - 1), out ignoreCount);

                    if (ignoreCount > 0)
                    {
                        word = word.Replace("{" + ignoreCount + "}", new String('*', ignoreCount));
                        i -= ignoreCount.ToString().Length + 1;
                        ignoreCount = 0;
                    }

                    startPos = 0;
                    endPos = 0;
                }
            }

            return word;
        }
    }
}
