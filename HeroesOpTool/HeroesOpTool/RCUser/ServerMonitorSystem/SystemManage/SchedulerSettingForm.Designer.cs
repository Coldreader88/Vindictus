namespace HeroesOpTool.RCUser.ServerMonitorSystem.SystemManage
{
	public partial class SchedulerSettingForm : global::System.Windows.Forms.Form
	{
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			this.buttonOK = new global::System.Windows.Forms.Button();
			this.buttonCancel = new global::System.Windows.Forms.Button();
			this.groupBoxSchedule = new global::System.Windows.Forms.GroupBox();
			this.DateTimeScheduleTime = new global::System.Windows.Forms.DateTimePicker();
			this.labelScheduleTime = new global::System.Windows.Forms.Label();
			this.labelScheduleType = new global::System.Windows.Forms.Label();
			this.comboBoxScheduleType = new global::System.Windows.Forms.ComboBox();
			this.groupBoxBasic = new global::System.Windows.Forms.GroupBox();
			this.checkBoxDisabled = new global::System.Windows.Forms.CheckBox();
			this.labelName = new global::System.Windows.Forms.Label();
			this.textBoxName = new global::System.Windows.Forms.TextBox();
			this.groupBoxExe = new global::System.Windows.Forms.GroupBox();
			this.labelExeType = new global::System.Windows.Forms.Label();
			this.textBoxExe = new global::System.Windows.Forms.TextBox();
			this.comboBoxExeType = new global::System.Windows.Forms.ComboBox();
			this.errorProviderAll = new global::System.Windows.Forms.ErrorProvider(this.components);
			this.groupBoxSchedule.SuspendLayout();
			this.groupBoxBasic.SuspendLayout();
			this.groupBoxExe.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.errorProviderAll).BeginInit();
			base.SuspendLayout();
			this.buttonOK.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonOK.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new global::System.Drawing.Point(42, 332);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new global::System.Drawing.Size(104, 24);
			this.buttonOK.TabIndex = 3;
			this.buttonOK.Text = "확인";
			this.buttonCancel.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonCancel.CausesValidation = false;
			this.buttonCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new global::System.Drawing.Point(152, 332);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new global::System.Drawing.Size(104, 24);
			this.buttonCancel.TabIndex = 4;
			this.buttonCancel.Text = "취소";
			this.groupBoxSchedule.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.groupBoxSchedule.Controls.Add(this.DateTimeScheduleTime);
			this.groupBoxSchedule.Controls.Add(this.labelScheduleTime);
			this.groupBoxSchedule.Controls.Add(this.labelScheduleType);
			this.groupBoxSchedule.Controls.Add(this.comboBoxScheduleType);
			this.groupBoxSchedule.Location = new global::System.Drawing.Point(8, 114);
			this.groupBoxSchedule.Name = "groupBoxSchedule";
			this.groupBoxSchedule.Size = new global::System.Drawing.Size(289, 77);
			this.groupBoxSchedule.TabIndex = 1;
			this.groupBoxSchedule.TabStop = false;
			this.groupBoxSchedule.Text = "실행 계획";
			this.DateTimeScheduleTime.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.DateTimeScheduleTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
			this.DateTimeScheduleTime.Enabled = false;
			this.DateTimeScheduleTime.Format = global::System.Windows.Forms.DateTimePickerFormat.Custom;
			this.DateTimeScheduleTime.Location = new global::System.Drawing.Point(118, 44);
			this.DateTimeScheduleTime.Name = "DateTimeScheduleTime";
			this.DateTimeScheduleTime.Size = new global::System.Drawing.Size(161, 21);
			this.DateTimeScheduleTime.TabIndex = 3;
			this.labelScheduleTime.Location = new global::System.Drawing.Point(6, 44);
			this.labelScheduleTime.Name = "labelScheduleTime";
			this.labelScheduleTime.Size = new global::System.Drawing.Size(112, 24);
			this.labelScheduleTime.TabIndex = 2;
			this.labelScheduleTime.Text = "날짜";
			this.labelScheduleTime.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.labelScheduleType.Location = new global::System.Drawing.Point(6, 17);
			this.labelScheduleType.Name = "labelScheduleType";
			this.labelScheduleType.Size = new global::System.Drawing.Size(112, 24);
			this.labelScheduleType.TabIndex = 0;
			this.labelScheduleType.Text = "실행 시점";
			this.labelScheduleType.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.comboBoxScheduleType.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.comboBoxScheduleType.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxScheduleType.FormattingEnabled = true;
			this.comboBoxScheduleType.Location = new global::System.Drawing.Point(118, 17);
			this.comboBoxScheduleType.Name = "comboBoxScheduleType";
			this.comboBoxScheduleType.Size = new global::System.Drawing.Size(161, 20);
			this.comboBoxScheduleType.TabIndex = 1;
			this.comboBoxScheduleType.SelectedIndexChanged += new global::System.EventHandler(this.comboBoxScheduleType_SelectedIndexChanged);
			this.groupBoxBasic.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.groupBoxBasic.Controls.Add(this.checkBoxDisabled);
			this.groupBoxBasic.Controls.Add(this.labelName);
			this.groupBoxBasic.Controls.Add(this.textBoxName);
			this.groupBoxBasic.Location = new global::System.Drawing.Point(8, 12);
			this.groupBoxBasic.Name = "groupBoxBasic";
			this.groupBoxBasic.Size = new global::System.Drawing.Size(289, 76);
			this.groupBoxBasic.TabIndex = 0;
			this.groupBoxBasic.TabStop = false;
			this.groupBoxBasic.Text = "기본 정보";
			this.checkBoxDisabled.AutoSize = true;
			this.checkBoxDisabled.Location = new global::System.Drawing.Point(8, 48);
			this.checkBoxDisabled.Name = "checkBoxDisabled";
			this.checkBoxDisabled.Size = new global::System.Drawing.Size(100, 16);
			this.checkBoxDisabled.TabIndex = 2;
			this.checkBoxDisabled.Text = "사용하지 않음";
			this.checkBoxDisabled.UseVisualStyleBackColor = true;
			this.labelName.Location = new global::System.Drawing.Point(6, 20);
			this.labelName.Name = "labelName";
			this.labelName.Size = new global::System.Drawing.Size(112, 24);
			this.labelName.TabIndex = 1;
			this.labelName.Text = "이름";
			this.labelName.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.textBoxName.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxName.Location = new global::System.Drawing.Point(118, 20);
			this.textBoxName.MaxLength = 256;
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new global::System.Drawing.Size(161, 21);
			this.textBoxName.TabIndex = 0;
			this.textBoxName.Validating += new global::System.ComponentModel.CancelEventHandler(this.textBoxName_Validating);
			this.groupBoxExe.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.groupBoxExe.Controls.Add(this.labelExeType);
			this.groupBoxExe.Controls.Add(this.textBoxExe);
			this.groupBoxExe.Controls.Add(this.comboBoxExeType);
			this.groupBoxExe.Location = new global::System.Drawing.Point(8, 197);
			this.groupBoxExe.Name = "groupBoxExe";
			this.groupBoxExe.Size = new global::System.Drawing.Size(289, 124);
			this.groupBoxExe.TabIndex = 2;
			this.groupBoxExe.TabStop = false;
			this.groupBoxExe.Text = "명령";
			this.labelExeType.Location = new global::System.Drawing.Point(6, 20);
			this.labelExeType.Name = "labelExeType";
			this.labelExeType.Size = new global::System.Drawing.Size(112, 24);
			this.labelExeType.TabIndex = 0;
			this.labelExeType.Text = "타입";
			this.labelExeType.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.textBoxExe.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxExe.Location = new global::System.Drawing.Point(8, 47);
			this.textBoxExe.MaxLength = 256;
			this.textBoxExe.Multiline = true;
			this.textBoxExe.Name = "textBoxExe";
			this.textBoxExe.Size = new global::System.Drawing.Size(271, 65);
			this.textBoxExe.TabIndex = 2;
			this.comboBoxExeType.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.comboBoxExeType.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxExeType.FormattingEnabled = true;
			this.comboBoxExeType.Location = new global::System.Drawing.Point(118, 20);
			this.comboBoxExeType.Name = "comboBoxExeType";
			this.comboBoxExeType.Size = new global::System.Drawing.Size(161, 20);
			this.comboBoxExeType.TabIndex = 1;
			this.errorProviderAll.ContainerControl = this;
			this.errorProviderAll.RightToLeft = true;
			base.AcceptButton = this.buttonOK;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 12f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.buttonCancel;
			base.ClientSize = new global::System.Drawing.Size(309, 368);
			base.Controls.Add(this.groupBoxExe);
			base.Controls.Add(this.groupBoxBasic);
			base.Controls.Add(this.groupBoxSchedule);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.buttonCancel);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SchedulerSettingForm";
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "스케쥴러 설정";
			this.groupBoxSchedule.ResumeLayout(false);
			this.groupBoxBasic.ResumeLayout(false);
			this.groupBoxBasic.PerformLayout();
			this.groupBoxExe.ResumeLayout(false);
			this.groupBoxExe.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.errorProviderAll).EndInit();
			base.ResumeLayout(false);
		}

		private global::System.ComponentModel.IContainer components;

		private global::System.Windows.Forms.Button buttonOK;

		private global::System.Windows.Forms.Button buttonCancel;

		private global::System.Windows.Forms.GroupBox groupBoxSchedule;

		private global::System.Windows.Forms.Label labelScheduleTime;

		private global::System.Windows.Forms.Label labelScheduleType;

		private global::System.Windows.Forms.ComboBox comboBoxScheduleType;

		private global::System.Windows.Forms.GroupBox groupBoxBasic;

		private global::System.Windows.Forms.TextBox textBoxName;

		private global::System.Windows.Forms.DateTimePicker DateTimeScheduleTime;

		private global::System.Windows.Forms.GroupBox groupBoxExe;

		private global::System.Windows.Forms.TextBox textBoxExe;

		private global::System.Windows.Forms.Label labelName;

		private global::System.Windows.Forms.Label labelExeType;

		private global::System.Windows.Forms.ComboBox comboBoxExeType;

		private global::System.Windows.Forms.ErrorProvider errorProviderAll;

		private global::System.Windows.Forms.CheckBox checkBoxDisabled;
	}
}
