using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using RemoteControlSystem;

namespace HeroesOpTool.RCUser.ServerMonitorSystem.SystemManage
{
	public partial class SchedulerSettingForm : Form
	{
		public string ScheduleName
		{
			get
			{
				return this.textBoxName.Text;
			}
		}

		public bool ScheduleEnabled
		{
			get
			{
				return !this.checkBoxDisabled.Checked;
			}
		}

		public RCProcessScheduler.EScheduleType ScheduleType
		{
			get
			{
				return (RCProcessScheduler.EScheduleType)this.comboBoxScheduleType.SelectedIndex;
			}
		}

		public DateTime ScheduleTime
		{
			get
			{
				return this.DateTimeScheduleTime.Value;
			}
		}

		public RCProcessScheduler.EExeType ExeType
		{
			get
			{
				return (RCProcessScheduler.EExeType)this.comboBoxExeType.SelectedIndex;
			}
		}

		public string Command
		{
			get
			{
				return this.textBoxExe.Text;
			}
		}

		public SchedulerSettingForm()
		{
			this.InitializeComponent();
			this.Text = LocalizeText.Get(435);
			this.groupBoxBasic.Text = LocalizeText.Get(436);
			this.labelName.Text = LocalizeText.Get(437);
			this.checkBoxDisabled.Text = LocalizeText.Get(452);
			this.groupBoxSchedule.Text = LocalizeText.Get(438);
			this.labelScheduleType.Text = LocalizeText.Get(439);
			this.labelScheduleTime.Text = LocalizeText.Get(440);
			this.groupBoxExe.Text = LocalizeText.Get(441);
			this.labelExeType.Text = LocalizeText.Get(442);
			this.buttonOK.Text = LocalizeText.Get(443);
			this.buttonCancel.Text = LocalizeText.Get(444);
			this.DateTimeScheduleTime.Value = DateTime.Now;
			for (int i = 0; i < 3; i++)
			{
				this.comboBoxScheduleType.Items.Add("");
			}
			RCProcessScheduler.EScheduleType[] array = (RCProcessScheduler.EScheduleType[])Enum.GetValues(typeof(RCProcessScheduler.EScheduleType));
			foreach (RCProcessScheduler.EScheduleType escheduleType in array)
			{
				if ((int)escheduleType < this.comboBoxScheduleType.Items.Count)
				{
					this.comboBoxScheduleType.Items[(int)escheduleType] = SchedulerSettingForm.GetScheduleTypeStr(escheduleType);
				}
			}
			this.comboBoxScheduleType.SelectedIndex = 0;
			for (int k = 0; k < 2; k++)
			{
				this.comboBoxExeType.Items.Add("");
			}
			RCProcessScheduler.EExeType[] array3 = (RCProcessScheduler.EExeType[])Enum.GetValues(typeof(RCProcessScheduler.EExeType));
			foreach (RCProcessScheduler.EExeType eexeType in array3)
			{
				if ((int)eexeType < this.comboBoxExeType.Items.Count)
				{
					this.comboBoxExeType.Items[(int)eexeType] = this.GetExeTypeStr(eexeType);
				}
			}
			this.comboBoxExeType.SelectedIndex = 0;
		}

		public SchedulerSettingForm(string _name, RCProcessScheduler.EScheduleType _schdType, DateTime _schdTime, RCProcessScheduler.EExeType _exeType, string _command, bool _bEnabled) : this()
		{
			this.textBoxName.Text = _name;
			this.textBoxName.Enabled = false;
			this.checkBoxDisabled.Checked = !_bEnabled;
			this.comboBoxScheduleType.SelectedIndex = (int)_schdType;
			this.DateTimeScheduleTime.Value = _schdTime;
			this.comboBoxExeType.SelectedIndex = (int)_exeType;
			this.textBoxExe.Text = _command;
		}

		public static string GetScheduleTypeStr(RCProcessScheduler.EScheduleType type)
		{
			switch (type)
			{
			case RCProcessScheduler.EScheduleType.AfterBoot:
				return LocalizeText.Get(445);
			case RCProcessScheduler.EScheduleType.AfterCrach:
				return LocalizeText.Get(446);
			case RCProcessScheduler.EScheduleType.Once:
				return LocalizeText.Get(447);
			default:
				return type.ToString();
			}
		}

		private string GetExeTypeStr(RCProcessScheduler.EExeType type)
		{
			switch (type)
			{
			case RCProcessScheduler.EExeType.StdInput:
				return LocalizeText.Get(449);
			case RCProcessScheduler.EExeType.ExternalExe:
				return LocalizeText.Get(448);
			default:
				return type.ToString();
			}
		}

		private void comboBoxScheduleType_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.DateTimeScheduleTime.Enabled = (this.comboBoxScheduleType.SelectedIndex == 2);
		}

		private void textBoxName_Validating(object sender, CancelEventArgs e)
		{
			string value = null;
			if (this.textBoxName.Text.Length == 0)
			{
				value = LocalizeText.Get(450);
				e.Cancel = true;
			}
			this.errorProviderAll.SetError(this.textBoxName, value);
		}
	}
}
