using System;

namespace HeroesCommandClient
{
	[AttributeUsage(AttributeTargets.Method)]
	public class CommandHandlerAttribute : Attribute
	{
		public string Name { get; set; }

		public string Command { get; set; }

		public string Argument { get; set; }

		public CommandHandlerAttribute(string name, string command, string argument)
		{
			this.Name = name;
			this.Command = command;
			this.Argument = argument;
		}
	}
}
