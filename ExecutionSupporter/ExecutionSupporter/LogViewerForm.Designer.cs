namespace ExecutionSupporter
{
	public partial class LogViewerForm : global::System.Windows.Forms.Form
	{

		private void InitializeComponent()
		{
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.TextFilter = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.ListFile = new System.Windows.Forms.ListBox();
            this.TextContent = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.TextFilter);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(678, 583);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // TextFilter
            // 
            this.TextFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilter.Location = new System.Drawing.Point(0, 0);
            this.TextFilter.Name = "TextFilter";
            this.TextFilter.Size = new System.Drawing.Size(678, 20);
            this.TextFilter.TabIndex = 0;
            this.TextFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextFilter_KeyPress);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.ListFile);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.TextContent);
            this.splitContainer2.Size = new System.Drawing.Size(678, 557);
            this.splitContainer2.SplitterDistance = 275;
            this.splitContainer2.SplitterWidth = 3;
            this.splitContainer2.TabIndex = 0;
            // 
            // ListFile
            // 
            this.ListFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListFile.FormattingEnabled = true;
            this.ListFile.Location = new System.Drawing.Point(0, 0);
            this.ListFile.Name = "ListFile";
            this.ListFile.Size = new System.Drawing.Size(275, 557);
            this.ListFile.TabIndex = 0;
            this.ListFile.SelectedIndexChanged += new System.EventHandler(this.ListFile_SelectedIndexChanged);
            // 
            // TextContent
            // 
            this.TextContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextContent.Location = new System.Drawing.Point(0, 0);
            this.TextContent.Multiline = true;
            this.TextContent.Name = "TextContent";
            this.TextContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TextContent.Size = new System.Drawing.Size(400, 557);
            this.TextContent.TabIndex = 0;
            this.TextContent.WordWrap = false;
            // 
            // LogViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 583);
            this.Controls.Add(this.splitContainer1);
            this.Name = "LogViewerForm";
            this.Text = "LogViewerForm";
            this.Load += new System.EventHandler(this.LogViewerForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		private global::System.Windows.Forms.SplitContainer splitContainer1;

		private global::System.Windows.Forms.TextBox TextFilter;

		private global::System.Windows.Forms.SplitContainer splitContainer2;

		private global::System.Windows.Forms.ListBox ListFile;

		private global::System.Windows.Forms.TextBox TextContent;
	}
}
