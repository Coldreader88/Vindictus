namespace HeroesOpTool
{
	public partial class ServerMonitorForm : global::System.Windows.Forms.Form
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
			global::System.Resources.ResourceManager resourceManager = new global::System.Resources.ResourceManager(typeof(global::HeroesOpTool.ServerMonitorForm));
			this.mainMenu1 = new global::System.Windows.Forms.MainMenu();
			this.menuItem1 = new global::System.Windows.Forms.MenuItem();
			this.menuItemControl = new global::System.Windows.Forms.MenuItem();
			this.mainMenu1.MenuItems.AddRange(new global::System.Windows.Forms.MenuItem[]
			{
				this.menuItem1
			});
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new global::System.Windows.Forms.MenuItem[]
			{
				this.menuItemControl
			});
			this.menuItem1.Text = "서버 모니터링 메뉴";
			this.menuItemControl.Index = 0;
			this.menuItemControl.Shortcut = global::System.Windows.Forms.Shortcut.F10;
			this.menuItemControl.Text = "환경 설정";
			this.menuItemControl.Click += new global::System.EventHandler(this.menuItemControl_Click);
			this.AutoScaleBaseSize = new global::System.Drawing.Size(6, 14);
			base.ClientSize = new global::System.Drawing.Size(792, 573);
			base.Icon = (global::System.Drawing.Icon)resourceManager.GetObject("$this.Icon");
			base.Menu = this.mainMenu1;
			base.Name = "ServerMonitorForm";
			this.Text = "서버 모니터링 창";
		}

		private global::System.Windows.Forms.MainMenu mainMenu1;

		private global::System.Windows.Forms.MenuItem menuItem1;

		private global::System.Windows.Forms.MenuItem menuItemControl;

		//private global::System.ComponentModel.Container components;
	}
}
