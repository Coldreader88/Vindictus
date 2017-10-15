using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryFeverPointMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("QueryFeverPointMessage[ ]", new object[0]);
		}
	}
}
