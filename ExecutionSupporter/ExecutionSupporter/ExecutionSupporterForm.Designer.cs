namespace ExecutionSupporter
{
	public partial class ExecutionSupporterForm : global::System.Windows.Forms.Form
	{

		private void InitializeComponent()
		{
			this.ListOperation = new global::System.Windows.Forms.ListBox();
			this.TextMachineInfo = new global::System.Windows.Forms.TextBox();
			this.TextLog = new global::System.Windows.Forms.RichTextBox();
			this.TextCommand = new global::System.Windows.Forms.TextBox();
			this.TextServiceStatus = new global::System.Windows.Forms.TextBox();
			this.ButtonRefreshUserCount = new global::System.Windows.Forms.Button();
			this.TextUserCount = new global::System.Windows.Forms.TextBox();
			this.ButtonCommand = new global::System.Windows.Forms.Button();
			this.ButtonStartServer = new global::System.Windows.Forms.Button();
			this.ButtonStartService = new global::System.Windows.Forms.Button();
			this.ButtonStopService = new global::System.Windows.Forms.Button();
			this.ButtonReloadSetting = new global::System.Windows.Forms.Button();
			this.ButtonUpdateServer = new global::System.Windows.Forms.Button();
			this.ButtonKillServer = new global::System.Windows.Forms.Button();
			this.TextMachineStatus = new global::System.Windows.Forms.TextBox();
			this.ButtonGatherLog = new global::System.Windows.Forms.Button();
			this.ListMachine = new global::System.Windows.Forms.ListView();
			this.colMachine = new global::System.Windows.Forms.ColumnHeader();
			this.ButtonLogViewer = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.ListOperation.FormattingEnabled = true;
			this.ListOperation.ItemHeight = 12;
			this.ListOperation.Location = new global::System.Drawing.Point(410, 66);
			this.ListOperation.Name = "ListOperation";
			this.ListOperation.Size = new global::System.Drawing.Size(268, 160);
			this.ListOperation.TabIndex = 1;
			this.TextMachineInfo.Location = new global::System.Drawing.Point(410, 232);
			this.TextMachineInfo.Multiline = true;
			this.TextMachineInfo.Name = "TextMachineInfo";
			this.TextMachineInfo.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.TextMachineInfo.Size = new global::System.Drawing.Size(268, 318);
			this.TextMachineInfo.TabIndex = 16;
			this.TextLog.BackColor = global::System.Drawing.Color.White;
			this.TextLog.Font = new global::System.Drawing.Font("굴림", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 129);
			this.TextLog.Location = new global::System.Drawing.Point(12, 556);
			this.TextLog.Name = "TextLog";
			this.TextLog.ReadOnly = true;
			this.TextLog.Size = new global::System.Drawing.Size(666, 103);
			this.TextLog.TabIndex = 17;
			this.TextLog.Text = "";
			this.TextCommand.Location = new global::System.Drawing.Point(12, 665);
			this.TextCommand.Name = "TextCommand";
			this.TextCommand.Size = new global::System.Drawing.Size(586, 21);
			this.TextCommand.TabIndex = 19;
			this.TextCommand.KeyDown += new global::System.Windows.Forms.KeyEventHandler(this.TextCommand_KeyDown);
			this.TextServiceStatus.Font = new global::System.Drawing.Font("굴림", 9f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 129);
			this.TextServiceStatus.Location = new global::System.Drawing.Point(12, 12);
			this.TextServiceStatus.Name = "TextServiceStatus";
			this.TextServiceStatus.ReadOnly = true;
			this.TextServiceStatus.Size = new global::System.Drawing.Size(666, 21);
			this.TextServiceStatus.TabIndex = 21;
			this.ButtonRefreshUserCount.Location = new global::System.Drawing.Point(577, 39);
			this.ButtonRefreshUserCount.Name = "ButtonRefreshUserCount";
			this.ButtonRefreshUserCount.Size = new global::System.Drawing.Size(101, 21);
			this.ButtonRefreshUserCount.TabIndex = 5;
			this.ButtonRefreshUserCount.Text = "Check";
			this.ButtonRefreshUserCount.UseVisualStyleBackColor = true;
			this.ButtonRefreshUserCount.Click += new global::System.EventHandler(this.ButtonRefreshUserCount_Click);
			this.TextUserCount.Location = new global::System.Drawing.Point(12, 39);
			this.TextUserCount.Name = "TextUserCount";
			this.TextUserCount.ReadOnly = true;
			this.TextUserCount.Size = new global::System.Drawing.Size(559, 21);
			this.TextUserCount.TabIndex = 21;
			this.ButtonCommand.Location = new global::System.Drawing.Point(604, 662);
			this.ButtonCommand.Name = "ButtonCommand";
			this.ButtonCommand.Size = new global::System.Drawing.Size(74, 25);
			this.ButtonCommand.TabIndex = 20;
			this.ButtonCommand.Text = "Go";
			this.ButtonCommand.UseVisualStyleBackColor = true;
			this.ButtonCommand.Click += new global::System.EventHandler(this.ButtonCommand_Click);
			this.ButtonStartServer.Location = new global::System.Drawing.Point(276, 176);
			this.ButtonStartServer.Name = "ButtonStartServer";
			this.ButtonStartServer.Size = new global::System.Drawing.Size(128, 53);
			this.ButtonStartServer.TabIndex = 27;
			this.ButtonStartServer.Text = "Start Server";
			this.ButtonStartServer.UseVisualStyleBackColor = true;
			this.ButtonStartServer.Click += new global::System.EventHandler(this.ButtonStartServer_Click);
			this.ButtonStartService.Location = new global::System.Drawing.Point(276, 66);
			this.ButtonStartService.Name = "ButtonStartService";
			this.ButtonStartService.Size = new global::System.Drawing.Size(128, 20);
			this.ButtonStartService.TabIndex = 30;
			this.ButtonStartService.Text = "Start Service";
			this.ButtonStartService.UseVisualStyleBackColor = true;
			this.ButtonStartService.Click += new global::System.EventHandler(this.ButtonStartService_Click);
			this.ButtonStopService.Location = new global::System.Drawing.Point(276, 92);
			this.ButtonStopService.Name = "ButtonStopService";
			this.ButtonStopService.Size = new global::System.Drawing.Size(128, 21);
			this.ButtonStopService.TabIndex = 31;
			this.ButtonStopService.Text = "Stop Service";
			this.ButtonStopService.UseVisualStyleBackColor = true;
			this.ButtonStopService.Click += new global::System.EventHandler(this.ButtonStopService_Click);
			this.ButtonReloadSetting.Location = new global::System.Drawing.Point(276, 529);
			this.ButtonReloadSetting.Name = "ButtonReloadSetting";
			this.ButtonReloadSetting.Size = new global::System.Drawing.Size(128, 21);
			this.ButtonReloadSetting.TabIndex = 29;
			this.ButtonReloadSetting.Text = "Reload Setting";
			this.ButtonReloadSetting.UseVisualStyleBackColor = true;
			this.ButtonReloadSetting.Click += new global::System.EventHandler(this.ButtonReloadSetting_Click);
			this.ButtonUpdateServer.Location = new global::System.Drawing.Point(275, 149);
			this.ButtonUpdateServer.Name = "ButtonUpdateServer";
			this.ButtonUpdateServer.Size = new global::System.Drawing.Size(128, 21);
			this.ButtonUpdateServer.TabIndex = 26;
			this.ButtonUpdateServer.Text = "Update Server";
			this.ButtonUpdateServer.UseVisualStyleBackColor = true;
			this.ButtonUpdateServer.Click += new global::System.EventHandler(this.ButtonUpdateServer_Click);
			this.ButtonKillServer.Location = new global::System.Drawing.Point(275, 235);
			this.ButtonKillServer.Name = "ButtonKillServer";
			this.ButtonKillServer.Size = new global::System.Drawing.Size(128, 21);
			this.ButtonKillServer.TabIndex = 28;
			this.ButtonKillServer.Text = "Kill Server";
			this.ButtonKillServer.UseVisualStyleBackColor = true;
			this.ButtonKillServer.Click += new global::System.EventHandler(this.ButtonKillServer_Click);
			this.TextMachineStatus.Location = new global::System.Drawing.Point(276, 262);
			this.TextMachineStatus.Multiline = true;
			this.TextMachineStatus.Name = "TextMachineStatus";
			this.TextMachineStatus.ReadOnly = true;
			this.TextMachineStatus.Size = new global::System.Drawing.Size(127, 207);
			this.TextMachineStatus.TabIndex = 32;
			this.ButtonGatherLog.Location = new global::System.Drawing.Point(276, 475);
			this.ButtonGatherLog.Name = "ButtonGatherLog";
			this.ButtonGatherLog.Size = new global::System.Drawing.Size(128, 21);
			this.ButtonGatherLog.TabIndex = 29;
			this.ButtonGatherLog.Text = "Gather Log";
			this.ButtonGatherLog.UseVisualStyleBackColor = true;
			this.ButtonGatherLog.Click += new global::System.EventHandler(this.ButtonGatherLog_Click);
			this.ListMachine.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.colMachine
			});
			this.ListMachine.Location = new global::System.Drawing.Point(12, 66);
			this.ListMachine.MultiSelect = false;
			this.ListMachine.Name = "ListMachine";
			this.ListMachine.Size = new global::System.Drawing.Size(258, 484);
			this.ListMachine.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.ListMachine.TabIndex = 34;
			this.ListMachine.UseCompatibleStateImageBehavior = false;
			this.ListMachine.View = global::System.Windows.Forms.View.List;
			this.ListMachine.SelectedIndexChanged += new global::System.EventHandler(this.ListMachine_SelectedIndexChanged);
			this.colMachine.Text = "Machine";
			this.colMachine.Width = 254;
			this.ButtonLogViewer.Location = new global::System.Drawing.Point(275, 502);
			this.ButtonLogViewer.Name = "ButtonLogViewer";
			this.ButtonLogViewer.Size = new global::System.Drawing.Size(128, 21);
			this.ButtonLogViewer.TabIndex = 35;
			this.ButtonLogViewer.Text = "Log Viewer";
			this.ButtonLogViewer.UseVisualStyleBackColor = true;
			this.ButtonLogViewer.Click += new global::System.EventHandler(this.ButtonLogViewer_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 12f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(691, 695);
			base.Controls.Add(this.ButtonLogViewer);
			base.Controls.Add(this.ListMachine);
			base.Controls.Add(this.TextMachineStatus);
			base.Controls.Add(this.ButtonStartServer);
			base.Controls.Add(this.ButtonStartService);
			base.Controls.Add(this.ButtonStopService);
			base.Controls.Add(this.ButtonGatherLog);
			base.Controls.Add(this.ButtonReloadSetting);
			base.Controls.Add(this.ButtonUpdateServer);
			base.Controls.Add(this.ButtonKillServer);
			base.Controls.Add(this.TextServiceStatus);
			base.Controls.Add(this.TextUserCount);
			base.Controls.Add(this.ButtonRefreshUserCount);
			base.Controls.Add(this.ListOperation);
			base.Controls.Add(this.TextMachineInfo);
			base.Controls.Add(this.TextLog);
			base.Controls.Add(this.TextCommand);
			base.Controls.Add(this.ButtonCommand);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			this.MinimumSize = new global::System.Drawing.Size(400, 600);
			base.Name = "ExecutionSupporterForm";
			this.Text = "Execution Supporter";
			base.Load += new global::System.EventHandler(this.ExecutionSupporterForm_Load);
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.ExecutionSupporterForm_FormClosing);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private global::System.Windows.Forms.TextBox TextCommand;

		private global::System.Windows.Forms.Button ButtonRefreshUserCount;

		public global::System.Windows.Forms.TextBox TextUserCount;

		public global::System.Windows.Forms.ListBox ListOperation;

		public global::System.Windows.Forms.TextBox TextMachineInfo;

		public global::System.Windows.Forms.RichTextBox TextLog;

		public global::System.Windows.Forms.TextBox TextServiceStatus;

		private global::System.Windows.Forms.Button ButtonCommand;

		public global::System.Windows.Forms.Button ButtonStartServer;

		public global::System.Windows.Forms.Button ButtonStartService;

		public global::System.Windows.Forms.Button ButtonStopService;

		private global::System.Windows.Forms.Button ButtonReloadSetting;

		public global::System.Windows.Forms.Button ButtonUpdateServer;

		public global::System.Windows.Forms.Button ButtonKillServer;

		public global::System.Windows.Forms.TextBox TextMachineStatus;

		private global::System.Windows.Forms.Button ButtonGatherLog;

		public global::System.Windows.Forms.ListView ListMachine;

		private global::System.Windows.Forms.ColumnHeader colMachine;

		private global::System.Windows.Forms.Button ButtonLogViewer;
	}
}
