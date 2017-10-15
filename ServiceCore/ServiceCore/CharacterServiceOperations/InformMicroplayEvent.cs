using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class InformMicroplayEvent : Operation
	{
		public List<int> TitleGoalID
		{
			get
			{
				return this.titlegoalID;
			}
		}

		public string QuestID
		{
			get
			{
				return this.questID;
			}
		}

		public List<int> Count
		{
			get
			{
				return this.count;
			}
		}

		public InformMicroplayEvent(List<int> titlegoalID, string questID, List<int> count)
		{
			this.titlegoalID = titlegoalID;
			this.questID = questID;
			this.count = count;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}

		private List<int> titlegoalID;

		private string questID;

		private List<int> count;
	}
}
