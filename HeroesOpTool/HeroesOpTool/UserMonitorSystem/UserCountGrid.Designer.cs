using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace HeroesOpTool.UserMonitorSystem
{
	public class UserCountGrid : UserControl
	{
		public UserCountGrid(DataTable data)
		{
			this.InitializeComponent();
			this.Dock = DockStyle.Top;
			base.Size = new Size(100, 200);
			this.dataGrid.RowHeadersWidth = 20;
			this.dataGrid.ColumnAdded += this.ApplyStyle;
			this.dataGrid.DataSource = data;
			this.label.Text = data.TableName;
			base.ParentChanged += delegate(object s, EventArgs e)
			{
				if (base.Parent == null)
				{
					this.dataGrid.DataSource = null;
				}
			};
			this.dataGrid.RowsAdded += delegate(object s, DataGridViewRowsAddedEventArgs e)
			{
				if (this.dataGrid.SelectedRows.Count > 0 && this.dataGrid.Rows.Count > 0 && this.dataGrid.SelectedRows[0].Cells[0].RowIndex + e.RowCount + 1 == this.dataGrid.Rows.Count)
				{
					int num = this.dataGrid.Rows.Count - 1;
					this.dataGrid.FirstDisplayedScrollingRowIndex = num;
					this.dataGrid.Rows[num].Selected = true;
				}
			};
		}

		private void ApplyStyle(object sender, DataGridViewColumnEventArgs e)
		{
			if (e.Column.Name == "Time")
			{
				e.Column.HeaderText = LocalizeText.Get(402);
				e.Column.Width = 160;
				e.Column.DefaultCellStyle.Format = "yyyy/MM/dd hh:mm:ss";
				e.Column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
				return;
			}
			if (e.Column.Name == "Total")
			{
				e.Column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
				e.Column.HeaderText = LocalizeText.Get(403);
				e.Column.Width = 80;
				return;
			}
			e.Column.Width = 60;
		}

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
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			this.dataGrid = new DataGridView();
			this.label = new Label();
			((ISupportInitialize)this.dataGrid).BeginInit();
			base.SuspendLayout();
			this.dataGrid.AllowUserToAddRows = false;
			this.dataGrid.AllowUserToDeleteRows = false;
			this.dataGrid.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = SystemColors.Control;
			dataGridViewCellStyle.Font = new Font("굴림", 9f, FontStyle.Regular, GraphicsUnit.Point, 129);
			dataGridViewCellStyle.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle.NullValue = "0";
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dataGrid.ColumnHeadersHeight = 22;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.BackColor = SystemColors.Window;
			dataGridViewCellStyle2.Font = new Font("굴림", 9f, FontStyle.Regular, GraphicsUnit.Point, 129);
			dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
			this.dataGrid.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGrid.Location = new Point(0, 27);
			this.dataGrid.Margin = new Padding(0);
			this.dataGrid.MultiSelect = false;
			this.dataGrid.Name = "dataGrid";
			this.dataGrid.ReadOnly = true;
			this.dataGrid.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
			this.dataGrid.RowTemplate.Height = 23;
			this.dataGrid.RowTemplate.ReadOnly = true;
			this.dataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dataGrid.Size = new Size(451, 116);
			this.dataGrid.TabIndex = 0;
			this.label.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.label.BackColor = SystemColors.ControlDarkDark;
			this.label.BorderStyle = BorderStyle.FixedSingle;
			this.label.Font = new Font("굴림", 9f, FontStyle.Bold, GraphicsUnit.Point, 129);
			this.label.ForeColor = Color.White;
			this.label.Location = new Point(0, 0);
			this.label.Margin = new Padding(0);
			this.label.Name = "label";
			this.label.Padding = new Padding(5);
			this.label.Size = new Size(451, 27);
			this.label.TabIndex = 1;
			this.label.Text = "테스트 서버 - Category ";
			this.label.TextAlign = ContentAlignment.MiddleLeft;
			base.AutoScaleDimensions = new SizeF(7f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.BorderStyle = BorderStyle.Fixed3D;
			base.Controls.Add(this.label);
			base.Controls.Add(this.dataGrid);
			base.Margin = new Padding(0);
			base.Name = "UserCountGrid";
			base.Size = new Size(451, 146);
			((ISupportInitialize)this.dataGrid).EndInit();
			base.ResumeLayout(false);
		}

		//private IContainer components;

		private DataGridView dataGrid;

		private Label label;
	}
}
