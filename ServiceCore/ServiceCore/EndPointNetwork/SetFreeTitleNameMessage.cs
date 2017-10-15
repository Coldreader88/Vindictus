using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SetFreeTitleNameMessage : IMessage
	{
		public string FreeTitleName
		{
			get
			{
				return this.freeTitleName;
			}
		}

		public SetFreeTitleNameMessage(string freeTitleName)
		{
			this.freeTitleName = freeTitleName;
		}

		public override string ToString()
		{
			return string.Format("SetFreeTitleNameMessage[ freeTItleName = {0} ]", this.freeTitleName);
		}

		private string freeTitleName;
	}
}
