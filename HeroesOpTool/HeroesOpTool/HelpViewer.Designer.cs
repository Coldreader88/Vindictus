namespace HeroesOpTool
{
	public partial class HelpViewer : global::System.Windows.Forms.Form
	{
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.Resources.ResourceManager resourceManager = new global::System.Resources.ResourceManager(typeof(global::HeroesOpTool.HelpViewer));
			this.TViewHelpIndex = new global::System.Windows.Forms.TreeView();
			this.TreeIcons = new global::System.Windows.Forms.ImageList(this.components);
			this.splitter1 = new global::System.Windows.Forms.Splitter();
			this.HtmlHelpContent = new global::AxSHDocVw.AxWebBrowser();
			((global::System.ComponentModel.ISupportInitialize)this.HtmlHelpContent).BeginInit();
			base.SuspendLayout();
			this.TViewHelpIndex.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.TViewHelpIndex.HideSelection = false;
			this.TViewHelpIndex.ImageIndex = 3;
			this.TViewHelpIndex.ImageList = this.TreeIcons;
			this.TViewHelpIndex.Indent = 19;
			this.TViewHelpIndex.Location = new global::System.Drawing.Point(0, 0);
			this.TViewHelpIndex.Name = "TViewHelpIndex";
			this.TViewHelpIndex.PathSeparator = ".";
			this.TViewHelpIndex.SelectedImageIndex = 3;
			this.TViewHelpIndex.Size = new global::System.Drawing.Size(184, 573);
			this.TViewHelpIndex.TabIndex = 0;
			this.TViewHelpIndex.AfterSelect += new global::System.Windows.Forms.TreeViewEventHandler(this.TViewHelpIndex_AfterSelect);
			this.TreeIcons.ImageSize = new global::System.Drawing.Size(16, 16);
			this.TreeIcons.ImageStream = (global::System.Windows.Forms.ImageListStreamer)resourceManager.GetObject("TreeIcons.ImageStream");
			this.TreeIcons.TransparentColor = global::System.Drawing.Color.Transparent;
			this.splitter1.Location = new global::System.Drawing.Point(184, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new global::System.Drawing.Size(4, 573);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			this.HtmlHelpContent.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.HtmlHelpContent.Enabled = true;
			this.HtmlHelpContent.Location = new global::System.Drawing.Point(188, 0);
			this.HtmlHelpContent.OcxState = (global::System.Windows.Forms.AxHost.State)resourceManager.GetObject("HtmlHelpContent.OcxState");
			this.HtmlHelpContent.Size = new global::System.Drawing.Size(604, 573);
			this.HtmlHelpContent.TabIndex = 2;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(6, 14);
			base.ClientSize = new global::System.Drawing.Size(792, 573);
			base.Controls.Add(this.HtmlHelpContent);
			base.Controls.Add(this.splitter1);
			base.Controls.Add(this.TViewHelpIndex);
			base.Name = "HelpViewer";
			this.Text = "Help";
			((global::System.ComponentModel.ISupportInitialize)this.HtmlHelpContent).EndInit();
			base.ResumeLayout(false);
		}

		private global::System.Windows.Forms.TreeView TViewHelpIndex;

		private global::System.Windows.Forms.Splitter splitter1;

		private global::System.Windows.Forms.ImageList TreeIcons;

		private global::AxSHDocVw.AxWebBrowser HtmlHelpContent;

		private global::System.ComponentModel.IContainer components;
	}
}
