namespace AdminService.Forms
{
	public partial class UserListForm : global::System.Windows.Forms.Form
	{
		private void InitializeComponent()
		{
			this.listBox1 = new global::System.Windows.Forms.ListBox();
			base.SuspendLayout();
			this.listBox1.FormattingEnabled = true;
			this.listBox1.ItemHeight = 12;
			this.listBox1.Location = new global::System.Drawing.Point(11, 16);
			this.listBox1.Name = "listBox1";
			this.listBox1.SelectionMode = global::System.Windows.Forms.SelectionMode.None;
			this.listBox1.Size = new global::System.Drawing.Size(444, 388);
			this.listBox1.TabIndex = 0;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 12f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(467, 421);
			base.Controls.Add(this.listBox1);
			base.Name = "UserListForm";
			this.Text = "UserListForm";
			base.ResumeLayout(false);
		}

		private global::System.Windows.Forms.ListBox listBox1;
	}
}
