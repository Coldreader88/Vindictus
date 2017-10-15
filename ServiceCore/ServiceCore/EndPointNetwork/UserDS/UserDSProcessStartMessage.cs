using System;

namespace ServiceCore.EndPointNetwork.UserDS
{
	[Serializable]
	public sealed class UserDSProcessStartMessage : IMessage
	{
		public string ServerAddress { get; set; }

		public int ServerPort { get; set; }

		public long EntityID { get; set; }

		public UserDSProcessStartMessage(string serverAddress, int serverPort, long entityID)
		{
			this.ServerAddress = serverAddress;
			this.ServerPort = serverPort;
			this.EntityID = entityID;
		}

		public override string ToString()
		{
			return string.Format("UserDSProcessStartMessage", new object[0]);
		}
	}
}
