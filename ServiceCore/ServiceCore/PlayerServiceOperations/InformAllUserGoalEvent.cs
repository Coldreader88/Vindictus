using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PlayerServiceOperations
{
	[Serializable]
	public class InformAllUserGoalEvent : Operation
	{
		public int GoalID { get; set; }

		public bool IsReset { get; set; }

		public Dictionary<int, long> PartyMember { get; set; }

		public InformAllUserGoalEvent(int GoalID, bool IsReset, Dictionary<int, long> Members)
		{
			this.GoalID = GoalID;
			this.IsReset = IsReset;
			this.PartyMember = Members;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
