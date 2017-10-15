using System;
using System.Collections.Generic;
using ServiceCore;
using ServiceCore.GuildServiceOperations;
using ServiceCore.ItemServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace GuildService.Processor
{
	internal class ArrangeGuildStorageItemProcessor : EntityProcessor<ArrangeGuildStorageItem, GuildEntity>
	{
		public ArrangeGuildStorageItemProcessor(GuildService service, ArrangeGuildStorageItem op) : base(op)
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
				storage.AddGuildStorageLedger(base.Operation.RequestingCID, GuildStorageOperationCode.ArrangeGuildItem, GuildStorageEventCode.Error_Invalid);
				base.Finished = true;
				yield return new FailMessage("[ArrangeGuildStorageItemProcessor] GuildStorage")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			else if (base.Entity.Storage.Processing)
			{
				member.SendOperationFailedDialog("GuildStorageFail_Processing");
				storage.AddGuildStorageLedger(base.Operation.RequestingCID, GuildStorageOperationCode.ArrangeGuildItem, GuildStorageEventCode.Error_Processing);
				base.Finished = true;
				yield return new FailMessage("[ArrangeGuildStorageItemProcessor] Entity.Storage.Processing")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			else if (!base.Entity.Storage.IsEnabled)
			{
				member.SendOperationFailedDialog("GuildStorageFail_Stopped");
				storage.AddGuildStorageLedger(base.Operation.RequestingCID, GuildStorageOperationCode.ArrangeGuildItem, GuildStorageEventCode.Error_Invalid);
				base.Finished = true;
				yield return new FailMessage("[ArrangeGuildStorageItemProcessor] Entity.Storage.IsEnabled")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			else
			{
				base.Entity.Storage.Processing = true;
				QueryItemInfoByItemID op0 = new QueryItemInfoByItemID
				{
					ItemID = base.Operation.ItemID
				};
				OperationSync sync0 = new OperationSync
				{
					Connection = base.Entity.Storage.ItemConn,
					Operation = op0
				};
				yield return sync0;
				if (op0.Result)
				{
					if (op0.SlotInfo.ItemClass == "gold")
					{
						member.SendOperationFailedDialog("GuildStorageFail_InvalidItem");
						base.Entity.Storage.Processing = false;
						base.Finished = true;
						yield return new FailMessage("[ArrangeGuildStorageItemProcessor] op0.SlotInfo.ItemClass")
						{
							Reason = FailMessage.ReasonCode.LogicalFail
						};
					}
					else if (base.Entity.Storage.IsPickLimited(member, op0.SlotInfo.ItemClass, op0.SlotInfo.Slot, 1) || base.Entity.Storage.IsPickLimited(member, "", base.Operation.Slot, 1))
					{
						base.Entity.Storage.Processing = false;
						base.Finished = true;
						yield return new FailMessage("[ArrangeGuildStorageItemProcessor] Entity.Storage.IsPickLimited")
						{
							Reason = FailMessage.ReasonCode.LogicalFail
						};
					}
					else
					{
						storage.AddGuildStorageLedger(base.Operation.RequestingCID, GuildStorageOperationCode.ArrangeGuildItem, GuildStorageEventCode.Process, base.Operation.ItemID.ToString(), base.Operation.Slot);
						MoveInventoryItem op = new MoveInventoryItem(base.Operation.ItemID, 0, base.Operation.Slot);
						OperationSync sync = new OperationSync
						{
							Connection = base.Entity.Storage.ItemConn,
							Operation = op
						};
						yield return sync;
						if (sync.Result && op.Result)
						{
							storage.AddGuildStorageLedger(base.Operation.RequestingCID, GuildStorageOperationCode.ArrangeGuildItem, GuildStorageEventCode.Done, base.Operation.ItemID.ToString(), base.Operation.Slot);
						}
						else
						{
							storage.AddGuildStorageLedger(base.Operation.RequestingCID, GuildStorageOperationCode.ArrangeGuildItem, GuildStorageEventCode.Error_TransferFail, base.Operation.ItemID.ToString(), base.Operation.Slot);
							member.SendOperationFailedDialog("GuildStorageFail_TransferFailed");
						}
						base.Entity.Storage.Processing = false;
						base.Finished = true;
						yield return new OkMessage();
					}
				}
				else
				{
					member.SendOperationFailedDialog("GuildStorageFail_InternalError");
					base.Entity.Storage.Processing = false;
					base.Finished = true;
					yield return new FailMessage("[ArrangeGuildStorageItemProcessor] op0.Result")
					{
						Reason = FailMessage.ReasonCode.LogicalFail
					};
				}
			}
			yield break;
		}

		private GuildService service;
	}
}
