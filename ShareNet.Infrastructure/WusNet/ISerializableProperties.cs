using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WusNet.Infrastructure.WusNet
{
   public interface ISerializableProperties
    {
        // Methods
        T GetExtendedProperty<T>(string propertyName);
        T GetExtendedProperty<T>(string propertyName, T defaultValue);
        void Serialize();
        void SetExtendedProperty(string propertyName, object propertyValue);

    }
}
