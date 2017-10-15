using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Devcat.Core;
using HeroesOpTool.RCUser.ServerMonitorSystem;
using RemoteControlSystem;
using RemoteControlSystem.ControlMessage;

namespace HeroesOpTool
{
	public partial class ServerMonitorForm : Form
	{
		public event MainForm.AlarmEventHandler OnAlarm;

		public ServerMonitorForm(Configuration config, ServerMonitorControl serverMonitorControl)
		{
			this.InitializeComponent();
			this.menuItem1.Text = LocalizeText.Get(390);
			this.menuItemControl.Text = LocalizeText.Get(395);
			this.Text = LocalizeText.Get(396);
			this._serverMonitorControl = serverMonitorControl;
			this._serverMonitorControl.OnProcessCrash += this.ProcessCrashed;
			this._serverMonitorControl.OnChildProcessList += this.ChildProcessListed;
			this._serverMonitorControl.OnExeInfo += this.ExeInfo;
			this._config = config;
		}

		public new void Show()
		{
			base.Controls.Add(this._serverMonitorControl);
			base.Show();
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			this._serverMonitorControl.OnProcessCrash -= this.ProcessCrashed;
			this._serverMonitorControl.OnChildProcessList -= this.ChildProcessListed;
			this._serverMonitorControl.OnExeInfo -= this.ExeInfo;
			base.Controls.Remove(this._serverMonitorControl);
			base.OnClosing(e);
		}

		private void ProcessCrashed(RCClient s, RCProcess e)
		{
			if (this.OnAlarm != null)
			{
				string message = string.Format("{0} - {1} {2}", s.Name, e.Description, (e.State == RCProcess.ProcessState.Freezing) ? "Freezing" : "Crash");
				this.OnAlarm(s, new MainForm.AlarmEventArgs(message));
			}
		}

		private void ChildProcessListed(object sender, EventArgs<ChildProcessLogListReplyMessage> args)
		{
			RCClient rcclient = sender as RCClient;
			RCProcess rcprocess = rcclient[args.Value.ProcessName];
			if (rcprocess != null)
			{
				ChildProcessListForm childProcessListForm = new ChildProcessListForm(rcclient, rcprocess);
				childProcessListForm.Processes = args.Value.Processes;
				Point point = base.Parent.PointToScreen(base.Location);
				Point point2 = base.PointToScreen(this._serverMonitorControl.Location);
				if (base.WindowState == FormWindowState.Maximized)
				{
					childProcessListForm.Location = new Point(point.X + base.Width - childProcessListForm.Width, point2.Y);
				}
				else
				{
					childProcessListForm.Location = new Point(point.X + base.Width, point.Y);
				}
				childProcessListForm.Show(this);
			}
		}

		private void ExeInfo(object sender, EventArgs<ExeInfoReplyMessage> args)
		{
			RCClient rcclient = sender as RCClient;
			RCProcess rcprocess = rcclient[args.Value.ProcessName];
			if (rcprocess != null)
			{
				ExeInfoForm exeInfoForm = new ExeInfoForm(rcclient, rcprocess);
				exeInfoForm.Files = args.Value.GetFiles();
				Point point = base.Parent.PointToScreen(base.Location);
				Point point2 = base.PointToScreen(this._serverMonitorControl.Location);
				if (base.WindowState == FormWindowState.Maximized)
				{
					exeInfoForm.Location = new Point(point.X + base.Width - exeInfoForm.Width, point2.Y);
				}
				else
				{
					exeInfoForm.Location = new Point(point.X + base.Width, point.Y);
				}
				exeInfoForm.Show(this);
			}
		}

		private void menuItemStart_Click(object sender, EventArgs args)
		{
			this._serverMonitorControl.BtnStart_Click(sender, args);
		}

		private void menuItemStop_Click(object sender, EventArgs args)
		{
			this._serverMonitorControl.BtnStop_Click(sender, args);
		}

		private void menuItemCommand_Click(object sender, EventArgs args)
		{
			this._serverMonitorControl.BtnCommand_Click(sender, args);
		}

		private void menuItemUpdate_Click(object sender, EventArgs args)
		{
			this._serverMonitorControl.BtnUpdate_Click(sender, args);
		}

		private void menuItemControl_Click(object sender, EventArgs args)
		{
			this._serverMonitorControl.BtnControl_Click(sender, args);
		}

		private ServerMonitorControl _serverMonitorControl;

		private Configuration _config;
	}
}
