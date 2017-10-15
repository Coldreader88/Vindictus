using System;
using System.Collections.Generic;
using GuildService.API;
using ServiceCore;
using ServiceCore.CashShopServiceOperation;
using ServiceCore.EndPointNetwork.GuildService;
using ServiceCore.GuildServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using Utility;

namespace GuildService.Processor
{
	internal class DirectPurchaseGuildItemProcessor : EntityProcessor<DirectPurchaseGuildItem, GuildEntity>
	{
		public DirectPurchaseGuildItemProcessor(GuildService service, DirectPurchaseGuildItem op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			base.Operation.Result = DirectPurchaseGuildItemResultCode.Success;
			GuildLedgerEventType result = GuildLedgerEventType.Success;
			GuildMember myInfo = base.Entity.GetGuildMember(base.Operation.Key.CharacterName);
			OnlineGuildMember member = base.Entity.GetOnlineMember(myInfo.Key.CID);
			string maxIncreaseCountParsing = base.Operation.ItemClass.Replace("guild_extension_member_", "");
			int maxIncreaseCount = 0;
			if (!base.Entity.IsInitialized || base.Entity.GuildInfo == null)
			{
				base.Operation.Result = DirectPurchaseGuildItemResultCode.GuildNotInitialize;
				result = GuildLedgerEventType.GuildNotInitialize;
			}
			else if (myInfo == null || !myInfo.Rank.IsMaster() || member == null)
			{
				base.Operation.Result = DirectPurchaseGuildItemResultCode.IsNotMaster;
				result = GuildLedgerEventType.IsNotMaster;
			}
			else if (!int.TryParse(maxIncreaseCountParsing, out maxIncreaseCount))
			{
				base.Operation.Result = DirectPurchaseGuildItemResultCode.CannotParseItem;
				result = GuildLedgerEventType.CannotParseItem;
			}
			else if (FeatureMatrix.GetInteger("InGameGuild_MaxMemberLimit") < base.Entity.GuildInfo.MaxMemberCount + maxIncreaseCount)
			{
				base.Operation.Result = DirectPurchaseGuildItemResultCode.OverMaxMemberLimit;
				result = GuildLedgerEventType.OverMaxMemberLimit;
			}
			if (base.Operation.Result != DirectPurchaseGuildItemResultCode.Success)
			{
				GuildLog.AddGuildLedger(new LogData((long)base.Entity.GuildSN, base.Operation.Key.CID, OperationType.DirectPurchaseGuildItem, result));
				base.Finished = true;
				yield return base.Operation.Result;
			}
			else
			{
				DirectPickUpByProductNo directPickupOp = new DirectPickUpByProductNo(new List<int>
				{
					base.Operation.ProductNo
				}, base.Operation.IsCredit);
				OperationSync directPickupSync = new OperationSync
				{
					Connection = member.CashShopConn,
					Operation = directPickupOp
				};
				yield return directPickupSync;
				if (!directPickupSync.Result)
				{
					if (directPickupOp.FailReasonString.StartsWith("BeginDirectPurchaseItem"))
					{
						base.Operation.Result = DirectPurchaseGuildItemResultCode.BeginDirectPurchaseItem;
						result = GuildLedgerEventType.BeginDirectPurchaseItem;
					}
					else if (directPickupOp.FailReasonString.StartsWith("EndDirectPurchaseItem"))
					{
						base.Operation.Result = DirectPurchaseGuildItemResultCode.EndDirectPurchaseItem;
						result = GuildLedgerEventType.EndDirectPurchaseItem;
					}
					else
					{
						base.Operation.Result = DirectPurchaseGuildItemResultCode.Unknown;
						result = GuildLedgerEventType.Unknown;
					}
					GuildLog.AddGuildLedger(new LogData((long)base.Entity.GuildSN, base.Operation.Key.CID, OperationType.DirectPurchaseGuildItem, result));
					base.Finished = true;
					yield return new FailMessage("[DirectPurchaseGuildItemProcessor] directPickupSync.Result");
				}
				else
				{
					bool updateResult = false;
					try
					{
						HeroesDataContext heroesDataContext = new HeroesDataContext();
						updateResult = heroesDataContext.UpdateMaxMemberLimit(base.Entity.GuildInfo.GuildSN, base.Entity.GuildInfo.MaxMemberCount + maxIncreaseCount);
						if (!updateResult)
						{
							base.Operation.Result = DirectPurchaseGuildItemResultCode.DatabaseFail;
							result = GuildLedgerEventType.DatabaseFail;
						}
					}
					catch (Exception ex)
					{
						Log<DirectPurchaseGuildItemProcessor>.Logger.Error("UpdateGuildInfoProcessor DataBase Exception ", ex);
						base.Operation.Result = DirectPurchaseGuildItemResultCode.DatabaseException;
						result = GuildLedgerEventType.DatabaseException;
					}
					HeroesGuildInfo guildInfo = GuildAPI.GetAPI().GetGuildInfo(base.Entity.GuildSN);
					if (guildInfo != null)
					{
						base.Entity.GuildInfo = guildInfo.ToGuildInfo();
						base.Entity.UpdateGroupInfo();
						base.Entity.Sync();
					}
					else
					{
						base.Operation.Result = DirectPurchaseGuildItemResultCode.ReloadFail;
						result = GuildLedgerEventType.ReloadFail;
					}
					if (!updateResult || base.Operation.Result != DirectPurchaseGuildItemResultCode.Success)
					{
						RollbackDirectPickUp rollbackPickupOp = new RollbackDirectPickUp
						{
							OrderNo = directPickupOp.OrderNo,
							ProductNoList = directPickupOp.ProductNoList
						};
						OperationSync rollbackPickupSync = new OperationSync
						{
							Connection = member.CashShopConn,
							Operation = rollbackPickupOp
						};
						yield return rollbackPickupSync;
						Log<DirectPurchaseGuildItemProcessor>.Logger.ErrorFormat("UpdateGuildInfoProcessor DataBase Fail {0}", base.Entity.GuildSN);
						GuildLog.AddGuildLedger(new LogData((long)base.Entity.GuildSN, base.Operation.Key.CID, OperationType.DirectPurchaseGuildItem, result));
						base.Finished = true;
						yield return base.Operation.Result;
					}
					else
					{
						base.Operation.Result = DirectPurchaseGuildItemResultCode.Success;
						result = GuildLedgerEventType.Success;
						GuildLog.AddGuildLedger(new LogData((long)base.Entity.GuildSN, base.Operation.Key.CID, OperationType.DirectPurchaseGuildItem, result));
						base.Finished = true;
						yield return base.Operation.Result;
					}
				}
			}
			yield break;
		}

		private GuildService service;
	}
}
