namespace HeroesOpTool.RCUser.ServerMonitorSystem.SystemManage
{
	public partial class TwoStringInputForm : global::System.Windows.Forms.Form
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
			this.textBox1 = new global::System.Windows.Forms.TextBox();
			this.textBox2 = new global::System.Windows.Forms.TextBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.labelInfo = new global::System.Windows.Forms.Label();
			this.buttonOK = new global::System.Windows.Forms.Button();
			this.buttonCancel = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.textBox1.Location = new global::System.Drawing.Point(120, 48);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new global::System.Drawing.Size(208, 21);
			this.textBox1.TabIndex = 0;
			this.textBox2.Location = new global::System.Drawing.Point(120, 80);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new global::System.Drawing.Size(208, 21);
			this.textBox2.TabIndex = 0;
			this.label1.Location = new global::System.Drawing.Point(16, 48);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(104, 24);
			this.label1.TabIndex = 1;
			this.label1.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.label2.Location = new global::System.Drawing.Point(16, 80);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(104, 24);
			this.label2.TabIndex = 1;
			this.label2.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.labelInfo.Location = new global::System.Drawing.Point(16, 8);
			this.labelInfo.Name = "labelInfo";
			this.labelInfo.Size = new global::System.Drawing.Size(312, 32);
			this.labelInfo.TabIndex = 2;
			this.labelInfo.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonOK.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new global::System.Drawing.Point(120, 112);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new global::System.Drawing.Size(88, 24);
			this.buttonOK.TabIndex = 3;
			this.buttonOK.Text = "확인";
			this.buttonOK.Click += new global::System.EventHandler(this.buttonOK_Click);
			this.buttonCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new global::System.Drawing.Point(240, 112);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new global::System.Drawing.Size(88, 24);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "취소";
			base.AcceptButton = this.buttonOK;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(6, 14);
			base.CancelButton = this.buttonCancel;
			base.ClientSize = new global::System.Drawing.Size(338, 143);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.labelInfo);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.textBox2);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.buttonCancel);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TwoStringInputForm";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private global::System.Windows.Forms.Label labelInfo;

		private global::System.Windows.Forms.Button buttonOK;

		private global::System.Windows.Forms.Button buttonCancel;

		private global::System.Windows.Forms.TextBox textBox1;

		private global::System.Windows.Forms.TextBox textBox2;

		private global::System.Windows.Forms.Label label1;

		private global::System.Windows.Forms.Label label2;

		//private global::System.ComponentModel.Container components;
	}
}
