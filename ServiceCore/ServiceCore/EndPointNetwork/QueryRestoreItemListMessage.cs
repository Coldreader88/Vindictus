using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryRestoreItemListMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("QueryRestoreItemListMessage", new object[0]);
		}
	}
}
