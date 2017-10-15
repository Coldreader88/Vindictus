using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using RemoteControlSystem;

namespace HeroesOpTool.RCUser.ServerMonitorSystem.SystemManage
{
	public partial class ProcessPropertyForm : Form
	{
		private ProcessPropertyForm()
		{
			this.InitializeComponent();
			this.checkBoxAutomaticStart.Text = LocalizeText.Get(248);
			this.toolTipProperty.SetToolTip(this.checkBoxAutomaticStart, LocalizeText.Get(249));
			this.checkBoxUse.Text = LocalizeText.Get(250);
			this.toolTipProperty.SetToolTip(this.checkBoxUse, LocalizeText.Get(251));
			this.checkBoxRunOnce.Text = LocalizeText.Get(252);
			this.toolTipProperty.SetToolTip(this.checkBoxRunOnce, LocalizeText.Get(253));
			this.toolTipProperty.SetToolTip(this.textBoxUpdateFileArgs, LocalizeText.Get(254));
			this.toolTipProperty.SetToolTip(this.textBoxUpdateFileName, LocalizeText.Get(255));
			this.toolTipProperty.SetToolTip(this.textBoxStandardOutLogLines, LocalizeText.Get(256));
			this.toolTipProperty.SetToolTip(this.textBoxExecuteFileArgs, LocalizeText.Get(260));
			this.toolTipProperty.SetToolTip(this.textBoxExecuteFileName, LocalizeText.Get(261));
			this.toolTipProperty.SetToolTip(this.textBoxWorkingDirectory, LocalizeText.Get(262));
			this.toolTipProperty.SetToolTip(this.textBoxDescription, LocalizeText.Get(263));
			this.toolTipProperty.SetToolTip(this.comboBoxName, LocalizeText.Get(264));
			this.toolTipProperty.SetToolTip(this.textBoxType, LocalizeText.Get(506));
			this.labelName.Text = LocalizeText.Get(265);
			this.labelType.Text = LocalizeText.Get(505);
			this.labelDescription.Text = LocalizeText.Get(266);
			this.labelWorkingDirectory.Text = LocalizeText.Get(267);
			this.labelExecuteFile.Text = LocalizeText.Get(268);
			this.labelUpdateFile.Text = LocalizeText.Get(269);
			this.labelStandardOutLogLines.Text = LocalizeText.Get(270);
			this.groupBoxBasicProperty.Text = LocalizeText.Get(273);
			this.toolTipProperty.SetToolTip(this.groupBoxBasicProperty, LocalizeText.Get(274));
			this.groupBoxFileProperty.Text = LocalizeText.Get(275);
			this.toolTipProperty.SetToolTip(this.groupBoxFileProperty, LocalizeText.Get(276));
			this.groupBoxEtcProperty.Text = LocalizeText.Get(283);
			this.toolTipProperty.SetToolTip(this.groupBoxEtcProperty, LocalizeText.Get(284));
			this.buttonOK.Text = LocalizeText.Get(285);
			this.buttonCancel.Text = LocalizeText.Get(286);
			this.Text = LocalizeText.Get(292);
			this.groupBoxSchedule.Text = LocalizeText.Get(431);
			this.columnHeader2.Text = LocalizeText.Get(432);
			this.columnHeader3.Text = LocalizeText.Get(433);
			this.columnHeader4.Text = LocalizeText.Get(434);
			this.detailForm = new ProcessPropertyDetailForm();
		}

		public ProcessPropertyForm(RCProcess process, int version, bool editable, bool multEdit) : this()
		{
			this.detailForm = new ProcessPropertyDetailForm(process, editable);
			this._editable = editable;
			this.comboBoxName.Text = process.Name;
			this.comboBoxName.Enabled = false;
			this.textBoxType.Text = process.Type;
			this.textBoxDescription.Text = process.Description;
			this.textBoxWorkingDirectory.Text = process.WorkingDirectory;
			this.textBoxExecuteFileName.Text = process.ExecuteName;
			this.textBoxExecuteFileArgs.Text = process.ExecuteArgs;
			if (string.IsNullOrEmpty(this.textBoxExecuteFileArgs.Text))
			{
				this.toolTipProperty.SetToolTip(this.textBoxExecuteFileArgs, LocalizeText.Get(260));
			}
			else
			{
				this.toolTipProperty.SetToolTip(this.textBoxExecuteFileArgs, process.ExecuteArgs);
			}
			this.textBoxUpdateFileName.Text = process.UpdateExecuteName;
			this.textBoxUpdateFileArgs.Text = process.UpdateExecuteArgs;
			this.textBoxStandardOutLogLines.Text = process.LogLines.ToString();
			this.checkBoxRunOnce.Checked = process.AutomaticRestart;
			this.checkBoxUse.Checked = process.DefaultSelect;
			this.checkBoxAutomaticStart.Checked = process.AutomaticStart;
			this.checkBoxPerformance.Checked = process.CheckPerformance;
			if (!editable)
			{
				this.textBoxType.Enabled = false;
				this.textBoxDescription.Enabled = false;
				this.textBoxWorkingDirectory.Enabled = false;
				this.textBoxExecuteFileName.Enabled = false;
				this.textBoxExecuteFileArgs.Enabled = false;
				this.textBoxUpdateFileName.Enabled = false;
				this.textBoxUpdateFileArgs.Enabled = false;
				this.textBoxStandardOutLogLines.Enabled = false;
				this.checkBoxRunOnce.Enabled = false;
				this.checkBoxUse.Enabled = false;
				this.checkBoxAutomaticStart.Enabled = false;
				this.checkBoxPerformance.Enabled = false;
				this.buttonScheduleAdd.Enabled = false;
				this.buttonScheduleSub.Enabled = false;
			}
			else if (multEdit)
			{
				this.textBoxDescription.Enabled = false;
			}
			if (version != 0 && version < 8)
			{
				this.textBoxType.Enabled = false;
			}
			foreach (RCProcessScheduler schedule in process.Schedules)
			{
				this.AddProcessSchedule(schedule);
			}
		}

		public ProcessPropertyForm(ICollection processTemplateList) : this()
		{
			foreach (object obj in processTemplateList)
			{
				RCProcess item = (RCProcess)obj;
				this.comboBoxName.Items.Add(item);
			}
		}

		public RCProcess RCProcess
		{
			get
			{
				return this._process;
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			int logLines;
			try
			{
				logLines = int.Parse(this.textBoxStandardOutLogLines.Text);
			}
			catch (Exception)
			{
				logLines = 100;
			}
			try
			{
				this._process = new RCProcess(this.comboBoxName.Text, this.textBoxType.Text, this.textBoxDescription.Text, this.textBoxWorkingDirectory.Text, this.textBoxExecuteFileName.Text, this.textBoxExecuteFileArgs.Text, this.detailForm.ShutdownString, this.detailForm.GetCustomCommand(), logLines, this.detailForm.BootedString, this.checkBoxPerformance.Checked, this.detailForm.PerformanceString, this.detailForm.GetPerformanceDescription(), this.checkBoxUse.Checked, this.checkBoxAutomaticStart.Checked, this.checkBoxRunOnce.Checked, this.textBoxUpdateFileName.Text, this.textBoxUpdateFileArgs.Text, this.detailForm.TraceChildProcess, this.detailForm.ChildProcessLogStr, this.detailForm.MaxChildProcessCount, this.GetSchedulerCollection(), this.detailForm.GetProperty());
				base.Close();
			}
			catch (Exception ex)
			{
				Utility.ShowErrorMessage(LocalizeText.Get(293) + ex.Message);
			}
		}

		private RCProcessSchedulerCollection GetSchedulerCollection()
		{
			RCProcessSchedulerCollection rcprocessSchedulerCollection = new RCProcessSchedulerCollection();
			if (this.listViewSchedule.Items.Count > 0)
			{
				foreach (object obj in this.listViewSchedule.Items)
				{
					ListViewItem listViewItem = (ListViewItem)obj;
					rcprocessSchedulerCollection.Add((RCProcessScheduler)listViewItem.Tag);
				}
			}
			return rcprocessSchedulerCollection;
		}

		private void comboBoxName_SelectedIndexChanged(object sender, EventArgs e)
		{
			RCProcess rcprocess = this.comboBoxName.SelectedItem as RCProcess;
			if (rcprocess != null)
			{
				this.textBoxType.Text = rcprocess.Type;
				this.textBoxDescription.Text = rcprocess.Description;
				this.textBoxWorkingDirectory.Text = rcprocess.WorkingDirectory;
				this.textBoxExecuteFileName.Text = rcprocess.ExecuteName;
				this.textBoxExecuteFileArgs.Text = rcprocess.ExecuteArgs;
				this.textBoxUpdateFileName.Text = rcprocess.UpdateExecuteName;
				this.textBoxUpdateFileArgs.Text = rcprocess.UpdateExecuteArgs;
				this.detailForm.BootedString = rcprocess.BootedString;
				this.detailForm.FillCustomCommand(rcprocess.CustomCommandString);
				this.textBoxStandardOutLogLines.Text = rcprocess.LogLines.ToString();
				this.detailForm.ShutdownString = rcprocess.ShutdownString;
				this.detailForm.PerformanceString = rcprocess.PerformanceString;
				this.detailForm.FillPerformanceDescription(rcprocess.PerformanceDescription);
				this.checkBoxUse.Checked = rcprocess.DefaultSelect;
				this.checkBoxAutomaticStart.Checked = rcprocess.AutomaticStart;
				this.checkBoxRunOnce.Checked = rcprocess.AutomaticRestart;
				this.checkBoxPerformance.Checked = rcprocess.CheckPerformance;
				this.detailForm.TraceChildProcess = rcprocess.TraceChildProcess;
				this.detailForm.MaxChildProcessCount = rcprocess.MaxChildProcessCount;
				this.detailForm.ChildProcessLogStr = rcprocess.ChildProcessLogStr;
			}
		}

		private void BtnDetail_Click(object sender, EventArgs e)
		{
			this.bDetailExpanded = !this.bDetailExpanded;
			this.BtnDetail.Text = (this.bDetailExpanded ? "<<" : ">>");
			if (this.bDetailExpanded)
			{
				this.RelocateDetailForm();
				this.detailForm.Show(base.Parent);
				base.Focus();
				return;
			}
			this.detailForm.Hide();
		}

		private void buttonScheduleAdd_Click(object sender, EventArgs e)
		{
			SchedulerSettingForm schedulerSettingForm = new SchedulerSettingForm();
			while (schedulerSettingForm.ShowDialog() == DialogResult.OK)
			{
				if (!this.listViewSchedule.Items.ContainsKey(schedulerSettingForm.ScheduleName))
				{
					RCProcessScheduler schedule = new RCProcessScheduler(schedulerSettingForm.ScheduleName, schedulerSettingForm.ScheduleType, schedulerSettingForm.ScheduleTime, schedulerSettingForm.ExeType, schedulerSettingForm.Command, true);
					this.AddProcessSchedule(schedule);
					return;
				}
				Utility.ShowErrorMessage(LocalizeText.Get(451));
			}
		}

		private void buttonScheduleSub_Click(object sender, EventArgs e)
		{
			if (this.listViewSchedule.SelectedIndices.Count > 0)
			{
				this.listViewSchedule.Items.RemoveAt(this.listViewSchedule.SelectedIndices[0]);
			}
		}

		private void AddProcessSchedule(RCProcessScheduler schedule)
		{
			ListViewItem listViewItem = this.listViewSchedule.Items.Add(schedule.Name, "", 0);
			listViewItem.SubItems.Add(schedule.Name);
			listViewItem.SubItems.Add("");
			listViewItem.SubItems.Add("");
			this.ModifyProcessSchedule(schedule);
		}

		private void ModifyProcessSchedule(RCProcessScheduler schedule)
		{
			ListViewItem listViewItem = this.listViewSchedule.Items[schedule.Name];
			listViewItem.Tag = schedule;
			string text;
			if (schedule.ScheduleType == RCProcessScheduler.EScheduleType.Once)
			{
				text = string.Format("{0}({1} {2})", SchedulerSettingForm.GetScheduleTypeStr(schedule.ScheduleType), schedule.ScheduleTime.ToShortDateString(), schedule.ScheduleTime.ToShortTimeString());
			}
			else
			{
				text = SchedulerSettingForm.GetScheduleTypeStr(schedule.ScheduleType);
			}
			listViewItem.SubItems[2].Text = text;
			listViewItem.SubItems[3].Text = schedule.Command;
			listViewItem.StateImageIndex = (schedule.Enabled ? 0 : 1);
		}

		private void listViewSchedule_DoubleClick(object sender, EventArgs e)
		{
			if (!this._editable)
			{
				return;
			}
			if (this.listViewSchedule.SelectedItems.Count > 0)
			{
				ListViewItem listViewItem = this.listViewSchedule.SelectedItems[0];
				RCProcessScheduler rcprocessScheduler = listViewItem.Tag as RCProcessScheduler;
				SchedulerSettingForm schedulerSettingForm = new SchedulerSettingForm(rcprocessScheduler.Name, rcprocessScheduler.ScheduleType, rcprocessScheduler.ScheduleTime, rcprocessScheduler.ExeType, rcprocessScheduler.Command, rcprocessScheduler.Enabled);
				if (schedulerSettingForm.ShowDialog() == DialogResult.OK)
				{
					RCProcessScheduler schedule = new RCProcessScheduler(rcprocessScheduler.Name, schedulerSettingForm.ScheduleType, schedulerSettingForm.ScheduleTime, schedulerSettingForm.ExeType, schedulerSettingForm.Command, schedulerSettingForm.ScheduleEnabled);
					this.ModifyProcessSchedule(schedule);
				}
			}
		}

		private void ProcessPropertyForm_LocationChanged(object sender, EventArgs e)
		{
			if (this.bDetailExpanded)
			{
				this.RelocateDetailForm();
			}
		}

		private void RelocateDetailForm()
		{
			this.detailForm.Location = new Point(base.Right + SystemInformation.FrameBorderSize.Width + 2, base.Top);
		}

		private bool _editable;

		private bool bDetailExpanded;

		private ProcessPropertyDetailForm detailForm;

		private RCProcess _process;
	}
}
