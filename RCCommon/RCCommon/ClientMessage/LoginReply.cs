using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ClientMessage
{
	[Guid("D17A141E-7CA2-4976-BB1B-BDF9C719D3F9")]
	[Serializable]
	public sealed class LoginReply
	{
		public Authority Authority { get; private set; }

		public int ServerVersion { get; private set; }

		public int ClientID { get; private set; }

		public LoginReply(int clientID, Authority authority, int serverVersion)
		{
			this.ClientID = clientID;
			this.Authority = authority;
			this.ServerVersion = serverVersion;
		}
	}
}
