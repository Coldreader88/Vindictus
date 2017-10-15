namespace HeroesOpTool.RCUser.ServerMonitorSystem.SystemManage
{
	public partial class ProcessPropertyDetailForm : global::System.Windows.Forms.Form
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
			this.groupBoxStandardInProperty = new global::System.Windows.Forms.GroupBox();
			this.listViewCustomCommand = new global::System.Windows.Forms.ListView();
			this.columnHeader1 = new global::System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new global::System.Windows.Forms.ColumnHeader();
			this.labelBootedString = new global::System.Windows.Forms.Label();
			this.textBoxBootedString = new global::System.Windows.Forms.TextBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.buttonCustomCommandDown = new global::System.Windows.Forms.Button();
			this.buttonCustomCommandAdd = new global::System.Windows.Forms.Button();
			this.buttonCustomCommandSub = new global::System.Windows.Forms.Button();
			this.buttonCustomCommandUp = new global::System.Windows.Forms.Button();
			this.groupBoxStandardOutProperty = new global::System.Windows.Forms.GroupBox();
			this.buttonPerformanceDescSub = new global::System.Windows.Forms.Button();
			this.listViewPerformanceDescription = new global::System.Windows.Forms.ListView();
			this.columnHeader3 = new global::System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new global::System.Windows.Forms.ColumnHeader();
			this.textBoxShutdownString = new global::System.Windows.Forms.TextBox();
			this.labelPerformanceString = new global::System.Windows.Forms.Label();
			this.textBoxPerformanceString = new global::System.Windows.Forms.TextBox();
			this.labelShutdownString = new global::System.Windows.Forms.Label();
			this.labelPerformanceDescription = new global::System.Windows.Forms.Label();
			this.buttonPerformanceDescAdd = new global::System.Windows.Forms.Button();
			this.buttonPerformanceDescDown = new global::System.Windows.Forms.Button();
			this.buttonPerformanceDescUp = new global::System.Windows.Forms.Button();
			this.toolTipProperty = new global::System.Windows.Forms.ToolTip(this.components);
			this.groupBoxProperty = new global::System.Windows.Forms.GroupBox();
			this.buttonPropertyDel = new global::System.Windows.Forms.Button();
			this.buttonPropertyAdd = new global::System.Windows.Forms.Button();
			this.listViewProperty = new global::System.Windows.Forms.ListView();
			this.groupBoxChild = new global::System.Windows.Forms.GroupBox();
			this.textBoxChildProcessLogStr = new global::System.Windows.Forms.TextBox();
			this.labelChildProcessLogStr = new global::System.Windows.Forms.Label();
			this.textBoxMaxChildProcessCount = new global::HeroesOpTool.Utility.NumericTextBox();
			this.labelMaxChildProcessCount = new global::System.Windows.Forms.Label();
			this.checkBoxTraceChildProcess = new global::System.Windows.Forms.CheckBox();
			global::System.Windows.Forms.ColumnHeader columnHeader = new global::System.Windows.Forms.ColumnHeader();
			global::System.Windows.Forms.ColumnHeader columnHeader2 = new global::System.Windows.Forms.ColumnHeader();
			this.groupBoxStandardInProperty.SuspendLayout();
			this.groupBoxStandardOutProperty.SuspendLayout();
			this.groupBoxProperty.SuspendLayout();
			this.groupBoxChild.SuspendLayout();
			base.SuspendLayout();
			columnHeader.Text = "Key";
			columnHeader.Width = 151;
			columnHeader2.Text = "Value";
			columnHeader2.Width = 216;
			this.groupBoxStandardInProperty.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.groupBoxStandardInProperty.Controls.Add(this.listViewCustomCommand);
			this.groupBoxStandardInProperty.Controls.Add(this.labelBootedString);
			this.groupBoxStandardInProperty.Controls.Add(this.textBoxBootedString);
			this.groupBoxStandardInProperty.Controls.Add(this.label1);
			this.groupBoxStandardInProperty.Controls.Add(this.buttonCustomCommandDown);
			this.groupBoxStandardInProperty.Controls.Add(this.buttonCustomCommandAdd);
			this.groupBoxStandardInProperty.Controls.Add(this.buttonCustomCommandSub);
			this.groupBoxStandardInProperty.Controls.Add(this.buttonCustomCommandUp);
			this.groupBoxStandardInProperty.Location = new global::System.Drawing.Point(6, 12);
			this.groupBoxStandardInProperty.Name = "groupBoxStandardInProperty";
			this.groupBoxStandardInProperty.Size = new global::System.Drawing.Size(424, 196);
			this.groupBoxStandardInProperty.TabIndex = 4;
			this.groupBoxStandardInProperty.TabStop = false;
			this.groupBoxStandardInProperty.Text = "표준 입력 관련 설정";
			this.listViewCustomCommand.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.listViewCustomCommand.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.columnHeader1,
				this.columnHeader2
			});
			this.listViewCustomCommand.FullRowSelect = true;
			this.listViewCustomCommand.HeaderStyle = global::System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listViewCustomCommand.HideSelection = false;
			this.listViewCustomCommand.Location = new global::System.Drawing.Point(40, 64);
			this.listViewCustomCommand.MultiSelect = false;
			this.listViewCustomCommand.Name = "listViewCustomCommand";
			this.listViewCustomCommand.ShowGroups = false;
			this.listViewCustomCommand.Size = new global::System.Drawing.Size(376, 126);
			this.listViewCustomCommand.TabIndex = 1;
			this.listViewCustomCommand.UseCompatibleStateImageBehavior = false;
			this.listViewCustomCommand.View = global::System.Windows.Forms.View.Details;
			this.listViewCustomCommand.DoubleClick += new global::System.EventHandler(this.ListViewCustomCommand_DoubleClick);
			this.columnHeader1.Text = "명령 이름";
			this.columnHeader1.Width = 120;
			this.columnHeader2.Text = "정의된 명령";
			this.columnHeader2.Width = 246;
			this.labelBootedString.Location = new global::System.Drawing.Point(8, 16);
			this.labelBootedString.Name = "labelBootedString";
			this.labelBootedString.Size = new global::System.Drawing.Size(112, 24);
			this.labelBootedString.TabIndex = 34;
			this.labelBootedString.Text = "부팅 완료 출력문";
			this.labelBootedString.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.textBoxBootedString.Location = new global::System.Drawing.Point(120, 16);
			this.textBoxBootedString.MaxLength = 32;
			this.textBoxBootedString.Name = "textBoxBootedString";
			this.textBoxBootedString.Size = new global::System.Drawing.Size(136, 21);
			this.textBoxBootedString.TabIndex = 0;
			this.label1.Location = new global::System.Drawing.Point(8, 40);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(112, 24);
			this.label1.TabIndex = 34;
			this.label1.Text = "사용자 정의 입력문";
			this.label1.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonCustomCommandDown.Location = new global::System.Drawing.Point(10, 136);
			this.buttonCustomCommandDown.Name = "buttonCustomCommandDown";
			this.buttonCustomCommandDown.Size = new global::System.Drawing.Size(24, 24);
			this.buttonCustomCommandDown.TabIndex = 3;
			this.buttonCustomCommandDown.Text = "↓";
			this.buttonCustomCommandDown.Click += new global::System.EventHandler(this.buttonCustomCommandDown_Click);
			this.buttonCustomCommandAdd.Location = new global::System.Drawing.Point(10, 64);
			this.buttonCustomCommandAdd.Name = "buttonCustomCommandAdd";
			this.buttonCustomCommandAdd.Size = new global::System.Drawing.Size(24, 24);
			this.buttonCustomCommandAdd.TabIndex = 4;
			this.buttonCustomCommandAdd.Text = "＋";
			this.buttonCustomCommandAdd.Click += new global::System.EventHandler(this.buttonCustomCommandAdd_Click);
			this.buttonCustomCommandSub.Location = new global::System.Drawing.Point(10, 88);
			this.buttonCustomCommandSub.Name = "buttonCustomCommandSub";
			this.buttonCustomCommandSub.Size = new global::System.Drawing.Size(24, 24);
			this.buttonCustomCommandSub.TabIndex = 5;
			this.buttonCustomCommandSub.Text = "－";
			this.buttonCustomCommandSub.Click += new global::System.EventHandler(this.buttonCustomCommandSub_Click);
			this.buttonCustomCommandUp.Location = new global::System.Drawing.Point(10, 112);
			this.buttonCustomCommandUp.Name = "buttonCustomCommandUp";
			this.buttonCustomCommandUp.Size = new global::System.Drawing.Size(24, 24);
			this.buttonCustomCommandUp.TabIndex = 2;
			this.buttonCustomCommandUp.Text = "↑";
			this.buttonCustomCommandUp.Click += new global::System.EventHandler(this.buttonCustomCommandUp_Click);
			this.groupBoxStandardOutProperty.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.groupBoxStandardOutProperty.Controls.Add(this.buttonPerformanceDescSub);
			this.groupBoxStandardOutProperty.Controls.Add(this.listViewPerformanceDescription);
			this.groupBoxStandardOutProperty.Controls.Add(this.textBoxShutdownString);
			this.groupBoxStandardOutProperty.Controls.Add(this.labelPerformanceString);
			this.groupBoxStandardOutProperty.Controls.Add(this.textBoxPerformanceString);
			this.groupBoxStandardOutProperty.Controls.Add(this.labelShutdownString);
			this.groupBoxStandardOutProperty.Controls.Add(this.labelPerformanceDescription);
			this.groupBoxStandardOutProperty.Controls.Add(this.buttonPerformanceDescAdd);
			this.groupBoxStandardOutProperty.Controls.Add(this.buttonPerformanceDescDown);
			this.groupBoxStandardOutProperty.Controls.Add(this.buttonPerformanceDescUp);
			this.groupBoxStandardOutProperty.Location = new global::System.Drawing.Point(6, 214);
			this.groupBoxStandardOutProperty.Name = "groupBoxStandardOutProperty";
			this.groupBoxStandardOutProperty.Size = new global::System.Drawing.Size(424, 220);
			this.groupBoxStandardOutProperty.TabIndex = 5;
			this.groupBoxStandardOutProperty.TabStop = false;
			this.groupBoxStandardOutProperty.Text = "표준 출력 관련 설정";
			this.buttonPerformanceDescSub.Location = new global::System.Drawing.Point(10, 112);
			this.buttonPerformanceDescSub.Name = "buttonPerformanceDescSub";
			this.buttonPerformanceDescSub.Size = new global::System.Drawing.Size(24, 24);
			this.buttonPerformanceDescSub.TabIndex = 7;
			this.buttonPerformanceDescSub.Text = "－";
			this.buttonPerformanceDescSub.Click += new global::System.EventHandler(this.buttonPerformanceDescSub_Click);
			this.listViewPerformanceDescription.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.listViewPerformanceDescription.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.columnHeader3,
				this.columnHeader4
			});
			this.listViewPerformanceDescription.FullRowSelect = true;
			this.listViewPerformanceDescription.HeaderStyle = global::System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listViewPerformanceDescription.HideSelection = false;
			this.listViewPerformanceDescription.Location = new global::System.Drawing.Point(40, 88);
			this.listViewPerformanceDescription.MultiSelect = false;
			this.listViewPerformanceDescription.Name = "listViewPerformanceDescription";
			this.listViewPerformanceDescription.Size = new global::System.Drawing.Size(376, 126);
			this.listViewPerformanceDescription.TabIndex = 3;
			this.listViewPerformanceDescription.UseCompatibleStateImageBehavior = false;
			this.listViewPerformanceDescription.View = global::System.Windows.Forms.View.Details;
			this.listViewPerformanceDescription.DoubleClick += new global::System.EventHandler(this.ListViewPerformanceDescription_DoubleClick);
			this.columnHeader3.Text = "성능 이름";
			this.columnHeader3.Width = 120;
			this.columnHeader4.Text = "자세한 설명";
			this.columnHeader4.Width = 245;
			this.textBoxShutdownString.Location = new global::System.Drawing.Point(120, 16);
			this.textBoxShutdownString.MaxLength = 32;
			this.textBoxShutdownString.Name = "textBoxShutdownString";
			this.textBoxShutdownString.Size = new global::System.Drawing.Size(136, 21);
			this.textBoxShutdownString.TabIndex = 1;
			this.labelPerformanceString.Location = new global::System.Drawing.Point(8, 40);
			this.labelPerformanceString.Name = "labelPerformanceString";
			this.labelPerformanceString.Size = new global::System.Drawing.Size(112, 24);
			this.labelPerformanceString.TabIndex = 34;
			this.labelPerformanceString.Text = "성능 출력 접두문";
			this.labelPerformanceString.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.textBoxPerformanceString.Location = new global::System.Drawing.Point(120, 40);
			this.textBoxPerformanceString.MaxLength = 32;
			this.textBoxPerformanceString.Name = "textBoxPerformanceString";
			this.textBoxPerformanceString.Size = new global::System.Drawing.Size(136, 21);
			this.textBoxPerformanceString.TabIndex = 2;
			this.labelShutdownString.Location = new global::System.Drawing.Point(8, 16);
			this.labelShutdownString.Name = "labelShutdownString";
			this.labelShutdownString.Size = new global::System.Drawing.Size(112, 24);
			this.labelShutdownString.TabIndex = 34;
			this.labelShutdownString.Text = "종료 요구 입력문";
			this.labelShutdownString.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.labelPerformanceDescription.Location = new global::System.Drawing.Point(8, 64);
			this.labelPerformanceDescription.Name = "labelPerformanceDescription";
			this.labelPerformanceDescription.Size = new global::System.Drawing.Size(112, 24);
			this.labelPerformanceDescription.TabIndex = 34;
			this.labelPerformanceDescription.Text = "성능 출력문 설정";
			this.labelPerformanceDescription.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonPerformanceDescAdd.Location = new global::System.Drawing.Point(10, 88);
			this.buttonPerformanceDescAdd.Name = "buttonPerformanceDescAdd";
			this.buttonPerformanceDescAdd.Size = new global::System.Drawing.Size(24, 24);
			this.buttonPerformanceDescAdd.TabIndex = 6;
			this.buttonPerformanceDescAdd.Text = "＋";
			this.buttonPerformanceDescAdd.Click += new global::System.EventHandler(this.buttonPerformanceDescAdd_Click);
			this.buttonPerformanceDescDown.Location = new global::System.Drawing.Point(10, 160);
			this.buttonPerformanceDescDown.Name = "buttonPerformanceDescDown";
			this.buttonPerformanceDescDown.Size = new global::System.Drawing.Size(24, 24);
			this.buttonPerformanceDescDown.TabIndex = 5;
			this.buttonPerformanceDescDown.Text = "↓";
			this.buttonPerformanceDescDown.Click += new global::System.EventHandler(this.buttonPerformanceDescDown_Click);
			this.buttonPerformanceDescUp.Location = new global::System.Drawing.Point(10, 136);
			this.buttonPerformanceDescUp.Name = "buttonPerformanceDescUp";
			this.buttonPerformanceDescUp.Size = new global::System.Drawing.Size(24, 24);
			this.buttonPerformanceDescUp.TabIndex = 4;
			this.buttonPerformanceDescUp.Text = "↑";
			this.buttonPerformanceDescUp.Click += new global::System.EventHandler(this.buttonPerformanceDescUp_Click);
			this.groupBoxProperty.Controls.Add(this.buttonPropertyDel);
			this.groupBoxProperty.Controls.Add(this.buttonPropertyAdd);
			this.groupBoxProperty.Controls.Add(this.listViewProperty);
			this.groupBoxProperty.Location = new global::System.Drawing.Point(6, 550);
			this.groupBoxProperty.Name = "groupBoxProperty";
			this.groupBoxProperty.Size = new global::System.Drawing.Size(424, 136);
			this.groupBoxProperty.TabIndex = 6;
			this.groupBoxProperty.TabStop = false;
			this.groupBoxProperty.Text = "프로세스 속성";
			this.buttonPropertyDel.Location = new global::System.Drawing.Point(10, 44);
			this.buttonPropertyDel.Name = "buttonPropertyDel";
			this.buttonPropertyDel.Size = new global::System.Drawing.Size(24, 24);
			this.buttonPropertyDel.TabIndex = 36;
			this.buttonPropertyDel.Text = "－";
			this.buttonPropertyDel.Click += new global::System.EventHandler(this.buttonPropertyDel_Click);
			this.buttonPropertyAdd.Location = new global::System.Drawing.Point(10, 20);
			this.buttonPropertyAdd.Name = "buttonPropertyAdd";
			this.buttonPropertyAdd.Size = new global::System.Drawing.Size(24, 24);
			this.buttonPropertyAdd.TabIndex = 35;
			this.buttonPropertyAdd.Text = "＋";
			this.buttonPropertyAdd.Click += new global::System.EventHandler(this.buttonPropertyAdd_Click);
			this.listViewProperty.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				columnHeader,
				columnHeader2
			});
			this.listViewProperty.FullRowSelect = true;
			this.listViewProperty.HeaderStyle = global::System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listViewProperty.Location = new global::System.Drawing.Point(40, 21);
			this.listViewProperty.Name = "listViewProperty";
			this.listViewProperty.ShowGroups = false;
			this.listViewProperty.Size = new global::System.Drawing.Size(376, 109);
			this.listViewProperty.TabIndex = 0;
			this.listViewProperty.UseCompatibleStateImageBehavior = false;
			this.listViewProperty.View = global::System.Windows.Forms.View.Details;
			this.listViewProperty.DoubleClick += new global::System.EventHandler(this.listViewProperty_DoubleClick);
			this.groupBoxChild.Controls.Add(this.textBoxChildProcessLogStr);
			this.groupBoxChild.Controls.Add(this.labelChildProcessLogStr);
			this.groupBoxChild.Controls.Add(this.textBoxMaxChildProcessCount);
			this.groupBoxChild.Controls.Add(this.labelMaxChildProcessCount);
			this.groupBoxChild.Controls.Add(this.checkBoxTraceChildProcess);
			this.groupBoxChild.Location = new global::System.Drawing.Point(6, 441);
			this.groupBoxChild.Name = "groupBoxChild";
			this.groupBoxChild.Size = new global::System.Drawing.Size(424, 103);
			this.groupBoxChild.TabIndex = 8;
			this.groupBoxChild.TabStop = false;
			this.groupBoxChild.Text = "자식 프로세스";
			this.textBoxChildProcessLogStr.Location = new global::System.Drawing.Point(120, 42);
			this.textBoxChildProcessLogStr.MaxLength = 32;
			this.textBoxChildProcessLogStr.Name = "textBoxChildProcessLogStr";
			this.textBoxChildProcessLogStr.Size = new global::System.Drawing.Size(296, 21);
			this.textBoxChildProcessLogStr.TabIndex = 37;
			this.textBoxChildProcessLogStr.Text = "Log [{pid}] {log}";
			this.labelChildProcessLogStr.AutoSize = true;
			this.labelChildProcessLogStr.Location = new global::System.Drawing.Point(8, 45);
			this.labelChildProcessLogStr.Name = "labelChildProcessLogStr";
			this.labelChildProcessLogStr.Size = new global::System.Drawing.Size(57, 12);
			this.labelChildProcessLogStr.TabIndex = 36;
			this.labelChildProcessLogStr.Text = "로그 형식";
			this.textBoxMaxChildProcessCount.Location = new global::System.Drawing.Point(120, 69);
			this.textBoxMaxChildProcessCount.MaxLength = 32;
			this.textBoxMaxChildProcessCount.Name = "textBoxMaxChildProcessCount";
			this.textBoxMaxChildProcessCount.Size = new global::System.Drawing.Size(136, 21);
			this.textBoxMaxChildProcessCount.TabIndex = 35;
			this.labelMaxChildProcessCount.AutoSize = true;
			this.labelMaxChildProcessCount.Location = new global::System.Drawing.Point(8, 72);
			this.labelMaxChildProcessCount.Name = "labelMaxChildProcessCount";
			this.labelMaxChildProcessCount.Size = new global::System.Drawing.Size(101, 12);
			this.labelMaxChildProcessCount.TabIndex = 1;
			this.labelMaxChildProcessCount.Text = "최대 추적 자식 수";
			this.checkBoxTraceChildProcess.AutoSize = true;
			this.checkBoxTraceChildProcess.Location = new global::System.Drawing.Point(10, 20);
			this.checkBoxTraceChildProcess.Name = "checkBoxTraceChildProcess";
			this.checkBoxTraceChildProcess.Size = new global::System.Drawing.Size(156, 16);
			this.checkBoxTraceChildProcess.TabIndex = 0;
			this.checkBoxTraceChildProcess.Text = "자식 프로세스 로그 추적";
			this.checkBoxTraceChildProcess.UseVisualStyleBackColor = true;
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			this.AutoSize = true;
			base.ClientSize = new global::System.Drawing.Size(442, 698);
			base.ControlBox = false;
			base.Controls.Add(this.groupBoxChild);
			base.Controls.Add(this.groupBoxProperty);
			base.Controls.Add(this.groupBoxStandardInProperty);
			base.Controls.Add(this.groupBoxStandardOutProperty);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ProcessPropertyDetailForm";
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "ProcessPropertyDetailForm";
			this.groupBoxStandardInProperty.ResumeLayout(false);
			this.groupBoxStandardInProperty.PerformLayout();
			this.groupBoxStandardOutProperty.ResumeLayout(false);
			this.groupBoxStandardOutProperty.PerformLayout();
			this.groupBoxProperty.ResumeLayout(false);
			this.groupBoxChild.ResumeLayout(false);
			this.groupBoxChild.PerformLayout();
			base.ResumeLayout(false);
		}

		private global::System.ComponentModel.IContainer components;

		private global::System.Windows.Forms.GroupBox groupBoxStandardInProperty;

		private global::System.Windows.Forms.ListView listViewCustomCommand;

		private global::System.Windows.Forms.ColumnHeader columnHeader1;

		private global::System.Windows.Forms.Label labelBootedString;

		private global::System.Windows.Forms.TextBox textBoxBootedString;

		private global::System.Windows.Forms.Label label1;

		private global::System.Windows.Forms.Button buttonCustomCommandDown;

		private global::System.Windows.Forms.Button buttonCustomCommandAdd;

		private global::System.Windows.Forms.Button buttonCustomCommandSub;

		private global::System.Windows.Forms.Button buttonCustomCommandUp;

		private global::System.Windows.Forms.GroupBox groupBoxStandardOutProperty;

		private global::System.Windows.Forms.Button buttonPerformanceDescSub;

		private global::System.Windows.Forms.ListView listViewPerformanceDescription;

		private global::System.Windows.Forms.ColumnHeader columnHeader3;

		private global::System.Windows.Forms.ColumnHeader columnHeader4;

		private global::System.Windows.Forms.TextBox textBoxShutdownString;

		private global::System.Windows.Forms.Label labelPerformanceString;

		private global::System.Windows.Forms.TextBox textBoxPerformanceString;

		private global::System.Windows.Forms.Label labelShutdownString;

		private global::System.Windows.Forms.Label labelPerformanceDescription;

		private global::System.Windows.Forms.Button buttonPerformanceDescAdd;

		private global::System.Windows.Forms.Button buttonPerformanceDescDown;

		private global::System.Windows.Forms.Button buttonPerformanceDescUp;

		private global::System.Windows.Forms.ToolTip toolTipProperty;

		private global::System.Windows.Forms.ListView listViewProperty;

		private global::System.Windows.Forms.ColumnHeader columnHeader2;

		private global::System.Windows.Forms.GroupBox groupBoxProperty;

		private global::System.Windows.Forms.Button buttonPropertyDel;

		private global::System.Windows.Forms.Button buttonPropertyAdd;

		private global::System.Windows.Forms.GroupBox groupBoxChild;

		private global::System.Windows.Forms.TextBox textBoxChildProcessLogStr;

		private global::System.Windows.Forms.Label labelChildProcessLogStr;

		private global::HeroesOpTool.Utility.NumericTextBox textBoxMaxChildProcessCount;

		private global::System.Windows.Forms.Label labelMaxChildProcessCount;

		private global::System.Windows.Forms.CheckBox checkBoxTraceChildProcess;
	}
}
