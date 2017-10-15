using System;
using System.Collections.Generic;
using System.Reflection;
using log4net.Core;
using Utility;

namespace Executer
{
	public class Commands
	{
		[CommandHandler("Set Verbose Level", "set-verboselevel", "[Level:string]")]
		public bool Cmd_announce(string command, IList<string> args)
		{
			try
			{
				if (args.Count > 0)
				{
					Level level = Log.ParseLevel(args[0]);
					if (level != Level.Off)
					{
						Log.VerboseLevel = level;
						Console.WriteLine("Verbose Level's changed to {0}", level);
						return true;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			return false;
		}

		internal Commands()
		{
			Console.WriteLine("== Begin RCClient Command Support ==");
			this.commandDict = new Dictionary<string, Commands.CommandHandler>();
			MethodInfo[] methods = base.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (MethodInfo methodInfo in methods)
			{
				object[] customAttributes = methodInfo.GetCustomAttributes(typeof(CommandHandlerAttribute), false);
				if (customAttributes.Length > 0)
				{
					Delegate @delegate = Delegate.CreateDelegate(typeof(Commands.CommandHandler), this, methodInfo, false);
					if (@delegate != null)
					{
						foreach (CommandHandlerAttribute commandHandlerAttribute in customAttributes)
						{
							if (!this.commandDict.ContainsKey(commandHandlerAttribute.Command))
							{
								this.commandDict.Add(commandHandlerAttribute.Command, @delegate as Commands.CommandHandler);
								Console.WriteLine("$AddCustomCommand '{0}|{1} {2}'", commandHandlerAttribute.Name, commandHandlerAttribute.Command, commandHandlerAttribute.Argument);
							}
						}
					}
				}
			}
			Console.WriteLine("== End RCClient Command Support ==");
		}

		public bool Handle(string command)
		{
			List<string> list = new List<string>();
			command = command.ParseCommand(list);
			return this.commandDict.ContainsKey(command) && this.commandDict[command](command, list);
		}

		private Dictionary<string, Commands.CommandHandler> commandDict;

		private delegate bool CommandHandler(string command, IList<string> args);
	}
}
