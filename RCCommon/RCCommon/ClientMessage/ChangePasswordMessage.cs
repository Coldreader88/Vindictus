using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ClientMessage
{
	[Guid("EE46EAE9-90AB-49e4-A061-6D88AFB52BA3")]
	[Serializable]
	public sealed class ChangePasswordMessage
	{
		public string Account { get; private set; }

		public byte[] NewPassword { get; private set; }

		public ChangePasswordMessage(string id, byte[] newPassword)
		{
			this.Account = id;
			this.NewPassword = new byte[newPassword.Length];
			Buffer.BlockCopy(newPassword, 0, this.NewPassword, 0, newPassword.Length);
		}
	}
}
