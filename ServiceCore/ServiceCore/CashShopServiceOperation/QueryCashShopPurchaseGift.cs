using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CashShopServiceOperation
{
	[Serializable]
	public sealed class QueryCashShopPurchaseGift : Operation
	{
		public string TargetName { get; set; }

		public string Message { get; set; }

		public int ProductNo { get; set; }

		public short Quantity { get; set; }

		public string Attribute0 { get; set; }

		public string Attribute1 { get; set; }

		public string Attribute2 { get; set; }

		public string Attribute3 { get; set; }

		public string Attribute4 { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
