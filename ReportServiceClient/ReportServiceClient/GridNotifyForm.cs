using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ReportServiceClient
{
	public partial class GridNotifyForm : Form
	{
		public GridNotifyForm()
		{
			this.InitializeComponent();
		}

		private void OKButton_Click(object sender, EventArgs e)
		{
			base.Close();
			base.Dispose();
		}

		public void Show(string notifystring)
		{
			new Dictionary<string, string>();
			char[] separator = new char[]
			{
				'\r',
				'\n'
			};
			string[] array = notifystring.Split(separator);
			this.dataGridView1.ColumnCount = 6;
			this.dataGridView1.Columns[0].Name = "Operation";
			this.dataGridView1.Columns[1].Name = "";
			this.dataGridView1.Columns[2].Name = "min(ms)";
			this.dataGridView1.Columns[3].Name = "max(ms)";
			this.dataGridView1.Columns[4].Name = "avg(ms)";
			this.dataGridView1.Columns[5].Name = "stdev(ms)";
			this.dataGridView1.Columns[2].ValueType = typeof(double);
			this.dataGridView1.Columns[3].ValueType = typeof(double);
			this.dataGridView1.Columns[4].ValueType = typeof(double);
			this.dataGridView1.Columns[5].ValueType = typeof(double);
			this.dataGridView1.Columns[0].Width = this.dataGridView1.Width / 2;
			this.dataGridView1.Columns[1].Width = this.dataGridView1.Width / 12;
			this.dataGridView1.Columns[2].Width = this.dataGridView1.Width / 12;
			this.dataGridView1.Columns[3].Width = this.dataGridView1.Width / 12;
			this.dataGridView1.Columns[4].Width = this.dataGridView1.Width / 12;
			this.dataGridView1.Columns[5].Width = this.dataGridView1.Width / 12;
			foreach (string text in array)
			{
				if (text.Length > 0)
				{
					char[] separator2 = new char[]
					{
						'\t'
					};
					string[] array3 = text.Split(separator2);
					List<object> list = new List<object>();
					list.Add(array3[0]);
					list.Add(array3[1]);
					list.Add(double.Parse(array3[2]));
					list.Add(double.Parse(array3[3]));
					list.Add(double.Parse(array3[4]));
					list.Add(double.Parse(array3[5]));
					this.dataGridView1.Rows.Add(list.ToArray());
				}
			}
			this.dataGridView1.CellFormatting += this.CellFormatting;
			base.Show();
		}

		private void CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.ColumnIndex == 1)
			{
				if (e.Value != null && ((string)e.Value).Equals("in"))
				{
					e.CellStyle.ForeColor = Color.Red;
				}
				else
				{
					e.CellStyle.ForeColor = Color.Blue;
				}
				e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			}
			if (e.ColumnIndex > 1)
			{
				e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
			}
		}

		private void GridNotifyForm_SizeChanged(object sender, EventArgs e)
		{
			this.dataGridView1.Width = base.Width - 36;
			this.dataGridView1.Height = base.Height - 83;
			this.OKButton.Left = (base.Width - this.OKButton.Size.Width) / 2;
			this.OKButton.Top = base.Height - 65;
		}
	}
}
