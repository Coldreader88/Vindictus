using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TodayMissinState
	{
		public int ID { get; set; }

		public int CurrentCount { get; set; }

		public bool IsFinished { get; set; }

		public TodayMissinState(int id, int currentCount, bool isFinished)
		{
			this.ID = id;
			this.CurrentCount = currentCount;
			this.IsFinished = isFinished;
		}

		public override string ToString()
		{
			return string.Format("ID : {0} / Count : {1} / IsFinished : {2}", this.ID, this.CurrentCount, this.IsFinished);
		}
	}
}
