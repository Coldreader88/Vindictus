using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CashShopServiceOperation
{
	[Serializable]
	public sealed class WishListSelect : Operation
	{
		public long CID { get; set; }

		public ICollection<WishItemInfo> List_Wish { get; set; }

		public WishListResult ResultCode { get; set; }

		public WishListSelect(long cid)
		{
			this.CID = cid;
			this.ResultCode = WishListResult.FAILUNKNOWN;
		}

		public WishListSelect()
		{
		}

		public override OperationProcessor RequestProcessor()
		{
			return new WishListSelect.Request(this);
		}

		private class Request : OperationProcessor<WishListSelect>
		{
			public Request(WishListSelect op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.ResultCode = (WishListResult)base.Feedback;
				yield return null;
				base.Result = true;
				base.Operation.List_Wish = (ICollection<WishItemInfo>)base.Feedback;
				if (base.Operation.List_Wish.Count == 0)
				{
					base.Operation.ResultCode = WishListResult.NOITEM;
				}
				yield break;
			}
		}
	}
}
