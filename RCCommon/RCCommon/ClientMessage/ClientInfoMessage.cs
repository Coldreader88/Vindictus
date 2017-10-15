using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ClientMessage
{
	[Guid("C11EEDC4-5A56-4dc9-8CBB-3BB0ABA0D3C8")]
	[Serializable]
	public sealed class ClientInfoMessage
	{
		public string WorkGroup { get; private set; }

		public string ServerGroup { get; private set; }

		public IEnumerable<KeyValuePair<int, RCClient>> Clients
		{
			get
			{
				return this.clients;
			}
		}

		public IEnumerable<NotifyMessage> Logs
		{
			get
			{
				return this.logs;
			}
		}

		public ClientInfoMessage(string workgroup, string servergroup)
		{
			this.WorkGroup = workgroup;
			this.ServerGroup = servergroup;
			this.clients = new Dictionary<int, RCClient>();
			this.logs = new List<NotifyMessage>();
		}

		public void AddClient(int id, RCClient client)
		{
			this.clients.Add(id, client);
		}

		public void AddLog(IEnumerable<NotifyMessage> log)
		{
			this.logs.AddRange(log);
		}

		private Dictionary<int, RCClient> clients;

		private List<NotifyMessage> logs;
	}
}
