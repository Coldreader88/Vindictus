using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryShipListMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("QueryShipListMessage[]", new object[0]);
		}
	}
}
