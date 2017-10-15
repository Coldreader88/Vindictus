using System;

namespace ExecutionSupporterMessages
{
	[Serializable]
	public sealed class ExecuteMessage
	{
		public string TargetFile { get; set; }

		public string Args { get; set; }

		public ExecuteMessage(string targetFile, string args)
		{
			this.TargetFile = targetFile;
			this.Args = args;
		}
	}
}
