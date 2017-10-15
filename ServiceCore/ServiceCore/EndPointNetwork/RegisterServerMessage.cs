using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RegisterServerMessage : IMessage
	{
		public GameInfo TheInfo
		{
			get
			{
				return this.theInfo;
			}
		}

		public RegisterServerMessage(GameInfo theInfo)
		{
			this.theInfo = theInfo;
		}

		public override string ToString()
		{
			return string.Format("RegisterServerMessage[ theInfo = {0} ]", this.theInfo);
		}

		private GameInfo theInfo;
	}
}
