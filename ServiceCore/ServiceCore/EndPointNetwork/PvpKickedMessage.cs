using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PvpKickedMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("PvpKickedMessage[  ]", new object[0]);
		}
	}
}
