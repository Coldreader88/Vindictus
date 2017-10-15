namespace HeroesOpTool.RCUser.ServerMonitorSystem.SystemManage
{
	public partial class SelfUpdateForm : global::System.Windows.Forms.Form
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
			this.label2 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.label4 = new global::System.Windows.Forms.Label();
			this.label5 = new global::System.Windows.Forms.Label();
			this.textBoxAddress = new global::System.Windows.Forms.TextBox();
			this.textBoxPort = new global::System.Windows.Forms.TextBox();
			this.textBoxAccount = new global::System.Windows.Forms.TextBox();
			this.textBoxPassword = new global::System.Windows.Forms.TextBox();
			this.textBoxSourceFolder = new global::System.Windows.Forms.TextBox();
			this.textBoxSourceFiles = new global::System.Windows.Forms.TextBox();
			this.buttonOK = new global::System.Windows.Forms.Button();
			this.buttonCancel = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.label2.Location = new global::System.Drawing.Point(8, 8);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(160, 24);
			this.label2.TabIndex = 1;
			this.label2.Text = "FTP 서버의 주소와 포트";
			this.label2.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.label3.Location = new global::System.Drawing.Point(8, 32);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(160, 24);
			this.label3.TabIndex = 1;
			this.label3.Text = "FTP 서버의 계정과 암호";
			this.label3.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.label4.Location = new global::System.Drawing.Point(8, 56);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(160, 24);
			this.label4.TabIndex = 1;
			this.label4.Text = "업데이트 파일이 있는 폴더";
			this.label4.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.label5.Location = new global::System.Drawing.Point(8, 80);
			this.label5.Name = "label5";
			this.label5.Size = new global::System.Drawing.Size(216, 24);
			this.label5.TabIndex = 1;
			this.label5.Text = "업데이트 파일 리스트 (공백으로 구분)";
			this.label5.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.textBoxAddress.Location = new global::System.Drawing.Point(176, 8);
			this.textBoxAddress.Name = "textBoxAddress";
			this.textBoxAddress.Size = new global::System.Drawing.Size(168, 21);
			this.textBoxAddress.TabIndex = 2;
			this.textBoxAddress.Text = "211.218.233.20";
			this.textBoxPort.Location = new global::System.Drawing.Point(344, 8);
			this.textBoxPort.Name = "textBoxPort";
			this.textBoxPort.Size = new global::System.Drawing.Size(56, 21);
			this.textBoxPort.TabIndex = 3;
			this.textBoxPort.Text = "21";
			this.textBoxAccount.Location = new global::System.Drawing.Point(176, 32);
			this.textBoxAccount.Name = "textBoxAccount";
			this.textBoxAccount.Size = new global::System.Drawing.Size(112, 21);
			this.textBoxAccount.TabIndex = 4;
			this.textBoxAccount.Text = "mabiftp";
			this.textBoxPassword.Location = new global::System.Drawing.Point(288, 32);
			this.textBoxPassword.Name = "textBoxPassword";
			this.textBoxPassword.PasswordChar = '●';
			this.textBoxPassword.Size = new global::System.Drawing.Size(112, 21);
			this.textBoxPassword.TabIndex = 5;
			this.textBoxPassword.Text = "";
			this.textBoxSourceFolder.Location = new global::System.Drawing.Point(176, 56);
			this.textBoxSourceFolder.Name = "textBoxSourceFolder";
			this.textBoxSourceFolder.Size = new global::System.Drawing.Size(224, 21);
			this.textBoxSourceFolder.TabIndex = 6;
			this.textBoxSourceFolder.Text = "/ServerPatch/RCSystem/Client";
			this.textBoxSourceFiles.Location = new global::System.Drawing.Point(8, 104);
			this.textBoxSourceFiles.Multiline = true;
			this.textBoxSourceFiles.Name = "textBoxSourceFiles";
			this.textBoxSourceFiles.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxSourceFiles.Size = new global::System.Drawing.Size(392, 48);
			this.textBoxSourceFiles.TabIndex = 7;
			this.textBoxSourceFiles.Text = "RCClientService.exe RCSCommon.dll SaharaCS.dll";
			this.buttonOK.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new global::System.Drawing.Point(72, 168);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new global::System.Drawing.Size(104, 24);
			this.buttonOK.TabIndex = 8;
			this.buttonOK.Text = "실행";
			this.buttonOK.Click += new global::System.EventHandler(this.buttonOK_Click);
			this.buttonCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new global::System.Drawing.Point(232, 168);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new global::System.Drawing.Size(104, 24);
			this.buttonCancel.TabIndex = 8;
			this.buttonCancel.Text = "취소";
			base.AcceptButton = this.buttonOK;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(6, 14);
			base.CancelButton = this.buttonCancel;
			base.ClientSize = new global::System.Drawing.Size(416, 197);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.textBoxSourceFiles);
			base.Controls.Add(this.textBoxSourceFolder);
			base.Controls.Add(this.textBoxPassword);
			base.Controls.Add(this.textBoxAccount);
			base.Controls.Add(this.textBoxPort);
			base.Controls.Add(this.textBoxAddress);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.buttonCancel);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SelfUpdateForm";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "원격 제어 컴퓨터 업데이트용 FTP 서버 정보";
			base.ResumeLayout(false);
		}

		private global::System.Windows.Forms.Label label2;

		private global::System.Windows.Forms.Label label3;

		private global::System.Windows.Forms.Label label4;

		private global::System.Windows.Forms.Label label5;

		private global::System.Windows.Forms.TextBox textBoxAddress;

		private global::System.Windows.Forms.TextBox textBoxPort;

		private global::System.Windows.Forms.TextBox textBoxAccount;

		private global::System.Windows.Forms.TextBox textBoxPassword;

		private global::System.Windows.Forms.TextBox textBoxSourceFolder;

		private global::System.Windows.Forms.TextBox textBoxSourceFiles;

		private global::System.Windows.Forms.Button buttonOK;

		private global::System.Windows.Forms.Button buttonCancel;

		//private global::System.ComponentModel.Container components;
	}
}
