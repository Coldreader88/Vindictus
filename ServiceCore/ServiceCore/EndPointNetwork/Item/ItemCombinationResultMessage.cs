using System;

namespace ServiceCore.EndPointNetwork.Item
{
	[Serializable]
	public sealed class ItemCombinationResultMessage : IMessage
	{
		public int ResultCode { get; private set; }

		public string ResultMessage { get; private set; }

		public string ItemClass { get; private set; }

		public int Color1 { get; private set; }

		public int Color2 { get; private set; }

		public int Color3 { get; private set; }

		public ItemCombinationResultMessage(int resultCode, string resultMessage)
		{
			this.ResultCode = resultCode;
			this.ResultMessage = resultMessage;
			this.ItemClass = "";
			this.Color1 = -1;
			this.Color2 = -1;
			this.Color3 = -1;
		}

		public ItemCombinationResultMessage(int resultCode, string resultMessage, string itemClass, int color1, int color2, int color3)
		{
			this.ResultCode = resultCode;
			this.ResultMessage = resultMessage;
			this.ItemClass = itemClass;
			this.Color1 = color1;
			this.Color2 = color2;
			this.Color3 = color3;
		}
	}
}
