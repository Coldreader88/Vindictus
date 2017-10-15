using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using NPlot;
using NPlot.Windows;

namespace HeroesOpTool.UserMonitorSystem
{
	public class UserCountGraph : UserControl
	{
		public UserCountGraph(DataTable data)
		{
			this.InitializeComponent();
			this.table = data;
			this.colorIdx = 0;
			this.colors = new Color[]
			{
				Color.Green,
				Color.Black,
				Color.Brown,
				Color.Blue,
				Color.Gold,
				Color.HotPink,
				Color.LightBlue,
				Color.LightSlateGray,
				Color.Orange,
				Color.Violet,
				Color.YellowGreen
			};
			this.Dock = DockStyle.Top;
			base.Size = new Size(100, 300);
			this.table.TableNewRow += this.table_NewRow;
			base.ParentChanged += delegate(object s, EventArgs e)
			{
				if (base.Parent == null)
				{
					this.table.ColumnChanged -= this.table_ColumnChanged;
					this.table.TableNewRow -= this.table_NewRow;
				}
			};
			if (this.table.Rows.Count == 0)
			{
				return;
			}
			this.Initialize();
		}

		private bool Initialize()
		{
			if (this.table.Rows.Count == 0)
			{
				return false;
			}
			if (this.initialized)
			{
				return false;
			}
			this.InitializePlot(this.table.TableName);
			this.lineTable = new Dictionary<string, LinePlot>();
			EnumerableRowCollection<DataRow> source = this.table.AsEnumerable();
			this.maxValue = (from total in source
			select total.Field<int>("Total")).Max();
			this.RecalculateScale();
			this.table_ColumnChanged();
			this.table.TableNewRow += this.table_NewRow;
			this.initialized = true;
			return true;
		}

		private void table_ColumnChanged()
		{
			int count = this.table.Columns.Count;
			if (this.columnLength != count)
			{
				this.columnLength = count;
				HashSet<string> hashSet = new HashSet<string>(this.lineTable.Keys);
				foreach (object obj in this.table.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					if (dataColumn.ColumnName != "Time" && !this.lineTable.ContainsKey(dataColumn.ColumnName))
					{
						this.AddNewLine(dataColumn.ColumnName);
					}
					if (hashSet.Contains(dataColumn.ColumnName))
					{
						hashSet.Remove(dataColumn.ColumnName);
					}
				}
				foreach (string key in hashSet)
				{
					this.plotSurface.Remove(this.lineTable[key], true);
					this.lineTable.Remove(key);
				}
				this.UpdateLegend();
			}
		}

		private void table_ColumnChanged(object sender, DataColumnChangeEventArgs e)
		{
			this.table_ColumnChanged();
		}

		private void InitializePlot(string title)
		{
			this.plotSurface = this.CreateDefaultPlot();
			this.plotSurface.Title = title;
			this.plotSurface.XAxis1.Label = LocalizeText.Get(414);
			this.plotSurface.YAxis1.Label = LocalizeText.Get(415);
			this.plotSurface.Dock = DockStyle.Fill;
			base.Controls.Add(this.plotSurface);
		}

		protected NPlot.Windows.PlotSurface2D CreateDefaultPlot()
		{
            NPlot.Windows.PlotSurface2D plotSurface2D = new NPlot.Windows.PlotSurface2D();
			plotSurface2D.Clear();
			plotSurface2D.Add(new Grid
			{
				VerticalGridType = Grid.GridType.Fine,
				HorizontalGridType = Grid.GridType.Fine
            });
			plotSurface2D.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.HorizontalGuideline());
			LinePlot linePlot = new LinePlot();
			linePlot.Color = Color.Green;
			plotSurface2D.Add(linePlot);
			DateTimeAxis xaxis = new DateTimeAxis(plotSurface2D.XAxis1);
			plotSurface2D.XAxis1 = xaxis;
			plotSurface2D.XAxis1.SmallTickSize = 0;
			plotSurface2D.XAxis1.LargeTickSize = 0;
			plotSurface2D.XAxis1.WorldMin = (double)DateTime.Now.Ticks;
			plotSurface2D.XAxis1.WorldMax = (double)(DateTime.Now.Ticks + 108000000000L);
			LinearAxis yaxis = new LinearAxis(plotSurface2D.YAxis1);
			plotSurface2D.YAxis1 = yaxis;
			plotSurface2D.YAxis1.WorldMin = 0.0;
			plotSurface2D.YAxis1.WorldMax = 10000.0;
			plotSurface2D.YAxis1.SmallTickSize = 0;
			plotSurface2D.YAxis1.LargeTickSize = 0;
			plotSurface2D.YAxis1.NumberFormat = "{0}";
			plotSurface2D.PlotBackColor = Color.OldLace;
			plotSurface2D.SmoothingMode = SmoothingMode.AntiAlias;
			plotSurface2D.DateTimeToolTip = true;
			plotSurface2D.Remove(linePlot, false);
			plotSurface2D.Refresh();
			return plotSurface2D;
		}

		private void InitializePlots()
		{
			foreach (object obj in this.table.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (dataColumn.ColumnName != "Time")
				{
					this.AddNewLine(dataColumn.ColumnName);
				}
			}
			this.UpdateLegend();
		}

		private void UpdateLegend()
		{
			if (plotSurface.Legend == null && lineTable.Count > 0)
			{
				Legend legend = new Legend();
				legend.AttachTo(NPlot.PlotSurface2D.XAxisPosition.Top, NPlot.PlotSurface2D.YAxisPosition.Right);
                legend.VerticalEdgePlacement = Legend.Placement.Outside;
				legend.HorizontalEdgePlacement = Legend.Placement.Inside;
				legend.XOffset = 5;
				legend.YOffset = 0;
				plotSurface.Legend = legend;
				return;
			}
			if (plotSurface.Legend != null && lineTable.Count == 0)
			{
				plotSurface.Legend = null;
			}
		}

		private LinePlot AddNewLine(string name)
		{
			LinePlot linePlot = new LinePlot();
			linePlot.Color = this.GetNewColor();
			linePlot.Label = name;
			linePlot.DataSource = this.table;
			linePlot.AbscissaData = "Time";
			linePlot.OrdinateData = name;
			linePlot.Pen.Width = 2f;
			this.plotSurface.Add(linePlot);
			this.lineTable.Add(name, linePlot);
			return linePlot;
		}

		private void RecalculateScale()
		{
			int num = 10;
			if (this.maxValue >= num)
			{
				while (this.maxValue / num > 0)
				{
					num *= 10;
				}
				int num2 = this.maxValue / (num / 10);
				if (num / 10 >= 100000)
				{
					int num3 = this.maxValue % (num / 10);
					num3 /= num / 100;
					num3 = (num3 + 1) * num / 100;
					num = num2 * num / 10 + num3;
				}
				else if (num / 10 >= 10)
				{
					int num4 = this.maxValue % (num / 10);
					if (num4 >= 5 * num / 100)
					{
						num4 = num / 10;
					}
					else
					{
						num4 = 5 * num / 100;
					}
					num = num2 * num / 10 + num4;
				}
				else
				{
					num = (num2 + 1) * (num / 10);
				}
				this.plotSurface.YAxis1.WorldMax = (double)num;
			}
		}

		private void table_NewRow(object sender, DataTableNewRowEventArgs arg)
		{
			this.Initialize();
			if (this.initialized && this.table.Rows.Count > 0)
			{
				int val = arg.Row.Field<int>("Total");
				this.maxValue = Math.Max(val, this.maxValue);
				this.RecalculateScale();
				this.plotSurface.Refresh();
			}
		}

		private Color GetNewColor()
		{
			if (this.colorIdx >= this.colors.Length)
			{
				this.colorIdx = 0;
			}
			return this.colors[this.colorIdx++];
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
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(7f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Name = "UserCountGraph";
			base.Size = new Size(581, 214);
			base.ResumeLayout(false);
		}

		private const long timeRange = 108000000000L;

		private int columnLength;

		private DataTable table;

		private NPlot.Windows.PlotSurface2D plotSurface;

		private Dictionary<string, LinePlot> lineTable;

		private Color[] colors;

		private int colorIdx;

		private int maxValue;

		private bool initialized;

		//private IContainer components;
	}
}
