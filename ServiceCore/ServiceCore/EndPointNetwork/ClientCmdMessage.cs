using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ClientCmdMessage : IMessage
	{
		public string Command { get; private set; }

		public ClientCmdMessage(string command)
		{
			this.Command = command;
		}
	}
}
