using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CompletedMissionInfoMessage : IMessage
	{
		public CompletedMissionInfoMessage(int completedMissionCount, DateTime nextCompletedMissionCountClearTime)
		{
			this.CompletedMissionCount = completedMissionCount;
			long num = (long)(nextCompletedMissionCountClearTime - DateTime.UtcNow).TotalSeconds;
			this.RemainTimeToCompletedMissionCountClear = ((num < (long)CompletedMissionInfoMessage.TimeMax) ? ((int)num) : CompletedMissionInfoMessage.TimeMax);
		}

		private static int TimeMax = 86400;

		public int CompletedMissionCount;

		public int RemainTimeToCompletedMissionCountClear;
	}
}
