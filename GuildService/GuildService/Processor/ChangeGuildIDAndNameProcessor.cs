using System;
using System.Collections.Generic;
using GuildService.API;
using ServiceCore.GuildServiceOperations;
using UnifiedNetwork.Entity;

namespace GuildService.Processor
{
	internal class ChangeGuildIDAndNameProcessor : EntityProcessor<ChangeGuildIDAndName, GuildEntity>
	{
		public ChangeGuildIDAndNameProcessor(GuildService service, ChangeGuildIDAndName op) : base(op)
		{
			this.service = service;
		}

        public override IEnumerable<object> Run()
        {
            this.Finished = true;
            string guildName = this.Operation.GuildName;
            string guildID = this.Operation.GuildID;
            string guildLog = string.Format("GuildSN={0}, GuildID={1}, GuildName={2}", (object)this.Operation.GuildSN, (object)guildID, (object)guildName);
            if (guildName != null)
            {
                GroupNameCheckResult namingResult = GuildAPI.GetAPI().CheckGroupName(this.Operation.GuildName);
                if (namingResult != GroupNameCheckResult.Succeed)
                {
                    GuildLog.AddGuildLedger(new LogData((long)this.Operation.GuildSN, this.Operation.MemberKey.CID, OperationType.HeroesCore_GuildIDAndNameModify, GuildLedgerEventType.ChangeIDAndName_InvalidName, guildLog));
                    yield return (object)ChangeGuildIDAndName.ResultCodeEnum.Fail_InvalidGuildName;
                    yield break;
                }
            }
            if (guildID != null)
            {
                GroupIDCheckResult checkResult = GuildAPI.GetAPI().CheckGroupID(guildID);
                if (checkResult != GroupIDCheckResult.Succeed)
                {
                    GuildLog.AddGuildLedger(new LogData((long)this.Operation.GuildSN, this.Operation.MemberKey.CID, OperationType.HeroesCore_GuildIDAndNameModify, GuildLedgerEventType.ChangeIDAndName_InvalidID, guildLog));
                    yield return (object)ChangeGuildIDAndName.ResultCodeEnum.Fail_InvalidGuildID;
                    yield break;
                }
            }
            using (HeroesDataContext heroesDataContext = new HeroesDataContext())
            {
                if (heroesDataContext.GuildInfoUpdate(new int?(this.Operation.GuildSN), guildID, guildName) != 0)
                {
                    GuildLog.AddGuildLedger(new LogData((long)this.Operation.GuildSN, this.Operation.MemberKey.CID, OperationType.HeroesCore_GuildIDAndNameModify, GuildLedgerEventType.ChangeIDAndName_UpdateFailed, guildLog));
                    yield return (object)ChangeGuildIDAndName.ResultCodeEnum.Fail_GuildInfoUpdateFailed;
                    yield break;
                }
                else
                    GuildLog.AddGuildLedger(new LogData((long)this.Operation.GuildSN, this.Operation.MemberKey.CID, OperationType.HeroesCore_GuildIDAndNameModify, GuildLedgerEventType.ChangeIDAndName_Success, guildLog));
            }
            if (this.Entity != null)
            {
                this.Entity.ReportGuildInfoChanged();
                this.Entity.Sync();
            }
            yield return (object)ChangeGuildIDAndName.ResultCodeEnum.Success;
        }

        private GuildService service;
	}
}
