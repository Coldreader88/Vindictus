using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ClientMessage
{
	[Guid("3F784FC1-C7A5-45f0-A692-C5306900D15D")]
	[Serializable]
	public sealed class GetUserListReply
	{
		public IEnumerable<KeyValuePair<string, Authority>> Users
		{
			get
			{
				return this.users;
			}
		}

		public GetUserListReply()
		{
			this.users = new Dictionary<string, Authority>();
		}

		public void AddUser(string id, Authority authority)
		{
			this.users.Add(id, authority);
		}

		private Dictionary<string, Authority> users;
	}
}
