using System;
using System.Collections.Generic;
using System.Data;
using Devcat.Core;
using HeroesOpTool.RCUser;

namespace HeroesOpTool.UserMonitorSystem
{
	public class UserCountData
	{
		public IEnumerable<DataTable> Tables
		{
			get
			{
				return this.tableDict.Values;
			}
		}

		public UserCountData(RCUserHandler client)
		{
			this.tableDict = new Dictionary<string, DataTable>();
			client.UserCountLogged += this.OnUserCountLog;
		}

		private void OnUserCountLog(object sender, EventArgs<string> args)
		{
			this.ParseUserCountLog(args.Value);
		}

		public void ParseUserCountLog(string log)
		{
			string[] array = log.Split(new char[]
			{
				'/'
			}, StringSplitOptions.RemoveEmptyEntries);
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				DataTable dataTable = null;
				int num = text.IndexOf(':');
				if (num == -1)
				{
					throw new Exception("Invalid log string");
				}
				string text2 = text.Substring(0, num);
				string row = text.Substring(num + 1);
				if (!this.tableDict.ContainsKey(text2))
				{
					dataTable = new DataTable(text2);
					DataColumn dataColumn = new DataColumn("Time", typeof(DateTime));
					dataColumn.AllowDBNull = false;
					dataTable.Columns.Add(dataColumn);
					DataColumn dataColumn2 = new DataColumn("Total", typeof(int));
					dataTable.Columns.Add(dataColumn2);
					dataColumn2.DefaultValue = 0;
					if (this.OnTableAdd != null && MainForm.Instance != null)
					{
						MainForm.Instance.UIThread(delegate
						{
							this.OnTableAdd(this, new EventArgs<DataTable>(dataTable));
						});
					}
					this.tableDict.Add(text2, dataTable);
				}
				else
				{
					dataTable = this.tableDict[text2];
				}
				if (MainForm.Instance != null)
				{
					MainForm.Instance.UIThread(delegate
					{
						this.ParseRow(row, dataTable);
					});
				}
				else
				{
					this.ParseRow(row, dataTable);
				}
			}
		}

		private void ParseRow(string row, DataTable table)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			foreach (string text in row.Split(new char[]
			{
				';'
			}, StringSplitOptions.RemoveEmptyEntries))
			{
				string[] array2 = text.Split(new char[]
				{
					'(',
					')'
				}, StringSplitOptions.RemoveEmptyEntries);
				if (array2.Length != 2)
				{
					throw new Exception("Invalid log string");
				}
				string text2 = array2[0];
				int value = int.Parse(array2[1]);
				if (table.Columns.Contains(text2))
				{
					DataColumn dataColumn = table.Columns[text2];
				}
				else
				{
					DataColumn dataColumn = new DataColumn(text2, typeof(int));
					dataColumn.DefaultValue = 0;
					table.Columns.Add(dataColumn);
				}
				dictionary.Add(text2, value);
			}
			DataRow dataRow = table.NewRow();
			int num = 500;
			int num2 = 3000;
			float num3 = 30f;
			int num4 = 0;
			dataRow["Time"] = DateTime.Now;
			foreach (object obj in table.Columns)
			{
				DataColumn dataColumn2 = (DataColumn)obj;
				if (!(dataColumn2.ColumnName == "Time"))
				{
					int num5 = 0;
					if (dictionary.ContainsKey(dataColumn2.ColumnName))
					{
						num5 = dictionary[dataColumn2.ColumnName];
					}
					num4 += num5;
					int num6 = 0;
					if (table.Rows.Count > 1 && !table.Rows[table.Rows.Count - 2].IsNull(dataColumn2.ColumnName))
					{
						num6 = (int)table.Rows[table.Rows.Count - 2][dataColumn2.ColumnName];
					}
					if (num5 != 0)
					{
						dataRow[dataColumn2.ColumnName] = num5;
						float num7 = (float)num6 * (num3 / 100f);
						if ((float)num6 - num7 >= (float)num5 && num6 - num5 > num && this.OnUserDrop != null)
						{
							this.OnUserDrop(this, new UserDropEventArgs(table.TableName, dataColumn2.ColumnName, num6 - num5));
						}
					}
				}
			}
			if (!dictionary.ContainsKey("Total"))
			{
				dataRow["Total"] = num4;
			}
			int num8 = 0;
			if (table.Rows.Count > 1 && !table.Rows[table.Rows.Count - 2].IsNull("Total"))
			{
				num8 = (int)table.Rows[table.Rows.Count - 2]["Total"];
			}
			if (num4 != 0)
			{
				float num9 = (float)num8 * (num3 / 100f);
				if ((float)num8 - num9 >= (float)num4 && num8 - num4 > num2 && this.OnUserDrop != null)
				{
					this.OnUserDrop(this, new UserDropEventArgs(table.TableName, "Total", num8 - num4));
				}
			}
			while (table.Rows.Count > 48)
			{
				table.Rows.RemoveAt(0);
			}
			table.Rows.Add(dataRow);
		}

		public const string TimeColumn = "Time";

		public const string TotalColumn = "Total";

		public EventHandler<EventArgs<DataTable>> OnTableAdd;

		public UserDropEventHandler OnUserDrop;

		private Dictionary<string, DataTable> tableDict;
	}
}
