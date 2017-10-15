namespace HeroesOpTool.RCUser.GeneralManage
{
	public partial class UserView : global::System.Windows.Forms.Form
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
			this.LViewUsers = new global::System.Windows.Forms.ListView();
			this.UserId = new global::System.Windows.Forms.ColumnHeader();
			this.UserAuthority = new global::System.Windows.Forms.ColumnHeader();
			this.BtnAddUser = new global::System.Windows.Forms.Button();
			this.BtnRemoveUser = new global::System.Windows.Forms.Button();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.BtnChangeUser = new global::System.Windows.Forms.Button();
			this.BtnOK = new global::System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.LViewUsers.AllowColumnReorder = true;
			this.LViewUsers.AutoArrange = false;
			this.LViewUsers.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.UserId,
				this.UserAuthority
			});
			this.LViewUsers.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.LViewUsers.FullRowSelect = true;
			this.LViewUsers.HeaderStyle = global::System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.LViewUsers.Location = new global::System.Drawing.Point(0, 0);
			this.LViewUsers.MultiSelect = false;
			this.LViewUsers.Name = "LViewUsers";
			this.LViewUsers.Size = new global::System.Drawing.Size(258, 159);
			this.LViewUsers.TabIndex = 0;
			this.LViewUsers.View = global::System.Windows.Forms.View.Details;
			this.LViewUsers.DoubleClick += new global::System.EventHandler(this.LViewUsers_DoubleClick);
			this.LViewUsers.SelectedIndexChanged += new global::System.EventHandler(this.LViewUsers_SelectedIndexChanged);
			this.UserId.Text = "사용자 ID";
			this.UserId.Width = 120;
			this.UserAuthority.Text = "사용자 권한";
			this.UserAuthority.Width = 120;
			this.BtnAddUser.Anchor = global::System.Windows.Forms.AnchorStyles.None;
			this.BtnAddUser.Location = new global::System.Drawing.Point(16, 8);
			this.BtnAddUser.Name = "BtnAddUser";
			this.BtnAddUser.Size = new global::System.Drawing.Size(104, 24);
			this.BtnAddUser.TabIndex = 1;
			this.BtnAddUser.Text = "사용자 추가";
			this.BtnAddUser.Click += new global::System.EventHandler(this.BtnAddUser_Click);
			this.BtnRemoveUser.Anchor = global::System.Windows.Forms.AnchorStyles.None;
			this.BtnRemoveUser.Location = new global::System.Drawing.Point(136, 8);
			this.BtnRemoveUser.Name = "BtnRemoveUser";
			this.BtnRemoveUser.Size = new global::System.Drawing.Size(104, 24);
			this.BtnRemoveUser.TabIndex = 2;
			this.BtnRemoveUser.Text = "사용자 삭제";
			this.BtnRemoveUser.Click += new global::System.EventHandler(this.BtnRemoveUser_Click);
			this.panel1.Controls.Add(this.BtnAddUser);
			this.panel1.Controls.Add(this.BtnRemoveUser);
			this.panel1.Controls.Add(this.BtnChangeUser);
			this.panel1.Controls.Add(this.BtnOK);
			this.panel1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new global::System.Drawing.Point(0, 159);
			this.panel1.Name = "panel1";
			this.panel1.Size = new global::System.Drawing.Size(258, 72);
			this.panel1.TabIndex = 4;
			this.BtnChangeUser.Anchor = global::System.Windows.Forms.AnchorStyles.None;
			this.BtnChangeUser.Location = new global::System.Drawing.Point(16, 40);
			this.BtnChangeUser.Name = "BtnChangeUser";
			this.BtnChangeUser.Size = new global::System.Drawing.Size(104, 24);
			this.BtnChangeUser.TabIndex = 3;
			this.BtnChangeUser.Text = "사용자 변경";
			this.BtnChangeUser.Click += new global::System.EventHandler(this.BtnChangeUser_Click);
			this.BtnOK.Anchor = global::System.Windows.Forms.AnchorStyles.None;
			this.BtnOK.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.BtnOK.Location = new global::System.Drawing.Point(136, 40);
			this.BtnOK.Name = "BtnOK";
			this.BtnOK.Size = new global::System.Drawing.Size(104, 24);
			this.BtnOK.TabIndex = 4;
			this.BtnOK.Text = "확인";
			this.BtnOK.Click += new global::System.EventHandler(this.BtnOK_Click);
			base.AcceptButton = this.BtnOK;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(6, 14);
			base.CancelButton = this.BtnOK;
			base.ClientSize = new global::System.Drawing.Size(258, 231);
			base.Controls.Add(this.LViewUsers);
			base.Controls.Add(this.panel1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "UserView";
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "사용자 보기";
			this.panel1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private global::System.Windows.Forms.Panel panel1;

		private global::System.Windows.Forms.ListView LViewUsers;

		private global::System.Windows.Forms.Button BtnAddUser;

		private global::System.Windows.Forms.Button BtnRemoveUser;

		private global::System.Windows.Forms.ColumnHeader UserId;

		private global::System.Windows.Forms.ColumnHeader UserAuthority;

		private global::System.Windows.Forms.Button BtnChangeUser;

		private global::System.Windows.Forms.Button BtnOK;

		//private global::System.ComponentModel.Container components;
	}
}
