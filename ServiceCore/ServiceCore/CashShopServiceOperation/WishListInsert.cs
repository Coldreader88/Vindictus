using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CashShopServiceOperation
{
	[Serializable]
	public sealed class WishListInsert : Operation
	{
		public long CID { get; set; }

		public ICollection<int> List_Wish { get; set; }

		public WishListResult ResultCode { get; set; }

		public WishListInsert(long cid, ICollection<int> list_wish)
		{
			this.CID = cid;
			this.List_Wish = list_wish;
			this.ResultCode = WishListResult.FAILUNKNOWN;
		}

		public WishListInsert()
		{
		}

		public override OperationProcessor RequestProcessor()
		{
			return new WishListInsert.Request(this);
		}

		private class Request : OperationProcessor<WishListInsert>
		{
			public Request(WishListInsert op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.ResultCode = (WishListResult)base.Feedback;
				base.Result = true;
				yield break;
			}
		}
	}
}
