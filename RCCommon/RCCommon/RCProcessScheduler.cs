using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Xml;

namespace RemoteControlSystem
{
	[Guid("184F679B-C2D2-46bf-9BA5-4B3B466A2CBF")]
	[Serializable]
	public class RCProcessScheduler
	{
		[DllImport("shell32.dll", SetLastError = true)]
		private static extern IntPtr CommandLineToArgvW([MarshalAs(UnmanagedType.LPWStr)] string lpCmdLine, out int pNumArgs);

		[DllImport("kernel32.dll")]
		private static extern IntPtr LocalFree(IntPtr hMem);

		public RCProcessScheduler.EScheduleType ScheduleType { get; private set; }

		public DateTime ScheduleTime { get; private set; }

		public RCProcessScheduler.EExeType ExeType { get; private set; }

		public string Command { get; private set; }

		public string Name { get; private set; }

		public bool Enabled { get; set; }

		public RCProcess.StateChangeEventHandler EventHandler
		{
			get
			{
				return this._eventHandler;
			}
		}

		public RCProcessScheduler()
		{
			this.ScheduleType = RCProcessScheduler.EScheduleType.None;
			this.ScheduleTime = default(DateTime);
			this.ExeType = RCProcessScheduler.EExeType.None;
			this.Command = string.Empty;
			this.Name = string.Empty;
			this.Enabled = true;
		}

		public RCProcessScheduler(string _name, RCProcessScheduler.EScheduleType _schdType, DateTime _dateTime, RCProcessScheduler.EExeType _exeType, string _command, bool _bEnabled) : this()
		{
			this.SetFromVariable(_name, _schdType, _dateTime, _exeType, _command, _bEnabled);
		}

		public RCProcessScheduler(XmlReader reader) : this()
		{
			this.SetFromXmlInfo(reader);
		}

		public RCProcessScheduler Clone()
		{
			RCProcessScheduler rcprocessScheduler = new RCProcessScheduler();
			rcprocessScheduler.SetFromVariable(this.Name, this.ScheduleType, this.ScheduleTime, this.ExeType, this.Command, this.Enabled);
			return rcprocessScheduler;
		}

		public void Clear()
		{
			if (this.timer != null)
			{
				this.timer.Change(-1, -1);
				this.timer = null;
			}
		}

		public void Modify(RCProcessScheduler newSchedule)
		{
			if (this.Name != newSchedule.Name)
			{
				throw new ArgumentException("Name must be same when modifying properties!");
			}
			this.SetFromVariable(newSchedule.Name, newSchedule.ScheduleType, newSchedule.ScheduleTime, newSchedule.ExeType, newSchedule.Command, newSchedule.Enabled);
		}

		private void SetFromVariable(string _name, RCProcessScheduler.EScheduleType _schdType, DateTime _dateTime, RCProcessScheduler.EExeType _exeType, string _command, bool _bEnabled)
		{
			this.Clear();
			if (_command == null)
			{
				_command = string.Empty;
			}
			if (string.IsNullOrEmpty(_name))
			{
				throw new ArgumentException("Name cannot be null or empty string");
			}
			this.Name = _name;
			this.ScheduleType = _schdType;
			this.ScheduleTime = _dateTime;
			this.ExeType = _exeType;
			this.Command = _command;
			this.Enabled = _bEnabled;
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("ProcessSchedule");
			writer.WriteAttributeString("Name", this.Name);
			writer.WriteAttributeString("ScheduleType", this.ScheduleType.ToString());
			writer.WriteAttributeString("ScheduleTime", this.ScheduleTime.ToString());
			writer.WriteAttributeString("ExeType", this.ExeType.ToString());
			writer.WriteAttributeString("Command", this.Command);
			writer.WriteAttributeString("Enabled", this.Enabled ? bool.TrueString : bool.FalseString);
			writer.WriteEndElement();
		}

		private void SetFromXmlInfo(XmlReader reader)
		{
			if (reader.Name != "ProcessSchedule")
			{
				throw new ArgumentException("Reader is not node named \"ProcessSchedule\"");
			}
			this.SetFromVariable(reader.GetAttribute("Name"), (RCProcessScheduler.EScheduleType)Enum.Parse(typeof(RCProcessScheduler.EScheduleType), reader.GetAttribute("ScheduleType")), DateTime.Parse(reader.GetAttribute("ScheduleTime")), (RCProcessScheduler.EExeType)Enum.Parse(typeof(RCProcessScheduler.EExeType), reader.GetAttribute("ExeType")), reader.GetAttribute("Command"), bool.Parse(reader.GetAttribute("Enabled")));
		}

		public void Run(RCProcess _process)
		{
			this.Clear();
			if (!this.Enabled || string.IsNullOrEmpty(this.Command))
			{
				return;
			}
			switch (this.ScheduleType)
			{
			case RCProcessScheduler.EScheduleType.AfterBoot:
				this._eventHandler = new RCProcess.StateChangeEventHandler(this.BootHandler);
				_process.StateChange += this.EventHandler;
				return;
			case RCProcessScheduler.EScheduleType.AfterCrach:
				this._eventHandler = new RCProcess.StateChangeEventHandler(this.CrashHander);
				_process.StateChange += this.EventHandler;
				return;
			case RCProcessScheduler.EScheduleType.Once:
				if (this.ScheduleTime > DateTime.Now)
				{
					TimeSpan dueTime = TimeSpan.FromTicks(this.ScheduleTime.Ticks - DateTime.Now.Ticks);
					this.timer = new Timer(new TimerCallback(this.TimerCallback), _process, dueTime, new TimeSpan(-1L));
				}
				return;
			default:
				return;
			}
		}

		private void CrashHander(RCProcess sender, RCProcess.StateChangeEventArgs args)
		{
			if (args.OldState == RCProcess.ProcessState.On && args.NewState == RCProcess.ProcessState.Crash)
			{
				this.RunExe(sender);
			}
		}

		private void BootHandler(RCProcess sender, RCProcess.StateChangeEventArgs args)
		{
			if (args.OldState == RCProcess.ProcessState.Booting && args.NewState == RCProcess.ProcessState.On)
			{
				this.RunExe(sender);
			}
		}

		private void TimerCallback(object state)
		{
			this.timer = null;
			this.RunExe((RCProcess)state);
		}

        private void RunExe(RCProcess process)
        {
            switch (ExeType)
            {
                case RCProcessScheduler.EExeType.StdInput:
                    break;
                case RCProcessScheduler.EExeType.ExternalExe:
                    {
                        string[] commands = RCProcessScheduler.ParseExternalCommand(Command);
                        if (commands == null || commands.Length <= 0)
                        {
                            return;
                        }
                        string workDir = Path.GetDirectoryName(commands[0]);
                        string fileName = Path.GetFileName(commands[0]);
                        if (!Path.IsPathRooted(workDir))
                        {
                            workDir = BaseConfiguration.WorkingDirectory + "\\" + workDir;
                        }
                        if (string.IsNullOrEmpty(workDir))
                        {
                            workDir = BaseConfiguration.WorkingDirectory;
                        }
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.WorkingDirectory = workDir;
                        StringBuilder sb = new StringBuilder();
                        for (int i = 1; i < commands.Length; i++)
                        {
                            sb.AppendFormat("\"{0}\" ", commands[i]);
                        }
                        RCProcess.MakeProcess(startInfo, fileName, sb.ToString());
                        Process proc = new Process();
                        proc.StartInfo = startInfo;
                        try
                        {
                            proc.Start();
                            return;
                        }
                        catch (Exception ex)
                        {
                            Log<RCClient>.Logger.ErrorFormat("[{0}] - Exception in Start Schedule - {1}", process.Name, Name);
                            process.AddLogManual("Exception in Start() - " + ex.Message);
                            return;
                        }
                    }
                default:
                    return;
            }
            process.StandardIn(Command);
        }

        private static string[] ParseExternalCommand(string commandline)
		{
			int num = 0;
			IntPtr intPtr = RCProcessScheduler.CommandLineToArgvW(commandline, out num);
			string[] result;
			try
			{
				string[] array = new string[num];
				int num2 = 0;
				for (int i = 0; i < num; i++)
				{
					IntPtr ptr = Marshal.ReadIntPtr(intPtr, num2);
					num2 += IntPtr.Size;
					array[i] = Marshal.PtrToStringUni(ptr);
				}
				result = array;
			}
			finally
			{
				RCProcessScheduler.LocalFree(intPtr);
			}
			return result;
		}

		[NonSerialized]
		private Timer timer;

		[NonSerialized]
		private RCProcess.StateChangeEventHandler _eventHandler;

		public enum EScheduleType : byte
		{
			AfterBoot,
			AfterCrach,
			Once,
			Max,
			None
		}

		public enum EExeType : byte
		{
			StdInput,
			ExternalExe,
			Max,
			None
		}
	}
}
