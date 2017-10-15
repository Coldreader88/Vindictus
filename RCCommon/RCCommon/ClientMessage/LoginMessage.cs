using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ClientMessage
{
	[Guid("F2EB248C-17D2-48a1-916F-EAA5D8785897")]
	[Serializable]
	public sealed class LoginMessage
	{
		public string Account { get; private set; }

		public byte[] Password { get; private set; }

		public LoginMessage(string account, byte[] password)
		{
			this.Account = account;
			this.Password = new byte[password.Length];
			Buffer.BlockCopy(password, 0, this.Password, 0, password.Length);
		}
	}
}
