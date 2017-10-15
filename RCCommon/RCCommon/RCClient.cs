using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using RemoteControlSystem.ServerMessage;

namespace RemoteControlSystem
{
	[Guid("B048AE94-D856-4f6e-AF48-4680E41A2BF5")]
	[Serializable]
	public class RCClient : IComparable
	{
		[field: NonSerialized]
		public event RCClient.RCClientChangeEventHandler ClientInit;

		[field: NonSerialized]
		public event RCClient.RCClientChangeEventHandler ServerInfoChange;

		[field: NonSerialized]
		public event RCClient.ProcessEventHandler ProcessAdd;

		[field: NonSerialized]
		public event RCClient.ProcessEventHandler ProcessModify;

		[field: NonSerialized]
		public event RCClient.ProcessEventHandler ProcessRemove;

		[field: NonSerialized]
		public event RCClient.ProcessEventHandler ProcessPerformanceUpdate;

		[field: NonSerialized]
		public event RCClient.ProcessStateEventHandler ProcessStateChange;

		[field: NonSerialized]
		public event RCClient.ProcessLogEventHandler ProcessLog;

		[field: NonSerialized]
		public event RCClient.FileDistributedDelgate FileDistributed;

		public bool Initialized
		{
			get
			{
				return this._initialized;
			}
		}

		public int ID
		{
			get
			{
				return this._id;
			}
		}

		public string ClientIP { get; private set; }

		public int Version { get; private set; }

		public string ServerIP { get; private set; }

		public int ServerPort { get; private set; }

		public string Name { get; private set; }

		public ICollection<RCProcess> ProcessList
		{
			get
			{
				return this._processCollection;
			}
		}

		public RCProcess this[string name]
		{
			get
			{
				if (this._processCollection.Contains(name))
				{
					return this._processCollection[name];
				}
				return null;
			}
		}

		public RCClient(int id, string clientIP)
		{
			this._id = id;
			this.ClientIP = clientIP;
		}

		public RCClient(int version, string serverIP, int serverPort, string displayName, RCProcessCollection processCollection) : this(0, "")
		{
			this.Version = version;
			this.ServerIP = serverIP;
			this.ServerPort = serverPort;
			this.Name = displayName;
			this._processCollection = new RCProcessCollection();
			foreach (RCProcess rcprocess in processCollection)
			{
				this.AddProcess(rcprocess.Clone());
			}
			this._initialized = true;
		}

		public RCClient Clone()
		{
			RCClient rcclient = new RCClient(this.ID, this.ClientIP);
			rcclient.CopyFrom(this);
			return rcclient;
		}

		public void AssignFrom(RCClient client)
		{
			if (client != null)
			{
				this.Version = client.Version;
				this.ServerIP = client.ServerIP;
				this.ServerPort = client.ServerPort;
				this.Name = client.Name;
				this._processCollection = new RCProcessCollection();
				if (client.ProcessList != null)
				{
					foreach (RCProcess process in client.ProcessList)
					{
						this.AddProcess(process);
					}
				}
				this._initialized = true;
			}
		}

		public void CopyFrom(RCClient client)
		{
			if (client != null)
			{
				this.Version = client.Version;
				this.ServerIP = client.ServerIP;
				this.ServerPort = client.ServerPort;
				this.Name = client.Name;
				this._processCollection = new RCProcessCollection();
				if (client.ProcessList != null)
				{
					foreach (RCProcess rcprocess in client.ProcessList)
					{
						this.AddProcess(rcprocess.Clone());
					}
				}
				this._initialized = true;
			}
		}

		public int CompareTo(object obj)
		{
			return this.ID.CompareTo(((RCClient)obj).ID);
		}

		public RCProcessCollection ProcessClone()
		{
			RCProcessCollection rcprocessCollection = new RCProcessCollection();
			foreach (RCProcess rcprocess in this.ProcessList)
			{
				rcprocessCollection.Add(rcprocess.Clone());
			}
			return rcprocessCollection;
		}

		private void ChildProcess_ProcessLog(RCProcess sender, RCProcess.LogEventArgs args)
		{
			if (this.ProcessLog != null)
			{
				this.ProcessLog(this, new RCClient.ProcessLogEventArgs(sender, args.Message));
			}
		}

		private void ChildProcess_StateChange(RCProcess sender, RCProcess.StateChangeEventArgs args)
		{
			if (this.ProcessStateChange != null)
			{
				this.ProcessStateChange(this, new RCClient.ProcessStateEventArgs(sender, args.ChangedTime, args.OldState));
			}
		}

		private void ChildProcess_PerformanceUpdate(object sender, EventArgs args)
		{
			if (this.ProcessPerformanceUpdate != null)
			{
				this.ProcessPerformanceUpdate(this, sender as RCProcess);
			}
		}

		private void InitClient(RCClient client)
		{
			this.CopyFrom(client);
			if (this.ClientInit != null)
			{
				this.ClientInit(this, null);
			}
		}

		private void SetServerInfo(ServerInfoMessage message)
		{
			this.ServerIP = message.ServerIP;
			this.ServerPort = message.ServerPort;
			if (this.ServerInfoChange != null)
			{
				this.ServerInfoChange(this, null);
			}
		}

		private RCProcess AddProcess(RCProcess process)
		{
			process.SetRCClient(this);
			process.Log += this.ChildProcess_ProcessLog;
			process.StateChange += this.ChildProcess_StateChange;
			process.PerformanceUpdate += this.ChildProcess_PerformanceUpdate;
			process.SysCommand += this.ChildProcess_SysCommand;
			this._processCollection.Add(process);
			if (this.ProcessAdd != null)
			{
				this.ProcessAdd(this, process);
			}
			return process;
		}

		private void ModifyProcess(RCProcess process)
		{
			RCProcess rcprocess = this._processCollection[process.Name];
			if (rcprocess != null)
			{
				rcprocess.Modify(process);
				if (this.ProcessModify != null)
				{
					this.ProcessModify(this, rcprocess);
				}
			}
		}

		private void RemoveProcess(string name)
		{
			RCProcess rcprocess = this._processCollection[name];
			if (rcprocess != null)
			{
				this._processCollection.Remove(rcprocess.Name);
				if (this.ProcessRemove != null)
				{
					this.ProcessRemove(this, rcprocess);
				}
				rcprocess.SetRCClient(null);
			}
		}

		private void StartProcess(string name)
		{
			RCProcess rcprocess = this._processCollection[name];
			if (rcprocess != null)
			{
				rcprocess.Start();
			}
		}

		private void CheckPatchProcess(List<string> name)
		{
			List<RCProcess> list = new List<RCProcess>();
			foreach (string key in name)
			{
				list.Add(this._processCollection[key]);
			}
			if (this.ProcessCheckPatch != null)
			{
				this.ProcessCheckPatch(this, list);
			}
		}

		private void StopProcess(string name)
		{
			RCProcess rcprocess = this._processCollection[name];
			if (rcprocess != null)
			{
				rcprocess.Stop();
			}
		}

		private void KillProcess(string name)
		{
			RCProcess rcprocess = this._processCollection[name];
			if (rcprocess != null)
			{
				rcprocess.Kill();
			}
		}

		private void UpdateProcess(string name)
		{
			RCProcess rcprocess = this._processCollection[name];
			if (rcprocess != null)
			{
				rcprocess.StartUpdate();
			}
		}

		private void KillUpdateProcess(string name)
		{
			RCProcess rcprocess = this._processCollection[name];
			if (rcprocess != null)
			{
				rcprocess.KillUpdate();
			}
		}

		private void StandardInProcess(StandardInProcessMessage message)
		{
			RCProcess rcprocess = this._processCollection[message.Name];
			if (rcprocess != null)
			{
				rcprocess.StandardIn(message.Message);
			}
		}

		private void StateChangeProcess(StateChangeProcessMessage message)
		{
			RCProcess rcprocess = this._processCollection[message.Name];
			if (rcprocess != null)
			{
				rcprocess.ChangeState(message.State);
			}
		}

		private void LogProcess(LogProcessMessage message)
		{
			RCProcess rcprocess = this._processCollection[message.Name];
			if (rcprocess != null)
			{
				rcprocess.AddLogManual(message.Message);
			}
		}

		private void ClientSelfUpdate(ClientSelfUpdateMessage message)
		{
			try
			{
				Process.Start(new ProcessStartInfo
				{
					WorkingDirectory = BaseConfiguration.WorkingDirectory,
					FileName = BaseConfiguration.WorkingDirectory + "\\RCClientPatcher.exe",
					Arguments = message.Argument
				});
			}
			catch (Exception)
			{
			}
		}

		private void OnFileDistributed(FileDistributeMessage message)
		{
			WatcherChangeTypes changeType = message.ChangeType;
			string path = message.Path;
			string fileName = message.FileName;
			string oldFileName = null;
			bool isDirectory = message.IsDirectory;
			byte[] fileData = null;
			if (changeType == WatcherChangeTypes.Renamed)
			{
				oldFileName = message.OldFileName;
			}
			if (changeType != WatcherChangeTypes.Deleted && !isDirectory)
			{
				fileData = message.FileData;
			}
			this.FileDistributed(this, changeType, path, fileName, oldFileName, isDirectory, fileData);
		}

		public void PerformaceUpdate(PerformanceUpdateMessage message)
		{
			foreach (KeyValuePair<string, RCProcess.SPerformance> keyValuePair in message.Performance)
			{
				RCProcess rcprocess = this._processCollection[keyValuePair.Key];
				if (rcprocess != null)
				{
					rcprocess.UpdatePerformance(keyValuePair.Value.PrivateMemorySize, keyValuePair.Value.VirtualMemorySize, keyValuePair.Value.CPU);
				}
			}
		}

		public void StartAutomaticStartProcess()
		{
			foreach (RCProcess rcprocess in this.ProcessList)
			{
				if (rcprocess.AutomaticStart)
				{
					rcprocess.Start();
				}
			}
		}

		public void KillAll()
		{
			foreach (RCProcess rcprocess in this.ProcessList)
			{
				switch (rcprocess.State)
				{
				case RCProcess.ProcessState.Updating:
					rcprocess.KillUpdate();
					break;
				case RCProcess.ProcessState.Booting:
				case RCProcess.ProcessState.On:
				case RCProcess.ProcessState.Closing:
				case RCProcess.ProcessState.Freezing:
					rcprocess.Kill();
					break;
				}
			}
		}

		public void ProcessMessage(object message)
		{
			if (message is PingMessage)
			{
				return;
			}
			if (message is ClientInitMessage)
			{
				this.InitClient((message as ClientInitMessage).Client);
				return;
			}
			if (message is ServerInfoMessage)
			{
				this.SetServerInfo(message as ServerInfoMessage);
				return;
			}
			if (message is AddProcessMessage)
			{
				this.AddProcess((message as AddProcessMessage).Process);
				return;
			}
			if (message is ModifyProcessMessage)
			{
				this.ModifyProcess((message as ModifyProcessMessage).Process);
				return;
			}
			if (message is RemoveProcessMessage)
			{
				this.RemoveProcess((message as RemoveProcessMessage).Name);
				return;
			}
			if (message is StartProcessMessage)
			{
				this.StartProcess((message as StartProcessMessage).Name);
				return;
			}
			if (message is CheckPatchProcessMessage)
			{
				this.CheckPatchProcess((message as CheckPatchProcessMessage).Name);
				return;
			}
			if (message is StopProcessMessage)
			{
				this.StopProcess((message as StopProcessMessage).Name);
				return;
			}
			if (message is KillProcessMessage)
			{
				this.KillProcess((message as KillProcessMessage).Name);
				return;
			}
			if (message is UpdateProcessMessage)
			{
				this.UpdateProcess((message as UpdateProcessMessage).Name);
				return;
			}
			if (message is KillUpdateProcessMessage)
			{
				this.KillUpdateProcess((message as KillUpdateProcessMessage).Name);
				return;
			}
			if (message is StandardInProcessMessage)
			{
				this.StandardInProcess(message as StandardInProcessMessage);
				return;
			}
			if (message is StateChangeProcessMessage)
			{
				this.StateChangeProcess(message as StateChangeProcessMessage);
				return;
			}
			if (message is LogProcessMessage)
			{
				this.LogProcess(message as LogProcessMessage);
				return;
			}
			if (message is ClientSelfUpdateMessage)
			{
				this.ClientSelfUpdate(message as ClientSelfUpdateMessage);
				return;
			}
			if (message is FileDistributeMessage)
			{
				this.OnFileDistributed(message as FileDistributeMessage);
				return;
			}
			if (message is PerformanceUpdateMessage)
			{
				this.PerformaceUpdate(message as PerformanceUpdateMessage);
				return;
			}
			throw new FormatException("Invalid Message : " + message.ToString());
		}

		public static void Console_AddProperty(string key, string value)
		{
			Console.WriteLine("{0}AddProperty '{1}' '{2}'", "$", key, value);
		}

		public static void Console_DelPropertyFromConsole(string key)
		{
			Console.WriteLine("{0}DelProperty '{1}'", "$", key);
		}

		public static void Console_AddCustomCommand(string command)
		{
			Console.WriteLine("{0}AddCustomCommand '{1}'", "$", command);
		}

		private void ChildProcess_SysCommand(RCProcess sender, RCProcess.LogEventArgs e)
		{
			List<string> list = new List<string>();
			string t = e.Message.Substring(1);
			string text = t.ParseCommand(list);
			string a;
			if ((a = text) != null)
			{
				if (a == "AddProperty")
				{
					this.SysCmd_AddProperty(sender, list);
					return;
				}
				if (a == "DelProperty")
				{
					this.SysCmd_DelProperty(sender, list);
					return;
				}
				if (!(a == "AddCustomCommand"))
				{
					return;
				}
				this.SysCmd_AddCustomCommand(sender, list);
			}
		}

		private void SysCmd_AddProperty(RCProcess process, IList<string> args)
		{
			if (args.Count < 2)
			{
				return;
			}
			if (process.Properties.ContainsKey(args[0]))
			{
				process.Properties[args[0]] = args[1];
			}
			else
			{
				process.Properties.Add(args[0], args[1]);
			}
			if (this.ProcessModify != null)
			{
				this.ProcessModify(this, process);
			}
		}

		private void SysCmd_DelProperty(RCProcess process, IList<string> args)
		{
			if (args.Count < 1)
			{
				return;
			}
			if (process.Properties.ContainsKey(args[0]))
			{
				process.Properties.Remove(args[0]);
				if (this.ProcessModify != null)
				{
					this.ProcessModify(this, process);
				}
			}
		}

		private void SysCmd_AddCustomCommand(RCProcess process, IList<string> args)
		{
			if (args.Count < 1)
			{
				return;
			}
			try
			{
				new RCProcess.CustomCommandParser(args[0]);
				process.AddSysCommand(args[0]);
				if (this.ProcessModify != null)
				{
					this.ProcessModify(this, process);
				}
			}
			catch
			{
			}
		}

		[NonSerialized]
		public RCClient.ProcessCheckPatchEventHandler ProcessCheckPatch;

		private RCProcessCollection _processCollection;

		[NonSerialized]
		private bool _initialized;

		[NonSerialized]
		private int _id;

		public delegate void RCClientChangeEventHandler(RCClient sender, object args);

		public delegate void ProcessCheckPatchEventHandler(RCClient sender, List<RCProcess> processList);

		public delegate void ProcessEventHandler(RCClient sender, RCProcess process);

		public delegate void ProcessStateEventHandler(RCClient sender, RCClient.ProcessStateEventArgs args);

		public class ProcessStateEventArgs : EventArgs
		{
			public RCProcess TargetProcess { get; private set; }

			public DateTime ChangedTime { get; private set; }

			public RCProcess.ProcessState OldState { get; private set; }

			internal ProcessStateEventArgs(RCProcess targetProcess, DateTime changedTime, RCProcess.ProcessState oldState)
			{
				this.TargetProcess = targetProcess;
				this.ChangedTime = changedTime;
				this.OldState = oldState;
			}
		}

		public delegate void ProcessLogEventHandler(RCClient sender, RCClient.ProcessLogEventArgs args);

		public class ProcessLogEventArgs : EventArgs
		{
			public RCProcess Process { get; private set; }

			public string Message { get; private set; }

			internal ProcessLogEventArgs(RCProcess process, string message)
			{
				this.Process = process;
				this.Message = message;
			}
		}

		public delegate int CompareDisplayName(string name1, string name2);

		public delegate void FileDistributedDelgate(RCClient sender, WatcherChangeTypes changeType, string pathName, string fileName, string oldFileName, bool isDirectory, byte[] fileData);
	}
}
