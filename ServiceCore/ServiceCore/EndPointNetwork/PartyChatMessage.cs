using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PartyChatMessage : IMessage
	{
		public string SenderName { get; set; }

		public string Message { get; set; }

		public bool isFakeTownChat { get; set; }

		public override string ToString()
		{
			return string.Format("PartyChatMessage[ sendername = {0} message = {1} ]", this.SenderName, this.Message);
		}
	}
}
