using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SecondPasswordMessage : IMessage
	{
		public string Password { get; set; }

		public SecondPasswordMessage(string password)
		{
			this.Password = password;
		}

		public override string ToString()
		{
			return string.Format("CheckSecondPasswordMessage [ {0} ]", this.Password);
		}
	}
}
