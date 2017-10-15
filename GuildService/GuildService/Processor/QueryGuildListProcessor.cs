using System;
using System.Collections.Generic;
using GuildService.API;
using ServiceCore;
using ServiceCore.EndPointNetwork.GuildService;
using ServiceCore.GuildServiceOperations;
using UnifiedNetwork.Cooperation;
using Utility;

namespace GuildService.Processor
{
	internal class QueryGuildListProcessor : OperationProcessor<QueryGuildList>
	{
		public QueryGuildListProcessor(GuildService service, QueryGuildList op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			if (base.Operation.SearchKey == null || base.Operation.SearchKey.Length < 2 || base.Operation.SearchKey.Length > FeatureMatrix.GetInteger("GuildNamingRuleMaxBytes"))
			{
				Log<QueryGuildList>.Logger.InfoFormat("길드명 또는 길드 마스터 이름이 잘못되었습니다.{0}", base.Operation.SearchKey);
				base.Finished = true;
				yield return new FailMessage("[QueryGuildListProcessor] GuildNamingRuleMaxBytes");
			}
			else
			{
				int total_page = 1;
				bool queryResult = false;
				List<InGameGuildInfo> queryFuncResult = new List<InGameGuildInfo>();
				if (FeatureMatrix.IsEnable("GuildProcessorNotUseAsync"))
				{
					queryResult = true;
					int num;
					ICollection<HeroesGuildInfo> list = GuildAPI.GetAPI().SearchGuild(base.Operation.QueryType, 0, base.Operation.Page, base.Operation.PageSize, base.Operation.SearchKey, out num);
					total_page = ((num == 0) ? 1 : ((num - 1) / (int)base.Operation.PageSize + 1));
					queryFuncResult = list.ToGuildInfoList();
				}
				else
				{
					AsyncFuncSync<List<InGameGuildInfo>> sync = new AsyncFuncSync<List<InGameGuildInfo>>(delegate
					{
						int num2;
						ICollection<HeroesGuildInfo> list2 = GuildAPI.GetAPI().SearchGuild(this.Operation.QueryType, 0, this.Operation.Page, this.Operation.PageSize, this.Operation.SearchKey, out num2);
						total_page = ((num2 == 0) ? 1 : ((num2 - 1) / (int)this.Operation.PageSize + 1));
						return list2.ToGuildInfoList();
					});
					yield return sync;
					queryResult = sync.Result;
					queryFuncResult = sync.FuncResult;
				}
				base.Finished = true;
				if (queryResult)
				{
					yield return queryFuncResult;
					yield return base.Operation.Page;
					yield return total_page;
				}
				else
				{
					yield return new FailMessage("[QueryGuildListProcessor] queryResult");
				}
			}
			yield break;
		}

		private GuildService service;
	}
}
