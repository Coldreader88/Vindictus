using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ClientMessage
{
	[Guid("382C0176-CE36-4217-A122-3F1E2BFC65DB")]
	[Serializable]
	public sealed class AddUserMessage
	{
		public string Account { get; private set; }

		public byte[] Password { get; private set; }

		public Authority Authority { get; private set; }

		public AddUserMessage(string id, byte[] password, Authority authority)
		{
			this.Account = id;
			this.Password = new byte[password.Length];
			Buffer.BlockCopy(password, 0, this.Password, 0, password.Length);
			this.Authority = authority;
		}
	}
}
