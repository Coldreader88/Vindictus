using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CashShopServiceOperation
{
	[Serializable]
	public sealed class WishListDelete : Operation
	{
		public long CID { get; set; }

		public IList<int> List_ProductNo { get; set; }

		public WishListResult ResultCode { get; set; }

		public WishListDelete(long cid, IList<int> list_productno)
		{
			this.CID = cid;
			this.List_ProductNo = list_productno;
			this.ResultCode = WishListResult.FAILUNKNOWN;
		}

		public WishListDelete()
		{
		}

		public override OperationProcessor RequestProcessor()
		{
			return new WishListDelete.Request(this);
		}

		private class Request : OperationProcessor<WishListDelete>
		{
			public Request(WishListDelete op) : base(op)
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
