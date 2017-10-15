using System;
using System.Configuration;
using System.IO;

namespace ServiceCore.Configuration
{
	public sealed class HeroesSection : ConfigurationSection
	{
		[ConfigurationProperty("channel")]
		public ChannelElement Channel
		{
			get
			{
				return base["channel"] as ChannelElement;
			}
		}

		[ConfigurationProperty("frontend")]
		public FrontendElement Frontend
		{
			get
			{
				return base["frontend"] as FrontendElement;
			}
		}

		[ConfigurationProperty("relay")]
		public RelayElement Relay
		{
			get
			{
				return base["relay"] as RelayElement;
			}
		}

		[ConfigurationProperty("channelrelay")]
		public RelayElement ChannelRelay
		{
			get
			{
				return base["channelrelay"] as RelayElement;
			}
		}

		[ConfigurationProperty("partyrelay")]
		public RelayElement PartyRelay
		{
			get
			{
				return base["partyrelay"] as RelayElement;
			}
		}

		[ConfigurationProperty("pvprelay")]
		public RelayElement PvpRelay
		{
			get
			{
				return base["pvprelay"] as RelayElement;
			}
		}

		[ConfigurationProperty("pingrelay")]
		public RelayElement PingRelay
		{
			get
			{
				return base["pingrelay"] as RelayElement;
			}
		}

		public static string FilePath
		{
			get
			{
				string directoryName = Path.GetDirectoryName(typeof(HeroesSection).Assembly.Location);
				return Path.Combine(directoryName, "ServiceCore.dll.config");
			}
		}

		public static HeroesSection Instance
		{
			get
			{
                System.Configuration.Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap
				{
					ExeConfigFilename = "ServiceCore.dll.config"
				}, ConfigurationUserLevel.None);
				return (configuration.GetSection("heroes") as HeroesSection) ?? new HeroesSection();
			}
		}
	}
}
