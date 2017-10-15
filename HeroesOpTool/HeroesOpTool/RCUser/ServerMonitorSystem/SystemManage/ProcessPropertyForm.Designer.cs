namespace HeroesOpTool.RCUser.ServerMonitorSystem.SystemManage
{
	public partial class ProcessPropertyForm : global::System.Windows.Forms.Form
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
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::HeroesOpTool.RCUser.ServerMonitorSystem.SystemManage.ProcessPropertyForm));
			this.checkBoxAutomaticStart = new global::System.Windows.Forms.CheckBox();
			this.checkBoxUse = new global::System.Windows.Forms.CheckBox();
			this.checkBoxRunOnce = new global::System.Windows.Forms.CheckBox();
			this.textBoxUpdateFileArgs = new global::System.Windows.Forms.TextBox();
			this.textBoxUpdateFileName = new global::System.Windows.Forms.TextBox();
			this.textBoxStandardOutLogLines = new global::System.Windows.Forms.TextBox();
			this.textBoxExecuteFileArgs = new global::System.Windows.Forms.TextBox();
			this.textBoxExecuteFileName = new global::System.Windows.Forms.TextBox();
			this.textBoxWorkingDirectory = new global::System.Windows.Forms.TextBox();
			this.textBoxDescription = new global::System.Windows.Forms.TextBox();
			this.comboBoxName = new global::System.Windows.Forms.ComboBox();
			this.textBoxType = new global::System.Windows.Forms.TextBox();
			this.labelName = new global::System.Windows.Forms.Label();
			this.labelDescription = new global::System.Windows.Forms.Label();
			this.labelWorkingDirectory = new global::System.Windows.Forms.Label();
			this.labelExecuteFile = new global::System.Windows.Forms.Label();
			this.labelUpdateFile = new global::System.Windows.Forms.Label();
			this.labelStandardOutLogLines = new global::System.Windows.Forms.Label();
			this.groupBoxBasicProperty = new global::System.Windows.Forms.GroupBox();
			this.labelType = new global::System.Windows.Forms.Label();
			this.groupBoxFileProperty = new global::System.Windows.Forms.GroupBox();
			this.groupBoxEtcProperty = new global::System.Windows.Forms.GroupBox();
			this.checkBoxPerformance = new global::System.Windows.Forms.CheckBox();
			this.buttonOK = new global::System.Windows.Forms.Button();
			this.buttonCancel = new global::System.Windows.Forms.Button();
			this.toolTipProperty = new global::System.Windows.Forms.ToolTip(this.components);
			this.BtnDetail = new global::System.Windows.Forms.Button();
			this.groupBoxSchedule = new global::System.Windows.Forms.GroupBox();
			this.buttonScheduleAdd = new global::System.Windows.Forms.Button();
			this.buttonScheduleSub = new global::System.Windows.Forms.Button();
			this.listViewSchedule = new global::System.Windows.Forms.ListView();
			this.columnHeader1 = new global::System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new global::System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new global::System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new global::System.Windows.Forms.ColumnHeader();
			this.imageList1 = new global::System.Windows.Forms.ImageList(this.components);
			this.groupBoxBasicProperty.SuspendLayout();
			this.groupBoxFileProperty.SuspendLayout();
			this.groupBoxEtcProperty.SuspendLayout();
			this.groupBoxSchedule.SuspendLayout();
			base.SuspendLayout();
			this.checkBoxAutomaticStart.Location = new global::System.Drawing.Point(8, 64);
			this.checkBoxAutomaticStart.Name = "checkBoxAutomaticStart";
			this.checkBoxAutomaticStart.Size = new global::System.Drawing.Size(408, 24);
			this.checkBoxAutomaticStart.TabIndex = 2;
			this.checkBoxAutomaticStart.Text = "컴퓨터가 켜졌을 때 자동으로 시작";
			this.toolTipProperty.SetToolTip(this.checkBoxAutomaticStart, "컴퓨터가 특별한 이유에 의해 다시 켜졌을 때 사용자의 명령을 기다리지 않고 바로 시작을 원할 경우 이 옵션을 켭니다.");
			this.checkBoxUse.Location = new global::System.Drawing.Point(8, 44);
			this.checkBoxUse.Name = "checkBoxUse";
			this.checkBoxUse.Size = new global::System.Drawing.Size(200, 24);
			this.checkBoxUse.TabIndex = 1;
			this.checkBoxUse.Text = "프로그램 사용 활성화";
			this.toolTipProperty.SetToolTip(this.checkBoxUse, "이 옵션을 끄면 작업그룹을 선택할 때 기본으로 선택되지 않고, '연속 실행/종료' 시에도 포함되지 않습니다.");
			this.checkBoxRunOnce.Location = new global::System.Drawing.Point(226, 44);
			this.checkBoxRunOnce.Name = "checkBoxRunOnce";
			this.checkBoxRunOnce.Size = new global::System.Drawing.Size(190, 24);
			this.checkBoxRunOnce.TabIndex = 0;
			this.checkBoxRunOnce.Text = "프로그램 종료시 자동 재시작";
			this.toolTipProperty.SetToolTip(this.checkBoxRunOnce, "종료 명령을 내리지 않고, 프로그램이 스스로 종료되었을 시, 자동으로 다시 시작시켜줄지를 결정합니다. 24시간 실행되고 있어야 해서 설령 프로그램 크래시가 나도 자동으로 재시작해야 할 경우 체크합니다.");
			this.textBoxUpdateFileArgs.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxUpdateFileArgs.Location = new global::System.Drawing.Point(256, 101);
			this.textBoxUpdateFileArgs.MaxLength = 256;
			this.textBoxUpdateFileArgs.Name = "textBoxUpdateFileArgs";
			this.textBoxUpdateFileArgs.Size = new global::System.Drawing.Size(160, 21);
			this.textBoxUpdateFileArgs.TabIndex = 4;
			this.toolTipProperty.SetToolTip(this.textBoxUpdateFileArgs, "업데이트 파일의 인자입니다.");
			this.textBoxUpdateFileName.Location = new global::System.Drawing.Point(120, 101);
			this.textBoxUpdateFileName.MaxLength = 32;
			this.textBoxUpdateFileName.Name = "textBoxUpdateFileName";
			this.textBoxUpdateFileName.Size = new global::System.Drawing.Size(136, 21);
			this.textBoxUpdateFileName.TabIndex = 3;
			this.toolTipProperty.SetToolTip(this.textBoxUpdateFileName, "업데이트를 필요로 할 때 실행되는 파일입니다.");
			this.textBoxStandardOutLogLines.Location = new global::System.Drawing.Point(124, 20);
			this.textBoxStandardOutLogLines.MaxLength = 4;
			this.textBoxStandardOutLogLines.Name = "textBoxStandardOutLogLines";
			this.textBoxStandardOutLogLines.Size = new global::System.Drawing.Size(136, 21);
			this.textBoxStandardOutLogLines.TabIndex = 0;
			this.toolTipProperty.SetToolTip(this.textBoxStandardOutLogLines, "기억할 프로그램의 로그라인 수입니다. 10~1000까지의 값을 가질 수 있으며, 통상 100줄을 기억합니다.");
			this.textBoxExecuteFileArgs.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxExecuteFileArgs.Location = new global::System.Drawing.Point(120, 70);
			this.textBoxExecuteFileArgs.MaxLength = 256;
			this.textBoxExecuteFileArgs.Name = "textBoxExecuteFileArgs";
			this.textBoxExecuteFileArgs.Size = new global::System.Drawing.Size(296, 21);
			this.textBoxExecuteFileArgs.TabIndex = 2;
			this.toolTipProperty.SetToolTip(this.textBoxExecuteFileArgs, "실행 파일의 인자입니다.");
			this.textBoxExecuteFileName.Location = new global::System.Drawing.Point(120, 46);
			this.textBoxExecuteFileName.MaxLength = 32;
			this.textBoxExecuteFileName.Name = "textBoxExecuteFileName";
			this.textBoxExecuteFileName.Size = new global::System.Drawing.Size(296, 21);
			this.textBoxExecuteFileName.TabIndex = 1;
			this.toolTipProperty.SetToolTip(this.textBoxExecuteFileName, "실행파일의 이름입니다.");
			this.textBoxWorkingDirectory.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxWorkingDirectory.Location = new global::System.Drawing.Point(120, 16);
			this.textBoxWorkingDirectory.MaxLength = 32;
			this.textBoxWorkingDirectory.Name = "textBoxWorkingDirectory";
			this.textBoxWorkingDirectory.Size = new global::System.Drawing.Size(296, 21);
			this.textBoxWorkingDirectory.TabIndex = 0;
			this.toolTipProperty.SetToolTip(this.textBoxWorkingDirectory, "실행 파일이 실행되는 작업 디렉토리 입니다. 상대적인 디렉토리를 입력합니다.");
			this.textBoxDescription.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxDescription.Location = new global::System.Drawing.Point(120, 68);
			this.textBoxDescription.MaxLength = 256;
			this.textBoxDescription.Name = "textBoxDescription";
			this.textBoxDescription.Size = new global::System.Drawing.Size(296, 21);
			this.textBoxDescription.TabIndex = 1;
			this.toolTipProperty.SetToolTip(this.textBoxDescription, "프로그램의 설명을 입력합니다. 화면에 표시하거나 작업 그룹을 나누는데 쓰이는 기준입니다.");
			this.comboBoxName.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.comboBoxName.Location = new global::System.Drawing.Point(120, 16);
			this.comboBoxName.MaxLength = 32;
			this.comboBoxName.Name = "comboBoxName";
			this.comboBoxName.Size = new global::System.Drawing.Size(296, 20);
			this.comboBoxName.TabIndex = 0;
			this.toolTipProperty.SetToolTip(this.comboBoxName, "프로그램의 대표 이름을 입력합니다. 새 프로그램을 등록할 경우 템플릿 목록에서 고를 수 있습니다.");
			this.comboBoxName.SelectedIndexChanged += new global::System.EventHandler(this.comboBoxName_SelectedIndexChanged);
			this.textBoxType.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxType.Location = new global::System.Drawing.Point(120, 42);
			this.textBoxType.MaxLength = 32;
			this.textBoxType.Name = "textBoxType";
			this.textBoxType.Size = new global::System.Drawing.Size(296, 21);
			this.textBoxType.TabIndex = 0;
			this.toolTipProperty.SetToolTip(this.textBoxType, "프로그램의 타입을 입력합니다. 타입이 같은 프로그램은 이름이 달라도 같은 프로그램으로 취급하여 동시 작업 명령이 가능합니다.");
			this.labelName.Location = new global::System.Drawing.Point(8, 16);
			this.labelName.Name = "labelName";
			this.labelName.Size = new global::System.Drawing.Size(112, 24);
			this.labelName.TabIndex = 34;
			this.labelName.Text = "프로그램 이름";
			this.labelName.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.labelDescription.Location = new global::System.Drawing.Point(8, 68);
			this.labelDescription.Name = "labelDescription";
			this.labelDescription.Size = new global::System.Drawing.Size(112, 24);
			this.labelDescription.TabIndex = 34;
			this.labelDescription.Text = "프로그램 설명";
			this.labelDescription.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.labelWorkingDirectory.Location = new global::System.Drawing.Point(8, 16);
			this.labelWorkingDirectory.Name = "labelWorkingDirectory";
			this.labelWorkingDirectory.Size = new global::System.Drawing.Size(112, 24);
			this.labelWorkingDirectory.TabIndex = 34;
			this.labelWorkingDirectory.Text = "작업 디렉토리";
			this.labelWorkingDirectory.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.labelExecuteFile.Location = new global::System.Drawing.Point(8, 46);
			this.labelExecuteFile.Name = "labelExecuteFile";
			this.labelExecuteFile.Size = new global::System.Drawing.Size(112, 24);
			this.labelExecuteFile.TabIndex = 34;
			this.labelExecuteFile.Text = "실행 파일";
			this.labelExecuteFile.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.labelUpdateFile.Location = new global::System.Drawing.Point(8, 101);
			this.labelUpdateFile.Name = "labelUpdateFile";
			this.labelUpdateFile.Size = new global::System.Drawing.Size(112, 24);
			this.labelUpdateFile.TabIndex = 34;
			this.labelUpdateFile.Text = "업데이트 파일";
			this.labelUpdateFile.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.labelStandardOutLogLines.Location = new global::System.Drawing.Point(8, 20);
			this.labelStandardOutLogLines.Name = "labelStandardOutLogLines";
			this.labelStandardOutLogLines.Size = new global::System.Drawing.Size(122, 24);
			this.labelStandardOutLogLines.TabIndex = 34;
			this.labelStandardOutLogLines.Text = "기억할 로그 라인 수";
			this.labelStandardOutLogLines.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.groupBoxBasicProperty.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.groupBoxBasicProperty.Controls.Add(this.labelType);
			this.groupBoxBasicProperty.Controls.Add(this.labelDescription);
			this.groupBoxBasicProperty.Controls.Add(this.labelName);
			this.groupBoxBasicProperty.Controls.Add(this.comboBoxName);
			this.groupBoxBasicProperty.Controls.Add(this.textBoxType);
			this.groupBoxBasicProperty.Controls.Add(this.textBoxDescription);
			this.groupBoxBasicProperty.Location = new global::System.Drawing.Point(8, 10);
			this.groupBoxBasicProperty.Name = "groupBoxBasicProperty";
			this.groupBoxBasicProperty.Size = new global::System.Drawing.Size(424, 92);
			this.groupBoxBasicProperty.TabIndex = 0;
			this.groupBoxBasicProperty.TabStop = false;
			this.groupBoxBasicProperty.Text = "기본 속성";
			this.toolTipProperty.SetToolTip(this.groupBoxBasicProperty, "프로그램의 기본 속성을 설정합니다.");
			this.labelType.Location = new global::System.Drawing.Point(8, 42);
			this.labelType.Name = "labelType";
			this.labelType.Size = new global::System.Drawing.Size(112, 24);
			this.labelType.TabIndex = 35;
			this.labelType.Text = "프로그램 타입";
			this.labelType.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.groupBoxFileProperty.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.groupBoxFileProperty.Controls.Add(this.textBoxExecuteFileArgs);
			this.groupBoxFileProperty.Controls.Add(this.textBoxUpdateFileArgs);
			this.groupBoxFileProperty.Controls.Add(this.textBoxWorkingDirectory);
			this.groupBoxFileProperty.Controls.Add(this.textBoxExecuteFileName);
			this.groupBoxFileProperty.Controls.Add(this.labelExecuteFile);
			this.groupBoxFileProperty.Controls.Add(this.labelUpdateFile);
			this.groupBoxFileProperty.Controls.Add(this.textBoxUpdateFileName);
			this.groupBoxFileProperty.Controls.Add(this.labelWorkingDirectory);
			this.groupBoxFileProperty.Location = new global::System.Drawing.Point(8, 108);
			this.groupBoxFileProperty.Name = "groupBoxFileProperty";
			this.groupBoxFileProperty.Size = new global::System.Drawing.Size(424, 128);
			this.groupBoxFileProperty.TabIndex = 1;
			this.groupBoxFileProperty.TabStop = false;
			this.groupBoxFileProperty.Text = "실행 파일 관련 정보";
			this.toolTipProperty.SetToolTip(this.groupBoxFileProperty, "프로그램과 관련된 파일에 대한 설정을 합니다.");
			this.groupBoxEtcProperty.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.groupBoxEtcProperty.Controls.Add(this.checkBoxPerformance);
			this.groupBoxEtcProperty.Controls.Add(this.checkBoxRunOnce);
			this.groupBoxEtcProperty.Controls.Add(this.checkBoxAutomaticStart);
			this.groupBoxEtcProperty.Controls.Add(this.checkBoxUse);
			this.groupBoxEtcProperty.Controls.Add(this.textBoxStandardOutLogLines);
			this.groupBoxEtcProperty.Controls.Add(this.labelStandardOutLogLines);
			this.groupBoxEtcProperty.Location = new global::System.Drawing.Point(12, 426);
			this.groupBoxEtcProperty.Name = "groupBoxEtcProperty";
			this.groupBoxEtcProperty.Size = new global::System.Drawing.Size(420, 94);
			this.groupBoxEtcProperty.TabIndex = 4;
			this.groupBoxEtcProperty.TabStop = false;
			this.groupBoxEtcProperty.Text = "기타 정보";
			this.toolTipProperty.SetToolTip(this.groupBoxEtcProperty, "기타 정보를 설정합니다.");
			this.checkBoxPerformance.AutoSize = true;
			this.checkBoxPerformance.Location = new global::System.Drawing.Point(226, 68);
			this.checkBoxPerformance.Name = "checkBoxPerformance";
			this.checkBoxPerformance.Size = new global::System.Drawing.Size(171, 16);
			this.checkBoxPerformance.TabIndex = 35;
			this.checkBoxPerformance.Text = "메모리/CPU 성능 모니터링";
			this.checkBoxPerformance.UseVisualStyleBackColor = true;
			this.buttonOK.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.buttonOK.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new global::System.Drawing.Point(87, 529);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new global::System.Drawing.Size(104, 24);
			this.buttonOK.TabIndex = 5;
			this.buttonOK.Text = "확인";
			this.buttonOK.Click += new global::System.EventHandler(this.buttonOK_Click);
			this.buttonCancel.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.buttonCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new global::System.Drawing.Point(197, 529);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new global::System.Drawing.Size(104, 24);
			this.buttonCancel.TabIndex = 6;
			this.buttonCancel.Text = "취소";
			this.BtnDetail.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.BtnDetail.Location = new global::System.Drawing.Point(357, 530);
			this.BtnDetail.Name = "BtnDetail";
			this.BtnDetail.Size = new global::System.Drawing.Size(75, 23);
			this.BtnDetail.TabIndex = 7;
			this.BtnDetail.Text = ">>";
			this.BtnDetail.UseVisualStyleBackColor = true;
			this.BtnDetail.Click += new global::System.EventHandler(this.BtnDetail_Click);
			this.groupBoxSchedule.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.groupBoxSchedule.Controls.Add(this.buttonScheduleAdd);
			this.groupBoxSchedule.Controls.Add(this.buttonScheduleSub);
			this.groupBoxSchedule.Controls.Add(this.listViewSchedule);
			this.groupBoxSchedule.Location = new global::System.Drawing.Point(12, 242);
			this.groupBoxSchedule.Name = "groupBoxSchedule";
			this.groupBoxSchedule.Size = new global::System.Drawing.Size(420, 178);
			this.groupBoxSchedule.TabIndex = 8;
			this.groupBoxSchedule.TabStop = false;
			this.groupBoxSchedule.Text = "스케쥴러";
			this.buttonScheduleAdd.Location = new global::System.Drawing.Point(6, 20);
			this.buttonScheduleAdd.Name = "buttonScheduleAdd";
			this.buttonScheduleAdd.Size = new global::System.Drawing.Size(24, 24);
			this.buttonScheduleAdd.TabIndex = 6;
			this.buttonScheduleAdd.Text = "＋";
			this.buttonScheduleAdd.Click += new global::System.EventHandler(this.buttonScheduleAdd_Click);
			this.buttonScheduleSub.Location = new global::System.Drawing.Point(6, 44);
			this.buttonScheduleSub.Name = "buttonScheduleSub";
			this.buttonScheduleSub.Size = new global::System.Drawing.Size(24, 24);
			this.buttonScheduleSub.TabIndex = 7;
			this.buttonScheduleSub.Text = "－";
			this.buttonScheduleSub.Click += new global::System.EventHandler(this.buttonScheduleSub_Click);
			this.listViewSchedule.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.listViewSchedule.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.columnHeader1,
				this.columnHeader2,
				this.columnHeader3,
				this.columnHeader4
			});
			this.listViewSchedule.FullRowSelect = true;
			this.listViewSchedule.GridLines = true;
			this.listViewSchedule.Location = new global::System.Drawing.Point(36, 20);
			this.listViewSchedule.MultiSelect = false;
			this.listViewSchedule.Name = "listViewSchedule";
			this.listViewSchedule.Size = new global::System.Drawing.Size(376, 152);
			this.listViewSchedule.StateImageList = this.imageList1;
			this.listViewSchedule.TabIndex = 0;
			this.listViewSchedule.UseCompatibleStateImageBehavior = false;
			this.listViewSchedule.View = global::System.Windows.Forms.View.Details;
			this.listViewSchedule.DoubleClick += new global::System.EventHandler(this.listViewSchedule_DoubleClick);
			this.columnHeader1.Text = "";
			this.columnHeader1.Width = 22;
			this.columnHeader2.Text = "이름";
			this.columnHeader2.Width = 52;
			this.columnHeader3.Text = "실행계획";
			this.columnHeader3.Width = 64;
			this.columnHeader4.Text = "명령";
			this.columnHeader4.Width = 229;
			this.imageList1.ImageStream = (global::System.Windows.Forms.ImageListStreamer)componentResourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = global::System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "Enabeld");
			this.imageList1.Images.SetKeyName(1, "Disabled");
			base.AcceptButton = this.buttonOK;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(6, 14);
			base.CancelButton = this.buttonCancel;
			base.ClientSize = new global::System.Drawing.Size(442, 562);
			base.Controls.Add(this.groupBoxSchedule);
			base.Controls.Add(this.BtnDetail);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.groupBoxEtcProperty);
			base.Controls.Add(this.groupBoxFileProperty);
			base.Controls.Add(this.groupBoxBasicProperty);
			base.Controls.Add(this.buttonCancel);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ProcessPropertyForm";
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "프로그램 상세 정보";
			base.LocationChanged += new global::System.EventHandler(this.ProcessPropertyForm_LocationChanged);
			this.groupBoxBasicProperty.ResumeLayout(false);
			this.groupBoxBasicProperty.PerformLayout();
			this.groupBoxFileProperty.ResumeLayout(false);
			this.groupBoxFileProperty.PerformLayout();
			this.groupBoxEtcProperty.ResumeLayout(false);
			this.groupBoxEtcProperty.PerformLayout();
			this.groupBoxSchedule.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private global::System.Windows.Forms.GroupBox groupBoxBasicProperty;

		private global::System.Windows.Forms.GroupBox groupBoxFileProperty;

		private global::System.Windows.Forms.GroupBox groupBoxEtcProperty;

		private global::System.Windows.Forms.ComboBox comboBoxName;

		private global::System.Windows.Forms.TextBox textBoxType;

		private global::System.Windows.Forms.TextBox textBoxDescription;

		private global::System.Windows.Forms.TextBox textBoxWorkingDirectory;

		private global::System.Windows.Forms.TextBox textBoxExecuteFileArgs;

		private global::System.Windows.Forms.TextBox textBoxExecuteFileName;

		private global::System.Windows.Forms.TextBox textBoxUpdateFileArgs;

		private global::System.Windows.Forms.TextBox textBoxUpdateFileName;

		private global::System.Windows.Forms.TextBox textBoxStandardOutLogLines;

		private global::System.Windows.Forms.CheckBox checkBoxAutomaticStart;

		private global::System.Windows.Forms.CheckBox checkBoxUse;

		private global::System.Windows.Forms.CheckBox checkBoxRunOnce;

		private global::System.Windows.Forms.Label labelName;

		private global::System.Windows.Forms.Label labelDescription;

		private global::System.Windows.Forms.Label labelWorkingDirectory;

		private global::System.Windows.Forms.Label labelExecuteFile;

		private global::System.Windows.Forms.Label labelUpdateFile;

		private global::System.Windows.Forms.Label labelStandardOutLogLines;

		private global::System.Windows.Forms.Button buttonOK;

		private global::System.Windows.Forms.Button buttonCancel;

		private global::System.Windows.Forms.ToolTip toolTipProperty;

		private global::System.Windows.Forms.Button BtnDetail;

		private global::System.Windows.Forms.GroupBox groupBoxSchedule;

		private global::System.Windows.Forms.ListView listViewSchedule;

		private global::System.Windows.Forms.Button buttonScheduleAdd;

		private global::System.Windows.Forms.Button buttonScheduleSub;

		private global::System.Windows.Forms.ColumnHeader columnHeader1;

		private global::System.Windows.Forms.ColumnHeader columnHeader2;

		private global::System.Windows.Forms.ColumnHeader columnHeader3;

		private global::System.Windows.Forms.ColumnHeader columnHeader4;

		private global::System.Windows.Forms.ImageList imageList1;

		private global::System.Windows.Forms.Label labelType;

		private global::System.Windows.Forms.CheckBox checkBoxPerformance;

		private global::System.ComponentModel.IContainer components;
	}
}
