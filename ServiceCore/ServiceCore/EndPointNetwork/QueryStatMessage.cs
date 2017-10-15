using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryStatMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("QueryStatMessage[]", new object[0]);
		}
	}
}
