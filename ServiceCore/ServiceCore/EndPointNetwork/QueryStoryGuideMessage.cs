using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryStoryGuideMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("QueryStoryGuideMessage[]", new object[0]);
		}
	}
}
