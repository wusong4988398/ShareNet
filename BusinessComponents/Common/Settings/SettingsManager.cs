using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareNet.Common.Common.Settings.Repositories;
using WusNet.Infrastructure.WusNet;

namespace ShareNet.Common.Common.Settings
{
    public class SettingsManager<TSettingsEntity> : ISettingsManager<TSettingsEntity> where TSettingsEntity : class, IEntity, new()
    {
        public ISettingsRepository<TSettingsEntity> repository { get; set; }
        public TSettingsEntity Get()
        {
           return repository.Get();
        }

        public void Save(TSettingsEntity settings)
        {
           repository.Save(settings);
        }
    }
}
