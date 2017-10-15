using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class GameStartedMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("GameStartedMessage[]", new object[0]);
		}
	}
}
