using System;
using System.Configuration;

namespace ServiceCore
{
	public class LocalServiceConfig : ConfigurationSection
	{
		[ConfigurationProperty("NamingServiceAddress", IsRequired = false)]
		public string NamingServiceAddress
		{
			get
			{
				return base["NamingServiceAddress"] as string;
			}
		}

		[IntegerValidator(MinValue = 1, MaxValue = 65535)]
		[ConfigurationProperty("NamingServicePort", DefaultValue = 42, IsRequired = false)]
		public int NamingServicePort
		{
			get
			{
				return (int)base["NamingServicePort"];
			}
		}

		[ConfigurationProperty("Services")]
		public LocalServiceConfig.Services ServiceInfos
		{
			get
			{
				return base["Services"] as LocalServiceConfig.Services;
			}
		}

		public class Services : ConfigurationElementCollection
		{
			protected override ConfigurationElement CreateNewElement()
			{
				return new LocalServiceConfig.Service();
			}

			protected override object GetElementKey(ConfigurationElement element)
			{
				LocalServiceConfig.Service service = element as LocalServiceConfig.Service;
				if (service.Order == 0)
				{
					service.Order = LocalServiceConfig.Services.order;
					LocalServiceConfig.Services.order++;
				}
				return (element as LocalServiceConfig.Service).Order;
			}

			private static int order = 1;
		}

		public class Service : ConfigurationElement
		{
			public int Order { get; set; }

			[ConfigurationProperty("ServiceClass", IsRequired = true)]
			public string ServiceClass
			{
				get
				{
					return (string)base["ServiceClass"];
				}
				set
				{
					base["ServiceClass"] = value;
				}
			}

			[ConfigurationProperty("AutoStart", IsRequired = false)]
			public bool AutoStart
			{
				get
				{
					return this.Properties.Contains("AutoStart") && (bool)base["AutoStart"];
				}
				set
				{
					base["AutoStart"] = value.ToString();
				}
			}
		}
	}
}
