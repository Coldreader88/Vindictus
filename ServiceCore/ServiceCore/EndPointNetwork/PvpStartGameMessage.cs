using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PvpStartGameMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("PvpStartGameMessage[ ]", new object[0]);
		}
	}
}
