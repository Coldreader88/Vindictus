using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryRankHeavyFavoritesInfoMessage : IMessage
	{
		public override string ToString()
		{
			return "QueryRankHeavyFavoritesInfoMessage";
		}
	}
}
