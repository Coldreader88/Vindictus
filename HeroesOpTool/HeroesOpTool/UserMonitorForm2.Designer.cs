namespace HeroesOpTool
{
	public partial class UserMonitorForm2 : global::System.Windows.Forms.Form
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
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::HeroesOpTool.UserMonitorForm2));
			this.TSComboStyle = new global::System.Windows.Forms.ToolStripComboBox();
			this.TSButtonCopy = new global::System.Windows.Forms.ToolStripButton();
			this.panel = new global::System.Windows.Forms.Panel();
			global::System.Windows.Forms.ToolStrip toolStrip = new global::System.Windows.Forms.ToolStrip();
			toolStrip.SuspendLayout();
			base.SuspendLayout();
			toolStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.TSComboStyle,
				this.TSButtonCopy
			});
			toolStrip.Location = new global::System.Drawing.Point(0, 0);
			toolStrip.Name = "toolStrip";
			toolStrip.Size = new global::System.Drawing.Size(792, 25);
			toolStrip.TabIndex = 5;
			toolStrip.Text = "toolStrip";
			this.TSComboStyle.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TSComboStyle.Name = "TSComboStyle";
			this.TSComboStyle.Size = new global::System.Drawing.Size(121, 25);
			this.TSComboStyle.SelectedIndexChanged += new global::System.EventHandler(this.TSComboStyle_SelectedIndexChanged);
			this.TSButtonCopy.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.TSButtonCopy.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("TSButtonCopy.Image");
			this.TSButtonCopy.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.TSButtonCopy.Name = "TSButtonCopy";
			this.TSButtonCopy.Size = new global::System.Drawing.Size(99, 22);
			this.TSButtonCopy.Text = global::HeroesOpTool.LocalizeText.Get(538);
			this.TSButtonCopy.Click += new global::System.EventHandler(this.TSButtonCopy_Click);
			this.panel.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.panel.AutoScroll = true;
			this.panel.BackColor = global::System.Drawing.SystemColors.AppWorkspace;
			this.panel.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel.Location = new global::System.Drawing.Point(0, 28);
			this.panel.Name = "panel";
			this.panel.Size = new global::System.Drawing.Size(792, 545);
			this.panel.TabIndex = 4;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(6, 14);
			this.BackColor = global::System.Drawing.SystemColors.Control;
			base.ClientSize = new global::System.Drawing.Size(792, 573);
			base.Controls.Add(toolStrip);
			base.Controls.Add(this.panel);
			base.Name = "UserMonitorForm2";
			this.Text = global::HeroesOpTool.LocalizeText.Get(397);
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.UserMonitorForm2_FormClosing);
			toolStrip.ResumeLayout(false);
			toolStrip.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		//private global::System.ComponentModel.IContainer components;

		private global::System.Windows.Forms.Panel panel;

		private global::System.Windows.Forms.ToolStripComboBox TSComboStyle;

		private global::System.Windows.Forms.ToolStripButton TSButtonCopy;
	}
}
