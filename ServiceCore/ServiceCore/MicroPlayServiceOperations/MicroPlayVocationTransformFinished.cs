using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class MicroPlayVocationTransformFinished : Operation
	{
		public int SlotNum { get; set; }

		public int TotalDamage { get; set; }

		public MicroPlayVocationTransformFinished(int slotNum, int totalDamage)
		{
			this.SlotNum = slotNum;
			this.TotalDamage = totalDamage;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
