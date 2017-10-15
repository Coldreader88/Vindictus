using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class UpdateCurrentQuestInfo : Operation
	{
		public string QuestID { get; set; }

		public UpdateCurrentQuestInfo(string questID)
		{
			this.QuestID = questID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
