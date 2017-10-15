using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class StartGameMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("StartGameMessage[]", new object[0]);
		}
	}
}
