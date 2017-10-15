using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class GiveManufactureRecipesByQuestID : Operation
	{
		public string QuestID { get; private set; }

		public GiveManufactureRecipesByQuestID(string questID)
		{
			this.QuestID = questID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
