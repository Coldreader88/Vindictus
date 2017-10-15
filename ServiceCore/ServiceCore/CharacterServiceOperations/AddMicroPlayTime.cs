using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class AddMicroPlayTime : Operation
	{
		public int StartTime { get; set; }

		public int EndTime { get; set; }

		public bool Success { get; set; }

		public bool IsFreeQuest { get; set; }

		public AddMicroPlayTime(int StartTime, int EndTime, bool Success, bool IsFreeQuest)
		{
			this.StartTime = StartTime;
			this.EndTime = EndTime;
			this.Success = Success;
			this.IsFreeQuest = IsFreeQuest;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
