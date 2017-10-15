using System;
using System.Collections.Generic;
using System.Linq;
using GuildService.API;
using ServiceCore;
using ServiceCore.GuildServiceOperations;
using UnifiedNetwork.Cooperation;
using Utility;

namespace GuildService.Processor
{
	internal class QueryGuildIDProcessor : OperationProcessor<QueryGuildID>
	{
		public QueryGuildIDProcessor(GuildService service, QueryGuildID op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			bool queryResult = false;
			HeroesUserGuildInfo queryFuncResult = null;
			if (FeatureMatrix.IsEnable("GuildProcessorNotUseAsync"))
			{
				queryResult = true;
				List<HeroesUserGuildInfo> guildInfo = GuildAPI.GetAPI().GetGuildInfo(base.Operation.GuildMemberKey);
				if (guildInfo.Count > 0)
				{
					queryFuncResult = guildInfo.First<HeroesUserGuildInfo>();
				}
			}
			else
			{
				AsyncFuncSync<HeroesUserGuildInfo> sync = new AsyncFuncSync<HeroesUserGuildInfo>(delegate
				{
					List<HeroesUserGuildInfo> guildInfo3 = GuildAPI.GetAPI().GetGuildInfo(base.Operation.GuildMemberKey);
					if (guildInfo3.Count == 0)
					{
						return null;
					}
					return guildInfo3.First<HeroesUserGuildInfo>();
				});
				yield return sync;
				queryResult = sync.Result;
				queryFuncResult = sync.FuncResult;
			}
			base.Finished = true;
			if (queryResult)
			{
				if (queryFuncResult == null)
				{
					Log<QueryGuildIDProcessor>.Logger.WarnFormat("[{0}] No Guild", base.Operation.GuildMemberKey.CharacterName);
					yield return 0L;
					yield return "";
					yield return 0;
					yield return 0;
					int maxMember = FeatureMatrix.IsEnable("InGameGuild") ? FeatureMatrix.GetInteger("InGameGuild_MaxMember") : 0;
					yield return maxMember;
				}
				else
				{
					Log<QueryGuildIDProcessor>.Logger.WarnFormat("[{0}] GuildSN[{1}] GuildName[{2}] Rank[{3}] ", new object[]
					{
						base.Operation.GuildMemberKey.CharacterName,
						queryFuncResult.CharacterSN,
						queryFuncResult.CharacterName,
						queryFuncResult.GroupUserType
					});
					int maxMemberLimit = 0;
					int level = 0;
					using (HeroesDataContext heroesDataContext = new HeroesDataContext())
					{
						GetInGameGuildInfo guildInfo2 = heroesDataContext.GetGuildInfo(queryFuncResult.GuildSN);
						maxMemberLimit = guildInfo2.MaxMemberLimit;
						level = guildInfo2.Level;
					}
					yield return (long)queryFuncResult.GuildSN;
					yield return queryFuncResult.GuildName;
					yield return level;
					yield return (int)queryFuncResult.GroupUserType.ToGuildMemberRank();
					yield return maxMemberLimit;
				}
			}
			else
			{
				yield return new FailMessage("[QueryGuildIDProcessor] queryResult");
			}
			yield break;
		}

		private GuildService service;
	}
}
