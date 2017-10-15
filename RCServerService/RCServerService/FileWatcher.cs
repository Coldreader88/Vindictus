using System;
using System.Collections;
using System.IO;
using System.Threading;

namespace RemoteControlSystem.Server
{
	internal class FileWatcher
	{
		public static void Exit()
		{
			FileWatcher._exit = true;
		}

		public static void Start()
		{
			if (FileWatcher._exit)
			{
				FileWatcher._exit = false;
				FileWatcher._pause = false;
				new Thread(new ThreadStart(FileWatcher.ProcessThread)).Start();
			}
		}

		public static void Pause()
		{
			if (!FileWatcher._pause && !FileWatcher._exit)
			{
				Log<RCServerService>.Logger.Info("FileWatcher >> Pause watching files...");
				FileWatcher._exit = true;
				FileWatcher._pause = true;
			}
		}

		public static void Resume()
		{
			if (FileWatcher._pause && FileWatcher._exit)
			{
				Log<RCServerService>.Logger.Info("FileWatcher >> Resume watching files...");
				FileWatcher.Start();
			}
		}

		public static void ProcessThread()
		{
			FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();
			string path = Directory.GetCurrentDirectory() + "\\DistributeFiles";
			try
			{
				fileSystemWatcher.Path = path;
				fileSystemWatcher.Filter = "*.*";
				fileSystemWatcher.IncludeSubdirectories = true;
				fileSystemWatcher.Changed += FileWatcher.OnFileChanged;
				fileSystemWatcher.Deleted += FileWatcher.OnFileDeleted;
				fileSystemWatcher.Renamed += FileWatcher.OnFileRenamed;
				fileSystemWatcher.EnableRaisingEvents = true;
			}
			catch (ArgumentException)
			{
				Log<RCServerService>.Logger.Warn("FileWatcher >> Is has not started. (Folder 'DistributeFiles' doesn't exist.");
				return;
			}
			Log<RCServerService>.Logger.Info("FileWatcher >> Started...");
			while (!FileWatcher._exit)
			{
				fileSystemWatcher.WaitForChanged(WatcherChangeTypes.All);
			}
		}

		private static void OnFileChanged(object source, FileSystemEventArgs e)
		{
			FileWatcher.OnFileChanged(e.FullPath, e.ChangeType);
		}

		private static void OnFileDeleted(object source, FileSystemEventArgs e)
		{
			FileWatcher.OnFileChanged(e.FullPath, e.ChangeType);
		}

		private static void OnFileRenamed(object source, RenamedEventArgs e)
		{
			FileWatcher.OnFileRenamed(e.OldFullPath, e.FullPath);
		}

		private static void OnFileChanged(string fullPath, WatcherChangeTypes changeType)
		{
			bool isDirectory = false;
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(fullPath);
				if (directoryInfo.Attributes == FileAttributes.Directory)
				{
					isDirectory = true;
				}
			}
			catch (UnauthorizedAccessException)
			{
				return;
			}
			string pathName;
			string fileName;
			FileWatcher.SplitePathAndFile(fullPath, out pathName, out fileName);
			FileDistributor.OnFileChanged(pathName, fileName, isDirectory, changeType);
		}

		private static void OnFileRenamed(string oldPath, string newPath)
		{
			bool isDirectory = false;
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(newPath);
				if (directoryInfo.Attributes == FileAttributes.Directory)
				{
					isDirectory = true;
				}
			}
			catch (UnauthorizedAccessException)
			{
				return;
			}
			string pathName;
			string oldFileName;
			FileWatcher.SplitePathAndFile(oldPath, out pathName, out oldFileName);
			string newFileName;
			FileWatcher.SplitePathAndFile(newPath, out pathName, out newFileName);
			FileDistributor.OnFileRenamed(pathName, oldFileName, newFileName, isDirectory);
		}

		private static void SplitePathAndFile(string fullPath, out string path, out string file)
		{
			try
			{
				file = "";
				int startIndex = fullPath.IndexOf("DistributeFiles") + "DistributeFiles".Length + 1;
				path = fullPath.Substring(startIndex);
				int num = path.IndexOf('\\');
				if (num >= 0)
				{
					file = path.Substring(num + 1);
					path = path.Substring(0, num);
				}
			}
			catch (ArgumentException)
			{
				file = null;
				path = null;
			}
		}

		public const string BaseFolder = "DistributeFiles";

		private static bool _exit = true;

		private static bool _pause = false;

		private static SortedList _fileChangeInfoList = new SortedList();

		private class FileChangedInfo
		{
			public FileChangedInfo(string pathName, string fileName, WatcherChangeTypes changeType)
			{
				this._pathName = pathName;
				this._fileName = fileName;
				this._changeType = changeType;
				this._lastChangedTime = long.Parse(DateTime.Now.ToString("yyMMddhhmmss"));
			}

			public bool IsSameFile(FileWatcher.FileChangedInfo r)
			{
				return this._pathName == r._pathName && this._fileName == r._fileName && this._changeType == r._changeType;
			}

			public long GetLastChangedTime()
			{
				return this._lastChangedTime;
			}

			private string _pathName;

			private string _fileName;

			private WatcherChangeTypes _changeType;

			private long _lastChangedTime;
		}
	}
}
