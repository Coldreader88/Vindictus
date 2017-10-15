using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryNpcListMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("QueryNpcListMessage[ ]", new object[0]);
		}
	}
}
