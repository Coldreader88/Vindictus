namespace HeroesOpTool
{
	public partial class ExeInfoForm : global::System.Windows.Forms.Form
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
			this.listView = new global::System.Windows.Forms.ListView();
			this.columnHeaderFile = new global::System.Windows.Forms.ColumnHeader();
			this.columnHeaderDate = new global::System.Windows.Forms.ColumnHeader();
			base.SuspendLayout();
			this.listView.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.columnHeaderFile,
				this.columnHeaderDate
			});
			this.listView.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.listView.FullRowSelect = true;
			this.listView.HideSelection = false;
			this.listView.Location = new global::System.Drawing.Point(0, 0);
			this.listView.Name = "listView";
			this.listView.Size = new global::System.Drawing.Size(279, 399);
			this.listView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.listView.TabIndex = 2;
			this.listView.UseCompatibleStateImageBehavior = false;
			this.listView.View = global::System.Windows.Forms.View.Details;
			this.listView.ColumnClick += new global::System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
			this.columnHeaderFile.Text = "file";
			this.columnHeaderFile.Width = 123;
			this.columnHeaderDate.Text = "date";
			this.columnHeaderDate.Width = 142;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 12f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(279, 399);
			base.Controls.Add(this.listView);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			base.Name = "ExeInfoForm";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "File information";
			base.ResumeLayout(false);
		}

		//private global::System.ComponentModel.IContainer components;

		private global::System.Windows.Forms.ListView listView;

		private global::System.Windows.Forms.ColumnHeader columnHeaderFile;

		private global::System.Windows.Forms.ColumnHeader columnHeaderDate;
	}
}
