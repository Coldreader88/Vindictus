namespace HeroesOpTool.RCUser.ServerMonitorSystem.SystemManage
{
	public partial class RemoteControlSystemManager : global::System.Windows.Forms.Form
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
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::HeroesOpTool.RCUser.ServerMonitorSystem.SystemManage.RemoteControlSystemManager));
			this.tabControl = new global::System.Windows.Forms.TabControl();
			this.tabPageRCCControl = new global::System.Windows.Forms.TabPage();
			this.rcClientControl = new global::HeroesOpTool.RCUser.ServerMonitorSystem.SystemManage.RCClientControl();
			this.tabPageWorkGroupControl = new global::System.Windows.Forms.TabPage();
			this.workGroupControl = new global::HeroesOpTool.RCUser.ServerMonitorSystem.SystemManage.WorkGroupControl();
			this.tabPageServerGroupControl = new global::System.Windows.Forms.TabPage();
			this.serverGroupControl = new global::HeroesOpTool.RCUser.ServerMonitorSystem.SystemManage.ServerGroupControl();
			this.tabPageProcessTemplate = new global::System.Windows.Forms.TabPage();
			this.processTemplateControl = new global::HeroesOpTool.RCUser.ServerMonitorSystem.SystemManage.ProcessTemplateControl();
			this.buttonOK = new global::System.Windows.Forms.Button();
			this.buttonCancel = new global::System.Windows.Forms.Button();
			this.tabControl.SuspendLayout();
			this.tabPageRCCControl.SuspendLayout();
			this.tabPageWorkGroupControl.SuspendLayout();
			this.tabPageServerGroupControl.SuspendLayout();
			this.tabPageProcessTemplate.SuspendLayout();
			base.SuspendLayout();
			this.tabControl.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.tabControl.Controls.Add(this.tabPageRCCControl);
			this.tabControl.Controls.Add(this.tabPageWorkGroupControl);
			this.tabControl.Controls.Add(this.tabPageServerGroupControl);
			this.tabControl.Controls.Add(this.tabPageProcessTemplate);
			this.tabControl.Location = new global::System.Drawing.Point(8, 8);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new global::System.Drawing.Size(616, 408);
			this.tabControl.TabIndex = 0;
			this.tabPageRCCControl.Controls.Add(this.rcClientControl);
			this.tabPageRCCControl.Location = new global::System.Drawing.Point(4, 22);
			this.tabPageRCCControl.Name = "tabPageRCCControl";
			this.tabPageRCCControl.Size = new global::System.Drawing.Size(608, 382);
			this.tabPageRCCControl.TabIndex = 0;
			this.tabPageRCCControl.Text = "원격 제어 컴퓨터 관리";
			this.tabPageRCCControl.UseVisualStyleBackColor = true;
			this.rcClientControl.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.rcClientControl.Location = new global::System.Drawing.Point(0, 0);
			this.rcClientControl.Name = "rcClientControl";
			this.rcClientControl.Size = new global::System.Drawing.Size(608, 383);
			this.rcClientControl.TabIndex = 0;
			this.tabPageWorkGroupControl.Controls.Add(this.workGroupControl);
			this.tabPageWorkGroupControl.Location = new global::System.Drawing.Point(4, 22);
			this.tabPageWorkGroupControl.Name = "tabPageWorkGroupControl";
			this.tabPageWorkGroupControl.Size = new global::System.Drawing.Size(608, 382);
			this.tabPageWorkGroupControl.TabIndex = 1;
			this.tabPageWorkGroupControl.Text = "작업 그룹 관리";
			this.tabPageWorkGroupControl.UseVisualStyleBackColor = true;
			this.workGroupControl.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.workGroupControl.Location = new global::System.Drawing.Point(0, 0);
			this.workGroupControl.Name = "workGroupControl";
			this.workGroupControl.Size = new global::System.Drawing.Size(608, 383);
			this.workGroupControl.TabIndex = 0;
			this.tabPageServerGroupControl.Controls.Add(this.serverGroupControl);
			this.tabPageServerGroupControl.Location = new global::System.Drawing.Point(4, 22);
			this.tabPageServerGroupControl.Name = "tabPageServerGroupControl";
			this.tabPageServerGroupControl.Size = new global::System.Drawing.Size(608, 382);
			this.tabPageServerGroupControl.TabIndex = 3;
			this.tabPageServerGroupControl.Text = "서버 그룹 관리";
			this.tabPageServerGroupControl.UseVisualStyleBackColor = true;
			this.serverGroupControl.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.serverGroupControl.Location = new global::System.Drawing.Point(0, 0);
			this.serverGroupControl.Name = "serverGroupControl";
			this.serverGroupControl.Size = new global::System.Drawing.Size(608, 382);
			this.serverGroupControl.TabIndex = 0;
			this.tabPageProcessTemplate.Controls.Add(this.processTemplateControl);
			this.tabPageProcessTemplate.Location = new global::System.Drawing.Point(4, 22);
			this.tabPageProcessTemplate.Name = "tabPageProcessTemplate";
			this.tabPageProcessTemplate.Size = new global::System.Drawing.Size(608, 382);
			this.tabPageProcessTemplate.TabIndex = 2;
			this.tabPageProcessTemplate.Text = "프로그램 템플릿 관리";
			this.tabPageProcessTemplate.UseVisualStyleBackColor = true;
			this.processTemplateControl.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.processTemplateControl.Location = new global::System.Drawing.Point(0, 0);
			this.processTemplateControl.Modified = false;
			this.processTemplateControl.Name = "processTemplateControl";
			this.processTemplateControl.Size = new global::System.Drawing.Size(608, 383);
			this.processTemplateControl.TabIndex = 0;
			this.buttonOK.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.buttonOK.Location = new global::System.Drawing.Point(416, 424);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new global::System.Drawing.Size(96, 23);
			this.buttonOK.TabIndex = 1;
			this.buttonOK.Text = "확인";
			this.buttonOK.Click += new global::System.EventHandler(this.buttonOK_Click);
			this.buttonCancel.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.buttonCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new global::System.Drawing.Point(528, 424);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new global::System.Drawing.Size(96, 23);
			this.buttonCancel.TabIndex = 2;
			this.buttonCancel.Text = "취소";
			base.AcceptButton = this.buttonOK;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(6, 14);
			base.CancelButton = this.buttonCancel;
			base.ClientSize = new global::System.Drawing.Size(632, 453);
			base.Controls.Add(this.tabControl);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.buttonCancel);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MinimizeBox = false;
			base.Name = "RemoteControlSystemManager";
			base.ShowInTaskbar = false;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "원격 제어 시스템 관리자";
			base.Closing += new global::System.ComponentModel.CancelEventHandler(this.RemoteControlSystemManager_Closing);
			this.tabControl.ResumeLayout(false);
			this.tabPageRCCControl.ResumeLayout(false);
			this.tabPageWorkGroupControl.ResumeLayout(false);
			this.tabPageServerGroupControl.ResumeLayout(false);
			this.tabPageProcessTemplate.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private global::System.Windows.Forms.TabControl tabControl;

		private global::System.Windows.Forms.TabPage tabPageWorkGroupControl;

		private global::System.Windows.Forms.TabPage tabPageProcessTemplate;

		private global::System.Windows.Forms.TabPage tabPageRCCControl;

		private global::System.Windows.Forms.Button buttonOK;

		private global::System.Windows.Forms.Button buttonCancel;

		private global::HeroesOpTool.RCUser.ServerMonitorSystem.SystemManage.RCClientControl rcClientControl;

		private global::HeroesOpTool.RCUser.ServerMonitorSystem.SystemManage.WorkGroupControl workGroupControl;

		private global::HeroesOpTool.RCUser.ServerMonitorSystem.SystemManage.ServerGroupControl serverGroupControl;

		private global::HeroesOpTool.RCUser.ServerMonitorSystem.SystemManage.ProcessTemplateControl processTemplateControl;

		private global::System.Windows.Forms.TabPage tabPageServerGroupControl;

		//private global::System.ComponentModel.Container components;
	}
}
