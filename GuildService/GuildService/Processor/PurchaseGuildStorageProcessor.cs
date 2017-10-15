using System;
using System.Collections.Generic;
using ServiceCore;
using ServiceCore.CashShopServiceOperation;
using ServiceCore.GuildServiceOperations;
using ServiceCore.ItemServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace GuildService.Processor
{
	internal class PurchaseGuildStorageProcessor : EntityProcessor<PurchaseGuildStorage, GuildEntity>
	{
		public PurchaseGuildStorageProcessor(GuildService service, PurchaseGuildStorage op) : base(op)
		{
			this.service = service;
		}

        public override IEnumerable<object> Run()
        {
            GuildStorageManager storage = this.Entity.Storage;
            OnlineGuildMember member = this.Entity.GetOnlineMember(this.Operation.PuchasedCID);
            if (!this.Entity.Storage.Valid || !FeatureMatrix.IsEnable("GuildStorage"))
            {
                member.SendOperationFailedDialog("GuildStorageFail_Processing");
                storage.AddGuildStorageLedger(this.Operation.PuchasedCID, GuildStorageOperationCode.PickGuildItem, GuildStorageEventCode.Error_Invalid);
                this.Finished = true;
                yield return (object)new FailMessage("[PurchaseGuildStorageProcessor] GuildStorage")
                {
                    Reason = FailMessage.ReasonCode.LogicalFail
                };
            }
            else if (this.Entity.Storage.Processing)
            {
                member.SendOperationFailedDialog("GuildStorageFail_Processing");
                storage.AddGuildStorageLedger(this.Operation.PuchasedCID, GuildStorageOperationCode.PickGuildItem, GuildStorageEventCode.Error_Processing);
                this.Finished = true;
                yield return (object)new FailMessage("[PurchaseGuildStorageProcessor] Entity.Storage.Processing")
                {
                    Reason = FailMessage.ReasonCode.LogicalFail
                };
            }
            else if (!this.Entity.Storage.IsEnabled)
            {
                member.SendOperationFailedDialog("GuildStorageFail_Stopped");
                storage.AddGuildStorageLedger(this.Operation.PuchasedCID, GuildStorageOperationCode.PickGuildItem, GuildStorageEventCode.Error_Invalid);
                this.Finished = true;
                yield return (object)new FailMessage("[PurchaseGuildStorageProcessor] Entity.Storage.IsEnabled")
                {
                    Reason = FailMessage.ReasonCode.LogicalFail
                };
            }
            else if (storage.StorageCount >= storage.GuildStorageSlotsMax)
            {
                member.SendOperationFailedDialog("GuildStorageFail_Full");
                storage.AddGuildStorageLedger(this.Operation.PuchasedCID, GuildStorageOperationCode.PickGuildItem, GuildStorageEventCode.Error_Invalid);
                this.Finished = true;
                yield return (object)new FailMessage("[PurchaseGuildStorageProcessor] storage.StorageCount")
                {
                    Reason = FailMessage.ReasonCode.LogicalFail
                };
            }
            else
            {
                this.Entity.Storage.Processing = true;
                int targetSlotCount = 0;
                if (this.Entity.Storage.StorageCount == 0 && !FeatureMatrix.IsEnable("GuildStoargeSlotAllCash"))
                {
                    storage.AddGuildStorageLedger(this.Operation.PuchasedCID, GuildStorageOperationCode.PurchaseGuildStorage, GuildStorageEventCode.PurchaseSlotByGold);
                    if (FeatureMatrix.GetInteger("GuildStorageSlotGoldPrice") > 0)
                    {
                        DestroyItem op = new DestroyItem("gold", FeatureMatrix.GetInteger("GuildStorageSlotGoldPrice"), GiveItem.SourceEnum.Unknown);
                        OperationSync sync = new OperationSync()
                        {
                            Connection = member.PlayerConn,
                            Operation = (Operation)op
                        };
                        yield return (object)sync;
                        if (!sync.Result || !op.Result)
                        {
                            this.Entity.Storage.Processing = false;
                            storage.AddGuildStorageLedger(this.Operation.PuchasedCID, GuildStorageOperationCode.PurchaseGuildStorage, GuildStorageEventCode.Error_PurchaseSlotByGold);
                            member.SendOperationFailedDialog("GuildStorageFail_PurchaseFailed");
                            this.Finished = true;
                            yield return (object)new FailMessage("[PurchaseGuildStorageProcessor] op.Result")
                            {
                                Reason = FailMessage.ReasonCode.LogicalFail
                            };
                            yield break;
                        }
                    }
                    targetSlotCount = this.Entity.Storage.GuildStorageSlotsFirstPurchase;
                }
                else
                {
                    storage.AddGuildStorageLedger(this.Operation.PuchasedCID, GuildStorageOperationCode.PurchaseGuildStorage, GuildStorageEventCode.PurchaseSlotByCash);
                    if (!FeatureMatrix.IsEnable("GuildStoargeSlotFree"))
                    {
                        if (this.Operation.ProductNo == -1)
                        {
                            member.SendOperationFailedDialog("GuildStorageFail_PurchaseFailed");
                            storage.AddGuildStorageLedger(this.Operation.PuchasedCID, GuildStorageOperationCode.PickGuildItem, GuildStorageEventCode.Error_Invalid);
                            this.Entity.Storage.Processing = false;
                            this.Finished = true;
                            yield return (object)new FailMessage("[PurchaseGuildStorageProcessor] Operation.ProductNo")
                            {
                                Reason = FailMessage.ReasonCode.LogicalFail
                            };
                            yield break;
                        }
                        else
                        {
                            DirectPickUpByProductNo op = new DirectPickUpByProductNo()
                            {
                                ProductNoList = new List<int>()
                {
                  this.Operation.ProductNo
                },
                                IsCredit = this.Operation.IsCredit
                            };
                            OperationSync sync = new OperationSync()
                            {
                                Connection = member.CashShopConn,
                                Operation = (Operation)op
                            };
                            yield return (object)sync;
                            if (!sync.Result || !op.Result || !op.ResultingItems.ContainsKey("guildstorage_ticket"))
                            {
                                storage.AddGuildStorageLedger(this.Operation.PuchasedCID, GuildStorageOperationCode.PurchaseGuildStorage, GuildStorageEventCode.Error_PurchaseSlotByCash);
                                member.SendOperationFailedDialog("GuildStorageFail_PurchaseFailed");
                                this.Entity.Storage.Processing = false;
                                this.Finished = true;
                                yield return (object)new FailMessage("[PurchaseGuildStorageProcessor] guildstorage_ticket")
                                {
                                    Reason = FailMessage.ReasonCode.LogicalFail
                                };
                                yield break;
                            }
                        }
                    }
                    targetSlotCount = this.Entity.Storage.GuildStorageSlotsPerPurchase;
                }
                storage.AddGuildStorageLedger(this.Operation.PuchasedCID, GuildStorageOperationCode.PurchaseGuildStorage, GuildStorageEventCode.Process);
                AddGuildStorageSlots requestOp = new AddGuildStorageSlots()
                {
                    SlotCount = targetSlotCount
                };
                requestOp.OnComplete += (Action<Operation>)(___ => this.Entity.Storage.StorageCount = requestOp.ResultSlotCount);
                OperationSync sync2 = new OperationSync()
                {
                    Connection = this.Entity.Storage.ItemConn,
                    Operation = (Operation)requestOp
                };
                yield return (object)sync2;
                if (requestOp.Result)
                    storage.AddGuildStorageLedger(this.Operation.PuchasedCID, GuildStorageOperationCode.PurchaseGuildStorage, GuildStorageEventCode.Done);
                else
                    storage.AddGuildStorageLedger(this.Operation.PuchasedCID, GuildStorageOperationCode.PurchaseGuildStorage, GuildStorageEventCode.Error_ProcessFail);
                this.Entity.Storage.Processing = false;
                this.Entity.Storage.ReportExtendSlot(member.CharacterName, this.Entity.Storage.GuildStorageSlotsPerPurchase);
                this.Finished = true;
                yield return (object)new OkMessage();
            }
        }

        private GuildService service;
	}
}
