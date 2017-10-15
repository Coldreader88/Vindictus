using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork.Item
{
	[Serializable]
	public sealed class BuyIngameCashshopUseTirResultMessage : IMessage
	{
		public List<int> Products { get; set; }

		public int Result { get; set; }

		public BuyIngameCashshopUseTirResultMessage(BuyIngameCashshopUseTirResultMessage.BuyItemUseTirResult result, List<int> products)
		{
			this.Result = (int)result;
			this.Products = products;
			if (this.Products == null)
			{
				this.Products = new List<int>();
			}
		}

		public override string ToString()
		{
			string str = "";
			if (this.Products != null)
			{
				for (int i = 0; i < this.Products.Count; i++)
				{
					str = str + this.Products[i].ToString() + ",";
				}
			}
			return str + "[ Result : " + this.Result.ToString() + "]";
		}

		public enum BuyItemUseTirResult
		{
			Result_OK,
			Result_ProductID_ERROR,
			Result_LessTir_ERROR
		}
	}
}
