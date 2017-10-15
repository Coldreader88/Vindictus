using System;
using System.Collections.Generic;
using ServiceCore.CashShopServiceOperation;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace CashShopService.Processors
{
	internal class QueryBeautyShopInfoProcessor : EntityProcessor<QueryBeautyShopInfo, CashShopPeer>
	{
		public QueryBeautyShopInfoProcessor(CashShopService service, QueryBeautyShopInfo op) : base(op)
		{
		}

		public override IEnumerable<object> Run()
		{
			base.Finished = true;
			if (base.Entity.QueryBeautyShopInfo(base.Operation.CharacterType))
			{
				yield return new OkMessage();
			}
			else
			{
				yield return new FailMessage("[QueryBeautyShopInfoProcessor] Entity.QueryBeautyShopInfo")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			yield break;
		}
	}
}
