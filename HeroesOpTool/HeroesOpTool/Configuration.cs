using System;
using System.Xml.Serialization;

namespace HeroesOpTool
{
	[XmlRoot]
	public class Configuration : ICloneable
	{
		public object Clone()
		{
			return this;
		}

		[XmlAttribute]
		public string RcsIP;

		[XmlAttribute]
		public int RcsPort;

		[XmlAttribute]
		public string TextFile = "Texts.xml";

		[XmlAttribute]
		public string Language = "korean";

		[XmlIgnore]
		public string ID;

		[XmlIgnore]
		public byte[] Password;
	}
}
