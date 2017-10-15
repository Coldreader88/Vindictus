using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class HostInfoMessage : IMessage
	{
		public GameInfo GameInfo { get; set; }

		public int UID { get; set; }

		public int Key { get; set; }

		public bool SkipLobby { get; set; }

		public bool IsTransferToDS { get; set; }

		public HostInfoMessage(GameInfo gameInfo, int tag, int key, bool skipLobby, bool isTransferToDS)
		{
			this.GameInfo = gameInfo;
			this.UID = tag;
			this.Key = key;
			this.SkipLobby = skipLobby;
			this.IsTransferToDS = isTransferToDS;
		}

		public override string ToString()
		{
			return string.Format("HostInfoMessage[ GameInfo = {0} UID = {1} Key = {2} ]", this.GameInfo, this.UID, this.Key);
		}
	}
}
