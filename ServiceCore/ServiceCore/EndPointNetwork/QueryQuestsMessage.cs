using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryQuestsMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("QueryQuestsMessage[]", new object[0]);
		}
	}
}
