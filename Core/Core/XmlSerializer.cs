using System;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Devcat.Core
{
	public static class XmlSerializer
	{
		public static T LoadFromXml<T>(string fileName, bool ignoreException) where T : new()
		{
			T t = default(T);
			try
			{
				using (XmlTextReader xmlTextReader = new XmlTextReader(fileName))
				{
                    t = (T)new System.Xml.Serialization.XmlSerializer(typeof(T)).Deserialize(xmlTextReader);
                }
			}
			catch
			{
				if (!ignoreException)
				{
					throw;
				}
			}
			if (t == null)
			{
				t = ((default(T) == null) ? Activator.CreateInstance<T>() : default(T));
			}
			return t;
		}

		public static void SaveToXml<T>(string fileName, T value, bool ignoreException)
		{
			try
			{
				using (XmlTextWriter xmlTextWriter = new XmlTextWriter(fileName, Encoding.UTF8))
				{
					xmlTextWriter.Formatting = Formatting.Indented;
					xmlTextWriter.Indentation = 2;
					xmlTextWriter.IndentChar = ' ';
                    new System.Xml.Serialization.XmlSerializer(typeof(T)).Serialize(xmlTextWriter, value, new XmlSerializerNamespaces(new XmlQualifiedName[] { XmlQualifiedName.Empty }));
				}
			}
			catch
			{
				if (!ignoreException)
				{
					throw;
				}
			}
		}
	}
}
