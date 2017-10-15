using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryQuestProgressMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("QueryQuestProgressMessage[]", new object[0]);
		}
	}
}
