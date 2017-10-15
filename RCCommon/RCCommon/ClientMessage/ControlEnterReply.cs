using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ClientMessage
{
	[Guid("01AEA250-D382-4c77-8B42-C1BF880CA20D")]
	[Serializable]
	public sealed class ControlEnterReply
	{
		public string Account { get; private set; }

		public IEnumerable<RCProcess> Processes
		{
			get
			{
				return this.processes;
			}
		}

		public IEnumerable<KeyValuePair<string, Authority>> Users
		{
			get
			{
				return this.users;
			}
		}

		public ControlEnterReply(string id)
		{
			this.Account = id;
			this.processes = new RCProcessCollection();
			this.users = new Dictionary<string, Authority>();
		}

		public void AddTemplate(RCProcessCollection template)
		{
			foreach (RCProcess item in template)
			{
				this.processes.Add(item);
			}
		}

		public void AddUser(string id, Authority authority)
		{
			this.users.Add(id, authority);
		}

		private RCProcessCollection processes;

		private Dictionary<string, Authority> users;
	}
}
