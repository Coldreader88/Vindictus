namespace HeroesOpTool.RCUser
{
	public partial class Login : global::System.Windows.Forms.Form
	{
	//	protected override void Dispose(bool disposing)
	//	{
	//		if (disposing && this.components != null)
	//		{
	//			this.components.Dispose();
	//		}
	//		base.Dispose(disposing);
	//	}

		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::HeroesOpTool.RCUser.Login));
			this.TBoxID = new global::System.Windows.Forms.TextBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.TBoxPasswd = new global::System.Windows.Forms.TextBox();
			this.BtnLogin = new global::System.Windows.Forms.Button();
			this.BtnExit = new global::System.Windows.Forms.Button();
			this.LbVersion = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.TBoxID.BackColor = global::System.Drawing.Color.White;
			this.TBoxID.Font = new global::System.Drawing.Font("Tahoma", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.TBoxID.Location = new global::System.Drawing.Point(128, 104);
			this.TBoxID.MaxLength = 16;
			this.TBoxID.Name = "TBoxID";
			this.TBoxID.Size = new global::System.Drawing.Size(112, 22);
			this.TBoxID.TabIndex = 0;
			this.label1.BackColor = global::System.Drawing.Color.Transparent;
			this.label1.Font = new global::System.Drawing.Font("Tahoma", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label1.Location = new global::System.Drawing.Point(56, 104);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(64, 24);
			this.label1.TabIndex = 1;
			this.label1.Text = "ID";
			this.label1.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.label2.BackColor = global::System.Drawing.Color.Transparent;
			this.label2.Font = new global::System.Drawing.Font("Tahoma", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label2.Location = new global::System.Drawing.Point(56, 136);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(64, 24);
			this.label2.TabIndex = 1;
			this.label2.Text = "Password";
			this.label2.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.TBoxPasswd.BackColor = global::System.Drawing.Color.White;
			this.TBoxPasswd.Font = new global::System.Drawing.Font("Tahoma", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.TBoxPasswd.Location = new global::System.Drawing.Point(128, 136);
			this.TBoxPasswd.MaxLength = 16;
			this.TBoxPasswd.Name = "TBoxPasswd";
			this.TBoxPasswd.PasswordChar = '*';
			this.TBoxPasswd.Size = new global::System.Drawing.Size(112, 22);
			this.TBoxPasswd.TabIndex = 0;
			this.BtnLogin.BackColor = global::System.Drawing.Color.Transparent;
			this.BtnLogin.FlatStyle = global::System.Windows.Forms.FlatStyle.Popup;
			this.BtnLogin.Location = new global::System.Drawing.Point(184, 168);
			this.BtnLogin.Name = "BtnLogin";
			this.BtnLogin.Size = new global::System.Drawing.Size(48, 24);
			this.BtnLogin.TabIndex = 2;
			this.BtnLogin.Text = "Login";
			this.BtnLogin.UseVisualStyleBackColor = false;
			this.BtnLogin.Click += new global::System.EventHandler(this.BtnLogin_Click);
			this.BtnExit.BackColor = global::System.Drawing.Color.Transparent;
			this.BtnExit.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.BtnExit.FlatStyle = global::System.Windows.Forms.FlatStyle.Popup;
			this.BtnExit.Location = new global::System.Drawing.Point(240, 168);
			this.BtnExit.Name = "BtnExit";
			this.BtnExit.Size = new global::System.Drawing.Size(48, 24);
			this.BtnExit.TabIndex = 2;
			this.BtnExit.Text = "Exit";
			this.BtnExit.UseVisualStyleBackColor = false;
			this.LbVersion.AutoSize = true;
			this.LbVersion.BackColor = global::System.Drawing.Color.Transparent;
			this.LbVersion.Location = new global::System.Drawing.Point(12, 173);
			this.LbVersion.Name = "LbVersion";
			this.LbVersion.Size = new global::System.Drawing.Size(32, 14);
			this.LbVersion.TabIndex = 3;
			this.LbVersion.Text = "Build";
			this.LbVersion.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			base.AcceptButton = this.BtnLogin;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(6, 15);
			this.BackgroundImage = (global::System.Drawing.Image)componentResourceManager.GetObject("$this.BackgroundImage");
			base.CancelButton = this.BtnExit;
			base.ClientSize = new global::System.Drawing.Size(300, 200);
			base.Controls.Add(this.LbVersion);
			base.Controls.Add(this.BtnLogin);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.TBoxID);
			base.Controls.Add(this.TBoxPasswd);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.BtnExit);
			this.Font = new global::System.Drawing.Font("Tahoma", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.None;
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "Login";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "HeroesOpTool Login";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		//private global::System.ComponentModel.IContainer components;

		private global::System.Windows.Forms.TextBox TBoxID;

		private global::System.Windows.Forms.Label label1;

		private global::System.Windows.Forms.Label label2;

		private global::System.Windows.Forms.TextBox TBoxPasswd;

		private global::System.Windows.Forms.Button BtnLogin;

		private global::System.Windows.Forms.Button BtnExit;

		private global::System.Windows.Forms.Label LbVersion;
	}
}
