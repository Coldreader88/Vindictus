namespace HeroesOpTool.RCUser.ServerMonitorSystem
{
	public partial class ChildProcessListForm : global::System.Windows.Forms.Form
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
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.btnClose = new global::System.Windows.Forms.Button();
			this.btnOK = new global::System.Windows.Forms.Button();
			this.listView = new global::System.Windows.Forms.ListView();
			global::System.Windows.Forms.ColumnHeader columnHeader = new global::System.Windows.Forms.ColumnHeader();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			columnHeader.Text = "프로세스 ID";
			columnHeader.Width = 189;
			this.panel1.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.panel1.Controls.Add(this.btnClose);
			this.panel1.Controls.Add(this.btnOK);
			this.panel1.Location = new global::System.Drawing.Point(0, 351);
			this.panel1.Name = "panel1";
			this.panel1.Size = new global::System.Drawing.Size(204, 31);
			this.panel1.TabIndex = 0;
			this.btnClose.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.btnClose.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new global::System.Drawing.Point(126, 5);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new global::System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "닫기";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new global::System.EventHandler(this.btnClose_Click);
			this.btnOK.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.btnOK.Location = new global::System.Drawing.Point(3, 5);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new global::System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "확인";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new global::System.EventHandler(this.btnOK_Click);
			this.listView.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.listView.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				columnHeader
			});
			this.listView.FullRowSelect = true;
			this.listView.GridLines = true;
			this.listView.HeaderStyle = global::System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listView.HideSelection = false;
			this.listView.Location = new global::System.Drawing.Point(3, 3);
			this.listView.MultiSelect = false;
			this.listView.Name = "listView";
			this.listView.Size = new global::System.Drawing.Size(201, 347);
			this.listView.TabIndex = 1;
			this.listView.UseCompatibleStateImageBehavior = false;
			this.listView.View = global::System.Windows.Forms.View.Details;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 12f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.btnClose;
			base.ClientSize = new global::System.Drawing.Size(204, 383);
			base.Controls.Add(this.listView);
			base.Controls.Add(this.panel1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ChildProcessListForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "자식 프로세스 리스트";
			base.TopMost = true;
			this.panel1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		//private global::System.ComponentModel.IContainer components;

		private global::System.Windows.Forms.Panel panel1;

		private global::System.Windows.Forms.Button btnClose;

		private global::System.Windows.Forms.Button btnOK;

		private global::System.Windows.Forms.ListView listView;
	}
}
