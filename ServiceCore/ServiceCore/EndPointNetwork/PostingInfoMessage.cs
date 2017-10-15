using System;
using System.Collections.Generic;
using ServiceCore.CharacterServiceOperations.RandomMissionOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PostingInfoMessage : IMessage
	{
		public PostingInfoMessage(DateTime NextPostingTime, IList<MissionInfo> missionList)
		{
			long num = (long)(NextPostingTime - DateTime.UtcNow).TotalSeconds;
			this.RemainTimeToNextPostingTime = ((num < (long)PostingInfoMessage.TimeMax) ? ((int)num) : PostingInfoMessage.TimeMax);
			this.MissionList = new List<MissionMessage>();
			foreach (MissionInfo mission in missionList)
			{
				this.MissionList.Add(new MissionMessage(mission));
			}
		}

		public int RemainTimeToNextPostingTime { get; set; }

		public ICollection<MissionMessage> MissionList { get; set; }

		private static int TimeMax = int.MaxValue;
	}
}
