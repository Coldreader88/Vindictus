using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ChangedCashShopMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("ChangedCashShopMessage ", new object[0]);
		}
	}
}
