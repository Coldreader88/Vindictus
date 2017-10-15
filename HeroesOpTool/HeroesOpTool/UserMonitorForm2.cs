using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Devcat.Core;
using HeroesOpTool.UserMonitorSystem;

namespace HeroesOpTool
{
	public partial class UserMonitorForm2 : Form
	{
		public event UserDropEventHandler OnUserDrop;

		public UserMonitorForm2(UserCountData data)
		{
			this.InitializeComponent();
			this.userCountData = data;
			UserCountData userCountData = this.userCountData;
			userCountData.OnUserDrop = (UserDropEventHandler)Delegate.Combine(userCountData.OnUserDrop, new UserDropEventHandler(this.UserDropped));
			UserCountData userCountData2 = this.userCountData;
			userCountData2.OnTableAdd = (EventHandler<EventArgs<DataTable>>)Delegate.Combine(userCountData2.OnTableAdd, new EventHandler<EventArgs<DataTable>>(this.TableAdded));
			this.TSComboStyle.Items.Add(LocalizeText.Get(536));
			this.TSComboStyle.Items.Add(LocalizeText.Get(537));
			this.TSComboStyle.SelectedIndex = 0;
		}

		private void AddControl(DataTable table)
		{
			switch (this.TSComboStyle.SelectedIndex)
			{
			case 0:
				this.panel.Controls.Add(new UserCountGraph(table));
				return;
			case 1:
				this.panel.Controls.Add(new UserCountGrid(table));
				return;
			default:
				return;
			}
		}

		private void TableAdded(object sender, EventArgs<DataTable> args)
		{
			this.UIThread(delegate
			{
				this.AddControl(args.Value);
			});
		}

		private void UserDropped(object sender, UserDropEventArgs args)
		{
			if (this.OnUserDrop != null)
			{
				this.OnUserDrop(sender, args);
			}
		}

		private void UserMonitorForm2_FormClosing(object sender, FormClosingEventArgs e)
		{
			UserCountData userCountData = this.userCountData;
			userCountData.OnUserDrop = (UserDropEventHandler)Delegate.Remove(userCountData.OnUserDrop, new UserDropEventHandler(this.UserDropped));
			UserCountData userCountData2 = this.userCountData;
			userCountData2.OnTableAdd = (EventHandler<EventArgs<DataTable>>)Delegate.Remove(userCountData2.OnTableAdd, new EventHandler<EventArgs<DataTable>>(this.TableAdded));
		}

		private void TSComboStyle_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.panel.Controls.Clear();
			foreach (DataTable table in this.userCountData.Tables)
			{
				this.AddControl(table);
			}
		}

		private void TSButtonCopy_Click(object sender, EventArgs e)
		{
			StringBuilder stringBuilder = new StringBuilder(1048576);
			foreach (DataTable dataTable in this.userCountData.Tables)
			{
				foreach (object obj in dataTable.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					stringBuilder.AppendFormat("{0}\t", dataColumn.Caption);
				}
				stringBuilder.Replace("\t", "\r\n", stringBuilder.Length - 1, 1);
				foreach (object obj2 in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj2;
					foreach (object obj3 in dataTable.Columns)
					{
						DataColumn dataColumn2 = (DataColumn)obj3;
						object obj4 = dataRow[dataColumn2.Caption];
						if (obj4 is DBNull)
						{
							obj4 = "0";
						}
						stringBuilder.AppendFormat("{0}\t", obj4.ToString());
					}
					stringBuilder.Replace("\t", "\r\n", stringBuilder.Length - 1, 1);
				}
			}
			DataObject data = new DataObject(stringBuilder.ToString());
			Clipboard.SetDataObject(data, false);
			Utility.ShowInformationMessage(LocalizeText.Get(404));
		}

		private UserCountData userCountData;
	}
}
