using System;
using System.Collections.Generic;
using Devcat.Core;
using RemoteControlSystem;
using RemoteControlSystem.ControlMessage;

namespace HeroesOpTool
{
	internal class ChildProcessLogGeneratorCollection
	{
		public event EventHandler<EventArgs<ChildProcessLogDisconnectMessage>> OnLogClosed;

		private static string MakeKey(int clientID, string processName, int pid)
		{
			return string.Format("{0}:{1}-{2}", clientID, processName, pid);
		}

		public ChildProcessLogGeneratorCollection()
		{
			this.dict = new Dictionary<string, ChildProcessLogGeneratorCollection.ChildProcessLogGenerator>();
		}

		public bool MakeGenerator(RCClient client, RCProcess process, int pid)
		{
			string key = ChildProcessLogGeneratorCollection.MakeKey(client.ID, process.Name, pid);
			if (this.dict.ContainsKey(key))
			{
				return false;
			}
			ChildProcessLogGeneratorCollection.ChildProcessLogGenerator childProcessLogGenerator = new ChildProcessLogGeneratorCollection.ChildProcessLogGenerator(client, process, pid);
			this.dict.Add(key, childProcessLogGenerator);
			childProcessLogGenerator.OnOpen += this.LogOpened;
			childProcessLogGenerator.OnClose += this.LogClosed;
			return true;
		}

		public LogGenerator FindGenerator(int clientID, string processName, int pid)
		{
			string key = ChildProcessLogGeneratorCollection.MakeKey(clientID, processName, pid);
			if (this.dict.ContainsKey(key))
			{
				return this.dict[key];
			}
			return null;
		}

		private void LogOpened(object sender, EventArgs args)
		{
			ChildProcessLogGeneratorCollection.ChildProcessLogGenerator childProcessLogGenerator = sender as ChildProcessLogGeneratorCollection.ChildProcessLogGenerator;
			if (!this.dict.ContainsKey(childProcessLogGenerator.Key))
			{
				this.dict.Add(childProcessLogGenerator.Key, childProcessLogGenerator);
			}
		}

		private void LogClosed(object sender, EventArgs args)
		{
			ChildProcessLogGeneratorCollection.ChildProcessLogGenerator childProcessLogGenerator = sender as ChildProcessLogGeneratorCollection.ChildProcessLogGenerator;
			if (childProcessLogGenerator != null && this.dict.ContainsKey(childProcessLogGenerator.Key))
			{
				this.dict.Remove(childProcessLogGenerator.Key);
				if (this.OnLogClosed != null)
				{
					this.OnLogClosed(this, new EventArgs<ChildProcessLogDisconnectMessage>(new ChildProcessLogDisconnectMessage(childProcessLogGenerator.ClientID, childProcessLogGenerator.ProcessName, childProcessLogGenerator.PID)));
				}
			}
		}

		public void LogGenerated(int clientID, string processName, int pid, string message)
		{
			string key = ChildProcessLogGeneratorCollection.MakeKey(clientID, processName, pid);
			if (this.dict.ContainsKey(key))
			{
				this.dict[key].LogGenerated(null, message);
			}
		}

		private Dictionary<string, ChildProcessLogGeneratorCollection.ChildProcessLogGenerator> dict;

		private class ChildProcessLogGenerator : LogGenerator
		{
			public int ClientID { get; private set; }

			public string ProcessName { get; private set; }

			public int PID { get; private set; }

			public ChildProcessLogGenerator(RCClient client, RCProcess process, int pid) : base(ChildProcessLogGeneratorCollection.MakeKey(client.ID, process.Name, pid), string.Format("{0}:{1}-{2}", client.Name, process.Description, pid))
			{
				this.ClientID = client.ID;
				this.ProcessName = process.Name;
				this.PID = pid;
			}
		}
	}
}
