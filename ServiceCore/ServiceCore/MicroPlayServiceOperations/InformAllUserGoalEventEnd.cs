using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class InformAllUserGoalEventEnd : Operation
	{
		public int SlotID { get; set; }

		public bool IsEnd { get; set; }

		public InformAllUserGoalEventEnd(int SlotID, bool IsEnd)
		{
			this.SlotID = SlotID;
			this.IsEnd = IsEnd;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
