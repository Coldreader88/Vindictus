using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RequestJoinPartyMessage : IMessage
	{
		public int RequestType { get; set; }

		public override string ToString()
		{
			return string.Format("RequestJoinPartyMessage[ {0} ]", this.RequestType);
		}
	}
}
