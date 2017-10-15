using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryStoryLinesMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("QueryStoryLinesMessage[]", new object[0]);
		}
	}
}
