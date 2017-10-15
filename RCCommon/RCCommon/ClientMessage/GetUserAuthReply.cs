using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ClientMessage
{
	[Guid("5BEE066E-142A-4d0d-8F45-42C357897C75")]
	[Serializable]
	public sealed class GetUserAuthReply
	{
		public int SessionKey { get; private set; }

		public Authority Authority { get; private set; }

		public GetUserAuthReply(int sessionKey, Authority authority)
		{
			this.SessionKey = sessionKey;
			this.Authority = authority;
		}
	}
}
