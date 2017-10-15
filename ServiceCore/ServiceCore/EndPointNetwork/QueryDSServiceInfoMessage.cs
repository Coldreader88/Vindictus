using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryDSServiceInfoMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("ShowDSServiceInfo", new object[0]);
		}
	}
}
