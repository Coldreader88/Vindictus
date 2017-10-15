using System;
using System.Configuration;

namespace Nexon.Com.Group.Game.Wrapper
{
	internal class GroupConfigSection : ConfigurationSection
	{
		[ConfigurationProperty("Service", IsRequired = true)]
		internal GroupConfigSection.ServiceElement Service
		{
			get
			{
				return base["Service"] as GroupConfigSection.ServiceElement;
			}
		}

		internal class ServiceElement : ConfigurationElement
		{
			[ConfigurationProperty("ConnectionTimeout", IsRequired = true)]
			[IntegerValidator(MinValue = 0, MaxValue = 20)]
			public int ConnectionTimeout
			{
				get
				{
					return (int)base["ConnectionTimeout"];
				}
				set
				{
					base["ConnectionTimeout"] = value;
				}
			}

			[ConfigurationProperty("DataBase_GuildMaster_SERVICE", IsRequired = true)]
			public string DataBase_GuildMaster_SERVICE
			{
				get
				{
					return (string)base["DataBase_GuildMaster_SERVICE"];
				}
				set
				{
					base["DataBase_GuildMaster_SERVICE"] = value;
				}
			}

			[ConfigurationProperty("DataBase_GuildMaster_TEST", IsRequired = true)]
			public string DataBase_GuildMaster_TEST
			{
				get
				{
					return (string)base["DataBase_GuildMaster_TEST"];
				}
				set
				{
					base["DataBase_GuildMaster_TEST"] = value;
				}
			}

			[ConfigurationProperty("DataBase_GuildMaster_WORK", IsRequired = true)]
			public string DataBase_GuildMaster_WORK
			{
				get
				{
					return (string)base["DataBase_GuildMaster_WORK"];
				}
				set
				{
					base["DataBase_GuildMaster_WORK"] = value;
				}
			}

			[ConfigurationProperty("DataBase_SERVICE", IsRequired = true)]
			public string DataBase_SERVICE
			{
				get
				{
					return (string)base["DataBase_SERVICE"];
				}
				set
				{
					base["DataBase_SERVICE"] = value;
				}
			}

			[ConfigurationProperty("DataBase_TEST", IsRequired = true)]
			public string DataBase_TEST
			{
				get
				{
					return (string)base["DataBase_TEST"];
				}
				set
				{
					base["DataBase_TEST"] = value;
				}
			}

			[ConfigurationProperty("DataBase_WORK", IsRequired = true)]
			public string DataBase_WORK
			{
				get
				{
					return (string)base["DataBase_WORK"];
				}
				set
				{
					base["DataBase_WORK"] = value;
				}
			}

			[ConfigurationProperty("GameCode", DefaultValue = 0, IsRequired = true)]
			[IntegerValidator(MinValue = 0)]
			public int Gamecode
			{
				get
				{
					return (int)base["GameCode"];
				}
				set
				{
					base["GameCode"] = value;
				}
			}

			[ConfigurationProperty("isDataLogging", DefaultValue = false, IsRequired = true)]
			public bool isDataLogging
			{
				get
				{
					return (bool)base["isDataLogging"];
				}
				set
				{
					base["isDataLogging"] = value;
				}
			}

			[ConfigurationProperty("isOverSea", DefaultValue = false, IsRequired = true)]
			public bool isOverSea
			{
				get
				{
					return (bool)base["isOverSea"];
				}
				set
				{
					base["isOverSea"] = value;
				}
			}

			[ConfigurationProperty("Mode", IsRequired = true)]
			public string Mode
			{
				get
				{
					return (string)base["Mode"];
				}
				set
				{
					base["Mode"] = value;
				}
			}
		}
	}
}
