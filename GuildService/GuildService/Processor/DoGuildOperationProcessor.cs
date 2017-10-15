using System;
using System.Collections.Generic;
using GuildService.API;
using ServiceCore;
using ServiceCore.EndPointNetwork.GuildService;
using ServiceCore.GuildServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using Utility;

namespace GuildService.Processor
{
	internal class DoGuildOperationProcessor : EntityProcessor<DoGuildOperation, GuildEntity>
	{
		public DoGuildOperationProcessor(GuildService service, DoGuildOperation op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			GuildMember operatorMember = base.Entity.GetGuildMember(base.Operation.GuildMemberKey.CharacterName);
			if (operatorMember == null)
			{
				Log<DoGuildOperationProcessor>.Logger.ErrorFormat("DoOperationFailed : invalid operatorMember [{0}]", base.Operation.GuildMemberKey.CharacterName);
				base.Finished = true;
				yield return new FailMessage("[DoGuildOperationProcessor] operatorMember");
			}
			GuildMemberRank operatorRank = operatorMember.Rank;
			bool funcResult = false;
			List<GuildOperationResult> opResultList = new List<GuildOperationResult>();
			if (FeatureMatrix.IsEnable("GuildProcessorNotUseAsync"))
			{
				try
				{
					opResultList = base.Entity.DoGuildOperation(base.Operation.Operations, operatorRank);
				}
				catch (Exception ex)
				{
					Log<DoGuildOperationProcessor>.Logger.Error(string.Format("Exception on DoGuildOperation. [ Entity : {0}, OperationCount : {1}, FirstOperation : {2} ]", base.Entity.ToString(), base.Operation.Operations.Count, base.Operation.Operations[0].ToString()), ex);
				}
				funcResult = true;
			}
			else
			{
				AsyncFuncSync<List<GuildOperationResult>> sync = new AsyncFuncSync<List<GuildOperationResult>>(delegate
				{
					try
					{
						return this.Entity.DoGuildOperation(this.Operation.Operations, operatorRank);
					}
					catch (Exception ex2)
					{
						Log<DoGuildOperationProcessor>.Logger.Error(string.Format("Exception on DoGuildOperation. [ Entity : {0}, OperationCount : {1}, FirstOperation : {2} ]", this.Entity.ToString(), this.Operation.Operations.Count, this.Operation.Operations[0].ToString()), ex2);
					}
					return new List<GuildOperationResult>
					{
						new GuildOperationResult(false, null, null)
					};
				});
				yield return sync;
				funcResult = sync.Result;
				opResultList = sync.FuncResult;
			}
			bool operationResult = true;
			if (funcResult)
			{
				List<GuildOperationInfo> successInfo = new List<GuildOperationInfo>();
				foreach (GuildOperationResult guildOperationResult in base.Entity.SyncOperationResult(opResultList))
				{
					if (guildOperationResult != null)
					{
						using (List<GuildOperationInfo>.Enumerator enumerator2 = base.Operation.Operations.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								GuildOperationInfo guildOperationInfo = enumerator2.Current;
								if (guildOperationInfo.Target.Equals(guildOperationResult.Key.CharacterName))
								{
									successInfo.Add(guildOperationInfo);
									break;
								}
							}
							continue;
						}
					}
					operationResult = false;
				}
				if (FeatureMatrix.IsEnable("RankTitle"))
				{
					base.Entity.RequestUpdateBriefGuildInfo(successInfo);
				}
				if (FeatureMatrix.IsEnable("GuildLevel"))
				{
					base.Entity.GiveGuildLevelUpRewardAP(successInfo);
				}
				base.Finished = true;
				if (operationResult)
				{
					yield return new OkMessage();
				}
				else
				{
					yield return new FailMessage("[DoGuildOperationProcessor] operationResult");
				}
			}
			else
			{
				base.Finished = true;
				yield return new FailMessage("[DoGuildOperationProcessor] funcResult");
			}
			yield break;
		}

		private GuildService service;
	}
}
