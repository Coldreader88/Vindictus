using System;
using System.Collections;
using System.IO;
using System.Xml;

namespace HeroesOpTool
{
	public class LocalizeText
	{
		static LocalizeText()
		{
			LocalizeText._localText = null;
		}

		public static void LoadText(string filename)
		{
			if (!File.Exists(filename))
			{
				throw new ArgumentException("File not exists! - " + filename);
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(filename);
			int num = -1;
			foreach (object obj in xmlDocument.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Name == "HeroesOpToolText")
				{
					XmlAttribute xmlAttribute = xmlNode.Attributes["locale"];
					if (xmlAttribute == null)
					{
						throw new XmlException("Cannot find locale attribute in MabiOpToolText");
					}
					int num2 = 0;
					foreach (string text in xmlAttribute.Value.Split(new char[]
					{
						'|'
					}))
					{
						if (text.Length != 0)
						{
							try
							{
								LocalizeText._localeList.Add(text, num2);
							}
							catch (ArgumentException)
							{
								throw new XmlException("Duplicated locale name : " + text);
							}
							num2++;
						}
					}
					foreach (object obj2 in xmlNode.ChildNodes)
					{
						XmlNode xmlNode2 = (XmlNode)obj2;
						if (xmlNode2.Name != "Text")
						{
							throw new XmlException("Invalid Element Name : " + xmlNode2.Name);
						}
						xmlAttribute = xmlNode2.Attributes["id"];
						if (xmlAttribute == null)
						{
							throw new XmlException("Cannot find ID in text element!");
						}
						try
						{
							int num3 = (int)ushort.Parse(xmlAttribute.Value);
							if (num < num3)
							{
								num = num3;
							}
						}
						catch (FormatException)
						{
							throw new XmlException("Invalid ID - not integer : " + xmlAttribute.Value);
						}
						catch (OverflowException)
						{
							throw new XmlException("Invalid ID - out of index : " + xmlAttribute.Value);
						}
					}
					LocalizeText._localText = new string[LocalizeText._localeList.Count, num + 1];
					foreach (object obj3 in xmlNode.ChildNodes)
					{
						XmlNode xmlNode3 = (XmlNode)obj3;
						int num4 = int.Parse(xmlNode3.Attributes["id"].Value);
						foreach (object obj4 in xmlNode3.Attributes)
						{
							XmlAttribute xmlAttribute2 = (XmlAttribute)obj4;
							if (!(xmlAttribute2.Name == "id"))
							{
								object obj5 = LocalizeText._localeList[xmlAttribute2.Name];
								if (obj5 == null)
								{
									throw new XmlException("Invalid Locale string : " + xmlAttribute2.Name);
								}
								if (LocalizeText._localText[(int)obj5, num4] != null)
								{
									throw new XmlException("Duplicated locale & id : " + xmlAttribute2.Name + "/" + num4.ToString());
								}
								LocalizeText._localText[(int)obj5, num4] = xmlAttribute2.Value;
							}
						}
					}
				}
			}
			for (int j = 0; j <= LocalizeText._localText.GetUpperBound(0); j++)
			{
				for (int k = 0; k <= LocalizeText._localText.GetUpperBound(1); k++)
				{
					if (LocalizeText._localText[j, k] == null)
					{
						LocalizeText._localText[j, k] = string.Format("id({0})", k);
					}
				}
			}
		}

		public static string Get(int id)
		{
			if (LocalizeText._localText == null)
			{
				throw new InvalidOperationException("LocalizeText not initialized!");
			}
			if (id < 0 || id > LocalizeText._localText.GetUpperBound(1))
			{
				throw new IndexOutOfRangeException("Message ID is out of index");
			}
			return LocalizeText._localText[LocalizeText._localeID, id];
		}

		public static string[] GetLocaleList()
		{
			string[] array = new string[LocalizeText._localeList.Count];
			LocalizeText._localeList.Keys.CopyTo(array, 0);
			return array;
		}

		public static void SetLocale(string locale)
		{
			if (LocalizeText._localText == null)
			{
				throw new InvalidOperationException("LocalizeText not initialized!");
			}
			object obj = LocalizeText._localeList[locale];
			if (obj == null)
			{
				throw new IndexOutOfRangeException("No locale information named " + locale);
			}
			LocalizeText.SetLocale((int)obj);
		}

		public static void SetLocale(int localeID)
		{
			if (LocalizeText._localText == null)
			{
				throw new InvalidOperationException("LocalizeText not initialized!");
			}
			if (localeID < 0 || localeID > LocalizeText._localText.GetUpperBound(0))
			{
				throw new IndexOutOfRangeException("Locale ID is out of index");
			}
			LocalizeText._localeID = localeID;
		}

		private static SortedList _localeList = new SortedList();

		private static string[,] _localText;

		private static int _localeID = 0;
	}
}
