using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WusNet.Infrastructure.WusNet
{
    [Serializable]
    public class PropertySerializer
    {
        // Fields
        private NameValueCollection extendedAttributes = new NameValueCollection();

        // Methods
        public PropertySerializer(string propertyNames, string propertyValues)
        {
            if (!string.IsNullOrEmpty(propertyNames) && !string.IsNullOrEmpty(propertyValues))
            {
                this.extendedAttributes = ConvertToNameValueCollection(propertyNames, propertyValues);
            }
            else
            {
                this.extendedAttributes = new NameValueCollection();
            }
        }

        private static void ConvertFromNameValueCollection(NameValueCollection nvc, ref string propertyNames, ref string propertyValues)
        {
            if ((nvc != null) && (nvc.Count != 0))
            {
                StringBuilder builder = new StringBuilder();
                StringBuilder builder2 = new StringBuilder();
                int num = 0;
                foreach (string str in nvc.AllKeys)
                {
                    if (str.IndexOf(':') != -1)
                    {
                        throw new ArgumentException("SerializableProperties Name can not contain the character \":\"");
                    }
                    string str2 = nvc[str];
                    if (!string.IsNullOrEmpty(str2))
                    {
                        builder.AppendFormat("{0}:S:{1}:{2}:", str, num, str2.Length);
                        builder2.Append(str2);
                        num += str2.Length;
                    }
                }
                propertyNames = builder.ToString();
                propertyValues = builder2.ToString();
            }
        }

        private static NameValueCollection ConvertToNameValueCollection(string propertyNames, string propertyValues)
        {
            NameValueCollection values = new NameValueCollection();
            if (((propertyNames != null) && (propertyValues != null)) && ((propertyNames.Length > 0) && (propertyValues.Length > 0)))
            {
                char[] separator = new char[] { ':' };
                string[] strArray = propertyNames.Split(separator);
                for (int i = 0; i < (strArray.Length / 4); i++)
                {
                    int startIndex = int.Parse(strArray[(i * 4) + 2], CultureInfo.InvariantCulture);
                    int length = int.Parse(strArray[(i * 4) + 3], CultureInfo.InvariantCulture);
                    string str = strArray[i * 4];
                    if (((strArray[(i * 4) + 1] == "S") && (startIndex >= 0)) && ((length > 0) && (propertyValues.Length >= (startIndex + length))))
                    {
                        values[str] = propertyValues.Substring(startIndex, length);
                    }
                }
            }
            return values;
        }

        public T GetExtendedProperty<T>(string propertyName)
        {
            if (typeof(T) == typeof(string))
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
                return this.GetExtendedProperty<T>(propertyName, (T)converter.ConvertFrom(string.Empty));
            }
            return this.GetExtendedProperty<T>(propertyName, default(T));
        }

        public T GetExtendedProperty<T>(string propertyName, T defaultValue)
        {
            string str = this.extendedAttributes[propertyName];
            if (str == null)
            {
                return defaultValue;
            }
            return (T)Convert.ChangeType(str, typeof(T));
        }

        public void Serialize(ref string propertyNames, ref string propertyValues)
        {
            ConvertFromNameValueCollection(this.extendedAttributes, ref propertyNames, ref propertyValues);
        }

        public void SetExtendedProperty(string propertyName, object propertyValue)
        {
            if (propertyValue == null)
            {
                this.extendedAttributes.Remove(propertyName);
            }
            string str = propertyValue.ToString().Trim();
            if (string.IsNullOrEmpty(str))
            {
                this.extendedAttributes.Remove(propertyName);
            }
            else
            {
                this.extendedAttributes[propertyName] = str;
            }
        }
    }


}
