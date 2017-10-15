using System;
using System.Configuration;

namespace ServiceCore
{
	public class FrontendConfig : ConfigurationSection
	{
		[ConfigurationProperty("Port")]
		public int Port
		{
			get
			{
				return (int)base["Port"];
			}
			set
			{
				base["Port"] = value;
			}
		}

		[ConfigurationProperty("PingPong")]
		public int PingPong
		{
			get
			{
				return (int)base["PingPong"];
			}
			set
			{
				base["PingPong"] = value;
			}
		}
	}
}
