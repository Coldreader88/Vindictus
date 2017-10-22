using System;
using System.Data.Linq.Mapping;

namespace TradeService
{
	public class TradeItemAvgPriceListResult
	{
		[Column(Storage = "_itemClass", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string itemClass
		{
			get
			{
				return this._itemClass;
			}
			set
			{
				if (this._itemClass != value)
				{
					this._itemClass = value;
				}
			}
		}

		[Column(Storage = "_AttributeEX", DbType = "NVarChar(2048)")]
		public string AttributeEX
		{
			get
			{
				return this._AttributeEX;
			}
			set
			{
				if (this._AttributeEX != value)
				{
					this._AttributeEX = value;
				}
			}
		}

		[Column(Storage = "_MinPrice", DbType = "Int NOT NULL")]
		public int MinPrice
		{
			get
			{
				return this._MinPrice;
			}
			set
			{
				if (this._MinPrice != value)
				{
					this._MinPrice = value;
				}
			}
		}

		[Column(Storage = "_MaxPrice", DbType = "Int NOT NULL")]
		public int MaxPrice
		{
			get
			{
				return this._MaxPrice;
			}
			set
			{
				if (this._MaxPrice != value)
				{
					this._MaxPrice = value;
				}
			}
		}

		[Column(Storage = "_BuyCount", DbType = "BigInt NOT NULL")]
		public long BuyCount
		{
			get
			{
				return this._BuyCount;
			}
			set
			{
				if (this._BuyCount != value)
				{
					this._BuyCount = value;
				}
			}
		}

		[Column(Storage = "_Sales", DbType = "BigInt NOT NULL")]
		public long Sales
		{
			get
			{
				return this._Sales;
			}
			set
			{
				if (this._Sales != value)
				{
					this._Sales = value;
				}
			}
		}

		private string _itemClass;

		private string _AttributeEX;

		private int _MinPrice;

		private int _MaxPrice;

		private long _BuyCount;

		private long _Sales;
	}
}
