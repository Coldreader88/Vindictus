using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryStoryVariablesMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("QueryStoryVariablesMessage[]", new object[0]);
		}
	}
}
