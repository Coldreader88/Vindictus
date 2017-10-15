using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ClientMessage
{
	[Guid("E038B886-7FB0-4fd2-94E6-9FAC2DB5A4D2")]
	[Serializable]
	public sealed class ChangeMyPasswordMessage
	{
		public byte[] OldPassword { get; private set; }

		public byte[] NewPassword { get; private set; }

		public ChangeMyPasswordMessage(byte[] oldPassword, byte[] newPassword)
		{
			this.OldPassword = new byte[oldPassword.Length];
			Buffer.BlockCopy(oldPassword, 0, this.OldPassword, 0, oldPassword.Length);
			this.NewPassword = new byte[newPassword.Length];
			Buffer.BlockCopy(newPassword, 0, this.NewPassword, 0, newPassword.Length);
		}
	}
}
