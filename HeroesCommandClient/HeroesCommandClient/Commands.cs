using System;
using System.Collections.Generic;
using System.Reflection;
using Devcat.Core.Net.Message;
using RemoteControlSystem;
using RemoteControlSystem.ClientMessage;
using RemoteControlSystem.ServerMessage;

namespace HeroesCommandClient
{
	public class Commands
	{
		[CommandHandler("Announce to client", "announce", "[Server:Server] [Message:lstring]")]
		public bool Cmd_announce(string command, IList<string> args)
		{
			if (args.Count > 1 && args[1].Length > 0)
			{
				HeroesCommandBridge.Notify("{0} Server Announce - \"{1}\".", new object[]
				{
					args[0],
					args[1]
				});
				this.manager[args[0]].RequestNotify(args[1]);
				return true;
			}
			return false;
		}

		[CommandHandler("Request shutdown", "shutdown", "[Server:Server] [Sec:numeric]")]
		public bool Cmd_shutdown(string command, IList<string> args)
		{
			if (args.Count > 1)
			{
				string text = string.Empty;
				if (args.Count > 2)
				{
					text = args[2];
				}
				int num = int.Parse(args[1]);
				if (num < 0)
				{
					this.manager[args[0]].RequestShutDown(-1, text);
					HeroesCommandBridge.Notify("Shutdown Canceled");
				}
				else
				{
					if (num == 0)
					{
						num = 1;
					}
					this.manager[args[0]].RequestShutDown(num, text);
					if (text == string.Empty)
					{
						HeroesCommandBridge.Notify("{0} Shutdown Reserved after {1} seconds", new object[]
						{
							args[0],
							num
						});
					}
					else
					{
						HeroesCommandBridge.Notify("{0} Shutdown Reserved after {1} seconds with announce - \"{2}\".", new object[]
						{
							args[0],
							num,
							text
						});
					}
				}
				return true;
			}
			return false;
		}

		[CommandHandler("Extend Cash Item", "extendcashitem", "[Server:Server] [From Date:date] [Minutes:numeric]")]
		public bool Cmd_extendcashitem(string command, IList<string> args)
		{
			if (args.Count > 2)
			{
				this.manager[args[0]].RequestExtendCashItem(DateTime.Parse(args[1]), int.Parse(args[2]));
				HeroesCommandBridge.Notify("{0} Extendcashitem {1} {2}", new object[]
				{
					args[0],
					args[1],
					args[2]
				});
				return true;
			}
			return false;
		}

		[CommandHandler("Push Items(Item Festival)", "pushitem", "[Server:Server] [Item] [Count:numeric]  [IsExpire:bool] [Expire Date:date] [IsCafe:bool] [Message]")]
		public bool Cmd_pushitem(string command, IList<string> args)
		{
			if (args.Count > 3)
			{
				string text = string.Empty;
				if (args.Count > 6)
				{
					text = args[6];
				}
				this.manager[args[0]].RequestItemFestivalEx(args[1], int.Parse(args[2]), int.Parse(args[3]) != 0, new DateTime?(DateTime.Parse(args[4])), int.Parse(args[5]) != 0, text);
				HeroesCommandBridge.Notify("{0} Pushitem {1} {2} {3} {4} {5} \"{6}\".", new object[]
				{
					args[0],
					args[1],
					args[2],
					args[3],
					args[4],
					args[5],
					text
				});
				return true;
			}
			return false;
		}

		[CommandHandler("Freetoken Event", "freetoken", "[Server:Server] [On/Off:bool] [Message]")]
		public bool Cmd_freetoken(string command, IList<string> args)
		{
			if (args.Count > 1)
			{
				bool flag = int.Parse(args[1]) != 0;
				this.manager[args[0]].RequestFreeToken(flag);
				string text = string.Empty;
				if (args.Count > 2 && args[2].Length > 0)
				{
					text = args[2];
					this.manager[args[0]].RequestNotify(text);
				}
				HeroesCommandBridge.Notify("{0} FreeToken Mode {1}. - \"{2}\"", new object[]
				{
					args[0],
					flag ? "On" : "Off",
					text
				});
				return true;
			}
			return false;
		}

		[CommandHandler("Kick Character", "kick_cid", "[Server:Server] [CharacterName]")]
		public bool Cmd_KickCharacter(string command, IList<string> args)
		{
			if (args.Count > 1 && args[1].Length > 0)
			{
				this.manager[args[0]].RequestKick("", args[1]);
				HeroesCommandBridge.Notify("{0} Kick Character [{1}].", new object[]
				{
					args[0],
					args[1]
				});
				return true;
			}
			return false;
		}

		[CommandHandler("Kick Account", "kick_uid", "[Server:Server] [Account]")]
		public bool Cmd_KickAccount(string command, IList<string> args)
		{
			if (args.Count > 1 && args[1].Length > 0)
			{
				this.manager[args[0]].RequestKick(args[1], "");
				HeroesCommandBridge.Notify("{0} Kick Account [{1}].", new object[]
				{
					args[0],
					args[1]
				});
				return true;
			}
			return false;
		}

		[CommandHandler("DS Serivce Cheat", "sv_cheats", "[Server:Server] [On/Off:bool] [Message]")]
		public bool Cmd_sv_cheats(string command, IList<string> args)
		{
			if (args.Count > 1)
			{
				bool flag = int.Parse(args[1]) != 0;
				this.manager[args[0]].RequestDSCheat(flag ? 1 : 0);
				string text = string.Empty;
				if (args.Count > 2 && args[2].Length > 0)
				{
					text = args[2];
				}
				HeroesCommandBridge.Notify("{0} DS Cheat(sv_cheat) {1}. - \"{2}\"", new object[]
				{
					args[0],
					flag ? "On" : "Off",
					text
				});
				return true;
			}
			return false;
		}

		[CommandHandler("ShutdownAll", "shutdownall", "[Server:Server]")]
		public bool Cmd_ShutdownAll(string command, IList<string> args)
		{
			if (args.Count > 0)
			{
				Dictionary<string, List<int>> dictionary = new Dictionary<string, List<int>>();
				foreach (KeyValuePair<int, string> keyValuePair in HeroesCommandBridge.Client.FindAllSubProcess(args[0]))
				{
					if (!dictionary.ContainsKey(keyValuePair.Value))
					{
						dictionary.Add(keyValuePair.Value, new List<int>());
					}
					List<int> list = dictionary[keyValuePair.Value];
					list.Add(keyValuePair.Key);
				}
				foreach (KeyValuePair<string, List<int>> keyValuePair2 in dictionary)
				{
					ControlRequestMessage controlRequestMessage = new ControlRequestMessage(SerializeWriter.ToBinary<StopProcessMessage>(new StopProcessMessage(keyValuePair2.Key)).Bytes);
					foreach (int id in keyValuePair2.Value)
					{
						controlRequestMessage.AddClientID(id);
					}
					HeroesCommandBridge.Client.SendMessage<ControlRequestMessage>(controlRequestMessage);
				}
				return true;
			}
			return false;
		}

		[CommandHandler("Console Command", "console", "[Server:Server] [Command:lstring]")]
		public bool Cmd_Console(string command, IList<string> args)
		{
			if (args.Count > 1)
			{
				string text = args[1].Trim();
				int num = text.IndexOf(' ');
				string text2;
				string args2;
				if (num == -1)
				{
					text2 = text;
					args2 = "";
				}
				else
				{
					text2 = text.Substring(0, num);
					args2 = text.Substring(num).Trim();
				}
				if (text2.Length > 2)
				{
					this.manager[args[0]].RequestConsoleCommand(text2, args2);
					return true;
				}
			}
			return false;
		}

		internal Commands(HeroesAdminManager _manager)
		{
			this.manager = _manager;
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
								RCClient.Console_AddCustomCommand(RCProcess.CustomCommandParser.ToRawString(commandHandlerAttribute.Name, commandHandlerAttribute.Command, commandHandlerAttribute.Argument));
							}
						}
					}
				}
			}
		}

		public bool Handle(string command, IList<string> args)
		{
			return this.commandDict.ContainsKey(command) && this.commandDict[command](command, args);
		}

		private Dictionary<string, Commands.CommandHandler> commandDict;

		private HeroesAdminManager manager;

		private delegate bool CommandHandler(string command, IList<string> args);
	}
}
