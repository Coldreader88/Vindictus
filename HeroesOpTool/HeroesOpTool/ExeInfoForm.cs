using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using RemoteControlSystem;

namespace HeroesOpTool
{
	public partial class ExeInfoForm : Form
	{
		public IEnumerable<KeyValuePair<string, DateTime>> Files
		{
			set
			{
				this.listView.Items.Clear();
				foreach (KeyValuePair<string, DateTime> keyValuePair in value)
				{
					ListViewItem listViewItem = this.listView.Items.Add(keyValuePair.Key);
					listViewItem.SubItems.Add(keyValuePair.Value.ToString());
				}
			}
		}

		public ExeInfoForm(RCClient client, RCProcess process)
		{
			this.InitializeComponent();
			this.Text = string.Format("{0} {1} {2}", client.Name, process.Name, this.Text);
			this.listViewSorter = new List<Utility.ListViewItemComparer>();
			for (int i = 0; i < this.listView.Columns.Count; i++)
			{
				Utility.ListViewItemComparer listViewItemComparer = new Utility.ListViewItemComparer(i);
				listViewItemComparer.IsAscending = true;
				this.listViewSorter.Add(listViewItemComparer);
			}
			this.listView.ListViewItemSorter = this.listViewSorter[0];
		}

		private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			if (this.listViewSorter[e.Column] == this.listView.ListViewItemSorter)
			{
				this.listViewSorter[e.Column].IsAscending = !this.listViewSorter[e.Column].IsAscending;
			}
			else
			{
				this.listViewSorter[e.Column].IsAscending = true;
				this.listView.ListViewItemSorter = this.listViewSorter[e.Column];
			}
			this.listView.Sort();
		}

		private List<Utility.ListViewItemComparer> listViewSorter;
	}
}
