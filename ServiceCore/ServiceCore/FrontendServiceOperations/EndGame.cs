using System;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.FrontendServiceOperations
{
	[Serializable]
	public sealed class EndGame : Operation
	{
		public QuestSummaryInfo Summary { get; set; }

		public string StorySectorBSP { get; set; }

		public string StorySectorEntity { get; set; }

		public bool LeavePartyIfTown { get; set; }

		public bool CanRestart { get; set; }

		public bool CanContinueFinishedQuest { get; set; }

		public long NewPartyID { get; set; }

		public EndGame(QuestSummaryInfo summary, string bsp, string entity, bool leavePartyIfTown, bool canRestart, bool canContinueFinishedQuest, long newPartyID)
		{
			this.Summary = summary;
			this.StorySectorBSP = bsp;
			this.StorySectorEntity = entity;
			this.LeavePartyIfTown = leavePartyIfTown;
			this.CanRestart = canRestart;
			this.CanContinueFinishedQuest = canContinueFinishedQuest;
			this.NewPartyID = newPartyID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
