using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class SwapHousingItem : Operation
	{
		public int FromSlot { get; set; }

		public int ToSlot { get; set; }

		public SwapHousingItem(int fromSlot, int toSlot)
		{
			this.FromSlot = fromSlot;
			this.ToSlot = toSlot;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
