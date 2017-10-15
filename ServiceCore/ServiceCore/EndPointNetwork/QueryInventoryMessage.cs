using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryInventoryMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("QueryInventoryMessage[]", new object[0]);
		}
	}
}
