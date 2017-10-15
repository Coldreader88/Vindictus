using System;
using System.Collections.Generic;
using Devcat.Core;
using RemoteControlSystem.ControlMessage;

namespace RemoteControlSystem.Server
{
	public class LogManager
	{
		public event EventHandler<ChildProcessLogEventArgs> OnChildProcessLogAdded;

		public event EventHandler<ListenerGroupEventArgs> OnAddLIstenGroup;

		public event EventHandler<ListenerGroupEventArgs> OnDeleteLIstenGroup;

		public LogManager()
		{
			this.dict = new Dictionary<LogManager.Key, HashSet<int>>();
		}

		private static LogManager.Key MakeKey(int clientID, string processName, int pid)
		{
			return new LogManager.Key
			{
				ClientID = clientID,
				ProcessName = processName,
				PID = pid
			};
		}

		public void AddListener(User user, int clientID, string processName, int pid)
		{
			LogManager.Key key = LogManager.MakeKey(clientID, processName, pid);
			HashSet<int> hashSet;
			if (!this.dict.ContainsKey(key))
			{
				hashSet = new HashSet<int>();
				this.dict.Add(key, hashSet);
				if (this.OnAddLIstenGroup != null)
				{
					this.OnAddLIstenGroup(null, new ListenerGroupEventArgs(clientID, processName, pid));
				}
			}
			else
			{
				hashSet = this.dict[key];
			}
			hashSet.Add(user.ClientId);
		}

		public void RemoveListener(User user)
		{
			List<LogManager.Key> list = new List<LogManager.Key>();
			foreach (KeyValuePair<LogManager.Key, HashSet<int>> keyValuePair in this.dict)
			{
				if (keyValuePair.Value.Contains(user.ClientId))
				{
					keyValuePair.Value.Remove(user.ClientId);
					if (keyValuePair.Value.Count == 0)
					{
						list.Add(keyValuePair.Key);
					}
				}
			}
			foreach (LogManager.Key key in list)
			{
				this.dict.Remove(key);
				if (this.OnDeleteLIstenGroup != null)
				{
					this.OnDeleteLIstenGroup(null, new ListenerGroupEventArgs(key.ClientID, key.ProcessName, key.PID));
				}
			}
		}

		public void RemoveListener(User user, int clientID, string processName, int pid)
		{
			LogManager.Key key = LogManager.MakeKey(clientID, processName, pid);
			if (this.dict.ContainsKey(key) && this.dict[key].Contains(user.ClientId))
			{
				this.dict[key].Remove(user.ClientId);
			}
		}

		public void ChildProcessLogAdded(object sender, EventArgs<ChildProcessLogMessage> args)
		{
			RCClient rcclient = sender as RCClient;
			LogManager.Key key = LogManager.MakeKey(rcclient.ID, args.Value.ProcessName, args.Value.ProcessID);
			if (this.dict.ContainsKey(key) && this.OnChildProcessLogAdded != null)
			{
				this.OnChildProcessLogAdded(rcclient, new ChildProcessLogEventArgs(this.dict[key], args.Value));
			}
		}

		private Dictionary<LogManager.Key, HashSet<int>> dict;

		private struct Key
		{
			public int ClientID;

			public string ProcessName;

			public int PID;
		}
	}
}
