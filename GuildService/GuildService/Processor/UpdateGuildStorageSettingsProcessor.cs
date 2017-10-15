using System;
using System.Collections.Generic;
using ServiceCore;
using ServiceCore.EndPointNetwork.GuildService;
using ServiceCore.GuildServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace GuildService.Processor
{
	internal class UpdateGuildStorageSettingsProcessor : EntityProcessor<UpdateGuildStorageSettings, GuildEntity>
	{
		public UpdateGuildStorageSettingsProcessor(GuildService service, UpdateGuildStorageSettings op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			GuildStorageManager storage = base.Entity.Storage;
			OnlineGuildMember member = base.Entity.GetOnlineMember(base.Operation.RequestingCID);
			if (!base.Entity.Storage.Valid || !FeatureMatrix.IsEnable("GuildStorage"))
			{
				member.SendOperationFailedDialog("GuildStorageFail_Processing");
				storage.AddGuildStorageLedger(base.Operation.RequestingCID, GuildStorageOperationCode.PickGuildItem, GuildStorageEventCode.Error_Invalid);
				base.Finished = true;
				yield return new FailMessage("[UpdateGuildStorageSettingsProcessor] GuildStorage")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			else if (base.Entity.Storage.Processing)
			{
				member.SendOperationFailedDialog("GuildStorageFail_Processing");
				storage.AddGuildStorageLedger(base.Operation.RequestingCID, GuildStorageOperationCode.PickGuildItem, GuildStorageEventCode.Error_Processing);
				base.Finished = true;
				yield return new FailMessage("[UpdateGuildStorageSettingsProcessor] Entity.Storage.Processing")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			else if (!base.Entity.Storage.IsEnabled)
			{
				member.SendOperationFailedDialog("GuildStorageFail_Stopped");
				storage.AddGuildStorageLedger(base.Operation.RequestingCID, GuildStorageOperationCode.PickGuildItem, GuildStorageEventCode.Error_Invalid);
				base.Finished = true;
				yield return new FailMessage("[UpdateGuildStorageSettingsProcessor] Entity.Storage.IsEnabled")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			else
			{
				if (member != null)
				{
					if (member.GuildMember.Rank == GuildMemberRank.Master)
					{
						storage.AddGuildStorageLedger(base.Operation.RequestingCID, GuildStorageOperationCode.UpdateSetting, GuildStorageEventCode.ChangeSetting, base.Operation.AccessLimtiTag.ToString(), base.Operation.GoldLimit);
						base.Entity.Storage.UpdateSetting(base.Operation.GoldLimit, base.Operation.AccessLimtiTag);
						base.Entity.Storage.BroadCastInventoryInfo();
					}
					else
					{
						member.SendOperationFailedDialog("GuildStorageFail_RankLimited");
					}
				}
				storage.AddGuildStorageLedger(base.Operation.RequestingCID, GuildStorageOperationCode.UpdateSetting, GuildStorageEventCode.Done);
				base.Finished = true;
				yield return new OkMessage();
			}
			yield break;
		}

		private GuildService service;
	}
}
