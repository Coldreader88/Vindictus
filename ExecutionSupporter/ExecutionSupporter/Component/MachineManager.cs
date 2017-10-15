using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AdminClientServiceCore.Messages;
using Devcat.Core.Net.Message;
using Devcat.Core.Threading;
using ExecutionSupporter.Properties;
using ExecutionSupporterMessages;
using Utility;

namespace ExecutionSupporter.Component
{
	public class MachineManager
	{
		public ExecutionSupportCore Core { get; set; }

		public ExecutionSupporterForm Form { get; set; }

		public Dictionary<string, Machine> MachineDict { get; set; }

		public HeroesSupportDataContext Context { get; set; }

		public MessageHandlerFactory MF { get; set; }

		public HashSet<string> RequestedMachines { get; set; }

		public MachineRequest RequestedCommand { get; set; }

		public FileList FileList { get; set; }

		public string LatestServerFile
		{
			get
			{
				return this.FileList.GetLatestServerFile();
			}
		}

		public DateTime LatestServerFileTime
		{
			get
			{
				return this.FileList.GetLatestServerFileTime();
			}
		}

		public string LatestClientFile
		{
			get
			{
				return this.FileList.GetLatestDSFile();
			}
		}

		public DateTime LatestClientFileTime
		{
			get
			{
				return this.FileList.GetLatestDSFileTime();
			}
		}

		public bool IsStarted
		{
			get
			{
				foreach (Machine machine in this.MachineDict.Values)
				{
					if (!machine.IsStarted)
					{
						return false;
					}
				}
				return true;
			}
		}

		public bool IsKilled
		{
			get
			{
				foreach (Machine machine in this.MachineDict.Values)
				{
					if (!machine.IsKilled)
					{
						return false;
					}
				}
				return true;
			}
		}

		public bool IsUpdated
		{
			get
			{
				foreach (Machine machine in this.MachineDict.Values)
				{
					if (!machine.IsUpdated)
					{
						return false;
					}
				}
				return true;
			}
		}

		public MachineManager(ExecutionSupportCore core)
		{
			this.Core = core;
			this.Form = core.Form;
			this.MachineDict = new Dictionary<string, Machine>();
			this.Context = new HeroesSupportDataContext();
			this.MF = new MessageHandlerFactory();
			this.MF.Register<Machine>(Messages.TypeConverters, "ProcessMessage");
			this.MF.Register<AdminClientNode>(AdminClientServiceOperationMessages.TypeConverters, "ProcessMessage");
			this.RequestedCommand = MachineRequest.None;
			this.RequestedMachines = new HashSet<string>();
			this.FileList = new FileList(Settings.Default.ServerBin, Settings.Default.DSBin);
			this.FileList.ExceptionOccurred += delegate(Exception ex)
			{
				this.Core.LogManager.AddLog(LogType.ERROR, "exception occurred in FileList : {0}", new object[]
				{
					ex
				});
			};
			this.LoadSettingFile();
			this.RefreshMachineInfo();
		}

		public void AddMachine(string hostName, List<string> op)
		{
			Machine machine = this.MachineDict.TryGetValue(hostName);
			if (machine != null)
			{
				machine.Operations = op;
				return;
			}
			try
			{
				MachineInfo machineInfo = (from info in this.Context.MachineInfo
				where info.Address == hostName
				select info).FirstOrDefault<MachineInfo>();
				if (machineInfo == null)
				{
					machineInfo = new MachineInfo
					{
						Address = hostName,
						Status = "",
						Info = ""
					};
					this.Context.MachineInfo.InsertOnSubmit(machineInfo);
				}
				machine = new Machine(this, hostName, op, machineInfo);
				this.MachineDict.Add(hostName, machine);
			}
			catch (Exception ex)
			{
				this.Core.LogManager.AddLog(LogType.ERROR, "Error occured while Inserting Machine Status : {0}", new object[]
				{
					ex.Message
				});
			}
		}

		public void RemoveMachine(string hostName)
		{
			Machine machine = this.MachineDict.TryGetValue(hostName);
			try
			{
				if (machine != null)
				{
					this.Context.MachineInfo.DeleteOnSubmit(machine.MachineInfo);
					this.MachineDict.Remove(hostName);
				}
				else
				{
					MachineInfo machineInfo = (from info in this.Context.MachineInfo
					where info.Address == hostName
					select info).FirstOrDefault<MachineInfo>();
					if (machineInfo != null)
					{
						this.Context.MachineInfo.DeleteOnSubmit(machineInfo);
					}
				}
			}
			catch (Exception ex)
			{
				this.Core.LogManager.AddLog(LogType.ERROR, "Error occured while Remove Machine Status : {0}", new object[]
				{
					ex.Message
				});
			}
		}

		public void LoadSettingFile()
		{
			Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
			try
			{
				using (StreamReader streamReader = File.OpenText("setting.txt"))
				{
					string text;
					while ((text = streamReader.ReadLine()) != null)
					{
						string[] array = text.Split(new char[]
						{
							','
						});
						if (array.Length == 2)
						{
							string key = array[0].Trim().ToUpper();
							string item = array[1].Trim();
							if (!dictionary.ContainsKey(key))
							{
								dictionary.Add(key, new List<string>());
							}
							dictionary[key].Add(item);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.Core.LogManager.AddLog(LogType.ERROR, string.Format("Error occurred while loading setting.txt : {0}", ex.Message), new object[0]);
			}
			foreach (Machine machine in this.MachineDict.Values.ToList<Machine>())
			{
				string hostName = machine.HostName;
				if (dictionary.ContainsKey(hostName))
				{
					List<string> operations = dictionary[hostName];
					machine.Operations = operations;
					dictionary.Remove(hostName);
				}
				else if (machine.TcpClient == null)
				{
					this.RemoveMachine(hostName);
				}
				else
				{
					machine.TcpClient.Disconnect();
					this.RemoveMachine(hostName);
				}
			}
			foreach (KeyValuePair<string, List<string>> keyValuePair in dictionary)
			{
				this.AddMachine(keyValuePair.Key, keyValuePair.Value);
			}
			this.Form.ListMachine.Items.Clear();
			foreach (Machine value in from x in this.MachineDict.Values
			orderby x
			select x)
			{
				this.Form.ListMachine.Items.Add(value);
			}
		}

		public void RefreshMachineInfo()
		{
			this.Core.JobProcessor.Enqueue(Job.Create(delegate
			{
				this.Form.Invoke(new Action(delegate
				{
					Machine selectedMachine = this.Form.SelectedMachine;
					this.Form.ListOperation.Items.Clear();
					if (selectedMachine == null)
					{
						this.Form.TextMachineInfo.Text = "";
					}
					else
					{
						foreach (string item in selectedMachine.Operations)
						{
							this.Form.ListOperation.Items.Add(item);
						}
						this.Form.TextMachineInfo.Text = selectedMachine.Info;
					}
					this.Form.ButtonStartServer.Enabled = this.IsKilled;
					this.Form.ButtonUpdateServer.Enabled = this.IsKilled;
					this.Form.ButtonKillServer.Enabled = !this.IsKilled;
					int num = 0;
					int num2 = 0;
					int num3 = 0;
					foreach (Machine machine in this.MachineDict.Values)
					{
						if (machine.IsConnected)
						{
							num++;
						}
						num2 += machine.ProcessCount;
						num3 += machine.Operations.Count;
					}
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendFormat("{0} / {1} Machine(s)", num, this.MachineDict.Count).AppendLine();
					stringBuilder.AppendFormat("{0} / {1} Service(s)", num2, num3).AppendLine();
					stringBuilder.AppendFormat("Bin Version : {0}", this.LatestServerFileTime).AppendLine();
					stringBuilder.AppendFormat("DS Version : {0}", this.LatestClientFileTime).AppendLine();
					stringBuilder.AppendFormat("Updated : {0}", this.IsUpdated).AppendLine();
					this.Form.TextMachineStatus.Text = stringBuilder.ToString();
				}));
			}));
		}

		public void SaveMachineStatus()
		{
			try
			{
				this.Context.SubmitChanges();
			}
			catch (Exception ex)
			{
				this.Core.LogManager.AddLog(LogType.ERROR, "Error occured while saving Machine Status : {0}", new object[]
				{
					ex.Message
				});
			}
		}

		public void QuerySelectedMachineInfo()
		{
			this.Form.Invoke(new Action(delegate
			{
				Machine selectedMachine = this.Form.SelectedMachine;
				if (selectedMachine != null && DateTime.Now - selectedMachine.LastReportTime > TimeSpan.FromMilliseconds(3000.0))
				{
					selectedMachine.QueryClientReport();
				}
			}));
		}

		public void WaitRequest(MachineRequest req)
		{
			this.RequestedCommand = req;
			this.RequestedMachines = new HashSet<string>(this.MachineDict.Keys);
		}

		public void CheckRequestCompleted(Machine machine)
		{
			if (this.RequestedCommand == MachineRequest.None)
			{
				return;
			}
			if (machine.IsUpdated && this.RequestedCommand == MachineRequest.Update)
			{
				this.RequestedMachines.Remove(machine.HostName);
			}
			if (machine.IsStarted && this.RequestedCommand == MachineRequest.Start)
			{
				this.RequestedMachines.Remove(machine.HostName);
			}
			if (machine.IsKilled && this.RequestedCommand == MachineRequest.Kill)
			{
				this.RequestedMachines.Remove(machine.HostName);
			}
			if (this.RequestedMachines.Count == 0)
			{
				this.Core.LogManager.AddLog(LogType.INFO, "=== {0} Server Completed ====", new object[]
				{
					this.RequestedCommand
				});
				this.RequestedCommand = MachineRequest.None;
			}
		}

		public void EndServer()
		{
			foreach (Machine machine in this.MachineDict.Values)
			{
				machine.ExecuteFile("KillServer.bat", new object[0]);
			}
			this.Core.LogManager.AddLog(LogType.INFO, "Ending Servers...", new object[0]);
			this.WaitRequest(MachineRequest.Kill);
		}

		public bool UpdateServer()
		{
			if (!this.IsKilled)
			{
				this.Core.LogManager.AddLog(LogType.ERROR, "Cannot Update Server while Server is Running !", new object[0]);
				return false;
			}
			foreach (Machine machine in this.MachineDict.Values)
			{
				if (machine.IsDSMachine)
				{
					machine.ExecuteFile("UpdateServer.bat", new object[]
					{
						1,
						Settings.Default.ServerBin,
						Settings.Default.DSBin
					});
				}
				else
				{
					machine.ExecuteFile("UpdateServer.bat", new object[]
					{
						0,
						Settings.Default.ServerBin,
						Settings.Default.DSBin
					});
				}
			}
			this.Core.LogManager.AddLog(LogType.INFO, "Updating Servers...", new object[0]);
			this.WaitRequest(MachineRequest.Update);
			return true;
		}

		public bool GatherLog()
		{
			if (this.IsKilled)
			{
				foreach (Machine machine in this.MachineDict.Values)
				{
					machine.ExecuteFile("GatherLog.bat", new object[]
					{
						0,
						Settings.Default.LogDir
					});
				}
				this.Core.LogManager.AddLog(LogType.INFO, "Move Logs to control", new object[0]);
			}
			else
			{
				foreach (Machine machine2 in this.MachineDict.Values)
				{
					machine2.ExecuteFile("GatherLog.bat", new object[]
					{
						1,
						Settings.Default.LogDir
					});
				}
				this.Core.LogManager.AddLog(LogType.INFO, "Copy Logs to control", new object[0]);
			}
			return true;
		}

		public void RunConsole(string cmd)
		{
			foreach (Machine machine in this.MachineDict.Values)
			{
				machine.ExecuteConsole(cmd);
			}
			this.Core.LogManager.AddLog(LogType.INFO, "Run Console : {0}", new object[]
			{
				cmd
			});
		}

		public void StartServer()
		{
			if (!this.IsKilled)
			{
				this.Core.LogManager.AddLog(LogType.ERROR, "Cannot Start Server while Server is Already Running !", new object[0]);
				return;
			}
			foreach (Machine machine in this.MachineDict.Values)
			{
				machine.ExecuteServices();
			}
			this.Core.LogManager.AddLog(LogType.INFO, "Starting Servers...", new object[0]);
			this.WaitRequest(MachineRequest.Start);
		}
	}
}
