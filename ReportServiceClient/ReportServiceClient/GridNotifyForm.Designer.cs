namespace ReportServiceClient
{
	public partial class GridNotifyForm : global::System.Windows.Forms.Form
	{
		private void InitializeComponent()
		{
			this.OKButton = new global::System.Windows.Forms.Button();
			this.dataGridView1 = new global::System.Windows.Forms.DataGridView();
			((global::System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
			base.SuspendLayout();
			this.OKButton.Location = new global::System.Drawing.Point(351, 305);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new global::System.Drawing.Size(84, 27);
			this.OKButton.TabIndex = 1;
			this.OKButton.Text = "OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new global::System.EventHandler(this.OKButton_Click);
			this.dataGridView1.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new global::System.Drawing.Point(12, 12);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowTemplate.Height = 23;
			this.dataGridView1.Size = new global::System.Drawing.Size(764, 287);
			this.dataGridView1.TabIndex = 2;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 12f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(789, 336);
			base.Controls.Add(this.dataGridView1);
			base.Controls.Add(this.OKButton);
			base.Name = "GridNotifyForm";
			this.Text = "GridNotifyForm";
			base.SizeChanged += new global::System.EventHandler(this.GridNotifyForm_SizeChanged);
			((global::System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
			base.ResumeLayout(false);
		}

		private global::System.Windows.Forms.Button OKButton;

		private global::System.Windows.Forms.DataGridView dataGridView1;
	}
}
