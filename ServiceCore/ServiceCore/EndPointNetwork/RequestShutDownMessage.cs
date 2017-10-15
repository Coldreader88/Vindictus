using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RequestShutDownMessage : IMessage
	{
		public int Delay { get; set; }

		public override string ToString()
		{
			return string.Format("RequestShutDownMessage [ Delay = {0} ]", this.Delay);
		}
	}
}
