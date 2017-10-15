using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Devcat.Core.Threading;
using Utility;

namespace ServiceCore
{
	public class CactiCsvFileDropper
	{
		public JobProcessor JobProcessor { get; private set; }

		public string FileName { get; private set; }

		public List<string> Columns { get; private set; }

		public Dictionary<string, int> Values { get; private set; }

		public event Action FileDropStarted;

		public CactiCsvFileDropper(string dir, string fileName, int interval_ms, List<string> columns)
		{
			this.JobProcessor = new JobProcessor();
			this.JobProcessor.Start();
			this.CactiDir = dir;
			if (!Directory.Exists(dir))
			{
				Directory.CreateDirectory(dir);
			}
			this.FileName = Path.Combine(dir, fileName);
			this.Columns = new List<string>(columns);
			this.Values = this.Columns.ToDictionary((string x) => x, (string x) => 0);
			ScheduleRepeater.Schedule(this.JobProcessor, Job.Create(new Action(this.DropFile)), interval_ms, false);
		}

		public void SetValue(string key, int value)
		{
			if (this.Values.ContainsKey(key))
			{
				this.Values[key] = value;
			}
		}

		public void DropFile()
		{
			if (this.FileDropStarted != null)
			{
				try
				{
					this.FileDropStarted();
				}
				catch (Exception ex)
				{
					Log<CactiCsvFileDropper>.Logger.Error("error while loading data", ex);
				}
			}
			try
			{
				if (!Directory.Exists(this.CactiDir))
				{
					Directory.CreateDirectory(this.CactiDir);
				}
				string path = string.Format("{0}_{1}.csv", this.FileName, DateTime.Now.ToString("yyMMdd"));
				using (StreamWriter streamWriter = new StreamWriter(path, true))
				{
					streamWriter.Write(DateTime.Now.ToString("yy-MM-dd HH:mm:ss"));
					foreach (string key in this.Columns)
					{
						streamWriter.Write(',');
						streamWriter.Write(this.Values.TryGetValue(key));
					}
					streamWriter.WriteLine();
				}
			}
			catch (Exception ex2)
			{
				Log<CactiCsvFileDropper>.Logger.Error("error while writing file", ex2);
			}
		}

		private string CactiDir;
	}
}
