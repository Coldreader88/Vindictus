using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PvpEndGameMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("PvpEndGameMessage[ ]", new object[0]);
		}
	}
}
