namespace HeroesOpTool.RCUser.ServerMonitorSystem
{
	public partial class ServerCommandForm : global::HeroesOpTool.RCUser.ServerMonitorSystem.CommandForm
	{
		//protected override void Dispose(bool disposing)
		//{
		//	if (disposing && this.components != null)
		//	{
		//		this.components.Dispose();
		//	}
		//	base.Dispose(disposing);
		//}

		private void InitializeComponent()
		{
			this.groupBoxCommand = new global::System.Windows.Forms.GroupBox();
			this.listBoxCommand = new global::System.Windows.Forms.ListBox();
			this.groupBoxServer = new global::System.Windows.Forms.GroupBox();
			this.checkedListBoxServer = new global::System.Windows.Forms.CheckedListBox();
			this.groupBoxArgList = new global::System.Windows.Forms.GroupBox();
			this.panelArgList = new global::System.Windows.Forms.Panel();
			this.panelMid = new global::System.Windows.Forms.Panel();
			this.panelBottom = new global::System.Windows.Forms.Panel();
			this.dateTimePicker = new global::System.Windows.Forms.DateTimePicker();
			this.checkSchedule = new global::System.Windows.Forms.CheckBox();
			this.buttonCancel = new global::System.Windows.Forms.Button();
			this.buttonOK = new global::System.Windows.Forms.Button();
			this.panelTop = new global::System.Windows.Forms.Panel();
			this.textBox1 = new global::System.Windows.Forms.TextBox();
			this.panelSchdule = new global::System.Windows.Forms.Panel();
			global::System.Windows.Forms.SplitContainer splitContainer = new global::System.Windows.Forms.SplitContainer();
			global::System.Windows.Forms.SplitContainer splitContainer2 = new global::System.Windows.Forms.SplitContainer();
			splitContainer.Panel1.SuspendLayout();
			splitContainer.Panel2.SuspendLayout();
			splitContainer.SuspendLayout();
			this.groupBoxCommand.SuspendLayout();
			splitContainer2.Panel1.SuspendLayout();
			splitContainer2.Panel2.SuspendLayout();
			splitContainer2.SuspendLayout();
			this.groupBoxServer.SuspendLayout();
			this.groupBoxArgList.SuspendLayout();
			this.panelMid.SuspendLayout();
			this.panelBottom.SuspendLayout();
			this.panelTop.SuspendLayout();
			base.SuspendLayout();
			splitContainer.Dock = global::System.Windows.Forms.DockStyle.Fill;
			splitContainer.Location = new global::System.Drawing.Point(0, 0);
			splitContainer.Name = "splitContainerLeftRight";
			splitContainer.Panel1.Controls.Add(this.groupBoxCommand);
			splitContainer.Panel2.Controls.Add(splitContainer2);
			splitContainer.Size = new global::System.Drawing.Size(530, 291);
			splitContainer.SplitterDistance = 186;
			splitContainer.TabIndex = 1;
			this.groupBoxCommand.Controls.Add(this.listBoxCommand);
			this.groupBoxCommand.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.groupBoxCommand.Location = new global::System.Drawing.Point(0, 0);
			this.groupBoxCommand.Name = "groupBoxCommand";
			this.groupBoxCommand.Size = new global::System.Drawing.Size(186, 291);
			this.groupBoxCommand.TabIndex = 5;
			this.groupBoxCommand.TabStop = false;
			this.groupBoxCommand.Text = "사용 가능한 명령";
			this.listBoxCommand.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.listBoxCommand.FormattingEnabled = true;
			this.listBoxCommand.ItemHeight = 12;
			this.listBoxCommand.Location = new global::System.Drawing.Point(3, 17);
			this.listBoxCommand.Name = "listBoxCommand";
			this.listBoxCommand.Size = new global::System.Drawing.Size(180, 268);
			this.listBoxCommand.TabIndex = 0;
			this.listBoxCommand.SelectedIndexChanged += new global::System.EventHandler(this.listBoxCommand_SelectedIndexChanged);
			splitContainer2.Dock = global::System.Windows.Forms.DockStyle.Fill;
			splitContainer2.Location = new global::System.Drawing.Point(0, 0);
			splitContainer2.Name = "splitContainerRightTopBottom";
			splitContainer2.Orientation = global::System.Windows.Forms.Orientation.Horizontal;
			splitContainer2.Panel1.Controls.Add(this.groupBoxServer);
			splitContainer2.Panel2.Controls.Add(this.groupBoxArgList);
			splitContainer2.Size = new global::System.Drawing.Size(340, 291);
			splitContainer2.SplitterDistance = 66;
			splitContainer2.TabIndex = 0;
			this.groupBoxServer.Controls.Add(this.checkedListBoxServer);
			this.groupBoxServer.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.groupBoxServer.Location = new global::System.Drawing.Point(0, 0);
			this.groupBoxServer.Name = "groupBoxServer";
			this.groupBoxServer.Size = new global::System.Drawing.Size(340, 66);
			this.groupBoxServer.TabIndex = 4;
			this.groupBoxServer.TabStop = false;
			this.groupBoxServer.Text = "서버군";
			this.checkedListBoxServer.CheckOnClick = true;
			this.checkedListBoxServer.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.checkedListBoxServer.FormattingEnabled = true;
			this.checkedListBoxServer.Location = new global::System.Drawing.Point(3, 17);
			this.checkedListBoxServer.Name = "checkedListBoxServer";
			this.checkedListBoxServer.Size = new global::System.Drawing.Size(334, 36);
			this.checkedListBoxServer.TabIndex = 0;
			this.groupBoxArgList.Controls.Add(this.panelArgList);
			this.groupBoxArgList.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.groupBoxArgList.Location = new global::System.Drawing.Point(0, 0);
			this.groupBoxArgList.Name = "groupBoxArgList";
			this.groupBoxArgList.Size = new global::System.Drawing.Size(340, 221);
			this.groupBoxArgList.TabIndex = 6;
			this.groupBoxArgList.TabStop = false;
			this.groupBoxArgList.Text = "실행 인자";
			this.panelArgList.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.panelArgList.Location = new global::System.Drawing.Point(3, 17);
			this.panelArgList.Name = "panelArgList";
			this.panelArgList.Size = new global::System.Drawing.Size(334, 201);
			this.panelArgList.TabIndex = 0;
			this.panelMid.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.panelMid.BackColor = global::System.Drawing.Color.Transparent;
			this.panelMid.Controls.Add(splitContainer);
			this.panelMid.Location = new global::System.Drawing.Point(6, 35);
			this.panelMid.Name = "panelMid";
			this.panelMid.Size = new global::System.Drawing.Size(530, 291);
			this.panelMid.TabIndex = 0;
			this.panelBottom.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.panelBottom.BackColor = global::System.Drawing.SystemColors.Control;
			this.panelBottom.Controls.Add(this.dateTimePicker);
			this.panelBottom.Controls.Add(this.checkSchedule);
			this.panelBottom.Controls.Add(this.buttonCancel);
			this.panelBottom.Controls.Add(this.buttonOK);
			this.panelBottom.Location = new global::System.Drawing.Point(0, 332);
			this.panelBottom.Name = "panelBottom";
			this.panelBottom.Size = new global::System.Drawing.Size(542, 49);
			this.panelBottom.TabIndex = 1;
			this.dateTimePicker.CustomFormat = "yyyy-MM-dd dddd HH:mm";
			this.dateTimePicker.Enabled = false;
			this.dateTimePicker.Format = global::System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePicker.Location = new global::System.Drawing.Point(57, 13);
			this.dateTimePicker.Name = "dateTimePicker";
			this.dateTimePicker.Size = new global::System.Drawing.Size(171, 21);
			this.dateTimePicker.TabIndex = 9;
			this.checkSchedule.AutoSize = true;
			this.checkSchedule.Location = new global::System.Drawing.Point(9, 15);
			this.checkSchedule.Name = "checkSchedule";
			this.checkSchedule.Size = new global::System.Drawing.Size(48, 16);
			this.checkSchedule.TabIndex = 8;
			this.checkSchedule.Text = "예약";
			this.checkSchedule.UseVisualStyleBackColor = true;
			this.checkSchedule.CheckedChanged += new global::System.EventHandler(this.checkSchedule_CheckedChanged);
			this.buttonCancel.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.buttonCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new global::System.Drawing.Point(434, 8);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new global::System.Drawing.Size(96, 29);
			this.buttonCancel.TabIndex = 7;
			this.buttonCancel.Text = "취소";
			this.buttonOK.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.buttonOK.FlatAppearance.BorderSize = 0;
			this.buttonOK.Location = new global::System.Drawing.Point(332, 8);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new global::System.Drawing.Size(96, 29);
			this.buttonOK.TabIndex = 6;
			this.buttonOK.Text = "실행";
			this.buttonOK.Click += new global::System.EventHandler(this.buttonOK_Click);
			this.panelTop.BackColor = global::System.Drawing.SystemColors.Control;
			this.panelTop.Controls.Add(this.textBox1);
			this.panelTop.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.panelTop.Location = new global::System.Drawing.Point(0, 0);
			this.panelTop.Name = "panelTop";
			this.panelTop.Size = new global::System.Drawing.Size(542, 29);
			this.panelTop.TabIndex = 2;
			this.textBox1.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
			this.textBox1.Cursor = global::System.Windows.Forms.Cursors.Default;
			this.textBox1.Location = new global::System.Drawing.Point(9, 3);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new global::System.Drawing.Size(411, 25);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "이 곳의 명령은 서버군 전체에 적용됩니다.\r\n명령은 CommandBridge 속성을 가진 프로세스에서 설정됩니다.";
			this.panelSchdule.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.panelSchdule.AutoSize = true;
			this.panelSchdule.BackColor = global::System.Drawing.SystemColors.Window;
			this.panelSchdule.Location = new global::System.Drawing.Point(0, 380);
			this.panelSchdule.Name = "panelSchdule";
			this.panelSchdule.Size = new global::System.Drawing.Size(542, 10);
			this.panelSchdule.TabIndex = 3;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 12f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.SystemColors.Window;
			base.CancelButton = this.buttonCancel;
			base.ClientSize = new global::System.Drawing.Size(542, 388);
			base.Controls.Add(this.panelSchdule);
			base.Controls.Add(this.panelTop);
			base.Controls.Add(this.panelBottom);
			base.Controls.Add(this.panelMid);
			base.Name = "ServerCommandForm";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "서버 명령을 선택하세요.";
			splitContainer.Panel1.ResumeLayout(false);
			splitContainer.Panel2.ResumeLayout(false);
			splitContainer.ResumeLayout(false);
			this.groupBoxCommand.ResumeLayout(false);
			splitContainer2.Panel1.ResumeLayout(false);
			splitContainer2.Panel2.ResumeLayout(false);
			splitContainer2.ResumeLayout(false);
			this.groupBoxServer.ResumeLayout(false);
			this.groupBoxArgList.ResumeLayout(false);
			this.panelMid.ResumeLayout(false);
			this.panelBottom.ResumeLayout(false);
			this.panelBottom.PerformLayout();
			this.panelTop.ResumeLayout(false);
			this.panelTop.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		//private global::System.ComponentModel.IContainer components;

		private global::System.Windows.Forms.GroupBox groupBoxCommand;

		private global::System.Windows.Forms.ListBox listBoxCommand;

		private global::System.Windows.Forms.GroupBox groupBoxServer;

		private global::System.Windows.Forms.CheckedListBox checkedListBoxServer;

		private global::System.Windows.Forms.GroupBox groupBoxArgList;

		private global::System.Windows.Forms.Button buttonCancel;

		private global::System.Windows.Forms.Button buttonOK;

		private global::System.Windows.Forms.Panel panelArgList;

		private global::System.Windows.Forms.Panel panelTop;

		private global::System.Windows.Forms.TextBox textBox1;

		private global::System.Windows.Forms.Panel panelSchdule;

		private global::System.Windows.Forms.Panel panelMid;

		private global::System.Windows.Forms.Panel panelBottom;

		private global::System.Windows.Forms.CheckBox checkSchedule;

		private global::System.Windows.Forms.DateTimePicker dateTimePicker;
	}
}
