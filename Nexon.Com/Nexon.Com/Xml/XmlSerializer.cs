using Nexon.Com;
using System;
using System.Collections;
using System.Reflection;
using System.Xml;

namespace Nexon.Com.Xml
{
    public class XmlSerializer
    {
        private Type childType;

        private PropertyInfo[] properties;

        public XmlSerializer(Type type)
        {
            this.childType = type;
            this.properties = this.childType.GetProperties();
        }

        public void Serialize(object o, XmlWriter writer)
        {
            if (writer == null)
            {
                throw new Exception("writer is null.");
            }
            if (o is IEnumerable || o is IEnumerator)
            {
                writer.WriteStartElement("ABC");
            }
            else
            {
                writer.WriteStartElement(this.childType.Name);
            }
            PropertyInfo[] propertyInfoArray = this.properties;
            for (int i = 0; i < (int)propertyInfoArray.Length; i++)
            {
                PropertyInfo propertyInfo = propertyInfoArray[i];
                bool ignore = false;
                bool cData = false;
                bool serializable = false;
                object[] customAttributes = propertyInfo.GetCustomAttributes(typeof(XmlSerializeAttribute), false);
                if ((int)customAttributes.Length == 1)
                {
                    XmlSerializeAttribute xmlSerializeAttribute = customAttributes[0] as XmlSerializeAttribute;
                    ignore = xmlSerializeAttribute.Ignore;
                    cData = xmlSerializeAttribute.CData;
                    serializable = xmlSerializeAttribute.Serializable;
                }
                if (serializable)
                {
                    IEnumerable value = propertyInfo.GetValue(o, null) as IEnumerable;
                    if (value != null)
                    {
                        foreach (object obj in value)
                        {
                            (new XmlSerializer(obj.GetType())).Serialize(obj, writer);
                        }
                    }
                }
                else if (!ignore)
                {
                    writer.WriteStartElement(propertyInfo.Name);
                    if (!cData)
                    {
                        writer.WriteString(propertyInfo.GetValue(o, null).Parse<string>(string.Empty));
                    }
                    else
                    {
                        writer.WriteCData(propertyInfo.GetValue(o, null).Parse<string>(string.Empty));
                    }
                    writer.WriteEndElement();
                }
            }
            writer.WriteEndElement();
        }
    }
}