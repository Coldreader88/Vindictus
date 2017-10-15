using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class NoticeShutDownMessage : IMessage
	{
		public int Delay { get; set; }

		public override string ToString()
		{
			return string.Format("NoticeShutDownMessage [ Delay = {0} ]", this.Delay);
		}
	}
}
