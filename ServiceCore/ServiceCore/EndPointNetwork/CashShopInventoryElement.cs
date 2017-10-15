using System;
using System.Collections.Generic;
using Nexon.Nisms;
using ServiceCore.ItemServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CashShopInventoryElement
	{
		public int OrderNo { get; set; }

		public int ProductNo { get; set; }

		public string ItemClass { get; set; }

		public string ItemClassEx { get; set; }

		public short Count { get; set; }

		public string r { get; set; }

		public string g { get; set; }

		public string b { get; set; }

		public short Expire { get; set; }

		public bool IsGift { get; set; }

		public string SenderMessage { get; set; }

		public short RemainQuantity { get; set; }

		public CashShopInventoryElement(Order order)
		{
			this.OrderNo = order.OrderNo;
			string text = order.ProductID;
			string attrEx = order.ProductAttribute3.ToString();
			Dictionary<string, ItemAttributeElement> attributes = new Dictionary<string, ItemAttributeElement>();
			ItemClassExBuilder.Parse(attrEx, out attributes);
			text = ItemClassExBuilder.Build(text, attributes);
			this.ProductNo = order.ProductNo;
			this.ItemClass = order.ProductID;
			this.ItemClassEx = (string.IsNullOrEmpty(text) ? this.ItemClass : text);
			this.Count = order.ProductPieces;
			this.r = (string.IsNullOrEmpty(order.ProductAttribute0) ? "-1" : order.ProductAttribute0);
			this.g = (string.IsNullOrEmpty(order.ProductAttribute1) ? "-1" : order.ProductAttribute1);
			this.b = (string.IsNullOrEmpty(order.ProductAttribute2) ? "-1" : order.ProductAttribute2);
			if (!string.IsNullOrEmpty(order.ProductAttribute3))
			{
				this.ItemClass += order.ProductAttribute3;
			}
			this.Expire = order.ProductExpire;
			this.IsGift = order.IsPresent;
			this.SenderMessage = (order.SenderPresentMessage ?? "");
			this.RemainQuantity = order.RemainOrderQuantity;
		}
	}
}
