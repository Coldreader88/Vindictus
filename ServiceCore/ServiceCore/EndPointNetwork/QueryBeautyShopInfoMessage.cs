using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryBeautyShopInfoMessage : IMessage
	{
		public int characterType { get; set; }
	}
}
