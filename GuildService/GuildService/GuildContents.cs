using System;
using System.Collections.Generic;
using System.Linq;
using ServiceCore;
using ServiceCore.EndPointNetwork.GuildService;
using ServiceCore.HeroesContents;
using Utility;

namespace GuildService
{
	internal class GuildContents
	{
		static GuildContents()
		{
			GuildContents.Load();
			HeroesContentsLoader.DB3Changed += GuildContents.Load;
			GuildContents.LoadGuildDailyGPResetTime();
		}

		public static void Initialize()
		{
		}

		public static Dictionary<int, GuildLevelUpInfo> GuildLevelUpInfoDic { get; set; }

		private static Dictionary<GPGainType, int> GuildDailyGPLimit { get; set; } = new Dictionary<GPGainType, int>();

		private static List<string> GuildForbiddenWords { get; set; }

		public static TimeSpan guildDailyGPResetInADay { get; set; }

		private static void Load()
		{
			GuildContents.GuildLevelUpInfoDic = (from x in HeroesContentsLoader.GetTable<GuildLevelUpInfo>()
			where ServiceCore.FeatureMatrix.IsEnable(x.Feature)
			select x).ToDictionary((GuildLevelUpInfo x) => x.Level, (GuildLevelUpInfo x) => x);
			foreach (GuildLevelUpInfo guildLevelUpInfo in GuildContents.GuildLevelUpInfoDic.Values)
			{
				guildLevelUpInfo.RequiredExp *= 10L;
			}
			GuildContents.LoadGuildDailyGPLimit();
			if (ServiceCore.FeatureMatrix.IsEnable("GuildHeroesCore_v1"))
			{
				GuildContents.LoadGuildForbiddenWords();
				return;
			}
			Log<GuildContents>.Logger.WarnFormat("doesn't need to load GuildForbiddenWords while using PlatformAPI(GuildHeroesCore_v1 feautre is off).", new object[0]);
		}

		public static void LoadGuildDailyGPLimit()
		{
			GuildContents.GuildDailyGPLimit.Clear();
			string @string = ServiceCore.FeatureMatrix.GetString("GuildLevel_DailyGPLimit");
			if (@string == null || @string.Length == 0)
			{
				return;
			}
			string[] array = @string.Split(new char[]
			{
				';'
			});
			int num = 1;
			foreach (string s in array)
			{
				if (num >= 3)
				{
					return;
				}
				int num2 = 0;
				try
				{
					num2 = int.Parse(s);
				}
				catch (Exception)
				{
					Log<GuildContents>.Logger.ErrorFormat("Unable to parse a DailyGPLimit to an integer. GPGainType: {0}, FeatureMatrix[GuildLevel_DailyGPLimit]: {1}", ((GPGainType)num).ToString(), ServiceCore.FeatureMatrix.GetString("GuildLevel_DailyGPLimit"));
					num2 = 0;
				}
				GuildContents.GuildDailyGPLimit[(GPGainType)num] = ((num2 != -1) ? (num2 * 10) : -1);
				num++;
			}
		}

		private static void LoadGuildDailyGPResetTime()
		{
			int integer = ServiceCore.FeatureMatrix.GetInteger("GuildLevel_DailyGPResetTimeAdder");
			TimeSpan timeSpan = new TimeSpan(integer, 0, 0);
			if (timeSpan.Days != 0)
			{
				Log<GuildContents>.Logger.ErrorFormat("GuildLevel_DailyGPResetTimeAdder should be shorter than 1 day. FeatureMatrix[GuildLevel_DailyGPResetTimeAdder]: {0}", integer);
				return;
			}
			if (timeSpan < TimeSpan.FromSeconds(0.0))
			{
				timeSpan.Add(TimeSpan.FromHours(24.0));
			}
			GuildContents.guildDailyGPResetInADay = timeSpan;
		}

		private static void LoadGuildForbiddenWords()
		{
			GuildContents.GuildForbiddenWords = new List<string>();
			try
			{
				List<GuildForbiddenWords> list = (from x in HeroesContentsLoader.GetTable<GuildForbiddenWords>()
				where x.Language.Equals(ServiceCore.FeatureMatrix.GetString("LanguageTag"), StringComparison.OrdinalIgnoreCase)
				select x).ToList<GuildForbiddenWords>();
				foreach (GuildForbiddenWords guildForbiddenWords in list)
				{
					GuildContents.GuildForbiddenWords.Add(guildForbiddenWords.ForbiddenName);
				}
			}
			catch (Exception ex)
			{
				Log<GuildContents>.Logger.ErrorFormat("No GuildForbidenWords. check LoadGuildForbiddenWords in code and data : error - {0}", ex.Message);
			}
		}

		public static long GetGuildLevelUpExp(int level)
		{
			GuildLevelUpInfo guildLevelUpInfo;
			GuildContents.GuildLevelUpInfoDic.TryGetValue(level, out guildLevelUpInfo);
			if (guildLevelUpInfo == null)
			{
				return -1L;
			}
			return guildLevelUpInfo.RequiredExp;
		}

		public static string GetGuildLevelUpRewardItemClass(int level)
		{
			GuildLevelUpInfo guildLevelUpInfo;
			GuildContents.GuildLevelUpInfoDic.TryGetValue(level, out guildLevelUpInfo);
			if (guildLevelUpInfo == null)
			{
				return "";
			}
			return guildLevelUpInfo.LevelUpRewardItemClass;
		}

		public static int GetDailyGPLimit(GPGainType gainType)
		{
			if (!GPGainTypeUtil.IsValidGPGainTypeValue(gainType, true))
			{
				return 0;
			}
			int result = 0;
			if (!GuildContents.GuildDailyGPLimit.TryGetValue(gainType, out result))
			{
				result = -1;
			}
			return result;
		}

		public static DateTime GetPrevDailyGPResetTime()
		{
			DateTime dateTime = DateTime.UtcNow.Date + GuildContents.guildDailyGPResetInADay;
			if (dateTime > DateTime.UtcNow)
			{
				dateTime -= TimeSpan.FromDays(1.0);
			}
			return dateTime;
		}

		public static bool HasForbiddenWords(string guildName)
		{
			if (GuildContents.GuildForbiddenWords == null)
			{
				return false;
			}
			bool result = false;
			foreach (string value in GuildContents.GuildForbiddenWords)
			{
				if (guildName.Contains(value))
				{
					result = true;
				}
			}
			return result;
		}
	}
}
