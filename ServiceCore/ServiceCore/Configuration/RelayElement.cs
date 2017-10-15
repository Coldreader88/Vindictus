using System;
using System.Configuration;

namespace ServiceCore.Configuration
{
	public class RelayElement : ConfigurationElement
	{
		[ConfigurationProperty("tcpPort", DefaultValue = 27005)]
		[IntegerValidator(MinValue = 0, MaxValue = 65535)]
		public int TcpPort
		{
			get
			{
				return (int)base["tcpPort"];
			}
			set
			{
				base["tcpPort"] = value;
			}
		}

		[ConfigurationProperty("udpPorts")]
		[ConfigurationCollection(typeof(PortElement))]
		public PortsCollection UdpPorts
		{
			get
			{
				return base["udpPorts"] as PortsCollection;
			}
		}

		[ConfigurationProperty("logFileName", DefaultValue = "ProudNet.log")]
		[StringValidator]
		public string LogFileName
		{
			get
			{
				return base["logFileName"] as string;
			}
			set
			{
				base["logFileName"] = value;
			}
		}
	}
}
