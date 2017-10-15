using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ClientMessage
{
	[Guid("01A393D1-1F5B-45c0-8063-666103131474")]
	[Serializable]
	public sealed class GetUserAuthMesssage
	{
		public int SessionKey { get; private set; }

		public string Account { get; private set; }

		public byte[] Password { get; private set; }

		public GetUserAuthMesssage(int sessionKey, string account, byte[] password)
		{
			this.SessionKey = sessionKey;
			this.Account = account;
			this.Password = new byte[password.Length];
			Buffer.BlockCopy(password, 0, this.Password, 0, password.Length);
		}
	}
}
