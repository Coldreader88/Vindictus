using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class DSHostConnectionQuery : IMessage
	{
		public override string ToString()
		{
			return string.Format("QueryDSHostConnection", new object[0]);
		}
	}
}
