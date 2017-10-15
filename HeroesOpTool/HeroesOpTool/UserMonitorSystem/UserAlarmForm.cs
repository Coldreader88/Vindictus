using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using RemoteControlSystem;

namespace HeroesOpTool.UserMonitorSystem
{
	public partial class UserAlarmForm : Form
	{
		public event EventHandler OnClose;

		public int SuppressMinute
		{
			get
			{
				if (!this.bSuppress)
				{
					return 0;
				}
				return this.suppressMinute;
			}
		}

		public List<EmergencyInformationData> emergencyMembers { get; private set; }

		public UserAlarmForm()
		{
			this.InitializeComponent();
			this.suppressMinutes = new List<int>();
			this.logs = new Dictionary<DateTime, string>();
			this.emergencyMembers = new List<EmergencyInformationData>();
			this.BtnOK.Text = LocalizeText.Get(358);
			this.clmnTime.Text = LocalizeText.Get(414);
			this.clmnDesc.Text = LocalizeText.Get(424);
			this.LabelDesc.Text = LocalizeText.Get(425);
			this.Text = LocalizeText.Get(156);
			this.EmergencyCallLabel.Text = LocalizeText.Get(545);
			this.Department.Text = LocalizeText.Get(539);
			this.Id.Text = LocalizeText.Get(540);
			this.CallName.Text = LocalizeText.Get(541);
			this.PhoneNumber.Text = LocalizeText.Get(542);
			this.Mail.Text = LocalizeText.Get(543);
			this.Rank.Text = LocalizeText.Get(544);
			for (int i = 10; i <= 60; i += 10)
			{
				this.ComboMinute.Items.Add(string.Format(LocalizeText.Get(426), i));
				this.suppressMinutes.Add(i);
			}
			this.ComboMinute.SelectedIndex = 0;
		}

		public void AddEmergencyCallInfo(EmergencyInformationData member)
		{
			this.emergencyMembers.Add(member);
		}

		public void OnUpdateEmergency()
		{
			foreach (EmergencyInformationData emergencyInformationData in this.emergencyMembers)
			{
				ListViewItem listViewItem = new ListViewItem(emergencyInformationData.Department);
				listViewItem.SubItems.Add(emergencyInformationData.ID);
				listViewItem.SubItems.Add(emergencyInformationData.Name);
				listViewItem.SubItems.Add(emergencyInformationData.PhoneNumber);
				listViewItem.SubItems.Add(emergencyInformationData.Mail);
				listViewItem.SubItems.Add(emergencyInformationData.Rank);
				this.EmergencyView.Items.Add(listViewItem);
			}
			this.emergencyMembers.Clear();
		}

		public void AddNewLog(string _message)
		{
			DateTime now = DateTime.Now;
			while (this.logs.ContainsKey(now))
			{
				now = new DateTime(now.Ticks + 1L);
			}
			this.logs.Add(now, _message);
			this.OnAddLog();
		}

		private void OnAddLog()
		{
			if (base.Visible)
			{
				this.SyncUpdateLog();
			}
		}

		private void UpdateLog()
		{
			foreach (KeyValuePair<DateTime, string> keyValuePair in this.logs)
			{
				string text = keyValuePair.Key.ToString("MM-dd HH:mm:ss");
				if (!this.ListLog.Items.ContainsKey(text) || !(this.ListLog.Items[text].SubItems[0].Text == keyValuePair.Value))
				{
					ListViewItem listViewItem = new ListViewItem(text);
					listViewItem.SubItems.Add(keyValuePair.Value).ForeColor = Color.Red;
					this.ListLog.Items.Add(listViewItem);
				}
			}
		}

		private void SyncUpdateLog()
		{
			this.UIThread(delegate
			{
				this.UpdateLog();
			});
		}

		private void BtnOK_Click(object sender, EventArgs e)
		{
			this.bSuppress = this.ChkSupress.Checked;
			this.suppressMinute = this.suppressMinutes[this.ComboMinute.SelectedIndex];
			this.ListLog.Items.Clear();
			this.logs.Clear();
			base.Hide();
		}

		private void UserAlarmForm_VisibleChanged(object sender, EventArgs e)
		{
			this.UpdateLog();
			if (!base.Visible)
			{
				this.OnClose(sender, e);
				return;
			}
			base.Focus();
		}

		private void UserAlarmForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.bSuppress = this.ChkSupress.Checked;
			this.suppressMinute = this.suppressMinutes[this.ComboMinute.SelectedIndex];
			this.ListLog.Items.Clear();
			this.logs.Clear();
			base.Hide();
			e.Cancel = true;
		}

		private bool bSuppress;

		private List<int> suppressMinutes;

		private int suppressMinute;

		private Dictionary<DateTime, string> logs;
	}
}
