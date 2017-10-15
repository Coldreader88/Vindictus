using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ClientMessage
{
	[Guid("9DF8057D-8A6D-4852-A6FB-43753EAF538B")]
	[Serializable]
	public sealed class ChangeAuthorityMessage
	{
		public string Account { get; private set; }

		public Authority Authority { get; private set; }

		public ChangeAuthorityMessage(string id, Authority authority)
		{
			this.Account = id;
			this.Authority = authority;
		}
	}
}
