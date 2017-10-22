using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Devcat.Core.Threading;
using ServiceCore;
using ServiceCore.EndPointNetwork;
using ServiceCore.ItemServiceOperations;
using UnifiedNetwork.OperationService;
using Utility;

namespace TradeService
{
	public class TradeItemAvgPrice
	{
		public int CountPerPage
		{
			get
			{
				return FeatureMatrix.GetInteger("ItemPriceInfoCountPerPage");
			}
		}

		public TradeItemAvgPrice(Service service)
		{
			this.service = service;
			this.ScheduleUpdatePrice();
		}

		private void ScheduleUpdatePrice()
		{
			ScheduleRepeater.Schedule(this.service.Thread, Job.Create(new Action(this.UpdatePrice)), 3600000, true);
		}

		private void UpdatePrice()
		{
			Dictionary<string, List<TradeItemAvgPriceResult>> dictionary = new Dictionary<string, List<TradeItemAvgPriceResult>>();
			using (TradeDBDataContext tradeDBDataContext = new TradeDBDataContext())
			{
				ISingleResult<TradeItemAvgPriceListResult> singleResult = tradeDBDataContext.TradeItemAvgPriceList();
				foreach (TradeItemAvgPriceListResult tradeItemAvgPriceListResult in singleResult)
				{
					try
					{
						Dictionary<string, ItemAttributeElement> dictionary2;
						if (tradeItemAvgPriceListResult.AttributeEX == null || !ItemClassExBuilder.Parse(tradeItemAvgPriceListResult.AttributeEX, out dictionary2))
						{
							dictionary2 = new Dictionary<string, ItemAttributeElement>();
						}
						ItemAttributeElement itemAttributeElement;
						if (dictionary2.TryGetValue("QUALITY", out itemAttributeElement))
						{
							int num = 0;
							if (itemAttributeElement.Arg > 0)
							{
								num = itemAttributeElement.Arg;
								itemAttributeElement.Value = "";
							}
							else if (!int.TryParse(itemAttributeElement.Value, out num))
							{
								Log<TradeService>.Logger.ErrorFormat("Error while parse quality level {0}", itemAttributeElement.Value);
							}
							if (num == 2)
							{
								dictionary2.Remove("QUALITY");
							}
						}
						List<ItemAttributeElement> attributes = (from x in dictionary2.Values
						orderby x.AttributeName
						select x).ToList<ItemAttributeElement>();
						string text = ItemClassExBuilder.Build(tradeItemAvgPriceListResult.itemClass, attributes);
						if (text != null)
						{
							List<TradeItemAvgPriceResult> list;
							if (!dictionary.TryGetValue(text, out list))
							{
								list = new List<TradeItemAvgPriceResult>();
								dictionary.Add(text, list);
							}
							list.Add(new TradeItemAvgPriceResult(tradeItemAvgPriceListResult.MinPrice, tradeItemAvgPriceListResult.MaxPrice, tradeItemAvgPriceListResult.BuyCount, tradeItemAvgPriceListResult.Sales));
						}
					}
					catch (Exception ex)
					{
						Log<TradeService>.Logger.ErrorFormat("Error while get average price of {0} {1} : {2}", tradeItemAvgPriceListResult.itemClass, tradeItemAvgPriceListResult.AttributeEX, ex.Message);
					}
				}
			}
			this.PricesPage = new Dictionary<int, Dictionary<string, PriceRange>>();
			int num2 = 1;
			this.PricesPage.Add(num2, new Dictionary<string, PriceRange>());
			foreach (KeyValuePair<string, List<TradeItemAvgPriceResult>> keyValuePair in dictionary)
			{
				List<TradeItemAvgPriceResult> value = keyValuePair.Value;
				long num3 = value.Sum((TradeItemAvgPriceResult x) => x.BuyCount);
				long num4 = value.Sum((TradeItemAvgPriceResult x) => x.Sales);
				if (num3 > 0L && num4 > 0L)
				{
					int min = value.Min((TradeItemAvgPriceResult x) => x.Min);
					int max = value.Max((TradeItemAvgPriceResult x) => x.Max);
					if (this.PricesPage[num2].Count<KeyValuePair<string, PriceRange>>() >= this.CountPerPage)
					{
						this.PricesPage.Add(++num2, new Dictionary<string, PriceRange>());
					}
					this.PricesPage[num2].Add(keyValuePair.Key, new PriceRange
					{
						Min = min,
						Max = max,
						Price = (int)(num4 / num3)
					});
				}
			}
		}

		public Dictionary<string, PriceRange> GetPricesPartially(int pageNum)
		{
			if (this.PricesPage == null)
			{
				Log<TradeService>.Logger.ErrorFormat("Error while get prices partially. prices data has not created yet", new object[0]);
				return null;
			}
			if (pageNum < 1)
			{
				Log<TradeService>.Logger.ErrorFormat("Error while get prices partially. requested page number is invalid : {0}", pageNum);
				return null;
			}
			Dictionary<string, PriceRange> result = null;
			this.PricesPage.TryGetValue(pageNum, out result);
			return result;
		}

		public bool IsLastPage(int pageNum)
		{
			Dictionary<string, PriceRange> dictionary = null;
			return !this.PricesPage.TryGetValue(pageNum + 1, out dictionary);
		}

		private Service service;

		private Dictionary<int, Dictionary<string, PriceRange>> PricesPage;
	}
}
