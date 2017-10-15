using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QuerySharedInventoryMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("QuerySharedInventoryMessage[]", new object[0]);
		}
	}
}
