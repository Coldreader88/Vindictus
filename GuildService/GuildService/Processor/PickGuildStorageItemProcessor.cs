using System;
using System.Collections.Generic;
using ServiceCore;
using ServiceCore.GuildServiceOperations;
using ServiceCore.ItemServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace GuildService.Processor
{
	internal class PickGuildStorageItemProcessor : EntityProcessor<PickGuildStorageItem, GuildEntity>
	{
		public PickGuildStorageItemProcessor(GuildService service, PickGuildStorageItem op) : base(op)
		{
			this.service = service;
		}

        public override IEnumerable<object> Run()
        {
            GuildStorageManager storage = this.Entity.Storage;
            OnlineGuildMember member = this.Entity.GetOnlineMember(this.Operation.OwnerCID);
            if (!this.Entity.Storage.Valid || !FeatureMatrix.IsEnable("GuildStorage"))
            {
                member.SendOperationFailedDialog("GuildStorageFail_Processing");
                storage.AddGuildStorageLedger(this.Operation.OwnerCID, GuildStorageOperationCode.PickGuildItem, GuildStorageEventCode.Error_Invalid);
                this.Finished = true;
                yield return (object)new FailMessage("[PickGuildStorageItemProcessor] GuildStorage")
                {
                    Reason = FailMessage.ReasonCode.LogicalFail
                };
            }
            else if (this.Entity.Storage.Processing)
            {
                member.SendOperationFailedDialog("GuildStorageFail_Processing");
                storage.AddGuildStorageLedger(this.Operation.OwnerCID, GuildStorageOperationCode.PickGuildItem, GuildStorageEventCode.Error_Processing);
                this.Finished = true;
                yield return (object)new FailMessage("[PickGuildStorageItemProcessor] Entity.Storage.Processing")
                {
                    Reason = FailMessage.ReasonCode.LogicalFail
                };
            }
            else if (this.Entity.Storage.StorageCount == 0)
            {
                member.SendOperationFailedDialog("GuildStorageFail_NoSlot");
                storage.AddGuildStorageLedger(this.Operation.OwnerCID, GuildStorageOperationCode.AddGuildItem, GuildStorageEventCode.Error_Invalid);
                this.Finished = true;
                yield return (object)new FailMessage("[PickGuildStorageItemProcessor] Entity.Storage.StorageCount")
                {
                    Reason = FailMessage.ReasonCode.LogicalFail
                };
            }
            else
            {
                this.Entity.Storage.Processing = true;
                int color1 = -1;
                int color2 = -1;
                int color3 = -1;
                int reduceDurability = -1;
                int maxDurabilityBonus = -1;
                byte tab = byte.MaxValue;
                int slot = -2;
                string loggingItemClass = this.Operation.ItemClass;
                if (this.Operation.ItemID != 0L)
                {
                    QueryItemInfoByItemID op0 = new QueryItemInfoByItemID()
                    {
                        ItemID = this.Operation.ItemID
                    };
                    OperationSync sync0 = new OperationSync()
                    {
                        Connection = this.Entity.Storage.ItemConn,
                        Operation = (Operation)op0
                    };
                    yield return (object)sync0;
                    if (op0.Result)
                    {
                        if (this.Entity.Storage.IsPickLimited(member, this.Operation.ItemClass, op0.SlotInfo.Slot, this.Operation.Amount))
                        {
                            storage.AddGuildStorageLedger(this.Operation.OwnerCID, GuildStorageOperationCode.PickGuildItem, GuildStorageEventCode.Error_Limited, this.Operation.ItemClass, this.Operation.Amount);
                            this.Entity.Storage.Processing = false;
                            this.Finished = true;
                            yield return (object)new FailMessage("[PickGuildStorageItemProcessor] Entity.Storage.IsPickLimited")
                            {
                                Reason = FailMessage.ReasonCode.LogicalFail
                            };
                            yield break;
                        }
                        else
                        {
                            color1 = op0.SlotInfo.Color1;
                            color2 = op0.SlotInfo.Color2;
                            color3 = op0.SlotInfo.Color3;
                            reduceDurability = op0.SlotInfo.MaxDurability - op0.SlotInfo.Durability;
                            maxDurabilityBonus = op0.SlotInfo.MaxDurabilityBonus;
                            tab = (byte)op0.SlotInfo.Tab;
                            slot = op0.SlotInfo.Slot;
                            loggingItemClass = op0.ItemClassEX;
                        }
                    }
                    else
                    {
                        storage.AddGuildStorageLedger(this.Operation.OwnerCID, GuildStorageOperationCode.PickGuildItem, GuildStorageEventCode.Error_Invalid, this.Operation.ItemClass, this.Operation.Amount);
                        member.SendOperationFailedDialog("GuildStorageFail_InternalError");
                        this.Entity.Storage.Processing = false;
                        this.Finished = true;
                        yield return (object)new FailMessage("[PickGuildStorageItemProcessor] op0.Result")
                        {
                            Reason = FailMessage.ReasonCode.LogicalFail
                        };
                        yield break;
                    }
                }
                else if (this.Entity.Storage.IsPickLimited(member, this.Operation.ItemClass, 0, this.Operation.Amount))
                {
                    storage.AddGuildStorageLedger(this.Operation.OwnerCID, GuildStorageOperationCode.PickGuildItem, GuildStorageEventCode.Error_Invalid, this.Operation.ItemClass, this.Operation.Amount);
                    this.Entity.Storage.Processing = false;
                    this.Finished = true;
                    yield return (object)new FailMessage("[PickGuildStorageItemProcessor] Entity.Storage.IsPickLimited")
                    {
                        Reason = FailMessage.ReasonCode.LogicalFail
                    };
                    yield break;
                }
                storage.AddGuildStorageLedger(this.Operation.OwnerCID, GuildStorageOperationCode.PickGuildItem, GuildStorageEventCode.TransferFrom, this.Operation.ItemClass, this.Operation.Amount, color1, color2, color3, reduceDurability, maxDurabilityBonus);
                TransferToSystem op1 = new TransferToSystem((ICollection<TransferItemInfo>)new List<TransferItemInfo>()
        {
          new TransferItemInfo(this.Operation.ItemID, this.Operation.ItemClass, this.Operation.Amount)
        }, TransferToSystem.SourceEnum.GuildStorage);
                OperationSync sync = new OperationSync()
                {
                    Connection = this.Entity.Storage.ItemConn,
                    Operation = (Operation)op1
                };
                yield return (object)sync;
                if (!op1.Result)
                {
                    storage.AddGuildStorageLedger(this.Operation.OwnerCID, GuildStorageOperationCode.PickGuildItem, GuildStorageEventCode.Error_TransferFail, this.Operation.ItemClass, this.Operation.Amount, color1, color2, color3, reduceDurability, maxDurabilityBonus);
                    member.SendOperationFailedDialog("GuildStorageFail_TransferFailed");
                    this.Entity.Storage.Processing = false;
                    this.Finished = true;
                    yield return (object)new FailMessage("[PickGuildStorageItemProcessor] op1.Result")
                    {
                        Reason = FailMessage.ReasonCode.LogicalFail
                    };
                }
                else
                {
                    storage.AddGuildStorageLedger(this.Operation.OwnerCID, GuildStorageOperationCode.PickGuildItem, GuildStorageEventCode.TransferTo, this.Operation.ItemClass, this.Operation.Amount, color1, color2, color3, reduceDurability, maxDurabilityBonus);
                    List<long> itemList = new List<long>();
                    foreach (TransferredItemInfo transferredItem in (IEnumerable<TransferredItemInfo>)op1.TransferredItemList)
                        itemList.Add(transferredItem.ItemID);
                    TransferFromSystem op2 = (int)this.Operation.TargetTab != (int)byte.MaxValue ? (this.Operation.TargetSlot != -1 ? new TransferFromSystem((ICollection<long>)itemList, true, this.Operation.TargetTab, this.Operation.TargetSlot, this.Operation.TargetSlot + 1, TransferFromSystem.SourceEnum.GuildStorage) : new TransferFromSystem((ICollection<long>)itemList, true, this.Operation.TargetTab, 0, 48, TransferFromSystem.SourceEnum.GuildStorage)) : new TransferFromSystem((ICollection<long>)itemList, true, byte.MaxValue, 0, 0, TransferFromSystem.SourceEnum.GuildStorage);
                    OperationSync sync2 = new OperationSync()
                    {
                        Connection = member.PlayerConn,
                        Operation = (Operation)op2
                    };
                    yield return (object)sync2;
                    if (!op2.Result)
                    {
                        storage.AddGuildStorageLedger(this.Operation.OwnerCID, GuildStorageOperationCode.PickGuildItem, GuildStorageEventCode.Error_TransferFail, this.Operation.ItemClass, this.Operation.Amount, color1, color2, color3, reduceDurability, maxDurabilityBonus);
                        if (op2.FailReason == TransferFromSystem.FailReasonEnum.NoEmptySlot)
                            member.SendOperationFailedDialog("GuildStorageFail_NoEmptySlot");
                        else
                            member.SendOperationFailedDialog("GuildStorageFail_TransferFailed");
                        TransferFromSystem op3 = new TransferFromSystem((ICollection<long>)itemList, false, tab, slot, slot + 1, TransferFromSystem.SourceEnum.GuildStorage);
                        OperationSync sync3 = new OperationSync()
                        {
                            Connection = this.Entity.Storage.ItemConn,
                            Operation = (Operation)op3
                        };
                        yield return (object)sync3;
                        if (op3.Result)
                            storage.AddGuildStorageLedger(this.Operation.OwnerCID, GuildStorageOperationCode.PickGuildItem, GuildStorageEventCode.Error_TransferFail_Recovered, this.Operation.ItemClass, this.Operation.Amount, color1, color2, color3, reduceDurability, maxDurabilityBonus);
                        else
                            storage.AddGuildStorageLedger(this.Operation.OwnerCID, GuildStorageOperationCode.PickGuildItem, GuildStorageEventCode.Error_TransferFail_RecoverFail, this.Operation.ItemClass, this.Operation.Amount, color1, color2, color3, reduceDurability, maxDurabilityBonus);
                        this.Entity.Storage.Processing = false;
                        this.Finished = true;
                        yield return (object)new FailMessage("[PickGuildStorageItemProcessor] op2.Result")
                        {
                            Reason = FailMessage.ReasonCode.LogicalFail
                        };
                    }
                    else
                    {
                        storage.AddGuildStorageLedger(this.Operation.OwnerCID, GuildStorageOperationCode.PickGuildItem, GuildStorageEventCode.Done, this.Operation.ItemClass, this.Operation.Amount, color1, color2, color3, reduceDurability, maxDurabilityBonus);
                        this.Entity.Storage.ReportPickItem(member.CharacterName, loggingItemClass, this.Operation.Amount, color1, color2, color3);
                        this.Entity.Storage.Processing = false;
                        this.Finished = true;
                        yield return (object)new OkMessage();
                    }
                }
            }
        }

        private GuildService service;
	}
}
