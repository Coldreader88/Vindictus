namespace HeroesOpTool
{
	public partial class LogViewForm : global::System.Windows.Forms.Form
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
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::HeroesOpTool.LogViewForm));
			this.panel2 = new global::System.Windows.Forms.Panel();
			this.buttonInput = new global::System.Windows.Forms.Button();
			this.TBoxInput = new global::System.Windows.Forms.TextBox();
			this.TBoxLog = new global::HeroesOpTool.LogTextBox();
			this.panel2.SuspendLayout();
			base.SuspendLayout();
			this.panel2.Controls.Add(this.buttonInput);
			this.panel2.Controls.Add(this.TBoxInput);
			this.panel2.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new global::System.Drawing.Point(0, 446);
			this.panel2.Name = "panel2";
			this.panel2.Size = new global::System.Drawing.Size(716, 26);
			this.panel2.TabIndex = 6;
			this.buttonInput.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.buttonInput.Enabled = false;
			this.buttonInput.Location = new global::System.Drawing.Point(641, 1);
			this.buttonInput.Name = "buttonInput";
			this.buttonInput.Size = new global::System.Drawing.Size(75, 23);
			this.buttonInput.TabIndex = 3;
			this.buttonInput.Text = "입력";
			this.buttonInput.UseVisualStyleBackColor = true;
			this.TBoxInput.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.TBoxInput.Enabled = false;
			this.TBoxInput.Location = new global::System.Drawing.Point(0, 1);
			this.TBoxInput.Name = "TBoxInput";
			this.TBoxInput.Size = new global::System.Drawing.Size(638, 21);
			this.TBoxInput.TabIndex = 2;
			this.TBoxLog.DisabledBackColor = global::System.Drawing.SystemColors.Control;
			this.TBoxLog.DisabledForeColor = global::System.Drawing.SystemColors.ControlText;
			this.TBoxLog.DisabledText = null;
			this.TBoxLog.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.TBoxLog.EnabledBackColor = global::System.Drawing.Color.Black;
			this.TBoxLog.EnabledForeColor = global::System.Drawing.Color.White;
			this.TBoxLog.Location = new global::System.Drawing.Point(0, 0);
			this.TBoxLog.Name = "TBoxLog";
			this.TBoxLog.Size = new global::System.Drawing.Size(716, 446);
			this.TBoxLog.TabIndex = 7;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 12f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(716, 472);
			base.Controls.Add(this.TBoxLog);
			base.Controls.Add(this.panel2);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "LogViewForm";
			this.Text = "로그 보기";
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			base.ResumeLayout(false);
		}

		//private global::System.ComponentModel.IContainer components;

		private global::System.Windows.Forms.Panel panel2;

		private global::System.Windows.Forms.Button buttonInput;

		private global::System.Windows.Forms.TextBox TBoxInput;

		private global::HeroesOpTool.LogTextBox TBoxLog;
	}
}
