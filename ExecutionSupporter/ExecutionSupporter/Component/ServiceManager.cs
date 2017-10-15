using System;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using ExecutionSupporter.Properties;

namespace ExecutionSupporter.Component
{
	public class ServiceManager
	{
		public ExecutionSupportCore Core { get; set; }

		public ExecutionSupporterForm Form { get; set; }

		public HeroesSupportDataContext Context { get; set; }

		public MachineInfo ServiceInfo { get; set; }

		public ServiceController ServiceController { get; set; }

		public ServiceStatus Status
		{
			get
			{
				if (this.ServiceController == null)
				{
					return ServiceStatus.None;
				}
				this.ServiceController.Refresh();
				switch (this.ServiceController.Status)
				{
				case ServiceControllerStatus.Stopped:
					return ServiceStatus.Off;
				case ServiceControllerStatus.StartPending:
					return ServiceStatus.On_Pending;
				case ServiceControllerStatus.StopPending:
					return ServiceStatus.Off_Pending;
				case ServiceControllerStatus.Running:
					return ServiceStatus.On;
				default:
					return ServiceStatus.Invalid;
				}
			}
		}

		public ServiceManager(ExecutionSupportCore core)
		{
			this.Core = core;
			this.Form = core.Form;
			this.Context = new HeroesSupportDataContext();
			try
			{
				this.ServiceInfo = (from m in this.Context.MachineInfo
				where m.Address == ""
				select m).FirstOrDefault<MachineInfo>();
				if (this.ServiceInfo == null)
				{
					this.ServiceInfo = new MachineInfo
					{
						Address = "",
						Status = "",
						Info = ""
					};
					this.Context.MachineInfo.InsertOnSubmit(this.ServiceInfo);
				}
			}
			catch (Exception ex)
			{
				this.Core.LogManager.AddLog(LogType.ERROR, string.Format("Cannot initialize Service Context. : {0}", Settings.Default.ServiceName), new object[0]);
				this.Core.LogManager.AddLog(LogType.ERROR, ex.Message, new object[0]);
			}
			try
			{
				this.ServiceController = new ServiceController(Settings.Default.ServiceName);
				if (this.Status == ServiceStatus.None)
				{
				}
			}
			catch (Exception ex2)
			{
				this.Core.LogManager.AddLog(LogType.ERROR, string.Format("Can not initialize Service. : {0}", Settings.Default.ServiceName), new object[0]);
				this.Core.LogManager.AddLog(LogType.ERROR, ex2.Message, new object[0]);
				this.ServiceController = null;
			}
		}

		public void RefreshServiceInfo()
		{
			this.Form.Invoke(new Action(delegate
			{
				ServiceStatus status = this.Status;
				this.Form.TextServiceStatus.Text = string.Format("[{0}] {1}", status, Settings.Default.ServiceName);
				this.ServiceInfo.Status = this.Status.ToString();
				this.ServiceInfo.Info = Settings.Default.ServiceName;
				this.SaveServiceStatus();
				switch (status)
				{
				case ServiceStatus.On:
					this.Form.ButtonStartService.Enabled = false;
					this.Form.ButtonStopService.Enabled = true;
					return;
				case ServiceStatus.Off:
					this.Form.ButtonStartService.Enabled = true;
					this.Form.ButtonStopService.Enabled = false;
					return;
				}
				this.Form.ButtonStartService.Enabled = false;
				this.Form.ButtonStopService.Enabled = false;
			}));
		}

		public void SaveServiceStatus()
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

		public void StartService()
		{
			if (this.ServiceController.Status == ServiceControllerStatus.Stopped || this.ServiceController.Status == ServiceControllerStatus.StopPending)
			{
				this.ServiceController.Start();
				this.Core.LogManager.AddLog(LogType.INFO, "Start Service", new object[0]);
				int num = 0;
				while (this.ServiceController.Status != ServiceControllerStatus.Running)
				{
					Thread.Sleep(500);
					this.ServiceController.Refresh();
					num++;
					if (num > 120)
					{
						this.Core.LogManager.AddLog(LogType.ERROR, "Service Start Timeout !", new object[0]);
						return;
					}
				}
				this.Core.LogManager.AddLog(LogType.INFO, string.Format("Service \"{0}\" Started.", Settings.Default.ServiceName), new object[0]);
				return;
			}
			if (this.ServiceController.Status == ServiceControllerStatus.Running)
			{
				this.Core.LogManager.AddLog(LogType.ERROR, string.Format("Service \"{0}\" is Already Running !", Settings.Default.ServiceName), new object[0]);
			}
		}

		public void EndService()
		{
			if (this.ServiceController.Status == ServiceControllerStatus.Running || this.ServiceController.Status == ServiceControllerStatus.StartPending)
			{
				this.ServiceController.Stop();
				this.Core.LogManager.AddLog(LogType.INFO, "Stop Service", new object[0]);
				int num = 0;
				while (this.ServiceController.Status != ServiceControllerStatus.Stopped)
				{
					Thread.Sleep(500);
					this.ServiceController.Refresh();
					num++;
					if (num > 120)
					{
						this.Core.LogManager.AddLog(LogType.ERROR, "Service Stop Timeout !", new object[0]);
						return;
					}
				}
				this.Core.LogManager.AddLog(LogType.INFO, string.Format("Service \"{0}\" Stopped.", Settings.Default.ServiceName), new object[0]);
				return;
			}
			if (this.ServiceController.Status == ServiceControllerStatus.Stopped)
			{
				this.Core.LogManager.AddLog(LogType.ERROR, string.Format("Service \"{0}\" is Already Stopped !", Settings.Default.ServiceName), new object[0]);
			}
		}
	}
}
