using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryMailListMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("QueryMailListMessage[ ]", new object[0]);
		}
	}
}
