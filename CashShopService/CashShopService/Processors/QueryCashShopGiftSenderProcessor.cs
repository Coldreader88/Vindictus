using System;
using System.Collections.Generic;
using ServiceCore.CashShopServiceOperation;
using ServiceCore.CharacterServiceOperations;
using ServiceCore.EndPointNetwork;
using ServiceCore.FrontendServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using Utility;

namespace CashShopService.Processors
{
	public class QueryCashShopGiftSenderProcessor : EntityProcessor<QueryCashShopGiftSender, CashShopPeer>
	{
		public QueryCashShopGiftSenderProcessor(CashShopService service, QueryCashShopGiftSender op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			CashShopPeer cashShopPeer = base.Entity;
			int orderNo = base.Operation.OrderNo;
			Log<QueryCashShopGiftSenderProcessor>.Logger.InfoFormat("Array Size {0}", cashShopPeer.GiftSenderCIDDict.Count);
			long senderCID;
			if (cashShopPeer.GiftSenderCIDDict.TryGetValue(base.Operation.OrderNo, out senderCID))
			{
				QueryNameByCID queryName = new QueryNameByCID(senderCID);
				queryName.OnComplete += delegate(Operation _)
				{
					SendPacket op = SendPacket.Create<CashShopGiftSenderMessage>(new CashShopGiftSenderMessage(orderNo, queryName.Name));
					cashShopPeer.FrontendConnection.RequestOperation(op);
				};
				queryName.OnFail += delegate(Operation _)
				{
					SendPacket op = SendPacket.Create<CashShopGiftSenderMessage>(new CashShopGiftSenderMessage(orderNo, ""));
					cashShopPeer.FrontendConnection.RequestOperation(op);
				};
				this.service.RequestOperation("PlayerService.PlayerService", queryName);
				base.Finished = true;
				yield return new OkMessage();
			}
			else
			{
				base.Finished = true;
				Log<QueryCashShopGiftSenderProcessor>.Logger.ErrorFormat("No CID match for sender. OrderNo [{0}]", base.Operation.OrderNo);
				yield return new FailMessage("[QueryCashShopGiftSenderProcessor] Operation.OrderNo")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			yield break;
		}

		private CashShopService service;
	}
}
