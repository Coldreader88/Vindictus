using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryReservedInfoMessage : IMessage
	{
		public override string ToString()
		{
			return "QueryReservedInfoMessage";
		}
	}
}
