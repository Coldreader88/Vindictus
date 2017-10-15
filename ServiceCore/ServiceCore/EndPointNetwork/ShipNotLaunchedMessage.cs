using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ShipNotLaunchedMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("ShipNotLaunchedMessage[ ]", new object[0]);
		}
	}
}
