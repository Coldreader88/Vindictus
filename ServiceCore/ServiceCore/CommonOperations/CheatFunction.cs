using System;

namespace ServiceCore.CommonOperations
{
	public class CheatFunction : Attribute
	{
		public string Command { get; set; }

		public string Desc { get; set; }

		public CheatFunction(string command, string desc)
		{
			this.Command = command;
			this.Desc = desc;
		}

		public CheatFunction(string command) : this(command, command)
		{
		}
	}
}
