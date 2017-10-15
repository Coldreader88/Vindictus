using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace RemoteControlSystem.Server
{
	public class Emergency
	{
		public ICollection Emergencies
		{
			get
			{
				return this._EmergencyList;
			}
		}

		public void LoadEmergencyInfo(XmlTextReader reader)
		{
			Emergency.EmergencyInformation[] array = (Emergency.EmergencyInformation[])new XmlSerializer(typeof(Emergency.EmergencyInformation[]), new XmlRootAttribute("EmergencyCall")).Deserialize(reader);
			foreach (Emergency.EmergencyInformation item in array)
			{
				this._EmergencyList.Add(item);
			}
		}

		public void WriteEmergencyInfo(XmlTextWriter writer)
		{
			Emergency.EmergencyInformation[] array = new Emergency.EmergencyInformation[this._EmergencyList.Count];
			this._EmergencyList.CopyTo(array, 0);
			new XmlSerializer(typeof(Emergency.EmergencyInformation[]), new XmlRootAttribute("EmergencyCall")).Serialize(writer, array, new XmlSerializerNamespaces(new XmlQualifiedName[]
			{
				XmlQualifiedName.Empty
			}));
		}

		public Emergency()
		{
			this._EmergencyList = new List<Emergency.EmergencyInformation>();
		}

		private List<Emergency.EmergencyInformation> _EmergencyList;

		public class EmergencyInformation
		{
			[XmlAttribute]
			public string Department { get; set; }

			[XmlAttribute]
			public string ID { get; set; }

			[XmlAttribute]
			public string Name { get; set; }

			[XmlAttribute]
			public string PhoneNumber { get; set; }

			[XmlAttribute]
			public string Mail { get; set; }

			[XmlAttribute]
			public string Rank { get; set; }
		}
	}
}
