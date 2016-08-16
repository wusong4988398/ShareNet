using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WusNet.Infrastructure.WusNet
{
    [Serializable]
    public abstract class SerializablePropertiesBase : ISerializableProperties
    {
        // Fields
        private string propertyNames;
        private PropertySerializer propertySerializer;
        private string propertyValues;

        // Methods
        protected SerializablePropertiesBase()
        {
        }

        public T GetExtendedProperty<T>(string propertyName)
        {
            return this.PropertySerializer.GetExtendedProperty<T>(propertyName);
        }

        public T GetExtendedProperty<T>(string propertyName, T defaultValue)
        {
            return this.PropertySerializer.GetExtendedProperty<T>(propertyName, defaultValue);
        }

        public void SetExtendedProperty(string propertyName, object propertyValue)
        {
            this.PropertySerializer.SetExtendedProperty(propertyName, propertyValue);
        }

        void ISerializableProperties.Serialize()
        {
            this.PropertySerializer.Serialize(ref this.propertyNames, ref this.propertyValues);
        }

        // Properties
        public string PropertyNames
        {
            get
            {
                return this.propertyNames;
            }
            protected set
            {
                this.propertyNames = value;
            }
        }

        protected PropertySerializer PropertySerializer
        {
            get
            {
                if (this.propertySerializer == null)
                {
                    this.propertySerializer = new PropertySerializer(this.PropertyNames, this.PropertyValues);
                }
                return this.propertySerializer;
            }
        }

        public string PropertyValues
        {
            get
            {
                return this.propertyValues;
            }
            protected set
            {
                this.propertyValues = value;
            }
        }
    }


}
