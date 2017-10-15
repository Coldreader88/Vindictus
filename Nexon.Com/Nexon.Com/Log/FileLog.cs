using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace Nexon.Com.Log
{
	public static class FileLog
	{
		public static void CreateLog(List<string> arrLogElements)
		{
			string strRootDirectory = ConfigurationManager.AppSettings["FILELOG_ROOT"].Parse("");
			FileLog.CreateLog(strRootDirectory, string.Empty, arrLogElements);
		}

		public static void CreateLog(string strRootDirectory, string strFileNamePrefix, List<string> arrLogElements)
		{
			try
			{
				if (string.IsNullOrEmpty(strRootDirectory))
				{
					throw new Exception("Please set the log root directory.");
				}
				DirectoryInfo directoryInfo = new DirectoryInfo(strRootDirectory);
				if (!directoryInfo.Exists)
				{
					directoryInfo.Create();
				}
				string path = string.Format("{0}\\{1}_{2}{3}{4}.log", new object[]
				{
					strRootDirectory,
					strFileNamePrefix.Parse(string.Empty),
					DateTime.Now.Year.ToString(),
					DateTime.Now.Month.ToString().PadLeft(2, '0'),
					DateTime.Now.Day.ToString().PadLeft(2, '0')
				});
				if (!File.Exists(path))
				{
					using (File.Create(path))
					{
					}
				}
				using (StreamWriter streamWriter = File.AppendText(path))
				{
					foreach (string value in arrLogElements)
					{
						streamWriter.Write("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]  ");
						streamWriter.WriteLine(value);
					}
				}
			}
			catch (Exception)
			{
			}
		}
	}
}
