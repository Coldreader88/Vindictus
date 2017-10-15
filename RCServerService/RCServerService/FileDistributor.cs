using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using RemoteControlSystem.ServerMessage;

namespace RemoteControlSystem.Server
{
	internal class FileDistributor
	{
		public static void Start()
		{
			if (FileDistributor._exit)
			{
				FileDistributor._exit = false;
				FileDistributor._queue = new Queue();
				FileDistributor._queueEvent = new ManualResetEvent(false);
				new Thread(new ThreadStart(FileDistributor.ProcessThread)).Start();
			}
		}

		public static void Exit()
		{
			if (!FileDistributor._exit)
			{
				FileDistributor._exit = true;
				FileDistributor._queueEvent.Set();
				FileDistributor._queue.Clear();
				FileDistributor._queueEvent = null;
				FileDistributor._queue = null;
			}
		}

		public static void OnFileChanged(string pathName, string fileName, bool isDirectory, WatcherChangeTypes changeType)
		{
			if (!FileDistributor._exit)
			{
				lock (FileDistributor._queue.SyncRoot)
				{
					FileDistributor._queue.Enqueue(new FileDistributor.FileChangedInfo(pathName, fileName, isDirectory, changeType));
					FileDistributor._queueEvent.Set();
				}
			}
		}

		public static void OnFileRenamed(string pathName, string oldFileName, string newFileName, bool isDirectory)
		{
			if (!FileDistributor._exit)
			{
				lock (FileDistributor._queue.SyncRoot)
				{
					FileDistributor._queue.Enqueue(new FileDistributor.FileChangedInfo(pathName, oldFileName, newFileName, isDirectory));
					FileDistributor._queueEvent.Set();
				}
			}
		}

		public static void ProcessThread()
		{
			Log<RCServerService>.Logger.Info("FileDistributor >> Started...");
			while (!FileDistributor._exit)
			{
				FileDistributor._queueEvent.WaitOne();
				if (FileDistributor._exit)
				{
					return;
				}
				FileDistributor.FileChangedInfo fileChangedInfo;
				lock (FileDistributor._queue.SyncRoot)
				{
					if (FileDistributor._queue.Count == 0)
					{
						FileDistributor._queueEvent.Reset();
						continue;
					}
					fileChangedInfo = (FileDistributor.FileChangedInfo)FileDistributor._queue.Dequeue();
				}
				IEnumerable<int> cliendIDList = Base.ClientServer.GetCliendIDList();
				foreach (int num in cliendIDList)
				{
					bool flag2 = false;
					RCClient client = Base.ClientServer.GetClient(num);
					RCProcessCollection rcprocessCollection = client.ProcessList as RCProcessCollection;
					foreach (RCProcess rcprocess in rcprocessCollection)
					{
						if (rcprocess.WorkingDirectory.IndexOf(fileChangedInfo.pathName) >= 0)
						{
							flag2 = true;
							break;
						}
					}
					if (flag2)
					{
						try
						{
							byte[] array = null;
							if (fileChangedInfo.changeType != WatcherChangeTypes.Deleted && !fileChangedInfo.isDirectory)
							{
								string path = string.Concat(new string[]
								{
									Directory.GetCurrentDirectory(),
									"\\DistributeFiles\\",
									fileChangedInfo.pathName,
									"\\",
									fileChangedInfo.fileName
								});
								FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
								array = new byte[fileStream.Length];
								fileStream.Read(array, 0, (int)fileStream.Length);
								fileStream.Close();
							}
							FileDistributeMessage message = new FileDistributeMessage(fileChangedInfo.changeType, fileChangedInfo.pathName, fileChangedInfo.fileName, fileChangedInfo.oldFileName, fileChangedInfo.isDirectory, array);
							Base.ClientServer.SendMessage<FileDistributeMessage>(num, message);
							Log<RCServerService>.Logger.InfoFormat("FileDistributor >> [{0}] : {1} {2}\\{3}.", new object[]
							{
								fileChangedInfo.changeType,
								fileChangedInfo.isDirectory ? "Dir " : "File ",
								fileChangedInfo.pathName,
								fileChangedInfo.fileName
							});
						}
						catch (Exception ex)
						{
							Log<RCServerService>.Logger.ErrorFormat("FileDistributor >> {0}\\{1} : {2}", fileChangedInfo.pathName, fileChangedInfo.fileName, ex.Message);
						}
					}
				}
			}
		}

		public static Queue _queue;

		public static bool _exit = true;

		private static ManualResetEvent _queueEvent;

		private struct FileChangedInfo
		{
			public FileChangedInfo(string pathName, string fileName, bool isDirectory, WatcherChangeTypes changeType)
			{
				this.pathName = pathName;
				this.fileName = fileName;
				this.oldFileName = "";
				this.isDirectory = isDirectory;
				this.changeType = changeType;
			}

			public FileChangedInfo(string pathName, string oldFileName, string newFileName, bool isDirectory)
			{
				this.pathName = pathName;
				this.fileName = newFileName;
				this.oldFileName = oldFileName;
				this.isDirectory = isDirectory;
				this.changeType = WatcherChangeTypes.Renamed;
			}

			public string pathName;

			public string fileName;

			public string oldFileName;

			public bool isDirectory;

			public WatcherChangeTypes changeType;
		}
	}
}
