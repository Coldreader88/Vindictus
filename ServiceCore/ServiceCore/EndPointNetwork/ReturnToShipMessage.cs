using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ReturnToShipMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("ReturnToShipMessage[]", new object[0]);
		}
	}
}
