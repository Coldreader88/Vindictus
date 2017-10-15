using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class HostRestartedMessage : IMessage
	{
		public GameInfo GameInfo { get; set; }

		public override string ToString()
		{
			return "HostRestartedMessage[]";
		}
	}
}
