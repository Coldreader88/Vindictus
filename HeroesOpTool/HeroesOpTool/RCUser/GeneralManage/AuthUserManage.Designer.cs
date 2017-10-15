namespace HeroesOpTool.RCUser.GeneralManage
{
	public partial class AuthUserManage : global::System.Windows.Forms.Form
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
			this.TBoxID = new global::System.Windows.Forms.TextBox();
			this.TBoxPassword = new global::System.Windows.Forms.TextBox();
			this.TBoxRePassword = new global::System.Windows.Forms.TextBox();
			this.CBoxAuthLevel = new global::System.Windows.Forms.ComboBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.label4 = new global::System.Windows.Forms.Label();
			this.BtnOK = new global::System.Windows.Forms.Button();
			this.BtnCancel = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.TBoxID.Location = new global::System.Drawing.Point(88, 8);
			this.TBoxID.MaxLength = 16;
			this.TBoxID.Name = "TBoxID";
			this.TBoxID.Size = new global::System.Drawing.Size(192, 21);
			this.TBoxID.TabIndex = 0;
			this.TBoxID.Text = "";
			this.TBoxPassword.Location = new global::System.Drawing.Point(88, 40);
			this.TBoxPassword.MaxLength = 16;
			this.TBoxPassword.Name = "TBoxPassword";
			this.TBoxPassword.PasswordChar = '*';
			this.TBoxPassword.Size = new global::System.Drawing.Size(192, 21);
			this.TBoxPassword.TabIndex = 1;
			this.TBoxPassword.Text = "";
			this.TBoxRePassword.Location = new global::System.Drawing.Point(88, 72);
			this.TBoxRePassword.MaxLength = 16;
			this.TBoxRePassword.Name = "TBoxRePassword";
			this.TBoxRePassword.PasswordChar = '*';
			this.TBoxRePassword.Size = new global::System.Drawing.Size(192, 21);
			this.TBoxRePassword.TabIndex = 2;
			this.TBoxRePassword.Text = "";
			this.CBoxAuthLevel.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CBoxAuthLevel.Location = new global::System.Drawing.Point(88, 104);
			this.CBoxAuthLevel.Name = "CBoxAuthLevel";
			this.CBoxAuthLevel.Size = new global::System.Drawing.Size(192, 20);
			this.CBoxAuthLevel.TabIndex = 3;
			this.label1.Location = new global::System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(72, 24);
			this.label1.TabIndex = 4;
			this.label1.Text = "사용자ID";
			this.label1.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.label2.Location = new global::System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(72, 24);
			this.label2.TabIndex = 4;
			this.label2.Text = "사용자 암호";
			this.label2.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.label3.Location = new global::System.Drawing.Point(8, 72);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(72, 24);
			this.label3.TabIndex = 4;
			this.label3.Text = "암호 재확인";
			this.label3.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.label4.Location = new global::System.Drawing.Point(8, 104);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(72, 24);
			this.label4.TabIndex = 4;
			this.label4.Text = "권한";
			this.label4.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.BtnOK.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.BtnOK.Location = new global::System.Drawing.Point(40, 136);
			this.BtnOK.Name = "BtnOK";
			this.BtnOK.Size = new global::System.Drawing.Size(80, 24);
			this.BtnOK.TabIndex = 5;
			this.BtnOK.Text = "확인";
			this.BtnCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.BtnCancel.Location = new global::System.Drawing.Point(168, 136);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new global::System.Drawing.Size(80, 24);
			this.BtnCancel.TabIndex = 5;
			this.BtnCancel.Text = "취소";
			base.AcceptButton = this.BtnOK;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(6, 14);
			base.CancelButton = this.BtnCancel;
			base.ClientSize = new global::System.Drawing.Size(290, 167);
			base.Controls.AddRange(new global::System.Windows.Forms.Control[]
			{
				this.BtnOK,
				this.label1,
				this.CBoxAuthLevel,
				this.TBoxRePassword,
				this.TBoxPassword,
				this.TBoxID,
				this.label2,
				this.label3,
				this.label4,
				this.BtnCancel
			});
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "AuthUserManage";
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "새 사용자를 추가합니다.";
			base.ResumeLayout(false);
		}

		internal global::System.Windows.Forms.TextBox TBoxID;

		internal global::System.Windows.Forms.TextBox TBoxPassword;

		internal global::System.Windows.Forms.TextBox TBoxRePassword;

		internal global::System.Windows.Forms.ComboBox CBoxAuthLevel;

		private global::System.Windows.Forms.Label label1;

		private global::System.Windows.Forms.Label label2;

		private global::System.Windows.Forms.Label label3;

		private global::System.Windows.Forms.Label label4;

		private global::System.Windows.Forms.Button BtnOK;

		private global::System.Windows.Forms.Button BtnCancel;

		//private global::System.ComponentModel.Container components;
	}
}
