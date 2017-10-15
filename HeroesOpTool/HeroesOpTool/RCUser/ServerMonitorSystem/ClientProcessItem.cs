using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using RemoteControlSystem;

namespace HeroesOpTool.RCUser.ServerMonitorSystem
{
	internal class ClientProcessItem : ListViewItem, IComparable
	{
		public RCClient Client { get; private set; }

		public RCProcess Process { get; private set; }

		public int ShowCount { get; set; }

		public static SortedList<ClientProcessItem, ClientProcessItem> ShowItem { get; private set; } = new SortedList<ClientProcessItem, ClientProcessItem>();

		public ClientProcessItem(RCClient client, RCProcess process)
		{
			this.Client = client;
			this.Process = process;
		}

		public static ClientProcessItem CreateListItem(RCClient client, RCProcess process)
		{
			ClientProcessItem clientProcessItem = new ClientProcessItem(client, process);
			clientProcessItem.SubItems.AddRange(new string[]
			{
				process.Description,
				client.Name,
				process.State.ToString(),
				"",
				client.ClientIP
			});
			clientProcessItem.Name = ClientProcessItem.MakeKey(client.Name, process.Name);
			clientProcessItem.Client = client;
			clientProcessItem.Process = process;
			clientProcessItem.ShowCount = 0;
			clientProcessItem.RefreshState();
			return clientProcessItem;
		}

		public int CompareTo(object obj)
		{
			ClientProcessItem clientProcessItem = (ClientProcessItem)obj;
			int num = this.Client.Name.CompareTo(clientProcessItem.Client.Name);
			if (num != 0)
			{
				return num;
			}
			return this.Process.Description.CompareTo(clientProcessItem.Process.Description);
		}

		public bool IsSameWith(RCClient client, RCProcess process)
		{
			return this.Client.CompareTo(client) == 0 && this.Process.CompareTo(process) == 0;
		}

		private string GetPerformanceStr(long _privateBytes, long _virtualBytes, float _cpu)
		{
			float num = (float)_privateBytes / 1048576f;
			float num2 = (float)_virtualBytes / 1048576f;
			return string.Format("{0:F1}/{1:F1}MB {2:00.0}%", num, num2, _cpu);
		}

		public void RefreshState()
		{
			if (base.ListView != null)
			{
				base.ListView.BeginUpdate();
			}
			base.SubItems[1].Text = this.Process.Description;
			base.SubItems[2].Text = this.Client.Name;
			base.SubItems[3].Text = this.Process.State.ToString();
			bool flag = this.Process.CheckPerformance;
			switch (this.Process.State)
			{
			case RCProcess.ProcessState.Off:
				base.ImageIndex = 0;
				base.BackColor = Color.FromKnownColor(KnownColor.Window);
				flag = false;
				break;
			case RCProcess.ProcessState.Updating:
				base.ImageIndex = 1;
				base.BackColor = Color.Yellow;
				flag = false;
				break;
			case RCProcess.ProcessState.Booting:
				base.ImageIndex = 2;
				base.BackColor = Color.Orange;
				flag = false;
				break;
			case RCProcess.ProcessState.On:
				base.ImageIndex = 3;
				base.BackColor = Color.LightGreen;
				break;
			case RCProcess.ProcessState.Closing:
				base.ImageIndex = 4;
				base.BackColor = Color.LightGray;
				flag = false;
				break;
			case RCProcess.ProcessState.Freezing:
				base.ImageIndex = 5;
				base.BackColor = Color.Pink;
				break;
			case RCProcess.ProcessState.Crash:
				base.ImageIndex = 0;
				base.BackColor = Color.Red;
				flag = false;
				break;
			}
			if (!flag)
			{
				base.SubItems[4].Text = "N/A";
			}
			else
			{
				base.SubItems[4].Text = this.GetPerformanceStr(this.Process.Performance.PrivateMemorySize, this.Process.Performance.VirtualMemorySize, this.Process.Performance.CPU);
			}
			if (base.ListView != null)
			{
				base.ListView.EndUpdate();
			}
		}

		public override bool Equals(object obj)
		{
			return this.CompareTo(obj) == 0;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public static ClientProcessItem GetShowItem(RCClient client, RCProcess process)
		{
			ClientProcessItem key = new ClientProcessItem(client, process);
			if (ClientProcessItem.ShowItem.ContainsKey(key))
			{
				return ClientProcessItem.ShowItem[key];
			}
			return null;
		}

		public static string MakeKey(string clientName, string processName)
		{
			return string.Format("{0}:{1}", clientName, processName);
		}

		private static Utility.StringNumberComparer comparer = new Utility.StringNumberComparer();
	}
}
