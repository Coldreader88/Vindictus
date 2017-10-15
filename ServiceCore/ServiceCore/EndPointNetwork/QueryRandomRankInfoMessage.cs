using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryRandomRankInfoMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("QueryRandomRankInfoMessage", new object[0]);
		}
	}
}
