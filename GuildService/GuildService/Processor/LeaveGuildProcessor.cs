using System;
using System.Collections.Generic;
using GuildService.API;
using ServiceCore;
using ServiceCore.GuildServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace GuildService.Processor
{
	internal class LeaveGuildProcessor : EntityProcessor<LeaveGuild, GuildEntity>
	{
		public LeaveGuildProcessor(GuildService service, LeaveGuild op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			bool leaveResult = false;
			HeroesGuildMemberInfo leaveFuncResult = null;
			if (FeatureMatrix.IsEnable("GuildProcessorNotUseAsync"))
			{
				leaveResult = true;
				GuildAPI.GetAPI().LeaveGuild(base.Entity, base.Operation.Key);
				leaveFuncResult = GuildAPI.GetAPI().GetMemberInfo(base.Entity, base.Operation.Key);
			}
			else
			{
				AsyncFuncSync<HeroesGuildMemberInfo> sync = new AsyncFuncSync<HeroesGuildMemberInfo>(delegate
				{
					GuildAPI.GetAPI().LeaveGuild(base.Entity, base.Operation.Key);
					return GuildAPI.GetAPI().GetMemberInfo(base.Entity, base.Operation.Key);
				});
				yield return sync;
				leaveResult = sync.Result;
				leaveFuncResult = sync.FuncResult;
			}
			base.Finished = true;
			if (leaveResult)
			{
				if (leaveFuncResult != null)
				{
					base.Entity.UpdateGroupMemberInfo(base.Operation.Key, leaveFuncResult);
					base.Entity.Sync();
					yield return new FailMessage("[LeaveGuildProcessor] leaveFuncResult");
				}
				else
				{
					using (HeroesDataContext heroesDataContext = new HeroesDataContext())
					{
						heroesDataContext.UpdateGuildCharacterInfo(new long?(base.Operation.Key.CID), new long?(0L));
						GuildLog.AddGuildLedger(new LogData((long)base.Entity.GuildSN, base.Operation.Key.CID, OperationType.GainGuildPoint, GuildLedgerEventType.UpdateGuildUserPoint, "0", "LeaveGuild"));
					}
					base.Entity.UpdateGroupMemberInfo(base.Operation.Key, leaveFuncResult);
					base.Entity.Sync();
					yield return new OkMessage();
				}
			}
			else
			{
				yield return new FailMessage("[LeaveGuildProcessor] leaveResult");
			}
			yield break;
		}

		private GuildService service;
	}
}
