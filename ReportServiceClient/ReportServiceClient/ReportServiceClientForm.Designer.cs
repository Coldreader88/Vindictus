namespace ReportServiceClient
{
	public partial class ReportServiceClientForm : global::System.Windows.Forms.Form
	{
		private void InitializeComponent()
		{
			this.TreeViewLeft = new global::System.Windows.Forms.TreeView();
			this.TreeViewRight = new global::System.Windows.Forms.TreeView();
			this.GetLookUpInfoButton = new global::System.Windows.Forms.Button();
			this.GetTimeReportButton = new global::System.Windows.Forms.Button();
			this.EnableTimeReportButton = new global::System.Windows.Forms.Button();
			this.DisableTimeReportButton = new global::System.Windows.Forms.Button();
			this.ShutDownEntity = new global::System.Windows.Forms.Button();
			this.textBoxSID = new global::System.Windows.Forms.TextBox();
			this.textBoxEID1 = new global::System.Windows.Forms.TextBox();
			this.textBoxEID2 = new global::System.Windows.Forms.TextBox();
			this.OpenEntityButton = new global::System.Windows.Forms.Button();
			this.ClearButton = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.TreeViewLeft.Location = new global::System.Drawing.Point(12, 64);
			this.TreeViewLeft.Name = "TreeViewLeft";
			this.TreeViewLeft.Size = new global::System.Drawing.Size(356, 727);
			this.TreeViewLeft.TabIndex = 0;
			this.TreeViewLeft.NodeMouseDoubleClick += new global::System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeViewLeft_NodeMouseDoubleClick);
			this.TreeViewLeft.AfterSelect += new global::System.Windows.Forms.TreeViewEventHandler(this.TreeViewLeft_AfterSelect);
			this.TreeViewLeft.AfterExpand += new global::System.Windows.Forms.TreeViewEventHandler(this.TreeViewLeft_AfterExpand);
			this.TreeViewRight.Location = new global::System.Drawing.Point(374, 64);
			this.TreeViewRight.Name = "TreeViewRight";
			this.TreeViewRight.Size = new global::System.Drawing.Size(356, 727);
			this.TreeViewRight.TabIndex = 1;
			this.TreeViewRight.AfterSelect += new global::System.Windows.Forms.TreeViewEventHandler(this.TreeViewRight_AfterSelect);
			this.TreeViewRight.AfterExpand += new global::System.Windows.Forms.TreeViewEventHandler(this.TreeViewRight_AfterExpand);
			this.GetLookUpInfoButton.Location = new global::System.Drawing.Point(12, 35);
			this.GetLookUpInfoButton.Name = "GetLookUpInfoButton";
			this.GetLookUpInfoButton.Size = new global::System.Drawing.Size(113, 23);
			this.GetLookUpInfoButton.TabIndex = 2;
			this.GetLookUpInfoButton.Text = "Service Info";
			this.GetLookUpInfoButton.UseVisualStyleBackColor = true;
			this.GetLookUpInfoButton.Click += new global::System.EventHandler(this.GetLookUpInfoButton_Click);
			this.GetTimeReportButton.Location = new global::System.Drawing.Point(131, 35);
			this.GetTimeReportButton.Name = "GetTimeReportButton";
			this.GetTimeReportButton.Size = new global::System.Drawing.Size(113, 23);
			this.GetTimeReportButton.TabIndex = 3;
			this.GetTimeReportButton.Text = "GetTimeReport";
			this.GetTimeReportButton.UseVisualStyleBackColor = true;
			this.GetTimeReportButton.Click += new global::System.EventHandler(this.GetTimeReportButton_Click);
			this.EnableTimeReportButton.Location = new global::System.Drawing.Point(250, 35);
			this.EnableTimeReportButton.Name = "EnableTimeReportButton";
			this.EnableTimeReportButton.Size = new global::System.Drawing.Size(128, 23);
			this.EnableTimeReportButton.TabIndex = 4;
			this.EnableTimeReportButton.Text = "EnableTimeReport";
			this.EnableTimeReportButton.UseVisualStyleBackColor = true;
			this.EnableTimeReportButton.Click += new global::System.EventHandler(this.EnableTimeReportButton_Click);
			this.DisableTimeReportButton.Location = new global::System.Drawing.Point(384, 35);
			this.DisableTimeReportButton.Name = "DisableTimeReportButton";
			this.DisableTimeReportButton.Size = new global::System.Drawing.Size(128, 23);
			this.DisableTimeReportButton.TabIndex = 5;
			this.DisableTimeReportButton.Text = "DisableTimeReport";
			this.DisableTimeReportButton.UseVisualStyleBackColor = true;
			this.DisableTimeReportButton.Click += new global::System.EventHandler(this.DisableTimeReportButton_Click);
			this.ShutDownEntity.Location = new global::System.Drawing.Point(518, 35);
			this.ShutDownEntity.Name = "ShutDownEntity";
			this.ShutDownEntity.Size = new global::System.Drawing.Size(128, 23);
			this.ShutDownEntity.TabIndex = 6;
			this.ShutDownEntity.Text = "ShutDownEntity";
			this.ShutDownEntity.UseVisualStyleBackColor = true;
			this.ShutDownEntity.Click += new global::System.EventHandler(this.ShutDownEntity_Click);
			this.textBoxSID.Location = new global::System.Drawing.Point(12, 8);
			this.textBoxSID.Name = "textBoxSID";
			this.textBoxSID.Size = new global::System.Drawing.Size(80, 21);
			this.textBoxSID.TabIndex = 7;
			this.textBoxSID.Enter += new global::System.EventHandler(this.textBoxSID_Enter);
			this.textBoxEID1.Location = new global::System.Drawing.Point(98, 8);
			this.textBoxEID1.Name = "textBoxEID1";
			this.textBoxEID1.Size = new global::System.Drawing.Size(70, 21);
			this.textBoxEID1.TabIndex = 8;
			this.textBoxEID1.Enter += new global::System.EventHandler(this.textBoxEID1_Enter);
			this.textBoxEID2.Location = new global::System.Drawing.Point(174, 8);
			this.textBoxEID2.Name = "textBoxEID2";
			this.textBoxEID2.Size = new global::System.Drawing.Size(70, 21);
			this.textBoxEID2.TabIndex = 9;
			this.textBoxEID2.Enter += new global::System.EventHandler(this.textBoxEID2_Enter);
			this.OpenEntityButton.Location = new global::System.Drawing.Point(250, 6);
			this.OpenEntityButton.Name = "OpenEntityButton";
			this.OpenEntityButton.Size = new global::System.Drawing.Size(128, 23);
			this.OpenEntityButton.TabIndex = 10;
			this.OpenEntityButton.Text = "Open Entity";
			this.OpenEntityButton.UseVisualStyleBackColor = true;
			this.OpenEntityButton.Click += new global::System.EventHandler(this.OpenEntityButton_Click);
			this.ClearButton.Location = new global::System.Drawing.Point(384, 6);
			this.ClearButton.Name = "ClearButton";
			this.ClearButton.Size = new global::System.Drawing.Size(128, 23);
			this.ClearButton.TabIndex = 11;
			this.ClearButton.Text = "Clear";
			this.ClearButton.UseVisualStyleBackColor = true;
			this.ClearButton.Click += new global::System.EventHandler(this.ClearButton_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 12f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(739, 805);
			base.Controls.Add(this.ClearButton);
			base.Controls.Add(this.OpenEntityButton);
			base.Controls.Add(this.textBoxEID2);
			base.Controls.Add(this.textBoxEID1);
			base.Controls.Add(this.textBoxSID);
			base.Controls.Add(this.ShutDownEntity);
			base.Controls.Add(this.DisableTimeReportButton);
			base.Controls.Add(this.EnableTimeReportButton);
			base.Controls.Add(this.GetTimeReportButton);
			base.Controls.Add(this.GetLookUpInfoButton);
			base.Controls.Add(this.TreeViewRight);
			base.Controls.Add(this.TreeViewLeft);
			base.MaximizeBox = false;
			base.Name = "ReportServiceClientForm";
			this.Text = "ReportService Client";
			base.Load += new global::System.EventHandler(this.ReportServiceClientForm_Load);
			base.SizeChanged += new global::System.EventHandler(this.ReportServiceClientForm_SizeChanged);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private global::System.Windows.Forms.TreeView TreeViewLeft;

		private global::System.Windows.Forms.TreeView TreeViewRight;

		private global::System.Windows.Forms.Button GetLookUpInfoButton;

		private global::System.Windows.Forms.Button GetTimeReportButton;

		private global::System.Windows.Forms.Button EnableTimeReportButton;

		private global::System.Windows.Forms.Button DisableTimeReportButton;

		private global::System.Windows.Forms.Button ShutDownEntity;

		private global::System.Windows.Forms.TextBox textBoxSID;

		private global::System.Windows.Forms.TextBox textBoxEID1;

		private global::System.Windows.Forms.TextBox textBoxEID2;

		private global::System.Windows.Forms.Button OpenEntityButton;

		private global::System.Windows.Forms.Button ClearButton;
	}
}
