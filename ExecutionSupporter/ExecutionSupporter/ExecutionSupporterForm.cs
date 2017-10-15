using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Devcat.Core.Threading;
using ExecutionSupporter.Component;
using ExecutionSupporter.Properties;

namespace ExecutionSupporter
{
	public partial class ExecutionSupporterForm : Form
	{
		public ExecutionSupportCore Core { get; set; }

		public ExecutionSupporterForm()
		{
			this.InitializeComponent();
		}

		private void ExecutionSupporterForm_Load(object sender, EventArgs e)
		{
			this.Core = new ExecutionSupportCore(this);
			this.ButtonStartService.Enabled = false;
			this.ButtonStopService.Enabled = false;
			this.ButtonStartServer.Enabled = false;
			this.ButtonUpdateServer.Enabled = false;
			this.ButtonKillServer.Enabled = false;
		}

		private void ExecutionSupporterForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				this.Core.JobProcessor.Stop();
			}
			catch
			{
			}
		}

        public Machine SelectedMachine
        {
            get
            {
                Machine current;
                IEnumerator enumerator = this.ListMachine.SelectedItems.GetEnumerator();
                try
                {
                    if (enumerator.MoveNext())
                    {
                        current = enumerator.Current as Machine;
                    }
                    else
                    {
                        return null;
                    }
                }
                finally
                {
                    IDisposable disposable = enumerator as IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }
                return current;
            }
        }

        private void ListMachine_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				this.Core.MachineManager.QuerySelectedMachineInfo();
				this.Core.MachineManager.RefreshMachineInfo();
			}
			catch
			{
			}
		}

		private void ButtonUpdateServer_Click(object sender, EventArgs e)
		{
			base.Enabled = false;
			DialogResult dialogResult = MessageBox.Show("Press Ok to Update Server Binary Files", "Update Server", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
			base.Enabled = true;
			if (dialogResult != DialogResult.OK)
			{
				return;
			}
			if (this.Core.MachineManager.UpdateServer())
			{
				return;
			}
			base.Enabled = false;
			MessageBox.Show("Can not update server while server is running.", "Update Server Fail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			base.Enabled = true;
		}

		private void ButtonStartServer_Click(object sender, EventArgs e)
		{
			base.Enabled = false;
			DialogResult dialogResult = MessageBox.Show("Will you start server?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
			base.Enabled = true;
			if (dialogResult != DialogResult.OK)
			{
				return;
			}
			this.Core.MachineManager.StartServer();
		}

		private void ButtonKillServer_Click(object sender, EventArgs e)
		{
			base.Enabled = false;
			DialogResult dialogResult = MessageBox.Show("Will you stop server?", "Confirm stop server", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
			base.Enabled = true;
			if (dialogResult != DialogResult.OK)
			{
				return;
			}
			this.Core.MachineManager.EndServer();
		}

		private void ButtonReloadSetting_Click(object sender, EventArgs e)
		{
			this.Core.MachineManager.LoadSettingFile();
			this.Core.MachineManager.RefreshMachineInfo();
		}

		private void ButtonCommand_Click(object sender, EventArgs e)
		{
			Scheduler.Schedule(this.Core.JobProcessor, Job.Create<string>(new Action<string>(this.Core.InputManager.DoCommand), this.TextCommand.Text), 0);
			this.TextCommand.Text = "";
		}

		private void TextCommand_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Return)
			{
				this.ButtonCommand_Click(sender, null);
			}
		}

		private void ButtonRefreshUserCount_Click(object sender, EventArgs e)
		{
			this.Core.AdminClientNode.RequestUserCount();
		}

		private void ButtonStartService_Click(object sender, EventArgs e)
		{
			base.Enabled = false;
			DialogResult dialogResult = MessageBox.Show(string.Format("Will you start service?\n {0}", Settings.Default.ServiceName), "Confirm start service", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
			base.Enabled = true;
			if (dialogResult != DialogResult.OK)
			{
				return;
			}
			Scheduler.Schedule(this.Core.JobProcessor, Job.Create<string>(new Action<string>(this.Core.InputManager.DoCommand), "startservice"), 0);
		}

		private void ButtonStopService_Click(object sender, EventArgs e)
		{
			base.Enabled = false;
			DialogResult dialogResult = MessageBox.Show(string.Format("Will you stop service?\n {0}", Settings.Default.ServiceName), "Confirm stop service", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
			base.Enabled = true;
			if (dialogResult != DialogResult.OK)
			{
				return;
			}
			Scheduler.Schedule(this.Core.JobProcessor, Job.Create<string>(new Action<string>(this.Core.InputManager.DoCommand), "endservice"), 0);
		}

		private void ButtonGatherLog_Click(object sender, EventArgs e)
		{
			this.Core.MachineManager.GatherLog();
		}

		private void ButtonLogViewer_Click(object sender, EventArgs e)
		{
			LogViewerForm logViewerForm = new LogViewerForm();
			logViewerForm.Show();
		}
	}
}
