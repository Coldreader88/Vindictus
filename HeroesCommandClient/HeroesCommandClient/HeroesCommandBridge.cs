using System;
using System.Collections.Generic;
using Devcat.Core;
using Devcat.Core.Threading;
using HeroesCommandClient.Properties;
using RemoteControlSystem;

namespace HeroesCommandClient
{
	internal class HeroesCommandBridge
	{
		public static JobProcessor Thread { get; private set; }

		public static RCServiceClient Client { get; private set; }

		private static void Main(string[] args)
		{
			HeroesCommandBridge.Client = new RCServiceClient();
			HeroesCommandBridge.Thread = new JobProcessor();
			HeroesCommandBridge.Thread.ExceptionOccur += delegate(object sender, EventArgs<Exception> e)
			{
				Console.WriteLine(e.Value.ToString());
			};
			HeroesCommandBridge.Thread.Start();
			HeroesCommandBridge.Instance.Loop();
			HeroesCommandBridge.Thread.Stop();
		}

		private void Loop()
		{
			HeroesCommandBridge.Client.LoginSuccess += delegate(object s, EventArgs e)
			{
				Console.WriteLine("connected");
			};
			HeroesCommandBridge.Client.LoginFail += delegate(object s, EventArgs<Exception> e)
			{
				Console.WriteLine("Login Failure:\n{0}", e.Value.ToString());
			};
			HeroesCommandBridge.Client.ServerGroupAdded += delegate(object s, EventArgs<string> e)
			{
				Console.WriteLine("ServerGroup {0} found", e.Value);
			};
			HeroesCommandBridge.Client.ServerGroupRemoved += delegate(object s, EventArgs<string> e)
			{
				Console.WriteLine("ServerGroup {0} removed", e.Value);
			};
			HeroesCommandBridge.Client.Start();
			HeroesAdminManager manager = new HeroesAdminManager(HeroesCommandBridge.Client);
			manager.ServerGroupConnected += delegate(object s, EventArgs<string> e)
			{
				RCClient.Console_AddProperty("commandbridge", manager.ServerString);
				Console.WriteLine("ServerGroup {0} conncted", e.Value);
			};
			manager.ServerGroupDisconnected += delegate(object s, EventArgs<string> e)
			{
				RCClient.Console_AddProperty("commandbridge", manager.ServerString);
				Console.WriteLine("ServerGroup {0} disconnected", e.Value);
			};
			manager.ServerGroupUserCounted += delegate(object s, EventArgs<string> e)
			{
				Console.WriteLine(e.Value);
			};
			manager.ServerGroupNotified += delegate(object s, EventArgs<string> e)
			{
				HeroesCommandBridge.Notify(e.Value);
			};
			EchoClient echoClient = null;
			if (Settings.Default.EchoServerUse)
			{
				echoClient = new EchoClient();
				echoClient.AutoReconnect = true;
				HeroesCommandBridge.Client.ProcessLogged += delegate(object s, EventArgs<string> e)
				{
					KeyValuePair<RCClient, RCProcess> keyValuePair = (KeyValuePair<RCClient, RCProcess>)s;
					if (keyValuePair.Key != null && keyValuePair.Value != null)
					{
						echoClient.SendLog(string.Format("{0}_{1}", keyValuePair.Key.ID, keyValuePair.Value.Name), string.Format("{0}({1})", keyValuePair.Value.Description, keyValuePair.Key.Name), e.Value);
					}
				};
				echoClient.ConnectionSucceed += delegate(object s, EventArgs e)
				{
					Console.WriteLine("Echo Client connected");
				};
				echoClient.ConnectionFailed += delegate(object s, EventArgs<Exception> e)
				{
					Console.WriteLine("Echo Client connection failed");
				};
				echoClient.Disconnected += delegate(object s, EventArgs e)
				{
					Console.WriteLine("Echo Client disconnected");
				};
				echoClient.Start();
			}
			this.commandHandler = new Commands(manager);
			for (;;)
			{
				string text = Console.ReadLine();
				if (text != null && text.ToLower() == "shutdown")
				{
					break;
				}
				HeroesCommandBridge.Thread.Enqueue(Job.Create<string>(new Action<string>(this.ProcessCommand), text));
			}
			if (echoClient != null)
			{
				echoClient.Stop();
				echoClient = null;
			}
			HeroesCommandBridge.Client.Stop();
			HeroesCommandBridge.Client = null;
		}

		public static void Notify(string output)
		{
			string[] array = output.Split(new string[]
			{
				"\r\n"
			}, StringSplitOptions.RemoveEmptyEntries);
			foreach (string arg in array)
			{
				Console.Write("{0} {1}\r\n", "!", arg);
			}
		}

		public static void Notify(string format, params object[] arg)
		{
			Console.Write("{0} ", "!");
			Console.WriteLine(format, arg);
		}

		private void ProcessCommand(string rawCommand)
		{
			List<string> list = new List<string>();
			string text = rawCommand.ParseCommand(list);
			bool flag = this.commandHandler.Handle(text, list);
			Console.WriteLine("Command [{0}] is {1}", text, flag ? "Executed" : "Not Executed");
		}

		public const string CommandBridge = "commandbridge";

		private Commands commandHandler;

		private static HeroesCommandBridge Instance = new HeroesCommandBridge();
	}
}
