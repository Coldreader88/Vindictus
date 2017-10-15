using System;

namespace ServiceCore.EndPointNetwork.Item
{
	[Serializable]
	public sealed class NotifyRandomItemMessage : IMessage
	{
		public HeroesString Message { get; set; }

		public bool IsTelepathyEnable { get; set; }

		public bool IsUIEnable { get; set; }

		public NotifyRandomItemMessage(HeroesString message, bool isTelepathyEnable, bool isUIEnable)
		{
			this.Message = message;
			this.IsTelepathyEnable = isTelepathyEnable;
			this.IsUIEnable = isUIEnable;
		}
	}
}
