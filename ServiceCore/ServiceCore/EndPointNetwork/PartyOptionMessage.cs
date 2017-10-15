using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PartyOptionMessage : IMessage
	{
		public int RestTime
		{
			get
			{
				return this.restTime;
			}
		}

		public PartyOptionMessage(int restTime)
		{
			this.restTime = restTime;
		}

		public override string ToString()
		{
			return string.Format("PartyOptionMessage[ restTime = {0} ]", this.restTime);
		}

		private int restTime;
	}
}
