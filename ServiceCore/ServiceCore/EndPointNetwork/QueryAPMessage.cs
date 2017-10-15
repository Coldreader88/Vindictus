using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryAPMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("QueryAPMessage[]", new object[0]);
		}
	}
}
