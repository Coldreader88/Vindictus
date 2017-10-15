using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class NotifyForceStartMessage : IMessage
	{
		public int UntilForceStart { get; set; }

		public NotifyForceStartMessage(int UntilForceStart)
		{
			this.UntilForceStart = UntilForceStart;
		}

		public override string ToString()
		{
			return string.Format("NotifyForceStartMessage [ UntilForceStart = {0} ]", this.UntilForceStart);
		}
	}
}
