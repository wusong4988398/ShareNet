using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareNet.Common.Utilities.WordFilter
{
    public class WordFilterTypeIds
    {
        #region Instance

        private static WordFilterTypeIds _instance = new WordFilterTypeIds();

        /// <summary>
        /// 对象实例化方法
        /// </summary>
        /// <returns></returns>
        public static WordFilterTypeIds Instance()
        {
            return _instance;
        }

        private WordFilterTypeIds()
        { }

        #endregion

        /// <summary>
        /// 敏感词
        /// </summary>
        /// <returns></returns>
        public int SensitiveWord()
        {
            return 1;
        }

        /// <summary>
        /// 表情
        /// </summary>
        /// <returns></returns>
        public int Emotion()
        {
            return 2;
        }
    }
}
