using System;
using AdminClientServiceCore.Messages;
using Devcat.Core.Threading;
using ExecutionSupporter.Component;
using ExecutionSupporter.Properties;

namespace ExecutionSupporter
{
	public class ExecutionSupportCore
	{
		public JobProcessor JobProcessor { get; set; }

		public ExecutionSupporterForm Form { get; set; }

		public MachineManager MachineManager { get; set; }

		public ServiceManager ServiceManager { get; set; }

		public LogManager LogManager { get; set; }

		public InputManager InputManager { get; set; }

		public ExecutionSupportCore(ExecutionSupporterForm form)
		{
			this.Form = form;
			this.JobProcessor = new JobProcessor();
			this.JobProcessor.Start();
			this.LogManager = new LogManager(this);
			this.MachineManager = new MachineManager(this);
			this.ServiceManager = new ServiceManager(this);
			this.InputManager = new InputManager(this);
			this.InitializeAdminService();
			ProcessCommandHelper.Initialize();
			Scheduler.Schedule(this.JobProcessor, Job.Create(new Action(this.TriggerSchedule)), 1000);
		}

		private void TriggerSchedule()
		{
			Scheduler.Schedule(this.JobProcessor, Job.Create(new Action(this.TriggerSchedule)), 15000);
			try
			{
				foreach (Machine machine in this.MachineManager.MachineDict.Values)
				{
					machine.TryConnect();
				}
				this.MachineManager.QuerySelectedMachineInfo();
				this.ServiceManager.RefreshServiceInfo();
				this.MachineManager.RefreshMachineInfo();
				this.InputManager.CheckCommand();
				this.AdminClientNode.TryConnect();
				if (!this.AdminClientNode.IsConnected)
				{
					this.LogManager.ClearUserCount();
				}
				if (this.MachineManager.RequestedCommand != MachineRequest.None)
				{
					foreach (Machine machine2 in this.MachineManager.MachineDict.Values)
					{
						machine2.QueryClientReport();
					}
				}
			}
			catch
			{
			}
		}

		public void NotifyServerMessage(NotifyCode code, string msg)
		{
			if (code == NotifyCode.ERROR)
			{
				this.LogManager.AddLog(LogType.ERROR, msg, new object[0]);
				return;
			}
			this.LogManager.AddLog(LogType.INFO, msg, new object[0]);
		}

		public AdminClientNode AdminClientNode { get; set; }

		public void InitializeAdminService()
		{
			this.AdminClientNode = new AdminClientNode(this);
			this.AdminClientNode.ConnectionSucceed += this.OnAdminServiceConnected;
			this.AdminClientNode.ConnectionFailed += this.OnAdminServiceConnectionFailed;
			this.AdminClientNode.Disconnected += this.OnAdminServiceDisconnected;
		}

		public void OnAdminServiceConnected(object sender, EventArgs e)
		{
			this.LogManager.AddLog(LogType.INFO, string.Format("AdminClientService Connected to {0}:{1}.", Settings.Default.AdminServiceAddr, Settings.Default.AdminServicePort), new object[0]);
		}

		public void OnAdminServiceConnectionFailed(object sender, EventArgs e)
		{
			if (this.ServiceManager.Status == ServiceStatus.On && this.MachineManager.IsStarted)
			{
				this.LogManager.AddLog(LogType.ERROR, string.Format("AdminClientService Connection({0}:{1}) Failed !", Settings.Default.AdminServiceAddr, Settings.Default.AdminServicePort), new object[0]);
			}
		}

		public void OnAdminServiceDisconnected(object sender, EventArgs e)
		{
			this.LogManager.AddLog(LogType.INFO, "AdminClientService Diconnected.", new object[0]);
		}

		public bool ProcessCmd(string text)
		{
			bool result;
			try
			{
				text = text.Trim();
				int num = text.IndexOf(' ');
				string text2;
				string text3;
				if (num == -1)
				{
					text2 = text;
					text3 = "";
				}
				else
				{
					text2 = text.Substring(0, num);
					text3 = text.Substring(num).Trim();
				}
				if (text2.Length > 2 && text2[0] == '/')
				{
					this.AdminClientNode.RequestConsoleCommand(text2.Substring(1), text3);
					result = true;
				}
				else if (text2.Trim() == "?")
				{
					result = ProcessCommandHelper.Process(this, "help", "");
				}
				else if (ProcessCommandHelper.Process(this, text2, text3))
				{
					result = true;
				}
				else
				{
					this.LogManager.AddLog(LogType.ERROR, "Invalid Command ! - \"{0}\"", new object[]
					{
						text
					});
					result = false;
				}
			}
			catch (Exception ex)
			{
				this.LogManager.AddLog(LogType.ERROR, "Exception occurred !  : {0}", new object[]
				{
					ex.Message
				});
				result = false;
			}
			return result;
		}

		public const int RefreshPeriod = 15000;
	}
}
