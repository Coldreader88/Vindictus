using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class HostRestartingMessage : IMessage
	{
		public override string ToString()
		{
			return "HostRestartingMessage[]";
		}
	}
}
