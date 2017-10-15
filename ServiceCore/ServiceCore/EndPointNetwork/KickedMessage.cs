using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class KickedMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("KickedMessage[]", new object[0]);
		}
	}
}
