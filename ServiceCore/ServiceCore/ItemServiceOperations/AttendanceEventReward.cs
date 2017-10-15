using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class AttendanceEventReward : Operation
	{
		public int EventType { get; set; }

		public bool SendByMail { get; set; }

		public bool IncludeToday { get; set; }

		public AttendanceRewardType RewardType { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
