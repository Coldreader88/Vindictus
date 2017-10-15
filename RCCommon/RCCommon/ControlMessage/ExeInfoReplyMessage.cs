using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ControlMessage
{
	[Guid("6CB655AC-1BA6-49c3-BA6C-8B8691DBD982")]
	[Serializable]
	public sealed class ExeInfoReplyMessage
	{
		public int ClientID { get; private set; }

		public string ProcessName { get; private set; }

		public ExeInfoReplyMessage(int clientID, string processName)
		{
			this.ClientID = clientID;
			this.ProcessName = processName;
			this.files = new List<ExeInfoReplyMessage.SFileInfo>();
		}

		public void AddFile(string path, DateTime modifiedTime)
		{
			this.files.Add(new ExeInfoReplyMessage.SFileInfo
			{
				Name = path,
				DateModified = modifiedTime
			});
		}

		public IEnumerable<KeyValuePair<string, DateTime>> GetFiles()
		{
			foreach (ExeInfoReplyMessage.SFileInfo s in this.files)
			{
				ExeInfoReplyMessage.SFileInfo sfileInfo = s;
				string name = sfileInfo.Name;
				ExeInfoReplyMessage.SFileInfo sfileInfo2 = s;
				yield return new KeyValuePair<string, DateTime>(name, sfileInfo2.DateModified);
			}
			yield break;
		}

		private List<ExeInfoReplyMessage.SFileInfo> files;

		[Guid("FCD9DB35-5BBA-4932-8E5C-2FF1719A948F")]
		[Serializable]
		public struct SFileInfo
		{
			public string Name { get; set; }

			public DateTime DateModified { get; set; }
		}
	}
}
