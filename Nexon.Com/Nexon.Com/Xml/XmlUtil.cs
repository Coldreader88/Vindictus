using System;
using System.IO;
using System.Xml;

namespace Nexon.Com.Xml
{
    public class XmlUtil
    {
        public XmlUtil()
        {
        }

        public static XmlElement AddAttributes(XmlDocument doc, XmlElement elm, string name, string value)
        {
            XmlAttribute xmlAttribute = doc.CreateAttribute(name);
            xmlAttribute.Value = value;
            elm.Attributes.Append(xmlAttribute);
            return elm;
        }

        public static XmlDocument CreateElement(XmlDocument doc, string name, string value, bool isCData)
        {
            XmlElement xmlElement = doc.CreateElement(name);
            if (!isCData)
            {
                xmlElement.InnerText = value;
            }
            else
            {
                xmlElement.AppendChild(doc.CreateCDataSection(value));
            }
            doc.DocumentElement.AppendChild(xmlElement);
            return doc;
        }

        public static string GetXmlNode(object obj)
        {
            StringWriter stringWriter = new StringWriter();
            XmlWriter xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true
            });
            XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
            xmlSerializer.Serialize(obj, xmlWriter);
            xmlWriter.Close();
            return stringWriter.ToString();
        }
    }
}