namespace AdminService
{
	public partial class AdminService : global::System.Windows.Forms.Form
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
			this.Services = new global::System.Windows.Forms.ListBox();
			this.StartButton = new global::System.Windows.Forms.Button();
			this.QueryButton = new global::System.Windows.Forms.Button();
			this.ExecutionServices = new global::System.Windows.Forms.ListBox();
			this.AppDomains = new global::System.Windows.Forms.ListBox();
			this.StopButton = new global::System.Windows.Forms.Button();
			this.groupBox1 = new global::System.Windows.Forms.GroupBox();
			this.Result = new global::System.Windows.Forms.Label();
			this.Notice = new global::System.Windows.Forms.Button();
			this.textBoxNotice = new global::System.Windows.Forms.TextBox();
			this.groupBox2 = new global::System.Windows.Forms.GroupBox();
			this.Result2 = new global::System.Windows.Forms.Label();
			this.KickUser = new global::System.Windows.Forms.Button();
			this.textBoxKick = new global::System.Windows.Forms.TextBox();
			this.CheckInterval = new global::System.Windows.Forms.Timer(this.components);
			this.CCU = new global::System.Windows.Forms.Button();
			this.Quests = new global::System.Windows.Forms.Button();
			this.Parties = new global::System.Windows.Forms.Button();
			this.MicroPlays = new global::System.Windows.Forms.Button();
			this.LogFileName = new global::System.Windows.Forms.Label();
			this.LogFileNameBox = new global::System.Windows.Forms.TextBox();
			this.LogFileUpdateButton = new global::System.Windows.Forms.Button();
			this.ErrorLog = new global::System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			base.SuspendLayout();
			this.Services.FormattingEnabled = true;
			this.Services.ItemHeight = 12;
			this.Services.Location = new global::System.Drawing.Point(135, 10);
			this.Services.Name = "Services";
			this.Services.SelectionMode = global::System.Windows.Forms.SelectionMode.MultiExtended;
			this.Services.Size = new global::System.Drawing.Size(129, 244);
			this.Services.TabIndex = 0;
			this.Services.DoubleClick += new global::System.EventHandler(this.StartButton_Click);
			this.StartButton.Location = new global::System.Drawing.Point(410, 76);
			this.StartButton.Name = "StartButton";
			this.StartButton.Size = new global::System.Drawing.Size(75, 46);
			this.StartButton.TabIndex = 1;
			this.StartButton.Text = "Start";
			this.StartButton.UseVisualStyleBackColor = true;
			this.StartButton.Click += new global::System.EventHandler(this.StartButton_Click);
			this.QueryButton.Location = new global::System.Drawing.Point(410, 12);
			this.QueryButton.Name = "QueryButton";
			this.QueryButton.Size = new global::System.Drawing.Size(75, 43);
			this.QueryButton.TabIndex = 2;
			this.QueryButton.Text = "Query";
			this.QueryButton.UseVisualStyleBackColor = true;
			this.QueryButton.Click += new global::System.EventHandler(this.QueryButton_Click);
			this.ExecutionServices.FormattingEnabled = true;
			this.ExecutionServices.ItemHeight = 12;
			this.ExecutionServices.Location = new global::System.Drawing.Point(12, 10);
			this.ExecutionServices.Name = "ExecutionServices";
			this.ExecutionServices.Size = new global::System.Drawing.Size(117, 244);
			this.ExecutionServices.TabIndex = 3;
			this.ExecutionServices.SelectedIndexChanged += new global::System.EventHandler(this.ExecutionServices_SelectedIndexChanged);
			this.AppDomains.FormattingEnabled = true;
			this.AppDomains.ItemHeight = 12;
			this.AppDomains.Location = new global::System.Drawing.Point(270, 10);
			this.AppDomains.Name = "AppDomains";
			this.AppDomains.SelectionMode = global::System.Windows.Forms.SelectionMode.MultiExtended;
			this.AppDomains.Size = new global::System.Drawing.Size(134, 244);
			this.AppDomains.TabIndex = 4;
			this.AppDomains.DoubleClick += new global::System.EventHandler(this.StopButton_Click);
			this.StopButton.Location = new global::System.Drawing.Point(410, 141);
			this.StopButton.Name = "StopButton";
			this.StopButton.Size = new global::System.Drawing.Size(75, 46);
			this.StopButton.TabIndex = 5;
			this.StopButton.Text = "Stop";
			this.StopButton.UseVisualStyleBackColor = true;
			this.StopButton.Click += new global::System.EventHandler(this.StopButton_Click);
			this.groupBox1.Controls.Add(this.Result);
			this.groupBox1.Controls.Add(this.Notice);
			this.groupBox1.Controls.Add(this.textBoxNotice);
			this.groupBox1.Location = new global::System.Drawing.Point(12, 273);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new global::System.Drawing.Size(488, 118);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "System message";
			this.Result.AutoSize = true;
			this.Result.Location = new global::System.Drawing.Point(7, 90);
			this.Result.Name = "Result";
			this.Result.Size = new global::System.Drawing.Size(29, 12);
			this.Result.TabIndex = 9;
			this.Result.Text = "Result";
			this.Notice.Location = new global::System.Drawing.Point(398, 20);
			this.Notice.Name = "Notice";
			this.Notice.Size = new global::System.Drawing.Size(75, 62);
			this.Notice.TabIndex = 8;
			this.Notice.Text = "Notice";
			this.Notice.UseVisualStyleBackColor = true;
			this.Notice.Click += new global::System.EventHandler(this.Notice_Click);
			this.textBoxNotice.Location = new global::System.Drawing.Point(6, 20);
			this.textBoxNotice.Multiline = true;
			this.textBoxNotice.Name = "textBoxNotice";
			this.textBoxNotice.Size = new global::System.Drawing.Size(386, 62);
			this.textBoxNotice.TabIndex = 7;
			this.groupBox2.Controls.Add(this.Result2);
			this.groupBox2.Controls.Add(this.KickUser);
			this.groupBox2.Controls.Add(this.textBoxKick);
			this.groupBox2.Location = new global::System.Drawing.Point(12, 397);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new global::System.Drawing.Size(488, 80);
			this.groupBox2.TabIndex = 11;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Kick User";
			this.Result2.Location = new global::System.Drawing.Point(6, 55);
			this.Result2.Name = "Result2";
			this.Result2.Size = new global::System.Drawing.Size(386, 23);
			this.Result2.TabIndex = 2;
			this.Result2.Text = "Result";
			this.KickUser.Location = new global::System.Drawing.Point(398, 17);
			this.KickUser.Name = "KickUser";
			this.KickUser.Size = new global::System.Drawing.Size(75, 43);
			this.KickUser.TabIndex = 1;
			this.KickUser.Text = "Kick";
			this.KickUser.UseVisualStyleBackColor = true;
			this.KickUser.Click += new global::System.EventHandler(this.KickUser_Click);
			this.textBoxKick.Location = new global::System.Drawing.Point(6, 29);
			this.textBoxKick.Name = "textBoxKick";
			this.textBoxKick.Size = new global::System.Drawing.Size(386, 21);
			this.textBoxKick.TabIndex = 0;
			this.CheckInterval.Interval = 60000;
			this.CheckInterval.Tick += new global::System.EventHandler(this.CheckInterval_Tick);
			this.CCU.Location = new global::System.Drawing.Point(11, 489);
			this.CCU.Name = "CCU";
			this.CCU.Size = new global::System.Drawing.Size(117, 38);
			this.CCU.TabIndex = 7;
			this.CCU.Text = "CCU";
			this.CCU.UseVisualStyleBackColor = true;
			this.CCU.Click += new global::System.EventHandler(this.CCU_Click);
			this.Quests.Location = new global::System.Drawing.Point(134, 489);
			this.Quests.Name = "Quests";
			this.Quests.Size = new global::System.Drawing.Size(115, 38);
			this.Quests.TabIndex = 8;
			this.Quests.Text = "Quests";
			this.Quests.UseVisualStyleBackColor = true;
			this.Quests.Click += new global::System.EventHandler(this.Quests_Click);
			this.Parties.Location = new global::System.Drawing.Point(382, 489);
			this.Parties.Name = "Parties";
			this.Parties.Size = new global::System.Drawing.Size(117, 38);
			this.Parties.TabIndex = 9;
			this.Parties.Text = "Parties";
			this.Parties.UseVisualStyleBackColor = true;
			this.Parties.Click += new global::System.EventHandler(this.Parties_Click);
			this.MicroPlays.Location = new global::System.Drawing.Point(255, 489);
			this.MicroPlays.Name = "MicroPlays";
			this.MicroPlays.Size = new global::System.Drawing.Size(117, 38);
			this.MicroPlays.TabIndex = 10;
			this.MicroPlays.Text = "MicroPlays";
			this.MicroPlays.UseVisualStyleBackColor = true;
			this.LogFileName.AutoSize = true;
			this.LogFileName.Location = new global::System.Drawing.Point(21, 534);
			this.LogFileName.Name = "LogFileName";
			this.LogFileName.Size = new global::System.Drawing.Size(123, 12);
			this.LogFileName.TabIndex = 12;
			this.LogFileName.Text = ".\\ClientCountLog.txt";
			this.LogFileNameBox.Location = new global::System.Drawing.Point(18, 549);
			this.LogFileNameBox.Name = "LogFileNameBox";
			this.LogFileNameBox.Size = new global::System.Drawing.Size(386, 21);
			this.LogFileNameBox.TabIndex = 13;
			this.LogFileNameBox.Text = ".\\ClientLog.txt";
			this.LogFileUpdateButton.Location = new global::System.Drawing.Point(410, 534);
			this.LogFileUpdateButton.Name = "LogFileUpdateButton";
			this.LogFileUpdateButton.Size = new global::System.Drawing.Size(90, 39);
			this.LogFileUpdateButton.TabIndex = 14;
			this.LogFileUpdateButton.Text = "Update";
			this.LogFileUpdateButton.UseVisualStyleBackColor = true;
			this.LogFileUpdateButton.Click += new global::System.EventHandler(this.LogFileUpdateButton_Click);
			this.ErrorLog.AutoSize = true;
			this.ErrorLog.Location = new global::System.Drawing.Point(19, 573);
			this.ErrorLog.Name = "ErrorLog";
			this.ErrorLog.Size = new global::System.Drawing.Size(73, 12);
			this.ErrorLog.TabIndex = 15;
			this.ErrorLog.Text = "Not Logging";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 12f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(512, 605);
			base.Controls.Add(this.ErrorLog);
			base.Controls.Add(this.LogFileUpdateButton);
			base.Controls.Add(this.LogFileNameBox);
			base.Controls.Add(this.LogFileName);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.MicroPlays);
			base.Controls.Add(this.Parties);
			base.Controls.Add(this.Quests);
			base.Controls.Add(this.CCU);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.StopButton);
			base.Controls.Add(this.AppDomains);
			base.Controls.Add(this.ExecutionServices);
			base.Controls.Add(this.QueryButton);
			base.Controls.Add(this.StartButton);
			base.Controls.Add(this.Services);
			base.Name = "AdminService";
			this.Text = "Administration Service";
			base.Load += new global::System.EventHandler(this.AdminService_Load);
			base.FormClosed += new global::System.Windows.Forms.FormClosedEventHandler(this.AdminService_FormClosed);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private global::System.ComponentModel.IContainer components;

		private global::System.Windows.Forms.ListBox Services;

		private global::System.Windows.Forms.Button StartButton;

		private global::System.Windows.Forms.Button QueryButton;

		private global::System.Windows.Forms.ListBox ExecutionServices;

		private global::System.Windows.Forms.ListBox AppDomains;

		private global::System.Windows.Forms.Button StopButton;

		private global::System.Windows.Forms.GroupBox groupBox1;

		private global::System.Windows.Forms.TextBox textBoxNotice;

		private global::System.Windows.Forms.Button Notice;

		private global::System.Windows.Forms.Label Result;

		private global::System.Windows.Forms.GroupBox groupBox2;

		private global::System.Windows.Forms.Button KickUser;

		private global::System.Windows.Forms.TextBox textBoxKick;

		private global::System.Windows.Forms.Label Result2;

		private global::System.Windows.Forms.Timer CheckInterval;

		private global::System.Windows.Forms.Button CCU;

		private global::System.Windows.Forms.Button Quests;

		private global::System.Windows.Forms.Button Parties;

		private global::System.Windows.Forms.Button MicroPlays;

		private global::System.Windows.Forms.Label LogFileName;

		private global::System.Windows.Forms.TextBox LogFileNameBox;

		private global::System.Windows.Forms.Button LogFileUpdateButton;

		private global::System.Windows.Forms.Label ErrorLog;
	}
}
