using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.FrontendServiceOperations
{
	[Serializable]
	public sealed class ExecCommand : Operation
	{
		public string Command
		{
			get
			{
				return this.command;
			}
		}

		public string Arg1
		{
			get
			{
				return this.arg1;
			}
		}

		public ExecCommand(string command, string arg1)
		{
			this.command = command;
			this.arg1 = arg1;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}

		private string command;

		private string arg1;
	}
}
