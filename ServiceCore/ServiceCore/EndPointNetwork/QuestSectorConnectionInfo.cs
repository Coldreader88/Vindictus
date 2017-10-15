using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QuestSectorConnectionInfo
	{
		public int From { get; set; }

		public string FromTrigger { get; set; }

		public int To { get; set; }

		public string ToSpawn { get; set; }

		public QuestSectorConnectionInfo(int from, string trigger, int to, string spawn)
		{
			this.From = from;
			this.FromTrigger = trigger;
			this.To = to;
			this.ToSpawn = spawn;
		}
	}
}
