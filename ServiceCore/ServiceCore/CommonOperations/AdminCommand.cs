using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CommonOperations
{
	[Serializable]
	public sealed class AdminCommand : Operation
	{
		public string Command { get; set; }

		public string Argument { get; set; }

		public AdminCommand(string command, string arg1)
		{
			this.Command = command;
			this.Argument = arg1;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
