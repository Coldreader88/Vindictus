using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ServerCmdMessage : IMessage
	{
		public string Command { get; private set; }

		public bool Reliable { get; private set; }

		public ServerCmdMessage(string command) : this(command, true)
		{
		}

		public ServerCmdMessage(string command, bool reliable)
		{
			this.Command = command;
			this.Reliable = reliable;
		}
	}
}
