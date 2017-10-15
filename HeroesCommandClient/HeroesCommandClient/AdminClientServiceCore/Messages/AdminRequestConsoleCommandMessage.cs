using System;
using System.Runtime.InteropServices;

namespace AdminClientServiceCore.Messages
{
	[Guid("563F664C-66BF-4a6e-A8C7-4CDD2768732C")]
	[Serializable]
	public class AdminRequestConsoleCommandMessage
	{
		public string Command { get; set; }

		public string Arguments { get; set; }

		public AdminRequestConsoleCommandMessage(string cmd, string arg)
		{
			this.Command = cmd;
			this.Arguments = arg;
		}

		public override string ToString()
		{
			return string.Format("AdminRequestConsoleCommandMessage[{0} / {1}]", this.Command, this.Arguments);
		}
	}
}
