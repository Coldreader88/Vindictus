using System;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class PayCoin : Operation
	{
		public CoinType CoinType { get; set; }

		public int OwnerSlot { get; set; }

		public int ReceiverSlot { get; set; }

		public int CoinSlot { get; set; }

		public bool IsInsert { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
