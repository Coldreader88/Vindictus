using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryBuybackListMessage : IMessage
	{
		public override string ToString()
		{
			return "QueryBuybackListMessage";
		}
	}
}
