using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SetSecondPasswordMessage : IMessage
	{
		public string NewPassword { get; set; }

		public SetSecondPasswordMessage(string newPassword)
		{
			this.NewPassword = newPassword;
		}

		public override string ToString()
		{
			return string.Format("SetSecondPasswordMessage [ ]", new object[0]);
		}
	}
}
