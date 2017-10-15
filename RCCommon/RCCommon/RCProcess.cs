using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Devcat.Core;

namespace RemoteControlSystem
{
	[Guid("F149D367-4F79-4420-89DA-30B4FAE06D93")]
	[Serializable]
	public class RCProcess : IComparable
	{
		public List<int> ChildProcesses
		{
			get
			{
				if (this.childLog != null)
				{
					return this.childLog.PIDs;
				}
				return new List<int>();
			}
		}

		public RCProcess.ChildProcessLogs GetChildLog(int pid)
		{
			if (this.childLog != null)
			{
				return this.childLog.GetLog(pid);
			}
			return null;
		}

		[field: NonSerialized]
		public event RCProcess.LogEventHandler Log;

		[field: NonSerialized]
		public event RCProcess.LogEventHandler SysCommand;

		[field: NonSerialized]
		public event RCProcess.StateChangeEventHandler StateChange;

		[field: NonSerialized]
		public event EventHandler PerformanceUpdate;

		public string Name { get; private set; }

		public string Type { get; private set; }

		public string Description { get; private set; }

		public string WorkingDirectory { get; private set; }

		public string ExecuteName { get; private set; }

		public string ExecuteArgs { get; private set; }

		public string ShutdownString { get; private set; }

		public string CustomCommandString { get; private set; }

		public int LogLines { get; private set; }

		public string BootedString { get; private set; }

		public string PerformanceString { get; private set; }

		public string PerformanceDescription { get; private set; }

		public bool DefaultSelect { get; private set; }

		public bool AutomaticStart { get; private set; }

		public bool AutomaticRestart { get; private set; }

		public string UpdateExecuteName { get; private set; }

		public string UpdateExecuteArgs { get; private set; }

		public bool TraceChildProcess { get; private set; }

		public string ChildProcessLogStr { get; private set; }

		public int MaxChildProcessCount { get; private set; }

		public bool CheckPerformance { get; private set; }

		public RCProcess.SPerformance Performance { get; private set; }

		public RCProcess.ProcessState State { get; private set; }

		public DateTime LastPerformanceLogTime { get; private set; }

		public RCClient RCClient
		{
			get
			{
				return this._rcClient;
			}
		}

		public ICollection<RCProcessScheduler> Schedules
		{
			get
			{
				return this._scheduleList;
			}
		}

		public IDictionary<string, string> StaticProperties
		{
			get
			{
				return this.staticProperties;
			}
		}

		public IDictionary<string, string> Properties
		{
			get
			{
				return this.dynamicProperties;
			}
		}

		public override string ToString()
		{
			return this.Name;
		}

		public static void MakeProcess(ProcessStartInfo startInfo, string exec, string args)
		{
			if (exec.ToLower().EndsWith(".ps1"))
			{
				startInfo.FileName = "PSConsole.exe";
				startInfo.Arguments = string.Format("{0}\\{1} {2}", startInfo.WorkingDirectory, exec, args);
				return;
			}
			startInfo.FileName = startInfo.WorkingDirectory + "\\" + exec;
			startInfo.Arguments = args;
		}

		internal void SetProfilingEnvironment(ref ProcessStartInfo startInfo)
		{
			foreach (string text in this.Properties.Keys)
			{
				if (text.ToLower().StartsWith("env_"))
				{
					string key = text.Remove(0, 4);
					startInfo.EnvironmentVariables[key] = this.Properties[text];
				}
			}
			if (this.Properties.ContainsKey("ENV_Cor_Enable_Profiling") && string.Compare(this.Properties["ENV_Cor_Enable_Profiling"], "1", false) == 0)
			{
				startInfo.EnvironmentVariables["COR_PROFILER"] = "{8C29BC4E-1F57-461a-9B51-1200C32E6F1F}";
				startInfo.EnvironmentVariables["OMV_USAGE"] = "objects";
				startInfo.EnvironmentVariables["OMV_SKIP"] = "0";
				startInfo.EnvironmentVariables["OMV_STACK"] = "1";
				startInfo.EnvironmentVariables["OMV_FORMAT"] = "v2";
				startInfo.EnvironmentVariables["OMV_DynamicObjectTracking"] = "0x0";
				startInfo.EnvironmentVariables["OMV_INITIAL_SETTING"] = "1";
				startInfo.EnvironmentVariables["OMV_TargetCLRVersion"] = "v4";
				startInfo.EnvironmentVariables["OMV_LOGFILE_UKEY"] = string.Format("{0}_{1}", this.Name, DateTime.Now.ToString("yyyy-MM-dd_H-mm-ss"));
			}
		}

		internal void Start()
		{
			lock (this)
			{
				if (this.State == RCProcess.ProcessState.Off || this.State == RCProcess.ProcessState.Crash)
				{
					this.ChangeState(RCProcess.ProcessState.Booting);
					this._isWritingStandardIn = false;
					this._stdInQueue = new Queue();
					this._standardOutputIdx = 0;
					this._runProcess = new Process();
					this.cpuUsage = new CpuUsage();
					ProcessStartInfo processStartInfo = new ProcessStartInfo();
					processStartInfo.WorkingDirectory = BaseConfiguration.WorkingDirectory + "\\" + this.WorkingDirectory;
					this.SetProfilingEnvironment(ref processStartInfo);
					RCProcess.MakeProcess(processStartInfo, this.ExecuteName, this.ExecuteArgs);
					processStartInfo.RedirectStandardInput = true;
					processStartInfo.RedirectStandardOutput = true;
					processStartInfo.UseShellExecute = false;
					this._runProcess.EnableRaisingEvents = true;
					this._runProcess.Exited += this.Exited;
					this._runProcess.StartInfo = processStartInfo;
					try
					{
						this._runProcess.Start();
					}
					catch (Exception ex)
					{
						Log<RCClient>.Logger.ErrorFormat("[{0}] - Exception in Start() - {1}", this.Name, ex.Message);
						this.AddExceptionLog("Exception in Start() - " + ex.Message);
						this.ChangeState(RCProcess.ProcessState.Crash);
						this._runProcess = null;
						GC.Collect();
						return;
					}
					if (this.BootedString.Length == 0)
					{
						this.ChangeState(RCProcess.ProcessState.On);
					}
					try
					{
						this._standardOutputAsyncResult = this._runProcess.StandardOutput.BaseStream.BeginRead(this._standardOutputBuffer, 0, this._standardOutputBuffer.Length, new AsyncCallback(this.StandardOutputAsync), null);
					}
					catch (Exception ex2)
					{
						Log<RCClient>.Logger.ErrorFormat("[{0}] - Exception in Start() 2 - {1}", this.Name, ex2.Message);
						this.AddExceptionLog("Exception in Start() 2 - " + ex2.Message);
					}
				}
			}
		}

		internal void Stop()
		{
			lock (this)
			{
				if (this.State == RCProcess.ProcessState.On)
				{
					if (this.ShutdownString.Length == 0)
					{
						this.Kill();
					}
					else
					{
						this.StandardIn(this.ShutdownString);
					}
				}
			}
		}

		internal void Kill()
		{
			lock (this)
			{
				if (this.State == RCProcess.ProcessState.Booting || this.State == RCProcess.ProcessState.On || this.State == RCProcess.ProcessState.Closing || this.State == RCProcess.ProcessState.Freezing)
				{
					try
					{
						this.ChangeState(RCProcess.ProcessState.Closing);
						this._runProcess.Kill();
					}
					catch (InvalidOperationException ex)
					{
						Log<RCClient>.Logger.ErrorFormat("[{0}] - InvalidOperationException in Kill() - {1}", this.Name, ex.Message);
						this.AddExceptionLog("InvalidOperationException in Kill() - " + ex.Message);
					}
					catch (Exception ex2)
					{
						Log<RCClient>.Logger.ErrorFormat("[{0}] - Exception in Kill() - {1}", this.Name, ex2.Message);
						this.AddExceptionLog("Exception in Kill() - " + ex2.Message);
					}
				}
			}
		}

		internal void StartUpdate()
		{
			lock (this)
			{
				if ((this.State == RCProcess.ProcessState.Off || this.State == RCProcess.ProcessState.Crash) && this.UpdateExecuteName.Length != 0)
				{
					this.ChangeState(RCProcess.ProcessState.Updating);
					this._isWritingStandardIn = false;
					this._stdInQueue = new Queue();
					this._standardOutputIdx = 0;
					this._runProcess = new Process();
					ProcessStartInfo processStartInfo = new ProcessStartInfo();
					processStartInfo.WorkingDirectory = BaseConfiguration.WorkingDirectory;
					RCProcess.MakeProcess(processStartInfo, this.UpdateExecuteName, this.UpdateExecuteArgs);
					processStartInfo.RedirectStandardInput = false;
					processStartInfo.RedirectStandardOutput = true;
					processStartInfo.UseShellExecute = false;
					this._runProcess.EnableRaisingEvents = true;
					this._runProcess.Exited += this.Exited;
					this._runProcess.StartInfo = processStartInfo;
					try
					{
						this._runProcess.Start();
					}
					catch (Exception ex)
					{
						Log<RCClient>.Logger.ErrorFormat("[{0}] - Exception in StartUpdate() - {1}", this.Name, ex.Message);
						this.AddExceptionLog("Exception in StartUpdate() - " + ex.Message);
						this.ChangeState(RCProcess.ProcessState.Off);
						this._runProcess = null;
						GC.Collect();
						return;
					}
					try
					{
						this._standardOutputAsyncResult = this._runProcess.StandardOutput.BaseStream.BeginRead(this._standardOutputBuffer, 0, this._standardOutputBuffer.Length, new AsyncCallback(this.StandardOutputAsync), null);
					}
					catch (Exception ex2)
					{
						Log<RCClient>.Logger.ErrorFormat("[{0}] - Exception in StartUpdate() 2 - {1}", this.Name, ex2.Message);
						this.AddExceptionLog("Exception in StartUpdate() 2 - " + ex2.Message);
					}
				}
			}
		}

		internal void KillUpdate()
		{
			lock (this)
			{
				if (this.State == RCProcess.ProcessState.Updating)
				{
					try
					{
						this._runProcess.Kill();
					}
					catch (InvalidOperationException ex)
					{
						Log<RCClient>.Logger.ErrorFormat("[{0}] - InvalidOperationException in KillUpdate() - {1}", this.Name, ex.Message);
						this.AddExceptionLog("InvalidOperationException in KillUpdate() - " + ex.Message);
					}
					catch (Exception ex2)
					{
						Log<RCClient>.Logger.ErrorFormat("[{0}] - Exception in KillUpdate() - {1}", this.Name, ex2.Message);
						this.AddExceptionLog("Exception in KillUpdate() - " + ex2.Message);
					}
				}
			}
		}

		private void Exited(object sender, EventArgs args)
		{
			lock (this)
			{
				for (;;)
				{
					int num = 0;
					try
					{
						if (this._standardOutputAsyncResult != null && !this._standardOutputAsyncResult.IsCompleted)
						{
							num = this._runProcess.StandardOutput.BaseStream.EndRead(this._standardOutputAsyncResult);
						}
					}
					catch (InvalidOperationException)
					{
						break;
					}
					catch (Exception ex)
					{
						Log<RCClient>.Logger.ErrorFormat("[{0}] - Exception in Exited() - {1}", this.Name, ex.Message);
						this.AddExceptionLog("Exception in Exited() - " + ex.Message);
						this._standardOutputIdx = 0;
					}
					if (num != 0)
					{
						this.ProcessStandardOutputBufferJob(num + this._standardOutputIdx);
						try
						{
							this._standardOutputAsyncResult = this._runProcess.StandardOutput.BaseStream.BeginRead(this._standardOutputBuffer, this._standardOutputIdx, this._standardOutputBuffer.Length - this._standardOutputIdx, null, null);
							continue;
						}
						catch (Exception ex2)
						{
							Log<RCClient>.Logger.ErrorFormat("[{0}] - Exception in Exited() 2 - {1}", this.Name, ex2.Message);
							this.AddExceptionLog("Exception in Exited() 2 - " + ex2.Message);
						}
						break;
					}
					break;
				}
				if (this._runProcess.StartInfo.RedirectStandardInput)
				{
					try
					{
						this._runProcess.StandardInput.Close();
					}
					catch (Exception ex3)
					{
						Log<RCClient>.Logger.ErrorFormat("[{0}] - Exception in Exited() 3 - {1}", this.Name, ex3.Message);
						this.AddExceptionLog("Exception in Exited() 3 - " + ex3.Message);
					}
				}
				this._standardOutputIdx = 0;
				this._stdInQueue = null;
				this.AddExceptionLog(string.Format("[EXITCODE:{0}]", this._runProcess.ExitCode));
				this._runProcess = null;
				if (this.childLog != null)
				{
					this.childLog.Clear();
				}
				GC.Collect();
				this.CustomCommandString = this.GetOriginalCustomCommandString();
				this.sysCommandString = null;
				if (this.State == RCProcess.ProcessState.Booting)
				{
					this.AddExceptionLog("Terminated while booting! Cannot continue!");
					this.ChangeState(RCProcess.ProcessState.Crash);
				}
				else if (this.State != RCProcess.ProcessState.Closing && this.State != RCProcess.ProcessState.Updating)
				{
					this.AddExceptionLog("Abnormaly Terminated.");
					this.ChangeState(RCProcess.ProcessState.Crash);
					if (this.AutomaticRestart)
					{
						this.Start();
					}
				}
				else
				{
					this.ChangeState(RCProcess.ProcessState.Off);
				}
			}
		}

		public void ChangeState(RCProcess.ProcessState newState)
		{
			RCProcess.ProcessState state = this.State;
			this.State = newState;
			if (this.StateChange != null)
			{
				this.StateChange(this, new RCProcess.StateChangeEventArgs(this, DateTime.Now, state));
			}
			if (newState == RCProcess.ProcessState.On)
			{
				this.LastPerformanceLogTime = DateTime.Now;
			}
		}

		public void RunScheduler()
		{
			foreach (RCProcessScheduler rcprocessScheduler in this.Schedules)
			{
				rcprocessScheduler.Run(this);
			}
		}

		internal void StandardIn(string command)
		{
			lock (this)
			{
				if (this.State == RCProcess.ProcessState.On)
				{
					if (command == this.ShutdownString)
					{
						this.ChangeState(RCProcess.ProcessState.Closing);
					}
					if (!command.EndsWith("\n"))
					{
						command += "\r\n";
					}
					if (this._isWritingStandardIn)
					{
						if (this._stdInQueue.Count > 100)
						{
							this.AddExceptionLog("Failed to write to stdin");
						}
						else
						{
							this._stdInQueue.Enqueue(command);
						}
					}
					else
					{
						this._isWritingStandardIn = true;
						try
						{
							byte[] bytes = Encoding.Default.GetBytes(command);
							this._runProcess.StandardInput.BaseStream.BeginWrite(bytes, 0, bytes.Length, new AsyncCallback(this.StandardInAsync), command);
						}
						catch (Exception ex)
						{
							this._isWritingStandardIn = false;
							Log<RCClient>.Logger.ErrorFormat("[{0}] - Exception in StandardIn() - {1}", this.Name, ex.Message);
							this.AddExceptionLog("Exception in StandardIn() - " + ex.Message);
						}
					}
				}
			}
		}

		private void StandardInAsync(IAsyncResult result)
		{
			lock (this)
			{
				this._isWritingStandardIn = false;
				if (this._runProcess != null)
				{
					try
					{
						this._runProcess.StandardInput.BaseStream.EndWrite(result);
						this._runProcess.StandardInput.BaseStream.Flush();
						string text = (string)result.AsyncState;
						this.AddStandardInputLog(text.Substring(0, text.Length - 2));
					}
					catch (Exception ex)
					{
						Log<RCClient>.Logger.ErrorFormat("[{0}] - Exception in StandardInAsync() - {1}", this.Name, ex.Message);
						this.AddExceptionLog("Exception in StandardInAsync() - " + ex.Message);
					}
					if (this._stdInQueue.Count > 0)
					{
						this._isWritingStandardIn = true;
						try
						{
							string text2 = (string)this._stdInQueue.Dequeue();
							byte[] bytes = Encoding.Default.GetBytes(text2);
							this._runProcess.StandardInput.BaseStream.BeginWrite(bytes, 0, bytes.Length, new AsyncCallback(this.StandardInAsync), text2);
						}
						catch (Exception ex2)
						{
							Log<RCClient>.Logger.ErrorFormat("[{0}] - Exception in StandardInAsync() 2 - {1}", this.Name, ex2.Message);
							this.AddExceptionLog("Exception in StandardInAsync() 2 -  " + ex2.Message);
						}
					}
				}
			}
		}

		private void StandardOutputAsync(IAsyncResult result)
		{
			lock (this)
			{
				if (this._runProcess != null)
				{
					int num = 0;
					try
					{
						if (this._standardOutputAsyncResult != null)
						{
							num = this._runProcess.StandardOutput.BaseStream.EndRead(this._standardOutputAsyncResult);
						}
					}
					catch (InvalidOperationException ex)
					{
						Log<RCClient>.Logger.ErrorFormat("[{0}] - InvalidOperationException in StandardOutput() - {1}", this.Name, ex.Message);
						this.AddExceptionLog("InvalidOperationException in StandardOutput() - " + ex.Message);
						return;
					}
					catch (Exception ex2)
					{
						Log<RCClient>.Logger.ErrorFormat("[{0}] - Exception in StandardOutput() - {1}", this.Name, ex2.Message);
						this.AddExceptionLog("Exception in StandardOutput() - " + ex2.Message);
						this._standardOutputIdx = 0;
					}
					this._standardOutputAsyncResult = null;
					if (num != 0)
					{
						this.ProcessStandardOutputBufferJob(num + this._standardOutputIdx);
						try
						{
							this._standardOutputAsyncResult = this._runProcess.StandardOutput.BaseStream.BeginRead(this._standardOutputBuffer, this._standardOutputIdx, this._standardOutputBuffer.Length - this._standardOutputIdx, new AsyncCallback(this.StandardOutputAsync), null);
						}
						catch (Exception ex3)
						{
							this._standardOutputAsyncResult = null;
							Log<RCClient>.Logger.ErrorFormat("[{0}] - Exception in StandardOutput() 2 - {1}", this.Name, ex3.Message);
							this.AddExceptionLog("Exception in StandardOutput() 2 - " + ex3.Message);
						}
					}
				}
			}
		}

		private void ProcessStandardOutputBufferJob(int totalRead)
		{
			if (totalRead == 0)
			{
				return;
			}
			try
			{
				char[] array = new char[this._standardOutputBuffer.Length];
				int num = 0;
				for (int i = 0; i < totalRead; i++)
				{
					if (this._standardOutputBuffer[i] == 10)
					{
						int num2;
						if (i > 0 && this._standardOutputBuffer[i - 1] == 13)
						{
							num2 = i - 1;
						}
						else
						{
							num2 = i;
						}
						int chars = Encoding.Default.GetChars(this._standardOutputBuffer, num, num2 - num, array, 0);
						string text = new string(array, 0, chars);
						if (this.State == RCProcess.ProcessState.Booting && this.BootedString == text)
						{
							this.ChangeState(RCProcess.ProcessState.On);
						}
						if (text.StartsWith("$"))
						{
							this.AddSysCommandLog(text);
							if (this.SysCommand != null)
							{
								this.SysCommand(this, new RCProcess.LogEventArgs(this.Name, text));
							}
						}
						else if (text.StartsWith("!"))
						{
							this.AddNotifyLog(text);
						}
						else if (this.childLog == null || !this.childLog.ParseLog(text))
						{
							this.AddStandardOutputLog(text);
						}
						num = i + 1;
					}
				}
				if (num == 0 && totalRead == this._standardOutputBuffer.Length)
				{
					byte[] array2 = new byte[this._standardOutputBuffer.Length * 2];
					this._standardOutputBuffer.CopyTo(array2, 0);
					this._standardOutputBuffer = array2;
				}
				this._standardOutputIdx = totalRead - num;
				if (this._standardOutputIdx > 0 && num > 0)
				{
					Array.Copy(this._standardOutputBuffer, num, this._standardOutputBuffer, 0, this._standardOutputIdx);
				}
			}
			catch (Exception ex)
			{
				Log<RCClient>.Logger.ErrorFormat("[{0}] - Exception in ProcessStandardOutputBufferJob() - {1}", this.Name, ex.ToString());
				this.AddExceptionLog("Exception in ProcessStandardOutputBufferJob() - " + ex.Message);
				this._standardOutputIdx = 0;
			}
		}

		private void AddStandardInputLog(string message)
		{
			string message2 = DateTime.Now.ToString("MM-dd HH:mm:ss") + " < " + message;
			this.AddLogManual(message2);
		}

		private void AddStandardOutputLog(string message)
		{
			string message2 = DateTime.Now.ToString("MM-dd HH:mm:ss") + " > " + message;
			this.AddLogManual(message2);
		}

		private void AddExceptionLog(string message)
		{
			string message2 = DateTime.Now.ToString("MM-dd HH:mm:ss") + " # " + message;
			this.AddLogManual(message2);
		}

		private void AddSysCommandLog(string message)
		{
			string message2 = DateTime.Now.ToString("MM-dd HH:mm:ss") + " " + message;
			this.AddLogManual(message2);
		}

		private void AddNotifyLog(string message)
		{
			string message2 = DateTime.Now.ToString("MM-dd HH:mm:ss") + " " + message;
			this.AddLogManual(message2);
		}

        internal void AddLogManual(string message)
        {
            List<string> log = _log;
            lock (log)
            {
                if (PerformanceString.Length != 0 && RCProcess.IsStandardOutputLog(message) && RCProcess.GetOriginalLog(message).StartsWith(PerformanceString))
                {
                    LastPerformanceLogTime = DateTime.Now;
                }
                else
                {
                    while (_log.Count >= LogLines)
                    {
                        _log.RemoveAt(0);
                    }
                    _log.Add(message);
                }
            }
            if (Log != null)
            {
                Log(this, new RCProcess.LogEventArgs(Name, message));
            }
        }

		public string[] GetLog()
		{
			string[] result;
			lock (this._log)
			{
				string[] array = new string[this._log.Count];
				this._log.CopyTo(array, 0);
				result = array;
			}
			return result;
		}

		private void AddProcessLog(string pid, string log)
		{
		}

		public void UpdatePerformance(long privateBytes, long virtualBytes, float cpu)
		{
			this.Performance = new RCProcess.SPerformance
			{
				PrivateMemorySize = privateBytes,
				VirtualMemorySize = virtualBytes,
				CPU = cpu
			};
			if (this.PerformanceUpdate != null)
			{
				this.PerformanceUpdate(this, EventArgs.Empty);
			}
		}

		public bool CanQueryPerformance()
		{
			return this._runProcess != null && this.CheckPerformance && (this.State == RCProcess.ProcessState.On || this.State == RCProcess.ProcessState.Freezing);
		}

		public bool QueryPerformance()
		{
			if (this.CanQueryPerformance())
			{
				this._runProcess.Refresh();
				long privateMemorySize = this._runProcess.PrivateMemorySize64;
				long virtualMemorySize = this._runProcess.VirtualMemorySize64;
				long ticks = this._runProcess.TotalProcessorTime.Ticks;
				float num = 0f;
				try
				{
					float num2;
					float num3;
					num = this.cpuUsage.CalcUsage(this._runProcess, out num2, out num3);
				}
				catch (OverflowException)
				{
					num = this.Performance.CPU;
				}
				if (Math.Abs(this.Performance.PrivateMemorySize - privateMemorySize) > 524288L || Math.Abs(this.Performance.VirtualMemorySize - virtualMemorySize) > 524288L || Math.Abs(this.Performance.CPU - num) > 0.01f)
				{
					this.UpdatePerformance(privateMemorySize, virtualMemorySize, num);
					return true;
				}
			}
			return false;
		}

		private bool IsStaticProperty(string key)
		{
			return this.staticProperties.ContainsKey(key);
		}

		internal void AddSysCommand(string right)
		{
			if (!string.IsNullOrEmpty(this.sysCommandString))
			{
				this.sysCommandString = this.sysCommandString + "||" + right;
			}
			else
			{
				this.sysCommandString = right;
			}
			if (!string.IsNullOrEmpty(this.CustomCommandString))
			{
				this.CustomCommandString = this.CustomCommandString + "||" + right;
				return;
			}
			this.CustomCommandString = right;
		}

		public bool IsPerformanceLog(string _log)
		{
			return RCProcess.GetOriginalLog(_log).StartsWith(this.PerformanceString);
		}

		public string GetPerformanceLog(string _log)
		{
			return RCProcess.GetOriginalLog(_log).Substring(this.PerformanceString.Length).Trim();
		}

		public static DateTime GetLogTime(string message)
		{
			return DateTime.Parse(message.Substring(0, 14));
		}

		public static bool IsSystemLog(string message)
		{
			return message[15] == '#';
		}

		public static bool IsStandardOutputLog(string message)
		{
			return message[15] == '>';
		}

		public static bool IsStandardInputLog(string message)
		{
			return message[15] == '<';
		}

		public static bool IsNotifyLog(string message)
		{
			return message[15] == '!';
		}

		public static string GetOriginalLog(string message)
		{
			return message.Substring(17);
		}

		public int CompareTo(object obj)
		{
			RCProcess rcprocess = obj as RCProcess;
			if (rcprocess == null)
			{
				throw new ArgumentException("Argument must be typeof RCProcess");
			}
			return this.Name.CompareTo(rcprocess.Name);
		}

		private RCProcess()
		{
			this.State = RCProcess.ProcessState.Off;
			this._standardOutputBuffer = new byte[1024];
			this._standardOutputIdx = 0;
			this._runProcess = null;
			this._stdInQueue = null;
			this._rcClient = null;
			this.LastPerformanceLogTime = DateTime.Now;
			this._scheduleList = new RCProcessSchedulerCollection();
			this.staticProperties = new Dictionary<string, string>();
			this.dynamicProperties = new Dictionary<string, string>();
		}

		public RCProcess(string name, string type, string description, string workingDirectory, string executeName, string executeArgs, string shutdownString, string customCommandString, int logLines, string bootedString, bool checkPerformance, string performanceString, string performanceDescription, bool defaultSelect, bool automaticStart, bool automaticRestart, string updateExecuteName, string updateExecuteArgs, bool traceChildProcess, string childProcessLogStr, int maxChildProcessCount, RCProcessSchedulerCollection scheduleList, IEnumerable<KeyValuePair<string, string>> staticProperty) : this()
		{
			this.SetFromVariable(name, type, description, workingDirectory, executeName, executeArgs, shutdownString, customCommandString, bootedString, checkPerformance, performanceString, performanceDescription, logLines, defaultSelect, automaticStart, automaticRestart, updateExecuteName, updateExecuteArgs, traceChildProcess, childProcessLogStr, maxChildProcessCount, staticProperty);
			this.LastPerformanceLogTime = DateTime.Now;
			this.CopyScheduler(scheduleList);
		}

		public RCProcess(XmlReader reader) : this()
		{
			this.SetFromXmlInfo(reader);
		}

		internal void SetRCClient(RCClient client)
		{
			if (this._rcClient != null && client != null)
			{
				throw new InvalidOperationException("Cannot set parent client already set");
			}
			this._rcClient = client;
		}

		public RCProcess Clone()
		{
			RCProcess rcprocess = new RCProcess();
			rcprocess.SetFromVariable(this.Name, this.Type, this.Description, this.WorkingDirectory, this.ExecuteName, this.ExecuteArgs, this.ShutdownString, this.CustomCommandString, this.BootedString, this.CheckPerformance, this.PerformanceString, this.PerformanceDescription, this.LogLines, this.DefaultSelect, this.AutomaticStart, this.AutomaticRestart, this.UpdateExecuteName, this.UpdateExecuteArgs, this.TraceChildProcess, this.ChildProcessLogStr, this.MaxChildProcessCount, this.StaticProperties);
			rcprocess.State = this.State;
			rcprocess.CopyScheduler(this._scheduleList);
			rcprocess.CopyDynamicProperty(this.Properties);
			return rcprocess;
		}

		public void Modify(RCProcess newProcess)
		{
			if (this.Name != newProcess.Name)
			{
				throw new ArgumentException("Name must be same when modifying properties!");
			}
			this.SetFromVariable(this.Name, newProcess.Type, newProcess.Description, newProcess.WorkingDirectory, newProcess.ExecuteName, newProcess.ExecuteArgs, newProcess.ShutdownString, newProcess.CustomCommandString, newProcess.BootedString, newProcess.CheckPerformance, newProcess.PerformanceString, newProcess.PerformanceDescription, newProcess.LogLines, newProcess.DefaultSelect, newProcess.AutomaticStart, newProcess.AutomaticRestart, newProcess.UpdateExecuteName, newProcess.UpdateExecuteArgs, newProcess.TraceChildProcess, newProcess.ChildProcessLogStr, newProcess.MaxChildProcessCount, newProcess.StaticProperties);
			this.CopyScheduler(newProcess._scheduleList);
			this.CopyDynamicProperty(newProcess.Properties);
		}

		public void ModifyEx(RCProcess newProcess)
		{
			if (this.Name != newProcess.Name)
			{
				throw new ArgumentException("Name must be same when modifying properties!");
			}
			this.SetFromVariable(this.Name, newProcess.Type, this.Description, newProcess.WorkingDirectory, newProcess.ExecuteName, newProcess.ExecuteArgs, newProcess.ShutdownString, newProcess.CustomCommandString, newProcess.BootedString, newProcess.CheckPerformance, newProcess.PerformanceString, newProcess.PerformanceDescription, newProcess.LogLines, newProcess.DefaultSelect, newProcess.AutomaticStart, newProcess.AutomaticRestart, newProcess.UpdateExecuteName, newProcess.UpdateExecuteArgs, newProcess.TraceChildProcess, newProcess.ChildProcessLogStr, newProcess.MaxChildProcessCount, newProcess.StaticProperties);
			this.CopyScheduler(newProcess._scheduleList);
			this.CopyDynamicProperty(newProcess.Properties);
		}

		private void SetFromVariable(string name, string type, string description, string workingDirectory, string executeName, string executeArgs, string shutdownString, string customCommandString, string bootedString, bool checkPerformance, string performanceString, string performanceDescription, int logLines, bool defaultSelect, bool automaticStart, bool automaticRestart, string updateExecuteName, string updateExecuteArgs, bool traceChildProcess, string childProcessLogStr, int maxChildProcessCount, IEnumerable<KeyValuePair<string, string>> staticProperty)
		{
			if (type == null)
			{
				type = string.Empty;
			}
			if (description == null)
			{
				description = string.Empty;
			}
			if (executeArgs == null)
			{
				executeArgs = string.Empty;
			}
			if (shutdownString == null)
			{
				shutdownString = string.Empty;
			}
			if (customCommandString == null)
			{
				customCommandString = string.Empty;
			}
			if (bootedString == null)
			{
				bootedString = string.Empty;
			}
			if (performanceString == null)
			{
				performanceString = string.Empty;
			}
			if (performanceDescription == null)
			{
				performanceDescription = string.Empty;
			}
			if (updateExecuteName == null)
			{
				updateExecuteName = string.Empty;
			}
			if (updateExecuteArgs == null)
			{
				updateExecuteArgs = string.Empty;
			}
			if (name == null || workingDirectory == null || executeName == null || name.Length == 0 || workingDirectory.Length == 0 || executeName.Length == 0)
			{
				throw new ArgumentException("Name, Working Directory, Execute Name cannot be null or empty string");
			}
			if (workingDirectory.IndexOf("/") != -1)
			{
				throw new ArgumentException("Cannot use character '/' in working directory");
			}
			if (workingDirectory.StartsWith("\\") || workingDirectory.EndsWith("\\..") || workingDirectory.IndexOf("\\..\\") != -1)
			{
				throw new ArgumentException("Invalid working directory!");
			}
			if (logLines <= 10)
			{
				logLines = 10;
			}
			if (logLines > 1000)
			{
				logLines = 1000;
			}
			this.Name = name;
			this.Type = type;
			this.Description = description;
			this.WorkingDirectory = workingDirectory;
			this.ExecuteName = executeName;
			this.ExecuteArgs = executeArgs;
			this.ShutdownString = shutdownString;
			this.CustomCommandString = customCommandString;
			this.BootedString = bootedString;
			this.CheckPerformance = checkPerformance;
			this.PerformanceString = performanceString.Trim();
			this.PerformanceDescription = performanceDescription.Trim();
			if (this.LogLines < logLines)
			{
				List<string> log = this._log;
				this._log = new List<string>(logLines);
				if (log != null)
				{
					if (log.Count <= logLines)
					{
						this._log.AddRange(log);
					}
					else
					{
						this._log.AddRange(log.GetRange(log.Count - logLines, logLines));
					}
				}
			}
			this.LogLines = logLines;
			this.DefaultSelect = defaultSelect;
			this.AutomaticStart = automaticStart;
			this.AutomaticRestart = automaticRestart;
			this.UpdateExecuteName = updateExecuteName;
			this.UpdateExecuteArgs = updateExecuteArgs;
			this.TraceChildProcess = traceChildProcess;
			this.ChildProcessLogStr = childProcessLogStr;
			this.MaxChildProcessCount = maxChildProcessCount;
			if (this.MaxChildProcessCount == 0)
			{
				this.MaxChildProcessCount = 10;
			}
			this.childLog = null;
			if (this.TraceChildProcess && this.ChildProcessLogStr != null)
			{
				this.childLog = new RCProcess.ChildProcessLogCollection(this, this.MaxChildProcessCount, this.LogLines, this.ChildProcessLogStr);
			}
			foreach (KeyValuePair<string, string> item in this.staticProperties)
			{
				if (this.Properties.Contains(item))
				{
					this.Properties.Remove(item);
				}
			}
			this.staticProperties.Clear();
			if (staticProperty != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in staticProperty)
				{
					this.StaticProperties.Add(keyValuePair.Key, keyValuePair.Value);
					if (!this.Properties.ContainsKey(keyValuePair.Key))
					{
						this.Properties.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
		}

		internal void CopyScheduler(IEnumerable<RCProcessScheduler> source)
		{
			foreach (RCProcessScheduler rcprocessScheduler in this.Schedules)
			{
				rcprocessScheduler.Clear();
				this.StateChange -= rcprocessScheduler.EventHandler;
			}
			this._scheduleList.Clear();
			if (source != null)
			{
				foreach (RCProcessScheduler rcprocessScheduler2 in source)
				{
					this._scheduleList.Add(rcprocessScheduler2.Clone());
				}
			}
		}

		internal void CopyDynamicProperty(IEnumerable<KeyValuePair<string, string>> property)
		{
			foreach (KeyValuePair<string, string> keyValuePair in property)
			{
				if (!this.Properties.ContainsKey(keyValuePair.Key))
				{
					this.Properties.Add(keyValuePair.Key, keyValuePair.Value);
				}
				else
				{
					this.Properties[keyValuePair.Key] = keyValuePair.Value;
				}
			}
		}

		private string GetOriginalCustomCommandString()
		{
			if (!string.IsNullOrEmpty(this.sysCommandString))
			{
				string text = this.CustomCommandString;
				string[] array = this.sysCommandString.Split(new string[]
				{
					"||"
				}, StringSplitOptions.RemoveEmptyEntries);
				foreach (string oldValue in array)
				{
					text = text.Replace(oldValue, "");
					text = text.Trim(new char[]
					{
						'|'
					});
				}
				return text;
			}
			return this.CustomCommandString;
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Process");
			writer.WriteAttributeString("Name", this.Name);
			if (this.Type.Length > 0)
			{
				writer.WriteAttributeString("Type", this.Type);
			}
			writer.WriteAttributeString("Description", this.Description);
			writer.WriteAttributeString("WorkingDirectory", this.WorkingDirectory);
			writer.WriteAttributeString("ExecuteName", this.ExecuteName);
			writer.WriteAttributeString("ExecuteArgs", this.ExecuteArgs);
			writer.WriteAttributeString("ShutdownString", this.ShutdownString);
			writer.WriteAttributeString("CustomCommandString", this.GetOriginalCustomCommandString());
			writer.WriteAttributeString("BootedString", this.BootedString);
			writer.WriteAttributeString("CheckPerformance", this.CheckPerformance.ToString());
			writer.WriteAttributeString("PerformanceString", this.PerformanceString);
			writer.WriteAttributeString("PerformanceDescription", this.PerformanceDescription);
			writer.WriteAttributeString("LogLines", this.LogLines.ToString());
			writer.WriteAttributeString("DefaultSelect", this.DefaultSelect.ToString());
			writer.WriteAttributeString("AutomaticStart", this.AutomaticStart.ToString());
			writer.WriteAttributeString("AutomaticRestart", this.AutomaticRestart.ToString());
			writer.WriteAttributeString("UpdateExecuteName", this.UpdateExecuteName);
			writer.WriteAttributeString("UpdateExecuteArgs", this.UpdateExecuteArgs);
			writer.WriteAttributeString("TraceChildProcess", this.TraceChildProcess.ToString());
			writer.WriteAttributeString("ChildProcessLogStr", this.ChildProcessLogStr);
			writer.WriteAttributeString("MaxChildProcessCount", this.MaxChildProcessCount.ToString());
			foreach (RCProcessScheduler rcprocessScheduler in this.Schedules)
			{
				rcprocessScheduler.WriteXml(writer);
			}
			foreach (KeyValuePair<string, string> keyValuePair in this.StaticProperties)
			{
				writer.WriteStartElement("Property");
				writer.WriteAttributeString("Key", keyValuePair.Key);
				writer.WriteAttributeString("Value", keyValuePair.Value);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		private void SetFromXmlInfo(XmlReader reader)
		{
			if (reader.Name != "Process")
			{
				throw new ArgumentException("Reader is not node named \"Process\"");
			}
			string attribute = reader.GetAttribute("LogLines");
			string attribute2 = reader.GetAttribute("CheckPerformanace");
			string attribute3 = reader.GetAttribute("DefaultSelect");
			string attribute4 = reader.GetAttribute("AutomaticStart");
			string attribute5 = reader.GetAttribute("AutomaticRestart");
			string attribute6 = reader.GetAttribute("TraceChildProcess");
			string attribute7 = reader.GetAttribute("MaxChildProcessCount");
			this.SetFromVariable(reader.GetAttribute("Name"), reader.GetAttribute("Type"), reader.GetAttribute("Description"), reader.GetAttribute("WorkingDirectory"), reader.GetAttribute("ExecuteName"), reader.GetAttribute("ExecuteArgs"), reader.GetAttribute("ShutdownString"), reader.GetAttribute("CustomCommandString"), reader.GetAttribute("BootedString"), attribute2 == null || bool.Parse(attribute2), reader.GetAttribute("PerformanceString"), reader.GetAttribute("PerformanceDescription"), (attribute == null) ? 100 : int.Parse(attribute), attribute3 == null || bool.Parse(attribute3), attribute4 != null && bool.Parse(attribute4), attribute5 == null || bool.Parse(attribute5), reader.GetAttribute("UpdateExecuteName"), reader.GetAttribute("UpdateExecuteArgs"), attribute6 != null && bool.Parse(attribute6), reader.GetAttribute("ChildProcessLogStr"), (attribute7 == null) ? 10 : int.Parse(attribute7), null);
			if (!reader.IsEmptyElement)
			{
				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.EndElement)
					{
						return;
					}
					if (reader.NodeType == XmlNodeType.Element && reader.Name == "ProcessSchedule")
					{
						this.Schedules.Add(new RCProcessScheduler(reader));
					}
					else if (reader.NodeType == XmlNodeType.Element && reader.Name == "Property")
					{
						this.StaticProperties.Add(reader.GetAttribute("Key"), reader.GetAttribute("Value"));
						this.Properties.Add(reader.GetAttribute("Key"), reader.GetAttribute("Value"));
					}
				}
			}
		}

		private const string CommandDelimiter = "||";

		private const string CommandNameDelimiter = "|";

		private const long MemoryUpdateThreshold = 524288L;

		private const float CPUUpdateThreshold = 0.01f;

		public const string SysCommandDelimiter = "$";

		public const string NotifyLogDelimiter = "!";

		[NonSerialized]
		private RCProcess.ChildProcessLogCollection childLog;

		[NonSerialized]
		private bool _isWritingStandardIn;

		[NonSerialized]
		private Queue _stdInQueue;

		[NonSerialized]
		private byte[] _standardOutputBuffer;

		[NonSerialized]
		private int _standardOutputIdx;

		[NonSerialized]
		private IAsyncResult _standardOutputAsyncResult;

		[NonSerialized]
		private Process _runProcess;

		[NonSerialized]
		private CpuUsage cpuUsage;

		[NonSerialized]
		private RCClient _rcClient;

		[NonSerialized]
		private string sysCommandString;

		private List<string> _log;

		private RCProcessSchedulerCollection _scheduleList;

		private Dictionary<string, string> staticProperties;

		private Dictionary<string, string> dynamicProperties;

		//[NonSerialized]
		//private long oldCPUUsage;

		//[NonSerialized]
		//private long oldCpuCheckTime;

		[Guid("BA507230-6FE8-485c-8A36-BFB129399A8E")]
		[Serializable]
		public struct ChildProcessLog
		{
			public string Log { get; set; }

			public DateTime LogTime { get; set; }

			public override string ToString()
			{
				return this.LogTime.ToString("MM-dd HH:mm:ss") + " > " + this.Log;
			}
		}

		public class ChildProcessLogs
		{
			public string Parent { get; private set; }

			public int PID { get; private set; }

			public DateTime LastUpdate { get; private set; }

			public List<RCProcess.ChildProcessLog> LogLiness
			{
				get
				{
					List<RCProcess.ChildProcessLog> result;
					lock (this.logLines)
					{
						List<RCProcess.ChildProcessLog> list = new List<RCProcess.ChildProcessLog>(this.logLines);
						result = list;
					}
					return result;
				}
			}

			public bool CanConnect
			{
				get
				{
					return Process.GetProcessById(this.PID) != null;
				}
			}

			public event EventHandler<EventArgs<RCProcess.ChildProcessLog>> OnLog;

			public ChildProcessLogs(string _parent, int _pid, int _size)
			{
				this.Parent = _parent;
				this.PID = _pid;
				this.size = _size;
				this.logLines = new List<RCProcess.ChildProcessLog>(this.size);
			}

			public void AddLog(string _log)
			{
				RCProcess.ChildProcessLog childProcessLog = new RCProcess.ChildProcessLog
				{
					Log = _log,
					LogTime = DateTime.Now
				};
				lock (this.logLines)
				{
					while (this.logLines.Count >= this.size)
					{
						this.logLines.RemoveAt(0);
					}
					this.logLines.Add(childProcessLog);
				}
				if (this.OnLog != null)
				{
					this.OnLog(this, new EventArgs<RCProcess.ChildProcessLog>(childProcessLog));
				}
			}

			private int size;

			private List<RCProcess.ChildProcessLog> logLines;
		}

		private class ChildProcessLogCollection
		{
			public List<int> PIDs
			{
				get
				{
					List<int> result;
					lock (this.dict)
					{
						List<int> list = new List<int>(this.dict.Keys);
						result = list;
					}
					return result;
				}
			}

			public ChildProcessLogCollection(RCProcess _parent, int _size, int _logSize, string logFormat)
			{
				this.parent = _parent;
				this.size = _size;
				this.logSize = _logSize;
				this.dict = new Dictionary<int, RCProcess.ChildProcessLogs>();
				logFormat = Regex.Escape(logFormat.ToLower());
				if (logFormat.Contains("\\{pid}") && logFormat.Contains("\\{log}"))
				{
					logFormat = logFormat.Replace("\\{pid}", "(?<PID>[0-9]+)");
					logFormat = logFormat.Replace("\\{log}", "(?<LOG>.*)");
					this.regEx = new Regex(logFormat, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
				}
			}

			public void Clear()
			{
				lock (this.dict)
				{
					this.dict.Clear();
				}
			}

			public bool ParseLog(string _logLines)
			{
				Match match = this.regEx.Match(_logLines);
				if (match.Success)
				{
					int num = int.Parse(match.Groups["PID"].ToString());
					if (num != 0)
					{
						this.AddNewLog(num, match.Groups["LOG"].ToString());
						return true;
					}
				}
				return false;
			}

			public void AddNewLog(int pid, string log)
			{
				RCProcess.ChildProcessLogs childProcessLogs = null;
				lock (this.dict)
				{
					if (this.dict.ContainsKey(pid))
					{
						childProcessLogs = this.dict[pid];
					}
					else
					{
						if (this.dict.Count > this.size)
						{
							IOrderedEnumerable<KeyValuePair<int, RCProcess.ChildProcessLogs>> orderedEnumerable = from pair in this.dict
							orderby Process.GetProcessById(pair.Key) == null descending, pair.Value.LastUpdate descending
							select pair;
							foreach (KeyValuePair<int, RCProcess.ChildProcessLogs> keyValuePair in orderedEnumerable)
							{
								this.dict.Remove(keyValuePair.Key);
								if (this.dict.Count < this.size)
								{
									break;
								}
							}
						}
						childProcessLogs = new RCProcess.ChildProcessLogs(this.parent.Name, pid, this.logSize);
						this.dict.Add(pid, childProcessLogs);
					}
				}
				if (childProcessLogs != null)
				{
					childProcessLogs.AddLog(log);
				}
			}

			public RCProcess.ChildProcessLogs GetLog(int pid)
			{
				RCProcess.ChildProcessLogs result;
				lock (this.dict)
				{
					if (this.dict.ContainsKey(pid))
					{
						RCProcess.ChildProcessLogs childProcessLogs = this.dict[pid];
						result = childProcessLogs;
					}
					else
					{
						result = null;
					}
				}
				return result;
			}

			private int size;

			private int logSize;

			private RCProcess parent;

			private Dictionary<int, RCProcess.ChildProcessLogs> dict;

			private Regex regEx;
		}

		public delegate void LogEventHandler(RCProcess sender, RCProcess.LogEventArgs args);

		[Guid("8F6F8195-4295-46fe-A215-46CEADC24F99")]
		[Serializable]
		public class LogEventArgs
		{
			public string ProcessName { get; private set; }

			public string Message { get; private set; }

			internal LogEventArgs(string processName, string message)
			{
				this.ProcessName = processName;
				this.Message = message;
			}
		}

		public delegate void StateChangeEventHandler(RCProcess sender, RCProcess.StateChangeEventArgs args);

		[Guid("5C083F31-21CA-4cba-9A09-B85C96AFAF08")]
		[Serializable]
		public class StateChangeEventArgs
		{
			public string ProcessName { get; private set; }

			public DateTime ChangedTime { get; private set; }

			public RCProcess.ProcessState OldState { get; private set; }

			public RCProcess.ProcessState NewState { get; private set; }

			internal StateChangeEventArgs(RCProcess process, DateTime changedTime, RCProcess.ProcessState oldState)
			{
				this.ProcessName = process.Name;
				this.ChangedTime = changedTime;
				this.OldState = oldState;
				this.NewState = process.State;
			}
		}

		public enum ProcessState
		{
			Off,
			Updating,
			Booting,
			On,
			Closing,
			Freezing,
			Crash
		}

		[Guid("AEA82345-7158-47f4-BDFD-210A08E7502D")]
		[Serializable]
		public struct SPerformance
		{
			public long PrivateMemorySize;

			public long VirtualMemorySize;

			public float CPU;
		}

		public class CustomCommandParser
		{
			public string Name { get; private set; }

			public string Command { get; private set; }

			public string RawCommand { get; private set; }

			public IEnumerable<RCProcess.CustomCommandParser.CommandArg> Arguments
			{
				get
				{
					return this.arguments;
				}
			}

			public CustomCommandParser(string name, string command)
			{
				if (name == null || name.Length == 0 || name.IndexOf('|') != -1)
				{
					throw new ArgumentException("Name cannot be null, be empty string nor have pipe character(|)");
				}
				if (command == null || command.Length == 0 || command.IndexOf('|') != -1)
				{
					throw new ArgumentException("command cannot be null, be empty string nor have pipe character(|)");
				}
				this.Name = name;
				this.RawCommand = command;
				this.ParseCommand(this.RawCommand);
			}

			internal CustomCommandParser(string rawCommand)
			{
				string[] array = rawCommand.Split(new char[]
				{
					'|'
				});
				if (array.Length < 2)
				{
					throw new ArgumentException("It doesn't have name nor command : " + rawCommand);
				}
				if (array.Length > 2)
				{
					throw new ArgumentException("Too many tokens : " + rawCommand);
				}
				this.Name = array[0];
				this.RawCommand = array[1];
				this.ParseCommand(this.RawCommand);
			}

			private void ParseCommand(string command)
			{
				command = command.Replace("{", "{{").Replace("}", "}}");
				int num = 0;
				this.arguments = new List<RCProcess.CustomCommandParser.CommandArg>();
				int num2 = command.IndexOf('[');
				if (num2 != -1)
				{
					this.Command = command.Substring(0, num2).Trim();
				}
				else
				{
					this.Command = command;
				}
				int num3;
				while ((num2 = command.IndexOf('[')) != -1 && (num3 = command.IndexOf(']')) != -1)
				{
					int num4 = num3;
					string text = command.Substring(num2 + 1, num3 - num2).ToLower();
					string text2 = "";
					string text3 = "";
					int num5 = 0;
					char[] anyOf = new char[]
					{
						':',
						'\\',
						']'
					};
					int num6;
					while ((num6 = text.IndexOfAny(anyOf, num5)) != -1 && (num5 = text.IndexOfAny(anyOf, num6 + 1)) != -1 && num6 != num5)
					{
						if (':' == text[num6] && text2.Length == 0)
						{
							text2 = text.Substring(num6 + 1, num5 - num6 - 1);
						}
						else if ('\\' == text[num6] && text3.Length == 0)
						{
							text3 = text.Substring(num6 + 1, num5 - num6 - 1);
						}
						num4 = ((num2 + 1 + num6 < num4) ? (num2 + 1 + num6) : num4);
					}
					RCProcess.CustomCommandParser.DataType dataTypeFromString = RCProcess.CustomCommandParser.GetDataTypeFromString(text2);
					string name = command.Substring(num2 + 1, num4 - num2 - 1);
					this.arguments.Add(new RCProcess.CustomCommandParser.CommandArg(name, dataTypeFromString, text3));
					command = command.Remove(num2, num3 - num2 + 1).Insert(num2, "{" + num.ToString() + "}");
					num++;
				}
				this.formatCommand = command;
			}

			internal static RCProcess.CustomCommandParser.DataType GetDataTypeFromString(string cmdDataType)
			{
				RCProcess.CustomCommandParser.DataType result = RCProcess.CustomCommandParser.DataType.String;
				int num = 0;
				foreach (string a in RCProcess.CustomCommandParser.DataTypes)
				{
					if (a == cmdDataType)
					{
						result = (RCProcess.CustomCommandParser.DataType)num;
					}
					num++;
				}
				return result;
			}

			internal static string GetStringFromDataType(RCProcess.CustomCommandParser.DataType type)
			{
				return RCProcess.CustomCommandParser.DataTypes[(int)type];
			}

			public static List<RCProcess.CustomCommandParser> GetFromRawString(string rawCustomCommand)
			{
				int num = 0;
				int num2 = 0;
				List<RCProcess.CustomCommandParser> list = new List<RCProcess.CustomCommandParser>();
				while (num2 != rawCustomCommand.Length)
				{
					num2 = rawCustomCommand.IndexOf("||", num);
					if (num2 == -1)
					{
						num2 = rawCustomCommand.Length;
					}
					list.Add(new RCProcess.CustomCommandParser(rawCustomCommand.Substring(num, num2 - num)));
					num = num2 + 2;
				}
				return list;
			}

			public string GetFinalCommand(params object[] args)
			{
				if (args.Length != this.arguments.Count)
				{
					throw new ArgumentException("Argument size mismatch!");
				}
				return string.Format(this.formatCommand, args);
			}

			public static string ToRawString(IEnumerable<RCProcess.CustomCommandParser> commandList)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (RCProcess.CustomCommandParser customCommandParser in commandList)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append("||");
					}
					stringBuilder.Append(RCProcess.CustomCommandParser.ToRawString(customCommandParser.Name, customCommandParser.RawCommand));
				}
				return stringBuilder.ToString();
			}

			public static string ToRawString(string name, string command)
			{
				return name + "|" + command;
			}

			public static string ToRawString(string name, string command, string args)
			{
				return string.Format("{0}{1}{2} {3}", new object[]
				{
					name,
					"|",
					command,
					args
				});
			}

			public static readonly string[] DataTypes = new string[]
			{
				"string",
				"numeric",
				"date",
				"bool",
				"server",
				"lstring"
			};

			private string formatCommand;

			private List<RCProcess.CustomCommandParser.CommandArg> arguments;

			public enum DataType
			{
				String,
				Numeric,
				Date,
				Boolean,
				ServerGroup,
				LargeString
			}

			public struct CommandArg
			{
				public CommandArg(string name, RCProcess.CustomCommandParser.DataType type, string comment)
				{
					this.Name = name;
					this.Type = type;
					this.Comment = comment;
				}

				public override string ToString()
				{
					return RCProcess.CustomCommandParser.CommandArg.GetRawString(this.Name, this.Type, this.Comment);
				}

				public static string GetRawString(string name, RCProcess.CustomCommandParser.DataType type, string comment)
				{
					string text = name;
					if (type != RCProcess.CustomCommandParser.DataType.String)
					{
						text = text + ":" + RCProcess.CustomCommandParser.GetStringFromDataType(type);
					}
					if (!string.IsNullOrEmpty(comment))
					{
						text = text + "\\" + comment;
					}
					return string.Format("[{0}]", text);
				}

				public string Name;

				public RCProcess.CustomCommandParser.DataType Type;

				public string Comment;
			}
		}

		public class PerformanceDescriptionParser
		{
			public string Name
			{
				get
				{
					return this._name;
				}
			}

			public string HelpMessage
			{
				get
				{
					return this._helpMessage;
				}
			}

			public PerformanceDescriptionParser(string name, string helpMessage)
			{
				if (name == null || name.Length == 0 || name.IndexOf('|') != -1)
				{
					throw new ArgumentException("Name cannot be null, be empty string nor have pipe character(|)");
				}
				if (helpMessage != null && helpMessage.IndexOf('|') != -1)
				{
					throw new ArgumentException("HelpMessage cannot have pipe character(|)");
				}
				if (helpMessage == null)
				{
					helpMessage = string.Empty;
				}
				this._name = name;
				this._helpMessage = helpMessage;
			}

			private PerformanceDescriptionParser(string rawCommand)
			{
				string[] array = rawCommand.Split(new char[]
				{
					'|'
				});
				if (array.Length < 2)
				{
					throw new ArgumentException("It doesn't have name nor command : " + rawCommand);
				}
				if (array.Length > 2)
				{
					throw new ArgumentException("Too many tokens : " + rawCommand);
				}
				this._name = array[0];
				this._helpMessage = array[1];
			}

			public static RCProcess.PerformanceDescriptionParser[] GetFromRawString(string rawPerformanceDescription)
			{
				int num = 0;
				int num2 = 0;
				ArrayList arrayList = new ArrayList();
				while (num2 != rawPerformanceDescription.Length)
				{
					num2 = rawPerformanceDescription.IndexOf("||", num);
					if (num2 == -1)
					{
						num2 = rawPerformanceDescription.Length;
					}
					arrayList.Add(new RCProcess.PerformanceDescriptionParser(rawPerformanceDescription.Substring(num, num2 - num)));
					num = num2 + 2;
				}
				return (RCProcess.PerformanceDescriptionParser[])arrayList.ToArray(typeof(RCProcess.PerformanceDescriptionParser));
			}

			public static string ToRawString(RCProcess.PerformanceDescriptionParser[] descriptionList)
			{
				string text = "";
				foreach (RCProcess.PerformanceDescriptionParser performanceDescriptionParser in descriptionList)
				{
					if (text.Length > 0)
					{
						text += "||";
					}
					text = text + performanceDescriptionParser.Name + "|" + performanceDescriptionParser.HelpMessage;
				}
				return text;
			}

			private string _name;

			private string _helpMessage;
		}
	}
}
