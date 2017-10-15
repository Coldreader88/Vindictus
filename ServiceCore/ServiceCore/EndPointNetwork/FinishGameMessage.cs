using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class FinishGameMessage : IMessage
	{
		public QuestSummaryInfo Summary { get; set; }

		public bool HasStorySector { get; set; }

		public bool CanRestart { get; set; }

		public bool CanContinueFinishedQuest { get; set; }

		public FinishGameMessage(QuestSummaryInfo summary, bool hasStorySector, bool canRestart, bool canContinueFinishedQuest)
		{
			this.Summary = summary;
			this.HasStorySector = hasStorySector;
			this.CanRestart = canRestart;
			this.CanContinueFinishedQuest = canContinueFinishedQuest;
		}

		public override string ToString()
		{
			return string.Format("FinishGameMessage[ {0} ]", new object[]
			{
				this.Summary,
				this.HasStorySector ? " Story" : "",
				this.CanRestart ? " Restart " : "",
				this.CanContinueFinishedQuest ? " Continueable" : ""
			});
		}
	}
}
