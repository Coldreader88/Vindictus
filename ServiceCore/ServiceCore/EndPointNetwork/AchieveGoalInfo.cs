using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class AchieveGoalInfo
	{
		public int ID { get; set; }

		public int Status { get; set; }

		public byte ClearCount { get; set; }

		public byte RegenedAP { get; set; }

		public int LastClearTimeDiff { get; set; }

		public DateTime GetLastClearTime(DateTime pivotTime)
		{
			return pivotTime.AddSeconds((double)(-(double)this.LastClearTimeDiff));
		}

		public AchieveGoalInfo(int id, int status, byte clearCount, byte regenedAP, DateTime lastClearTime)
		{
			this.ID = id;
			this.Status = status;
			this.ClearCount = clearCount;
			this.RegenedAP = regenedAP;
			this.LastClearTimeDiff = (int)(DateTime.Now - lastClearTime).TotalSeconds;
		}

		public override string ToString()
		{
			return string.Format("{0}({1})", this.ID, this.Status);
		}
	}
}
