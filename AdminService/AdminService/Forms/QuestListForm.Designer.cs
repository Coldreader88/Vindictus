namespace AdminService.Forms
{
	public partial class QuestListForm : global::System.Windows.Forms.Form
	{
		private void InitializeComponent()
		{
			this.listBox1 = new global::System.Windows.Forms.ListBox();
			base.SuspendLayout();
			this.listBox1.FormattingEnabled = true;
			this.listBox1.ItemHeight = 12;
			this.listBox1.Location = new global::System.Drawing.Point(13, 13);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new global::System.Drawing.Size(551, 340);
			this.listBox1.TabIndex = 0;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 12f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(576, 367);
			base.Controls.Add(this.listBox1);
			base.Name = "QuestListForm";
			this.Text = "QuestListForm";
			base.ResumeLayout(false);
		}

		private global::System.Windows.Forms.ListBox listBox1;
	}
}
