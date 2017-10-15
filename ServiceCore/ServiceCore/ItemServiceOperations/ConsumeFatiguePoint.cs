using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class ConsumeFatiguePoint : Operation
	{
		public string QuestID { get; set; }

		public ConsumeFatiguePoint(string questID)
		{
			this.QuestID = questID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
