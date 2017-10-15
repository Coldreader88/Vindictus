using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using Devcat.Core;
using HeroesOpTool.Properties;
using HeroesOpTool.RCUser;
using HeroesOpTool.RCUser.GeneralManage;
using HeroesOpTool.RCUser.ServerMonitorSystem;
using HeroesOpTool.UserMonitorSystem;
using RemoteControlSystem;
using RemoteControlSystem.ControlMessage;

namespace HeroesOpTool
{
	public partial class MainForm : Form
	{
		public static MainForm Instance { get; private set; }

		public MainForm(Configuration config, RCUserHandler rcUser, ServerMonitorControl control)
		{
			this.InitializeComponent();
			this.Text = LocalizeText.Get(151);
			this.menuItem1.Text = LocalizeText.Get(138);
			this.MenuItemAboutMe.Text = LocalizeText.Get(139);
			this.MenuItemClose.Text = LocalizeText.Get(140);
			this.menuItem5.Text = LocalizeText.Get(141);
			this.MenuItemUser.Text = LocalizeText.Get(142);
			this.menuItem4.Text = LocalizeText.Get(143);
			this.MenuItemHelp.Text = LocalizeText.Get(144);
			this.TSBServer.Text = LocalizeText.Get(147);
			this.TSBServer.ToolTipText = LocalizeText.Get(148);
			this.TSBUser.Text = LocalizeText.Get(149);
			this.TSBUser.ToolTipText = LocalizeText.Get(150);
			this.Text = LocalizeText.Get(151);
			if (rcUser.Authority < Authority.GSM)
			{
				this.toolStrip.Items.Remove(this.TSBServer);
				if (rcUser.Authority != Authority.UserMonitor)
				{
					this.toolStrip.Items.Remove(this.TSBUser);
				}
			}
			this._config = config;
			this._rcUser = rcUser;
			this._rcUser.ReceivedUserListReply += this.RC_ReceivedUserListReply;
			this._rcUser.ChildProcessLogOpened += this.OnSplitChildProcessLog;
			this._rcUser.ChildProcessLogged += delegate(object s, EventArgs<ChildProcessLogMessage> e)
			{
				LogGenerator logGenerator = this.childProcessLogs.FindGenerator(e.Value.ClientID, e.Value.ProcessName, e.Value.ProcessID);
				if (logGenerator != null)
				{
					logGenerator.LogGenerated(null, e.Value.ToString());
				}
			};
			this._serverMonitorControl = control;
			this._serverMonitorControl.OnSplitGeneralLog += this.OnSplitGeneralLog;
			this._serverMonitorControl.OnSplitProcessLog += this.OnSplitProcessLog;
			this._serverMonitorControl.OnSplitAllProcessLog += this.OnSplitAllProcessLog;
			this.bAlarm = true;
			this.alarmForm = this._serverMonitorControl.alarmForm;
			this.alarmForm.OnClose += this.UpdateAlarmTime;
			this.UpdateAlarm(this.bAlarm);
			this.alarmSound = new SoundPlayer(Resources.AlarmSound);
			this._userCountData = new UserCountData(this._rcUser);
			this.childProcessLogs = new ChildProcessLogGeneratorCollection();
			this.childProcessLogs.OnLogClosed += delegate(object s, EventArgs<ChildProcessLogDisconnectMessage> e)
			{
				this._rcUser.SendMessage<ChildProcessLogDisconnectMessage>(e.Value);
			};
			MainForm.Instance = this;
		}

		private void ToolBarButtonServerMonitorSystem_Click()
		{
			if (this._serverMonitorForm == null)
			{
				if (this._rcUser.Authority < Authority.UserKicker)
				{
					Utility.ShowErrorMessage(LocalizeText.Get(152));
					return;
				}
				try
				{
					this._serverMonitorForm = new ServerMonitorForm(this._config, this._serverMonitorControl);
					this._serverMonitorForm.OnAlarm += this.Alarm;
				}
				catch (InvalidOperationException)
				{
					return;
				}
				this.TSBServer.Tag = this._serverMonitorForm;
				this._serverMonitorForm.MdiParent = this;
				this._serverMonitorForm.Closed += this.ServerMonitorForm_Closed;
				this._serverMonitorForm.Show();
				this.TSBServer.Checked = true;
				return;
			}
			else
			{
				this._serverMonitorForm.Focus();
			}
		}

		private void ToolBarButtonUserMonitorSystem_Click()
		{
			if (this._userMonitorForm == null)
			{
				if (this._rcUser.Authority < Authority.GSM && this._rcUser.Authority != Authority.UserMonitor)
				{
					Utility.ShowErrorMessage(LocalizeText.Get(406));
					return;
				}
				try
				{
					this._userMonitorForm = new UserMonitorForm2(this._userCountData);
					this._userMonitorForm.OnUserDrop += new UserDropEventHandler(this.Alarm);
				}
				catch (InvalidOperationException)
				{
					return;
				}
				this.TSBUser.Tag = this._userMonitorForm;
				this._userMonitorForm.MdiParent = this;
				this._userMonitorForm.Closed += this.UserMonitorForm_Closed;
				this._userMonitorForm.Show();
				return;
			}
			else
			{
				this._userMonitorForm.Focus();
			}
		}

		private void MenuItemAboutMe_Click(object sender, EventArgs e)
		{
			PersonalInfo personalInfo = new PersonalInfo(this._config.ID, this._rcUser.Authority.ToString());
			while (personalInfo.ShowDialog() == DialogResult.OK && personalInfo.TBoxOldPassword.Text.Length > 0)
			{
				if (!(personalInfo.TBoxNewPassword.Text != personalInfo.TBoxRePassword.Text))
				{
					this._rcUser.ChangePassword(Utility.GetHashedPassword(personalInfo.TBoxOldPassword.Text), Utility.GetHashedPassword(personalInfo.TBoxNewPassword.Text));
					break;
				}
				Utility.ShowErrorMessage(LocalizeText.Get(153));
			}
		}

		private void MenuItemClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void MenuItemUser_Click(object sender, EventArgs e)
		{
			if (this._rcUser.Authority < Authority.Supervisor)
			{
				Utility.ShowErrorMessage(LocalizeText.Get(154));
				return;
			}
			this._rcUser.GetUserList();
		}

		private void MenuItemHelp_Click(object sender, EventArgs e)
		{
			new HelpViewer().Show();
		}

		private void MainForm_Closing(object sender, CancelEventArgs args)
		{
			if (MessageBox.Show(LocalizeText.Get(155), LocalizeText.Get(156), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
			{
				args.Cancel = true;
			}
		}

		private void ServerMonitorForm_Closed(object sender, EventArgs e)
		{
			this._serverMonitorForm = null;
			this.TSBServer.Tag = null;
		}

		private void UserMonitorForm_Closed(object sender, EventArgs e)
		{
			this._userMonitorForm = null;
			this.TSBUser.Tag = null;
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			try
			{
				this._rcUser.Stop();
			}
			catch (InvalidOperationException)
			{
			}
		}

		private void RC_ReceivedUserListReply(object sender, EventArgs<IEnumerable<Member>> args)
		{
			base.BeginInvoke(new Action<IEnumerable<Member>>(delegate(IEnumerable<Member> e)
			{
				new UserView(this._rcUser, e).ShowDialog();
			}), new object[]
			{
				args.Value
			});
		}

		private void UpdateAlarm(bool _bAlarm)
		{
			this.toolStripStatusAlarm.Image = (this.bAlarm ? Resources.alarmon : Resources.alarmoff);
		}

		private void UpdateAlarmTime(object sender, EventArgs args)
		{
			this.lastAlarmUpdated = DateTime.Now;
		}

		private void Alarm(object sender, MainForm.AlarmEventArgsBase args)
		{
			this.UIThread(delegate
			{
				this.alarmForm.AddNewLog(args.Message);
				this.alarmForm.OnUpdateEmergency();
				bool flag = false;
				if (this.alarmForm.SuppressMinute > 0 && this.lastAlarmUpdated + new TimeSpan(0, this.alarmForm.SuppressMinute, 0) < DateTime.Now)
				{
					flag = true;
				}
				if (flag || this.alarmForm.SuppressMinute == 0)
				{
					this.alarmSound.Play();
					this.alarmForm.Show();
				}
			});
		}

		private void toolStripStatusAlarm_Click(object sender, EventArgs e)
		{
			this.bAlarm = !this.bAlarm;
			this.UpdateAlarm(this.bAlarm);
		}

		private void TSBServer_Click(object sender, EventArgs e)
		{
			this.ToolBarButtonServerMonitorSystem_Click();
		}

		private void TSBUser_Click(object sender, EventArgs e)
		{
			this.ToolBarButtonUserMonitorSystem_Click();
		}

		private void MainForm_MdiChildActivate(object sender, EventArgs e)
		{
			foreach (object obj in this.toolStrip.Items)
			{
				ToolStripButton toolStripButton = (ToolStripButton)obj;
				if (base.ActiveMdiChild != null && toolStripButton.Tag == base.ActiveMdiChild)
				{
					toolStripButton.Checked = true;
				}
				else
				{
					toolStripButton.Checked = false;
				}
			}
		}

		public void OpenChildProcessLog(RCClient client, RCProcess process, int pid)
		{
			if (this.childProcessLogs.MakeGenerator(client, process, pid))
			{
				ChildProcessLogRequestMessage message = new ChildProcessLogRequestMessage(client.ID, process.Name, pid);
				this._rcUser.SendMessage<ChildProcessLogRequestMessage>(message);
			}
		}

		private void OnSplitChildProcessLog(object sender, EventArgs<ChildProcessLogReplyMessage> args)
		{
			LogGenerator logGenerator = this.childProcessLogs.FindGenerator(args.Value.ClientID, args.Value.ProcessName, args.Value.ProcessID);
			if (logGenerator != null)
			{
				this.ShowLog<RCProcess.ChildProcessLog>(logGenerator, args.Value.Log, delegate(LogViewForm f)
				{
					f.EnableInput = false;
				});
			}
		}

		private void OnSplitGeneralLog(object sender, EventArgs arg)
		{
			this.ShowLog<string>(this._serverMonitorControl.LogGenerator, this._serverMonitorControl.GeneralLogs.Split(new string[]
			{
				"\r\n"
			}, StringSplitOptions.RemoveEmptyEntries), delegate(LogViewForm f)
			{
				f.EnableInput = false;
				f.ForeColor = Color.Black;
				f.BackColor = Color.White;
			});
		}

		private void OnSplitProcessLog(object sender, EventArgs<LogGenerator> args)
		{
			RCProcess rcprocess = sender as RCProcess;
			if (rcprocess != null)
			{
				this.ShowLog<string>(args.Value, rcprocess.GetLog(), delegate(LogViewForm f)
				{
					f.EnableInput = true;
				});
			}
		}

		private void OnSplitAllProcessLog(object sender, EventArgs<LogGenerator> args)
		{
			this.ShowLog<char>(args.Value, "", delegate(LogViewForm f)
			{
				f.EnableInput = false;
				f.ShowProcessName = true;
			});
		}

		private void ShowLog<T>(LogGenerator logGenerator, IEnumerable<T> prevLog, Action<LogViewForm> preLoadFunc)
		{
			this.UIThread(delegate
			{
				if (!this.logViews.ContainsKey(logGenerator.Key))
				{
					LogViewForm logViewForm = new LogViewForm();
					if (preLoadFunc != null)
					{
						preLoadFunc(logViewForm);
					}
					logViewForm.Text = logGenerator.Name;
					logViewForm.Name = logGenerator.Key;
					this.logViews.Add(logGenerator.Key, logViewForm);
					logViewForm.LogGenerator = logGenerator;
					logViewForm.MdiParent = this;
					logViewForm.Closed += this.LogViewForm_Closed;
					logViewForm.AddLog<T>(prevLog);
					logViewForm.Show();
					ToolStripItem toolStripItem = this.toolStrip.Items.Add(logGenerator.Name);
					toolStripItem.Name = logGenerator.Key;
					toolStripItem.Tag = logViewForm;
					toolStripItem.Click += this.LogItem_Click;
					logViewForm.Tag = toolStripItem;
					return;
				}
				this.logViews[logGenerator.Key].Activate();
			});
		}

		private void LogItem_Click(object sender, EventArgs e)
		{
			ToolStripItem toolStripItem = sender as ToolStripItem;
			this.logViews[toolStripItem.Name].Activate();
		}

		private void LogViewForm_Closed(object sender, EventArgs e)
		{
			LogViewForm logViewForm = sender as LogViewForm;
			if (logViewForm != null)
			{
				this.logViews.Remove(logViewForm.Name);
				this.toolStrip.Items.Remove(logViewForm.Tag as ToolStripItem);
			}
		}

		public string GetCurrentUserID()
		{
			return this._config.ID;
		}

		private Configuration _config;

		private ServerMonitorForm _serverMonitorForm;

		private UserMonitorForm2 _userMonitorForm;

		private RCUserHandler _rcUser;

		private ServerMonitorControl _serverMonitorControl;

		private UserCountData _userCountData;

		private ChildProcessLogGeneratorCollection childProcessLogs;

		private UserAlarmForm alarmForm;

		private bool bAlarm;

		private DateTime lastAlarmUpdated;

		private SoundPlayer alarmSound;

		private Dictionary<string, LogViewForm> logViews = new Dictionary<string, LogViewForm>();

		public abstract class AlarmEventArgsBase : EventArgs
		{
			public abstract string Message { get; }
		}

		public class AlarmEventArgs : MainForm.AlarmEventArgsBase
		{
			public override string Message
			{
				get
				{
					return this.message;
				}
			}

			public AlarmEventArgs(string _message)
			{
				this.message = _message;
			}

			private string message;
		}

		public delegate void AlarmEventHandler(object sender, MainForm.AlarmEventArgsBase args);
	}
}
