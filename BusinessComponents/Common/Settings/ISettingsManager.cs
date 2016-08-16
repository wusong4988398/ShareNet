using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareNet.Common.Common.Settings
{
    public interface ISettingsManager<TSettingsEntity> where TSettingsEntity : class
    {
        /// <summary>
        /// 获取设置
        /// </summary>
        /// <returns>settings</returns>
        TSettingsEntity Get();

        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="settings">settings</param>
        void Save(TSettingsEntity settings);
    }
}
