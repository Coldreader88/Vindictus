using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class MarbleSetTimerMessage : IMessage
	{
		public int Time { get; set; }

		public MarbleSetTimerMessage(int time)
		{
			this.Time = time;
		}
	}
}
