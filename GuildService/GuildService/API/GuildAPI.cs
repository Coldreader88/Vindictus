using System;
using System.Text.RegularExpressions;
using GuildService.API.HeroesAPI;
using GuildService.API.PlatformAPI;
using ServiceCore;

namespace GuildService.API
{
	public class GuildAPI
	{
		private GuildAPI()
		{
			if (FeatureMatrix.IsEnable("GuildHeroesCore_v1"))
			{
				this.guildCore = new HeroesGuildAPI();
				return;
			}
			this.guildCore = new PlatformGuildAPI();
		}

		public static IGuildCore GetAPI()
		{
			return GuildAPI.guildApiInstance.guildCore;
		}

		public static int ServerCode
		{
			get
			{
				if (!FeatureMatrix.IsEnable("koKR"))
				{
					return FeatureMatrix.ServerCode;
				}
				if (FeatureMatrix.ServerCode != 11)
				{
					return 1;
				}
				return 11;
			}
		}

		public static GroupNameCheckResult _CheckGroupName(string name)
		{
			Regex regex = new Regex(FeatureMatrix.GetString("GuildNamingRule"), RegexOptions.Compiled);
			Regex regex2 = new Regex("^[a-zA-Z0-9_]*$", RegexOptions.Compiled);
			bool flag = regex2.IsMatch(name);
			if ((flag && name.Length > FeatureMatrix.GetInteger("GuildNamingRuleMaxBytes")) || (!flag && name.Length > FeatureMatrix.GetInteger("GuildNamingRuleMaxBytes") / 2))
			{
				return GroupNameCheckResult.NotMatchedNamingRuleMaxBytes;
			}
			if (!regex.IsMatch(name))
			{
				return GroupNameCheckResult.NotMatchedNamingRule;
			}
			int integer = FeatureMatrix.GetInteger("NamingRuleRepeatingCharCheckCount");
			if (integer > 0)
			{
				string text = string.Empty;
				foreach (char c in name)
				{
					if (!string.IsNullOrEmpty(text) && text[text.Length - 1] != c)
					{
						text = string.Empty;
					}
					text += c;
					if (text.Length > integer)
					{
						return GroupNameCheckResult.RepeatedCharacters;
					}
				}
			}
			return GroupNameCheckResult.Succeed;
		}

		private static GuildAPI guildApiInstance = new GuildAPI();

		private IGuildCore guildCore;
	}
}
