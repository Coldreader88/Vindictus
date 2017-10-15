using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using RemoteControlSystem;

namespace HeroesOpTool.RCUser.ServerMonitorSystem
{
	public partial class ChildProcessListForm : Form
	{
		public RCClient Client { get; private set; }

		public RCProcess Process { get; private set; }

		public List<int> Processes
		{
			set
			{
				this.listView.Items.Clear();
				foreach (int num in value)
				{
					ListViewItem listViewItem = this.listView.Items.Add(num.ToString());
					listViewItem.Tag = num;
				}
			}
		}

		public ChildProcessListForm(RCClient client, RCProcess process)
		{
			this.Client = client;
			this.Process = process;
			this.InitializeComponent();
			this.Text = string.Format("{0} {1} {2}", this.Client.Name, this.Process.Name, this.Text);
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (this.listView.SelectedItems.Count > 0)
			{
				MainForm.Instance.OpenChildProcessLog(this.Client, this.Process, (int)this.listView.SelectedItems[0].Tag);
			}
		}
	}
}
