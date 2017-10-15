using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class NoticeMessage : IMessage
	{
		public string Message { get; set; }

		public override string ToString()
		{
			return string.Format("NoticeMessage[ message = {0} ]", this.Message);
		}
	}
}
