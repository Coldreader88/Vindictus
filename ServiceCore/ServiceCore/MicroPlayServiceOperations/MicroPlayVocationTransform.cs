using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class MicroPlayVocationTransform : Operation
	{
		public int SlotNum { get; set; }

		public int TransformLevel { get; set; }

		public MicroPlayVocationTransform(int slotNum, int transformLevel)
		{
			this.SlotNum = slotNum;
			this.TransformLevel = transformLevel;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
