using System;
using System.Configuration;

namespace NPL.SSO.Configuration
{
	public class AuthenticatorSection : ConfigurationSection
	{
		[ConfigurationProperty("soap", IsRequired = true)]
		public AuthenticatorSection.SoapElement Soap
		{
			get
			{
				return base["soap"] as AuthenticatorSection.SoapElement;
			}
		}

		[ConfigurationCollection(typeof(AuthenticatorSection.ServerCollection), AddItemName = "server")]
		[ConfigurationProperty("servers", IsRequired = false)]
		public AuthenticatorSection.ServerCollection Servers
		{
			get
			{
				return base["servers"] as AuthenticatorSection.ServerCollection;
			}
		}

		public class SoapElement : ConfigurationElement
		{
			[ConfigurationProperty("defaultTimeout", DefaultValue = 1000, IsRequired = false)]
			[IntegerValidator(MinValue = 0, MaxValue = 30000)]
			public int DefaultTimeout
			{
				get
				{
					return (int)base["defaultTimeout"];
				}
				set
				{
					base["defaultTimeout"] = value;
				}
			}

			[ConfigurationProperty("longTimeout", DefaultValue = 3000, IsRequired = false)]
			[IntegerValidator(MinValue = 0, MaxValue = 30000)]
			public int LongTimeout
			{
				get
				{
					return (int)base["longTimeout"];
				}
				set
				{
					base["longTimeout"] = value;
				}
			}

			[ConfigurationProperty("retryCount", DefaultValue = "3", IsRequired = false)]
			public int RetryCount
			{
				get
				{
					return (int)base["retryCount"];
				}
				set
				{
					base["retryCount"] = value;
				}
			}

			[ConfigurationProperty("useSingleHost", DefaultValue = false, IsRequired = false)]
			public bool UseSingleHost
			{
				get
				{
					return (bool)base["useSingleHost"];
				}
				set
				{
					base["useSingleHost"] = value;
				}
			}

			[ConfigurationProperty("host", DefaultValue = "", IsRequired = false)]
			public string Host
			{
				get
				{
					return (string)base["host"];
				}
				set
				{
					base["host"] = value;
				}
			}

			[ConfigurationProperty("hostProxy", DefaultValue = "", IsRequired = false)]
			public string HostProxy
			{
				get
				{
					return (string)base["hostProxy"];
				}
				set
				{
					base["hostProxy"] = value;
				}
			}

			[ConfigurationProperty("useIP", DefaultValue = false, IsRequired = false)]
			public bool UseIP
			{
				get
				{
					return (bool)base["useIP"];
				}
				set
				{
					base["useIP"] = value;
				}
			}

			[ConfigurationProperty("domain", DefaultValue = "", IsRequired = false)]
			public string Domain
			{
				get
				{
					return (string)base["domain"];
				}
				set
				{
					base["domain"] = value;
				}
			}

			[ConfigurationProperty("errorlog", DefaultValue = false, IsRequired = false)]
			public bool ErrorLog
			{
				get
				{
					return (bool)base["errorlog"];
				}
				set
				{
					base["errorlog"] = value;
				}
			}

			[ConfigurationProperty("soapVersion", DefaultValue = "2", IsRequired = false)]
			public int SoapVersion
			{
				get
				{
					return (int)base["soapVersion"];
				}
				set
				{
					base["soapVersion"] = value;
				}
			}
		}

		public class ServerElement : ConfigurationElement
		{
			[ConfigurationProperty("host", IsRequired = true)]
			public string Host
			{
				get
				{
					return (string)base["host"];
				}
				set
				{
					base["host"] = value;
				}
			}

			[ConfigurationProperty("ip", IsRequired = false)]
			public string IP
			{
				get
				{
					return (string)base["ip"];
				}
				set
				{
					base["ip"] = value;
				}
			}
		}

		public class ServerCollection : ConfigurationElementCollection
		{
			protected override ConfigurationElement CreateNewElement()
			{
				return new AuthenticatorSection.ServerElement();
			}

			protected override object GetElementKey(ConfigurationElement element)
			{
				return (element as AuthenticatorSection.ServerElement).Host;
			}

			public AuthenticatorSection.ServerElement this[int index]
			{
				get
				{
					return base.BaseGet(index % base.Count) as AuthenticatorSection.ServerElement;
				}
			}

			public new AuthenticatorSection.ServerElement this[string host]
			{
				get
				{
					return base.BaseGet(host) as AuthenticatorSection.ServerElement;
				}
			}
		}
	}
}
