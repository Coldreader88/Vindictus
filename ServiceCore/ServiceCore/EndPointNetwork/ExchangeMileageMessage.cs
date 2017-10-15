using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ExchangeMileageMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("ExchangeMileageMessage", new object[0]);
		}
	}
}
