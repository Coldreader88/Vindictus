using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryFishingResultMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("QueryFishingResultMessage[ ]", new object[0]);
		}
	}
}
