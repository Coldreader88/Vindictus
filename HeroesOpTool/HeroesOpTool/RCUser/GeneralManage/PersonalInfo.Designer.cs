namespace HeroesOpTool.RCUser.GeneralManage
{
	public partial class PersonalInfo : global::System.Windows.Forms.Form
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
			this.label1 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.label4 = new global::System.Windows.Forms.Label();
			this.label5 = new global::System.Windows.Forms.Label();
			this.TBoxOldPassword = new global::System.Windows.Forms.TextBox();
			this.TBoxNewPassword = new global::System.Windows.Forms.TextBox();
			this.TBoxRePassword = new global::System.Windows.Forms.TextBox();
			this.BtnOK = new global::System.Windows.Forms.Button();
			this.BtnCancel = new global::System.Windows.Forms.Button();
			this.LabelID = new global::System.Windows.Forms.Label();
			this.LabelAuth = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.label1.Location = new global::System.Drawing.Point(16, 8);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(88, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "당신의 ID";
			this.label1.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.label2.Location = new global::System.Drawing.Point(16, 24);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(88, 16);
			this.label2.TabIndex = 0;
			this.label2.Text = "당신의 권한";
			this.label2.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.label3.Location = new global::System.Drawing.Point(16, 48);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(64, 24);
			this.label3.TabIndex = 0;
			this.label3.Text = "현재 암호";
			this.label3.TextAlign = global::System.Drawing.ContentAlignment.MiddleRight;
			this.label4.Location = new global::System.Drawing.Point(16, 80);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(64, 24);
			this.label4.TabIndex = 0;
			this.label4.Text = "새 암호";
			this.label4.TextAlign = global::System.Drawing.ContentAlignment.MiddleRight;
			this.label5.Location = new global::System.Drawing.Point(16, 112);
			this.label5.Name = "label5";
			this.label5.Size = new global::System.Drawing.Size(64, 24);
			this.label5.TabIndex = 0;
			this.label5.Text = "암호 확인";
			this.label5.TextAlign = global::System.Drawing.ContentAlignment.MiddleRight;
			this.TBoxOldPassword.Location = new global::System.Drawing.Point(88, 48);
			this.TBoxOldPassword.MaxLength = 16;
			this.TBoxOldPassword.Name = "TBoxOldPassword";
			this.TBoxOldPassword.PasswordChar = '*';
			this.TBoxOldPassword.Size = new global::System.Drawing.Size(184, 21);
			this.TBoxOldPassword.TabIndex = 1;
			this.TBoxOldPassword.Text = "";
			this.TBoxOldPassword.TextChanged += new global::System.EventHandler(this.TBoxOldPassword_TextChanged);
			this.TBoxNewPassword.Enabled = false;
			this.TBoxNewPassword.Location = new global::System.Drawing.Point(88, 80);
			this.TBoxNewPassword.MaxLength = 16;
			this.TBoxNewPassword.Name = "TBoxNewPassword";
			this.TBoxNewPassword.PasswordChar = '*';
			this.TBoxNewPassword.Size = new global::System.Drawing.Size(184, 21);
			this.TBoxNewPassword.TabIndex = 2;
			this.TBoxNewPassword.Text = "";
			this.TBoxRePassword.Enabled = false;
			this.TBoxRePassword.Location = new global::System.Drawing.Point(88, 112);
			this.TBoxRePassword.MaxLength = 16;
			this.TBoxRePassword.Name = "TBoxRePassword";
			this.TBoxRePassword.PasswordChar = '*';
			this.TBoxRePassword.Size = new global::System.Drawing.Size(184, 21);
			this.TBoxRePassword.TabIndex = 3;
			this.TBoxRePassword.Text = "";
			this.BtnOK.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.BtnOK.Location = new global::System.Drawing.Point(40, 152);
			this.BtnOK.Name = "BtnOK";
			this.BtnOK.Size = new global::System.Drawing.Size(88, 24);
			this.BtnOK.TabIndex = 4;
			this.BtnOK.Text = "확인";
			this.BtnCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.BtnCancel.Location = new global::System.Drawing.Point(160, 152);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new global::System.Drawing.Size(88, 24);
			this.BtnCancel.TabIndex = 4;
			this.BtnCancel.Text = "취소";
			this.LabelID.Location = new global::System.Drawing.Point(120, 8);
			this.LabelID.Name = "LabelID";
			this.LabelID.Size = new global::System.Drawing.Size(152, 16);
			this.LabelID.TabIndex = 5;
			this.LabelID.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.LabelAuth.Location = new global::System.Drawing.Point(120, 24);
			this.LabelAuth.Name = "LabelAuth";
			this.LabelAuth.Size = new global::System.Drawing.Size(152, 16);
			this.LabelAuth.TabIndex = 5;
			this.LabelAuth.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			base.AcceptButton = this.BtnOK;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(6, 14);
			base.CancelButton = this.BtnCancel;
			base.ClientSize = new global::System.Drawing.Size(292, 183);
			base.Controls.AddRange(new global::System.Windows.Forms.Control[]
			{
				this.LabelID,
				this.BtnOK,
				this.TBoxRePassword,
				this.TBoxNewPassword,
				this.TBoxOldPassword,
				this.label1,
				this.label2,
				this.label3,
				this.label4,
				this.label5,
				this.BtnCancel,
				this.LabelAuth
			});
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PersonalInfo";
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "나의 정보";
			base.ResumeLayout(false);
		}

		private global::System.Windows.Forms.Label label3;

		private global::System.Windows.Forms.Label label4;

		private global::System.Windows.Forms.Label label5;

		internal global::System.Windows.Forms.TextBox TBoxOldPassword;

		internal global::System.Windows.Forms.TextBox TBoxNewPassword;

		internal global::System.Windows.Forms.TextBox TBoxRePassword;

		private global::System.Windows.Forms.Button BtnOK;

		private global::System.Windows.Forms.Button BtnCancel;

		private global::System.Windows.Forms.Label label1;

		private global::System.Windows.Forms.Label label2;

		private global::System.Windows.Forms.Label LabelID;

		private global::System.Windows.Forms.Label LabelAuth;

		//private global::System.ComponentModel.Container components;
	}
}
