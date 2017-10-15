using System;
using System.Net;
using Utility;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ConnectionRequestMessage : IMessage
	{
		public ConnectionRequestMessage(IPEndPoint endpoint, DateTime dt, int key, string category)
		{
			this.Address = endpoint.Address.ToString();
			this.Port = (ushort)endpoint.Port;
			this.PosixTime = dt.ToInt32();
			this.Key = key;
			this.Category = category;
			this.PingHostCID = -1L;
			this.GroupID = 0L;
		}

		public string Address { get; private set; }

		public ushort Port { get; private set; }

		public int PosixTime { get; private set; }

		public int Key { get; private set; }

		public string Category { get; private set; }

		public long PingHostCID { get; set; }

		public long GroupID { get; set; }
	}
}
