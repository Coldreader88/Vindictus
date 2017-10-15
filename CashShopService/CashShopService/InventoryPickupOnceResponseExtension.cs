using System;
using System.Collections.Generic;
using Nexon.Nisms;
using Nexon.Nisms.Packets;
using ServiceCore.EndPointNetwork;
using ServiceCore.ItemServiceOperations;

namespace CashShopService
{
	public static class InventoryPickupOnceResponseExtension
	{
		public static GiveItem ToGiveItem(this InventoryPickupOnceResponse result, CashShopService service, bool isGift)
		{
			ItemRequestInfo itemRequestInfo = new ItemRequestInfo();
			if (result.SubProduct.Count == 0)
			{
				string text = result.ProductID;
				string attrEx = result.ProductAttribute3.ToString();
				Dictionary<string, ItemAttributeElement> attributes = new Dictionary<string, ItemAttributeElement>();
				ItemClassExBuilder.Parse(attrEx, out attributes);
				text = ItemClassExBuilder.Build(text, attributes);
				itemRequestInfo.Add(text, (int)(result.OrderQuantity * result.ProductPieces), string.IsNullOrEmpty(result.ProductAttribute0) ? -1 : result.ProductAttribute0.ParseInt(), string.IsNullOrEmpty(result.ProductAttribute1) ? -1 : result.ProductAttribute1.ParseInt(), string.IsNullOrEmpty(result.ProductAttribute2) ? -1 : result.ProductAttribute2.ParseInt(), (result.ProductExpire > 0) ? new DateTime?(DateTime.UtcNow.AddDays((double)result.ProductExpire)) : null, !(result.ProductID == "gold") && isGift);
			}
			else
			{
				foreach (SubProduct subProduct in result.SubProduct)
				{
					CashShopProductListElement cashShopProductListElement;
					if (!service.ProductByProductID.TryGetValue(subProduct.ProductNo, out cashShopProductListElement))
					{
						return null;
					}
					string text2 = cashShopProductListElement.ProductID;
					string attrEx2 = subProduct.ProductAttribute3.ToString();
					Dictionary<string, ItemAttributeElement> attributes2 = new Dictionary<string, ItemAttributeElement>();
					ItemClassExBuilder.Parse(attrEx2, out attributes2);
					text2 = ItemClassExBuilder.Build(text2, attributes2);
					itemRequestInfo.Add(text2, (int)cashShopProductListElement.ProductPieces, string.IsNullOrEmpty(subProduct.ProductAttribute0) ? -1 : subProduct.ProductAttribute0.ParseInt(), string.IsNullOrEmpty(subProduct.ProductAttribute1) ? -1 : subProduct.ProductAttribute1.ParseInt(), string.IsNullOrEmpty(subProduct.ProductAttribute2) ? -1 : subProduct.ProductAttribute2.ParseInt(), (cashShopProductListElement.ProductExpire > 0) ? new DateTime?(DateTime.UtcNow.AddDays((double)cashShopProductListElement.ProductExpire)) : null, !(cashShopProductListElement.ProductID == "gold") && isGift);
				}
			}
			foreach (BonusProduct bonusProduct in result.BonusProduct)
			{
				CashShopProductListElement cashShopProductListElement2;
				if (!service.ProductByProductID.TryGetValue(bonusProduct.ProductNo, out cashShopProductListElement2))
				{
					return null;
				}
				itemRequestInfo.Add(cashShopProductListElement2.ProductID, (int)cashShopProductListElement2.ProductPieces, -1, -1, -1, (cashShopProductListElement2.ProductExpire > 0) ? new DateTime?(DateTime.UtcNow.AddDays((double)cashShopProductListElement2.ProductExpire)) : null, !(cashShopProductListElement2.ProductID == "gold") && isGift);
			}
			return new GiveItem(itemRequestInfo, GiveItem.FailMethodEnum.OperationFail, GiveItem.SourceEnum.CashShop);
		}

		public static int ParseInt(this string str)
		{
			int result;
			if (int.TryParse(str, out result))
			{
				return result;
			}
			return -1;
		}
	}
}
