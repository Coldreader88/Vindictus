using System;
using System.Collections.Generic;
using RemoteControlSystem;

namespace HeroesOpTool.RCUser.ServerMonitorSystem
{
	internal class ProcessLogGeneratorCollection
	{
		public LogGenerator AllProcessLog { get; private set; }

		private static string MakeKey(RCClient client, RCProcess process)
		{
			return string.Format("{0} - {1}", client.Name, process.Name);
		}

		public ProcessLogGeneratorCollection()
		{
			this.dict = new Dictionary<string, LogGenerator>();
			this.AllProcessLog = new LogGenerator("all_process", "프로세스 로그");
			this.AllProcessLog.OnOpen += this.AllProcessLogOpened;
			this.AllProcessLog.OnClose += this.AllProcessLogClosed;
		}

		public LogGenerator GetGenerator(RCClient client, RCProcess process)
		{
			string key = ProcessLogGeneratorCollection.MakeKey(client, process);
			if (this.dict.ContainsKey(key))
			{
				return this.dict[key];
			}
			LogGenerator logGenerator = new LogGenerator(key, process.Description);
			logGenerator.OnOpen += this.LogOpened;
			logGenerator.OnClose += this.LogClosed;
			return logGenerator;
		}

		private void AllProcessLogOpened(object sender, EventArgs args)
		{
			this.allProcessLogActive++;
		}

		private void AllProcessLogClosed(object sender, EventArgs args)
		{
			this.allProcessLogActive--;
		}

		private void LogOpened(object sender, EventArgs args)
		{
			LogGenerator logGenerator = sender as LogGenerator;
			if (!this.dict.ContainsKey(logGenerator.Key))
			{
				this.dict.Add(logGenerator.Key, logGenerator);
			}
		}

		private void LogClosed(object sender, EventArgs args)
		{
			LogGenerator logGenerator = sender as LogGenerator;
			if (this.dict.ContainsKey(logGenerator.Key))
			{
				this.dict.Remove(logGenerator.Key);
			}
		}

		public void LogGenerated(RCClient client, RCProcess process, string message)
		{
			string key = ProcessLogGeneratorCollection.MakeKey(client, process);
			if (this.dict.ContainsKey(key))
			{
				this.dict[key].LogGenerated(process, message);
			}
			if (this.allProcessLogActive > 0)
			{
				this.AllProcessLog.LogGenerated(process, message);
			}
		}

		private int allProcessLogActive;

		private Dictionary<string, LogGenerator> dict;
	}
}
