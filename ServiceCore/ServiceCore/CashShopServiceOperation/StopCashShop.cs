using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CashShopServiceOperation
{
	[Serializable]
	public sealed class StopCashShop : Operation
	{
		public bool TargetState
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}

		private bool state;
	}
}
