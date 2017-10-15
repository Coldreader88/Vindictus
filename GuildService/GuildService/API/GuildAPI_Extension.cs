using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceCore;
using ServiceCore.EndPointNetwork.GuildService;
using Utility;

namespace GuildService.API
{
	public static class GuildAPI_Extension
	{
		public static GuildMemberRank ToGuildMemberRank(this HeroesGuildUserType value)
		{
			if (value <= HeroesGuildUserType.sysop)
			{
				if (value == HeroesGuildUserType.master)
				{
					return GuildMemberRank.Master;
				}
				if (value == HeroesGuildUserType.sysop)
				{
					return GuildMemberRank.Operator;
				}
			}
			else
			{
				if (value == HeroesGuildUserType.member_lv1)
				{
					return GuildMemberRank.Member;
				}
				if (value != HeroesGuildUserType.webmember)
				{
					if (value == HeroesGuildUserType.memberWaiting)
					{
						return GuildMemberRank.Wait;
					}
				}
			}
			return GuildMemberRank.Ignorable;
		}

		public static HeroesGuildUserType ToGroupUserType(this GuildMemberRank value)
		{
			switch (value)
			{
			case GuildMemberRank.Master:
				return HeroesGuildUserType.master;
			case GuildMemberRank.Operator:
				return HeroesGuildUserType.sysop;
			case GuildMemberRank.Member:
				return HeroesGuildUserType.member_lv1;
			case GuildMemberRank.Wait:
				return HeroesGuildUserType.memberWaiting;
			default:
				return HeroesGuildUserType.unknown;
			}
		}

		public static string RemoveServerHeader(this string headerAttachedStr)
		{
			string[] array = headerAttachedStr.Split(new char[]
			{
				'[',
				']'
			});
			if (array.Length == 0)
			{
				return "";
			}
			return array.Last<string>();
		}

		public static InGameGuildInfo ToGuildInfo(this HeroesGuildInfo groupInfo)
		{
			InGameGuildInfo result = null;
			try
			{
				HeroesDataContext heroesDataContext = new HeroesDataContext();
				int maxMemberLimit = heroesDataContext.GetMaxMemberLimit(groupInfo.GuildSN);
				if (FeatureMatrix.IsEnable("NewbieGuildRecommend"))
				{
					GetInGameGuildInfo guildInfo = heroesDataContext.GetGuildInfo(groupInfo.GuildSN);
					int guildLevel = 1;
					long guildPoint = 0L;
					DateTime guildLastDailyGPReset = heroesDataContext.GetGuildLastDailyGPReset(groupInfo.GuildSN);
					Dictionary<byte, int> dailyGainGP;
					if (FeatureMatrix.IsEnable("GuildLevel"))
					{
						guildLevel = guildInfo.Level;
						guildPoint = guildInfo.Exp;
						DateTime prevDailyGPResetTime = GuildContents.GetPrevDailyGPResetTime();
						if (guildLastDailyGPReset >= prevDailyGPResetTime)
						{
							dailyGainGP = heroesDataContext.GetGuildDailyGainGP(groupInfo.GuildSN);
						}
						else
						{
							dailyGainGP = new Dictionary<byte, int>();
							heroesDataContext.ResetInGameGuildDailyGainGP(groupInfo.GuildSN);
						}
					}
					else
					{
						dailyGainGP = new Dictionary<byte, int>();
					}
					result = new InGameGuildInfo(groupInfo.GuildSN, groupInfo.GuildName, guildLevel, groupInfo.RealUserCount, groupInfo.NameInGroup_Master, maxMemberLimit, guildInfo.NewbieRecommend, guildPoint, guildInfo.Notice, dailyGainGP);
				}
				else
				{
					result = new InGameGuildInfo(groupInfo.GuildSN, groupInfo.GuildName, 1, groupInfo.RealUserCount, groupInfo.NameInGroup_Master, maxMemberLimit);
				}
			}
			catch (Exception ex)
			{
				Log<GuildAPI>.Logger.ErrorFormat("Exception occured in GuildAPI_Extension::ToGuildInfo( GroupInfo ) : {0}", ex.ToString());
				return null;
			}
			return result;
		}

		public static List<InGameGuildInfo> ToGuildInfoList(this IEnumerable<HeroesGuildInfo> list)
		{
			List<InGameGuildInfo> list2 = new List<InGameGuildInfo>();
			foreach (HeroesGuildInfo groupInfo in list)
			{
				InGameGuildInfo inGameGuildInfo = groupInfo.ToGuildInfo();
				if (inGameGuildInfo != null)
				{
					list2.Add(new InGameGuildInfo(inGameGuildInfo));
				}
			}
			return list2;
		}

		public static bool HasRankChangePermission(this GuildMemberRank operatorRank, GuildMemberRank fromRank, GuildMemberRank toRank)
		{
			if (operatorRank == GuildMemberRank.Master)
			{
				if (fromRank == GuildMemberRank.Member && toRank == GuildMemberRank.Operator)
				{
					return true;
				}
				if (fromRank == GuildMemberRank.Operator && toRank == GuildMemberRank.Member)
				{
					return true;
				}
			}
			return false;
		}

		public static bool HasSecedePermission(this GuildMemberRank operatorRank, GuildMemberRank fromRank, GuildMemberRank toRank)
		{
			if (toRank == GuildMemberRank.Unknown)
			{
				if (operatorRank == GuildMemberRank.Master)
				{
					if (fromRank == GuildMemberRank.Operator || fromRank == GuildMemberRank.Member)
					{
						return true;
					}
				}
				else if (operatorRank == GuildMemberRank.Operator && fromRank == GuildMemberRank.Member)
				{
					return true;
				}
			}
			return false;
		}

		public static bool HasAcceptJoinPermission(this GuildMemberRank operatorRank, GuildMemberRank targetRank)
		{
			return (operatorRank == GuildMemberRank.Master || operatorRank == GuildMemberRank.Operator) && targetRank == GuildMemberRank.Wait;
		}

		public static bool HasNewbieRecommendPermission(this GuildMemberRank operatorRank)
		{
			return operatorRank == GuildMemberRank.Master || operatorRank == GuildMemberRank.Operator;
		}

		public static string FuncFormat(this string format, params object[] parameters)
		{
			if (parameters.Length == 0)
			{
				return format;
			}
			StringBuilder stringBuilder = new StringBuilder(format);
			foreach (int num in Enumerable.Range(0, parameters.Length))
			{
				if (num == 0)
				{
					stringBuilder.Append("(").Append(parameters[num]);
				}
				else
				{
					stringBuilder.Append(", ").Append(parameters[num]);
				}
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}
	}
}
