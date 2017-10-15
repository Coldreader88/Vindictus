using System;

namespace Nexon.Com.Xml
{
	[AttributeUsage(AttributeTargets.Property)]
	public class XmlSerializeAttribute : Attribute
	{
		public bool Ignore;

		public bool CData;

		public bool Serializable;
	}
}
