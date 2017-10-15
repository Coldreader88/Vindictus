using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.QuestOwnershipServiceOperations
{
	[Serializable]
	public sealed class NotifyStartQuest : Operation
	{
		public string QuestID { get; set; }

		public DateTime StartTime { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
