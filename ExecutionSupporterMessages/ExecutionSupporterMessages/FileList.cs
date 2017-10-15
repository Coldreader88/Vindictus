using System;
using System.Collections.Generic;
using System.IO;

namespace ExecutionSupporterMessages
{
	public class FileList
	{
		private string ServerBin { get; set; }

		private string DSBin { get; set; }

		private FileSystemWatcher ServerFSW { get; set; }

		private FileSystemWatcher DSFSW { get; set; }

		private bool IsServerFileChanged { get; set; }

		private bool IsDSFileChanged { get; set; }

		private string LatestServerFile { get; set; }

		private DateTime LatestServerFileTime { get; set; }

		private string LatestDSFile { get; set; }

		private DateTime LatestDSFileTime { get; set; }

		public event Action<Exception> ExceptionOccurred;

		public FileList(string serverBin, string dsBin)
		{
			try
			{
				this.ServerBin = serverBin;
				this.DSBin = dsBin;
				this.ServerFSW = new FileSystemWatcher
				{
					Path = serverBin,
					NotifyFilter = (NotifyFilters.Size | NotifyFilters.LastWrite),
					Filter = "*.*",
					IncludeSubdirectories = true,
					EnableRaisingEvents = true
				};
				this.ServerFSW.Created += delegate(object _, FileSystemEventArgs __)
				{
					this.IsServerFileChanged = true;
				};
				this.ServerFSW.Changed += delegate(object _, FileSystemEventArgs __)
				{
					this.IsServerFileChanged = true;
				};
				this.UpdateServerTimeStamp();
			}
			catch
			{
			}
			try
			{
				this.DSFSW = new FileSystemWatcher
				{
					Path = dsBin,
					NotifyFilter = (NotifyFilters.Size | NotifyFilters.LastWrite),
					Filter = "*.*",
					IncludeSubdirectories = true,
					EnableRaisingEvents = true
				};
				this.DSFSW.Created += delegate(object _, FileSystemEventArgs __)
				{
					this.IsDSFileChanged = true;
				};
				this.DSFSW.Changed += delegate(object _, FileSystemEventArgs __)
				{
					this.IsDSFileChanged = true;
				};
				this.UpdateDSTimeStamp();
			}
			catch
			{
			}
		}

		public DateTime GetLatestServerFileTime()
		{
			if (this.IsServerFileChanged)
			{
				this.UpdateServerTimeStamp();
			}
			return this.LatestServerFileTime;
		}

		public string GetLatestServerFile()
		{
			if (this.IsServerFileChanged)
			{
				this.UpdateServerTimeStamp();
			}
			return this.LatestServerFile;
		}

		public DateTime GetLatestDSFileTime()
		{
			if (this.IsDSFileChanged)
			{
				this.UpdateDSTimeStamp();
			}
			return this.LatestDSFileTime;
		}

		public string GetLatestDSFile()
		{
			if (this.IsDSFileChanged)
			{
				this.UpdateDSTimeStamp();
			}
			return this.LatestDSFile;
		}

		public void UpdateServerTimeStamp()
		{
			DateTime minValue = DateTime.MinValue;
			string latestServerFile = "file not found";
			this.UpdateTimeStamp(ref minValue, ref latestServerFile, this.ServerBin, "", new string[]
			{
				".dll",
				".exe",
				".config",
				"AntiCpX.hsb",
				"HSPub.key",
				"3N.mhe",
				"HShield.dat"
			});
			this.UpdateTimeStamp(ref minValue, ref latestServerFile, this.ServerBin, "x86", new string[]
			{
				".dll"
			});
			this.UpdateTimeStamp(ref minValue, ref latestServerFile, this.ServerBin, "x64", new string[]
			{
				".dll"
			});
			this.LatestServerFileTime = minValue;
			this.LatestServerFile = latestServerFile;
			this.IsServerFileChanged = false;
		}

		public void UpdateDSTimeStamp()
		{
			DateTime minValue = DateTime.MinValue;
			string latestDSFile = "file not found";
			this.UpdateTimeStamp(ref minValue, ref latestDSFile, this.DSBin, "", new string[]
			{
				".dll",
				".exe"
			});
			this.UpdateTimeStamp(ref minValue, ref latestDSFile, this.DSBin, "bin", new string[]
			{
				".dll"
			});
			this.UpdateTimeStamp(ref minValue, ref latestDSFile, this.DSBin, "maps", new string[]
			{
				".bsp"
			});
			this.UpdateTimeStamp(ref minValue, ref latestDSFile, this.DSBin, "sqlite", new string[]
			{
				".db3"
			});
			this.UpdateTimeStamp_Rec(ref minValue, ref latestDSFile, this.DSBin, "scripts", new string[]
			{
				".txt"
			});
			this.LatestDSFileTime = minValue;
			this.LatestDSFile = latestDSFile;
			this.IsDSFileChanged = false;
		}

		private void UpdateTimeStamp(ref DateTime timestamp, ref string fileName, string binPath, string subPath, params string[] filterList)
		{
			try
			{
				string path = Path.Combine(binPath, subPath);
				DirectoryInfo directoryInfo = new DirectoryInfo(path);
				HashSet<string> hashSet = new HashSet<string>(filterList, StringComparer.CurrentCultureIgnoreCase);
				foreach (FileInfo fileInfo in directoryInfo.GetFiles())
				{
					if ((hashSet.Contains(fileInfo.Name) || hashSet.Contains(fileInfo.Extension)) && fileInfo.LastWriteTime > timestamp)
					{
						timestamp = fileInfo.LastWriteTime;
						fileName = fileInfo.Name;
					}
				}
			}
			catch (Exception obj)
			{
				if (this.ExceptionOccurred != null)
				{
					this.ExceptionOccurred(obj);
				}
			}
		}

		private void UpdateTimeStamp_Rec(ref DateTime timestamp, ref string fileName, string binPath, string subPath, params string[] filterList)
		{
			try
			{
				string text = Path.Combine(binPath, subPath);
				DirectoryInfo directoryInfo = new DirectoryInfo(text);
				HashSet<string> hashSet = new HashSet<string>(filterList, StringComparer.CurrentCultureIgnoreCase);
				foreach (FileInfo fileInfo in directoryInfo.GetFiles())
				{
					if ((hashSet.Contains(fileInfo.Name) || hashSet.Contains(fileInfo.Extension)) && fileInfo.LastWriteTime > timestamp)
					{
						timestamp = fileInfo.LastWriteTime;
						fileName = fileInfo.Name;
					}
				}
				foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
				{
					this.UpdateTimeStamp_Rec(ref timestamp, ref fileName, text, directoryInfo2.Name, filterList);
				}
			}
			catch (Exception obj)
			{
				if (this.ExceptionOccurred != null)
				{
					this.ExceptionOccurred(obj);
				}
			}
		}
	}
}
