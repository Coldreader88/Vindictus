using System;

namespace ServiceCore.EndPointNetwork.Item
{
	[Serializable]
	public sealed class NotifyBurnMessage : IMessage
	{
		public HeroesString Message { get; set; }

		public bool IsTelepathyEnable { get; set; }

		public bool IsUIEnable { get; set; }

		public NotifyBurnMessage(HeroesString message, bool isTelepathyEnable, bool isUIEnable)
		{
			this.Message = message;
			this.IsTelepathyEnable = isTelepathyEnable;
			this.IsUIEnable = isUIEnable;
		}
	}
}
