using System;
using System.Windows.Forms;

namespace ExecutionSupporter
{
	public class ProcessCommandFunc
	{
		[Help(HelpText = "start service")]
		public void Cmd_startservice(ExecutionSupportCore core, string arg)
		{
			core.ServiceManager.StartService();
		}

		[Help(HelpText = "end service")]
		public void Cmd_endservice(ExecutionSupportCore core, string arg)
		{
			core.ServiceManager.EndService();
		}

		[Help(HelpText = "start server")]
		public void Cmd_startserver(ExecutionSupportCore core, string arg)
		{
			core.MachineManager.StartServer();
		}

		[Help(HelpText = "end server")]
		public void Cmd_endserver(ExecutionSupportCore core, string arg)
		{
			core.MachineManager.EndServer();
		}

		[Help(HelpText = "update server")]
		public void Cmd_updateserver(ExecutionSupportCore core, string arg)
		{
			core.MachineManager.UpdateServer();
		}

		[Help(HelpText = "request user count")]
		public void Cmd_requestusercount(ExecutionSupportCore core, string arg)
		{
			core.AdminClientNode.RequestUserCount();
		}

		[Help(Usage = "announce MESSAGE", HelpText = "announce to client")]
		public void Cmd_announce(ExecutionSupportCore core, string arg)
		{
			if (arg.Length > 0)
			{
				core.LogManager.AddLog(LogType.INFO, string.Format("Server Announce - \"{0}\".", arg), new object[0]);
				core.AdminClientNode.RequestNotify(arg);
			}
		}

		[Help(Usage = "shutdown SEC [MESSAGE]", HelpText = "request shutdown")]
		public void Cmd_shutdown(ExecutionSupportCore core, string arg)
		{
			if (arg.Length > 0)
			{
				string text = "";
				int num = arg.IndexOf(' ');
				int num2;
				if (num == -1)
				{
					num2 = int.Parse(arg);
				}
				else
				{
					num2 = int.Parse(arg.Substring(0, num));
					text = arg.Substring(num).Trim();
				}
				if (num2 < 0)
				{
					core.AdminClientNode.RequestShutDown(-1, text);
					core.LogManager.AddLog(LogType.INFO, "Shutdown Canceled", new object[0]);
					return;
				}
				if (num2 == 0)
				{
					num2 = 1;
				}
				core.AdminClientNode.RequestShutDown(num2, text);
				if (text == "")
				{
					core.LogManager.AddLog(LogType.INFO, string.Format("Shutdown Reserved after {0} seconds", num2), new object[0]);
					return;
				}
				core.LogManager.AddLog(LogType.INFO, string.Format("Shutdown Reserved after {0} seconds with announce - \"{1}\".", num2, text), new object[0]);
			}
		}

		[Help(Usage = "pushitem ITEM COUNT ISCAFE [MESSAGE]", HelpText = "push items(item festival) : ISCAFE = 0/1")]
		public void Cmd_pushitem(ExecutionSupportCore core, string arg)
		{
			if (arg.Length > 0)
			{
				string[] array = new string[3];
				int i;
				for (i = 0; i < 3; i++)
				{
					int num = arg.IndexOf(' ');
					if (num == -1)
					{
						array[i] = arg.Substring(0);
						arg = "";
						break;
					}
					array[i] = arg.Substring(0, num);
					arg = arg.Substring(num).Trim();
				}
				if (i >= 2)
				{
					core.AdminClientNode.RequestItemFestival(array[0], int.Parse(array[1]), arg, int.Parse(array[2]) != 0);
					core.LogManager.AddLog(LogType.INFO, string.Format("Pushitem {0} {1} {2} \"{3}\".", new object[]
					{
						array[0],
						array[1],
						array[2],
						arg
					}), new object[0]);
				}
			}
		}

		[Help(Usage = "freetoken (on | off) [MESSAGE]", HelpText = "let freetoken event on/off")]
		public void Cmd_freetoken(ExecutionSupportCore core, string arg)
		{
			string text = "";
			int num = arg.IndexOf(' ');
			if (num != -1)
			{
				text = arg.Substring(num).Trim();
				arg = arg.Substring(0, num);
			}
			if (arg == "on")
			{
				core.AdminClientNode.RequestFreeToken(true);
				if (text.Length > 0)
				{
					core.AdminClientNode.RequestNotify(text);
				}
				core.LogManager.AddLog(LogType.INFO, string.Format("FreeToken Mode On. - \"{0}\"", text), new object[0]);
				return;
			}
			if (arg == "off")
			{
				core.AdminClientNode.RequestFreeToken(false);
				if (text.Length > 0)
				{
					core.AdminClientNode.RequestNotify(text);
				}
				core.LogManager.AddLog(LogType.INFO, string.Format("FreeToken Mode Off. - \"{0}\"", text), new object[0]);
			}
		}

		[Help(Usage = "kick (cid CHARNAME | uid ACCOUNT)", HelpText = "kick user")]
		public void Cmd_kick(ExecutionSupportCore core, string arg)
		{
			char[] separator = new char[]
			{
				' '
			};
			string[] array = arg.Split(separator);
			if (array.Length >= 2)
			{
				if (array[0] == "cid")
				{
					core.AdminClientNode.RequestKick("", array[1]);
					core.LogManager.AddLog(LogType.INFO, string.Format("Kick Character [{0}].", array[1]), new object[0]);
					return;
				}
				if (array[0] == "uid")
				{
					core.AdminClientNode.RequestKick(array[1], "");
					core.LogManager.AddLog(LogType.INFO, string.Format("Kick Account [{0}].", array[1]), new object[0]);
				}
			}
		}

		[Help(Usage = "run COMMAND", HelpText = "run console command")]
		public void Cmd_run(ExecutionSupportCore core, string arg)
		{
			core.MachineManager.RunConsole(arg);
		}

		[Help(HelpText = "this popup")]
		public void Cmd_help(ExecutionSupportCore core, string arg)
		{
			core.Form.Invoke(new Action(delegate
			{
				MessageBox.Show(ProcessCommandHelper.HelpText, "Help", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}));
		}

		[Help(Usage = "sv_cheats (on | off)", HelpText = "(DS service cheat toggle) sv_cheats on/off")]
		public void Cmd_sv_cheats(ExecutionSupportCore core, string arg)
		{
			string arg2 = "";
			int num = arg.IndexOf(' ');
			if (num != -1)
			{
				arg2 = arg.Substring(num).Trim();
				arg = arg.Substring(0, num);
			}
			if (arg == "on")
			{
				core.AdminClientNode.RequestDSCheat(1);
				core.LogManager.AddLog(LogType.INFO, string.Format("DS Cheat(sv_cheat) On. - \"{0}\"", arg2), new object[0]);
				return;
			}
			if (arg == "off")
			{
				core.AdminClientNode.RequestDSCheat(0);
				core.LogManager.AddLog(LogType.INFO, string.Format("DS Cheat(sv_cheat) Off. - \"{0}\"", arg2), new object[0]);
			}
		}
	}
}
