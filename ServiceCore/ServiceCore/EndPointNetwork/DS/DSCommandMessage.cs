using System;

namespace ServiceCore.EndPointNetwork.DS
{
	[Serializable]
	public sealed class DSCommandMessage : IMessage
	{
		public int CommandType { get; set; }

		public string Command { get; set; }

		public DSCommandType DSCommandType
		{
			get
			{
				return (DSCommandType)this.CommandType;
			}
		}

		public DSCommandMessage(DSCommandType type, string command)
		{
			this.CommandType = (int)type;
			this.Command = command;
		}

		public override string ToString()
		{
			return string.Format("DSCommandMessage[ {0} {1} ]", this.DSCommandType, this.Command);
		}
	}
}
