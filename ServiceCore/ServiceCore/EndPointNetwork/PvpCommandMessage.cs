using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PvpCommandMessage : IMessage
	{
		public int CommandInt { get; set; }

		public string Arg { get; set; }

		public PvpCommandMessage(PvpCommandType command, string arg)
		{
			this.CommandInt = (int)command;
			this.Arg = arg;
		}

		public override string ToString()
		{
			return string.Format("PvpCommandMessage[ {0} : {1}]", (PvpCommandType)this.CommandInt, this.Arg);
		}
	}
}
