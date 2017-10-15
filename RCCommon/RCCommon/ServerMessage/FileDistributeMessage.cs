using System;
using System.IO;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ServerMessage
{
	[Guid("588FB95E-988D-4540-AB01-1EF918AB87D8")]
	[Serializable]
	public sealed class FileDistributeMessage
	{
		public WatcherChangeTypes ChangeType { get; private set; }

		public string Path { get; private set; }

		public string FileName { get; private set; }

		public string OldFileName { get; private set; }

		public bool IsDirectory { get; private set; }

		public byte[] FileData { get; private set; }

		public FileDistributeMessage(WatcherChangeTypes changeType, string path, string fileName, string oldFileName, bool isDirectory, byte[] fileData)
		{
			this.ChangeType = changeType;
			this.Path = path;
			this.FileName = fileName;
			this.OldFileName = oldFileName;
			this.IsDirectory = isDirectory;
			if (fileData != null && fileData.Length > 0)
			{
				this.FileData = new byte[fileData.Length];
				Buffer.BlockCopy(fileData, 0, this.FileData, 0, fileData.Length);
			}
		}
	}
}
