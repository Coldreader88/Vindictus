namespace HeroesOpTool
{
	public partial class MainForm : global::System.Windows.Forms.Form
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
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::HeroesOpTool.MainForm));
			this.toolStripStatusAlarm = new global::System.Windows.Forms.ToolStripStatusLabel();
			this.MainMenuOpTool = new global::System.Windows.Forms.MainMenu(this.components);
			this.menuItem1 = new global::System.Windows.Forms.MenuItem();
			this.MenuItemAboutMe = new global::System.Windows.Forms.MenuItem();
			this.menuItem3 = new global::System.Windows.Forms.MenuItem();
			this.MenuItemClose = new global::System.Windows.Forms.MenuItem();
			this.menuItem5 = new global::System.Windows.Forms.MenuItem();
			this.MenuItemUser = new global::System.Windows.Forms.MenuItem();
			this.menuItem4 = new global::System.Windows.Forms.MenuItem();
			this.MenuItemHelp = new global::System.Windows.Forms.MenuItem();
			this.imageListToolIcon = new global::System.Windows.Forms.ImageList(this.components);
			this.toolStrip = new global::System.Windows.Forms.ToolStrip();
			this.TSBUser = new global::System.Windows.Forms.ToolStripButton();
			this.TSBServer = new global::System.Windows.Forms.ToolStripButton();
			global::System.Windows.Forms.StatusStrip statusStrip = new global::System.Windows.Forms.StatusStrip();
			global::System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel = new global::System.Windows.Forms.ToolStripStatusLabel();
			statusStrip.SuspendLayout();
			this.toolStrip.SuspendLayout();
			base.SuspendLayout();
			statusStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				toolStripStatusLabel,
				this.toolStripStatusAlarm
			});
			statusStrip.Location = new global::System.Drawing.Point(0, 678);
			statusStrip.Name = "statusStrip";
			statusStrip.Size = new global::System.Drawing.Size(1016, 22);
			statusStrip.TabIndex = 2;
			statusStrip.Text = "statusStrip1";
			toolStripStatusLabel.Name = "toolStripStatusLabel1";
			toolStripStatusLabel.Size = new global::System.Drawing.Size(985, 17);
			toolStripStatusLabel.Spring = true;
			this.toolStripStatusAlarm.BorderStyle = global::System.Windows.Forms.Border3DStyle.SunkenInner;
			this.toolStripStatusAlarm.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripStatusAlarm.Image = global::HeroesOpTool.Properties.Resources.alarmon;
			this.toolStripStatusAlarm.ImageTransparentColor = global::System.Drawing.Color.White;
			this.toolStripStatusAlarm.Name = "toolStripStatusAlarm";
			this.toolStripStatusAlarm.Size = new global::System.Drawing.Size(16, 17);
			this.toolStripStatusAlarm.Text = "알람";
			this.toolStripStatusAlarm.ToolTipText = "알람을 키거나 끕니다.";
			this.toolStripStatusAlarm.Click += new global::System.EventHandler(this.toolStripStatusAlarm_Click);
			this.MainMenuOpTool.MenuItems.AddRange(new global::System.Windows.Forms.MenuItem[]
			{
				this.menuItem1,
				this.menuItem5,
				this.menuItem4
			});
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new global::System.Windows.Forms.MenuItem[]
			{
				this.MenuItemAboutMe,
				this.menuItem3,
				this.MenuItemClose
			});
			this.menuItem1.Text = "파일(&F)";
			this.MenuItemAboutMe.Index = 0;
			this.MenuItemAboutMe.Text = "내 정보(&A)";
			this.MenuItemAboutMe.Click += new global::System.EventHandler(this.MenuItemAboutMe_Click);
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "-";
			this.MenuItemClose.Index = 2;
			this.MenuItemClose.Text = "종료(&X)";
			this.MenuItemClose.Click += new global::System.EventHandler(this.MenuItemClose_Click);
			this.menuItem5.Index = 1;
			this.menuItem5.MenuItems.AddRange(new global::System.Windows.Forms.MenuItem[]
			{
				this.MenuItemUser
			});
			this.menuItem5.Text = "관리(&M)";
			this.MenuItemUser.Index = 0;
			this.MenuItemUser.Text = "운영툴 사용자 관리(&U)";
			this.MenuItemUser.Click += new global::System.EventHandler(this.MenuItemUser_Click);
			this.menuItem4.Index = 2;
			this.menuItem4.MenuItems.AddRange(new global::System.Windows.Forms.MenuItem[]
			{
				this.MenuItemHelp
			});
			this.menuItem4.Text = "도움말(&H)";
			this.MenuItemHelp.Index = 0;
			this.MenuItemHelp.Shortcut = global::System.Windows.Forms.Shortcut.F1;
			this.MenuItemHelp.Text = "도움말(&H)";
			this.MenuItemHelp.Click += new global::System.EventHandler(this.MenuItemHelp_Click);
			this.imageListToolIcon.ImageStream = (global::System.Windows.Forms.ImageListStreamer)componentResourceManager.GetObject("imageListToolIcon.ImageStream");
			this.imageListToolIcon.TransparentColor = global::System.Drawing.Color.White;
			this.imageListToolIcon.Images.SetKeyName(0, "RCSControl.bmp");
			this.imageListToolIcon.Images.SetKeyName(1, "UserMonitor.bmp");
			this.toolStrip.BackColor = global::System.Drawing.SystemColors.Control;
			this.toolStrip.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.TSBServer,
				this.TSBUser
			});
			this.toolStrip.Location = new global::System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new global::System.Drawing.Size(1016, 25);
			this.toolStrip.TabIndex = 4;
			this.toolStrip.Text = "toolStrip1";
			this.TSBUser.AutoSize = false;
			this.TSBUser.BackColor = global::System.Drawing.SystemColors.Control;
			this.TSBUser.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("TSBUser.Image");
			this.TSBUser.ImageTransparentColor = global::System.Drawing.Color.White;
			this.TSBUser.Name = "TSBUser";
			this.TSBUser.Size = new global::System.Drawing.Size(83, 22);
			this.TSBUser.Text = "유저 상황";
			this.TSBUser.Click += new global::System.EventHandler(this.TSBUser_Click);
			this.TSBServer.AutoSize = false;
			this.TSBServer.BackColor = global::System.Drawing.SystemColors.Control;
			this.TSBServer.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("TSBServer.Image");
			this.TSBServer.ImageTransparentColor = global::System.Drawing.Color.White;
			this.TSBServer.Name = "TSBServer";
			this.TSBServer.Size = new global::System.Drawing.Size(83, 22);
			this.TSBServer.Text = "서버 상태";
			this.TSBServer.Click += new global::System.EventHandler(this.TSBServer_Click);
			this.AutoScaleBaseSize = new global::System.Drawing.Size(6, 14);
			base.ClientSize = new global::System.Drawing.Size(1016, 700);
			base.Controls.Add(this.toolStrip);
			base.Controls.Add(statusStrip);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.IsMdiContainer = true;
			base.Menu = this.MainMenuOpTool;
			base.Name = "MainForm";
			this.Text = "영웅전 운영 툴";
			base.MdiChildActivate += new global::System.EventHandler(this.MainForm_MdiChildActivate);
			base.Closing += new global::System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
			statusStrip.ResumeLayout(false);
			statusStrip.PerformLayout();
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private global::System.ComponentModel.IContainer components;

		private global::System.Windows.Forms.MainMenu MainMenuOpTool;

		private global::System.Windows.Forms.MenuItem menuItem1;

		private global::System.Windows.Forms.MenuItem MenuItemAboutMe;

		private global::System.Windows.Forms.MenuItem menuItem3;

		private global::System.Windows.Forms.MenuItem MenuItemClose;

		private global::System.Windows.Forms.MenuItem menuItem5;

		private global::System.Windows.Forms.MenuItem MenuItemUser;

		private global::System.Windows.Forms.MenuItem menuItem4;

		private global::System.Windows.Forms.MenuItem MenuItemHelp;

		private global::System.Windows.Forms.ImageList imageListToolIcon;

		private global::System.Windows.Forms.ToolStripStatusLabel toolStripStatusAlarm;

		private global::System.Windows.Forms.ToolStrip toolStrip;

		private global::System.Windows.Forms.ToolStripButton TSBUser;

		private global::System.Windows.Forms.ToolStripButton TSBServer;
	}
}
