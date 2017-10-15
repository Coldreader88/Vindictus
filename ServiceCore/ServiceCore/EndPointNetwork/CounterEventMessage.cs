using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CounterEventMessage : IMessage
	{
		public int TotalCount { get; set; }

		public int Level { get; set; }

		public string EventName { get; set; }

		public CounterEventMessage(int TotalCount, int Level, string EventName)
		{
			this.TotalCount = TotalCount;
			this.Level = Level;
			this.EventName = EventName;
		}

		public override string ToString()
		{
			return string.Format("CounterEventMessage[ CurrentCount = {0} ,Level = {1}, EventName = {2}]", this.TotalCount, this.Level, this.EventName);
		}
	}
}
