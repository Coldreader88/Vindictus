using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PvpUnregisterMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("PvpUnregisterMessage[ ]", new object[0]);
		}
	}
}
