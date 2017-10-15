using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.Cashshop_IngameSellInfo")]
	public class Cashshop_IngameSellInfo
	{
		[Column(Storage = "_UID", DbType = "Int NOT NULL")]
		public int UID
		{
			get
			{
				return this._UID;
			}
			set
			{
				if (this._UID != value)
				{
					this._UID = value;
				}
			}
		}

		[Column(Storage = "_Category", DbType = "Int NOT NULL")]
		public int Category
		{
			get
			{
				return this._Category;
			}
			set
			{
				if (this._Category != value)
				{
					this._Category = value;
				}
			}
		}

		[Column(Storage = "_ItemClass", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string ItemClass
		{
			get
			{
				return this._ItemClass;
			}
			set
			{
				if (this._ItemClass != value)
				{
					this._ItemClass = value;
				}
			}
		}

		[Column(Storage = "_Amount", DbType = "Int NOT NULL")]
		public int Amount
		{
			get
			{
				return this._Amount;
			}
			set
			{
				if (this._Amount != value)
				{
					this._Amount = value;
				}
			}
		}

		[Column(Storage = "_CouponClass", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string CouponClass
		{
			get
			{
				return this._CouponClass;
			}
			set
			{
				if (this._CouponClass != value)
				{
					this._CouponClass = value;
				}
			}
		}

		[Column(Storage = "_Price", DbType = "Int NOT NULL")]
		public int Price
		{
			get
			{
				return this._Price;
			}
			set
			{
				if (this._Price != value)
				{
					this._Price = value;
				}
			}
		}

		[Column(Storage = "_DisplayOrder", DbType = "Int NOT NULL")]
		public int DisplayOrder
		{
			get
			{
				return this._DisplayOrder;
			}
			set
			{
				if (this._DisplayOrder != value)
				{
					this._DisplayOrder = value;
				}
			}
		}

		[Column(Storage = "_CashShopType", DbType = "VarChar(50)")]
		public string CashShopType
		{
			get
			{
				return this._CashShopType;
			}
			set
			{
				if (this._CashShopType != value)
				{
					this._CashShopType = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "VarChar(50)")]
		public string Feature
		{
			get
			{
				return this._Feature;
			}
			set
			{
				if (this._Feature != value)
				{
					this._Feature = value;
				}
			}
		}

		private int _UID;

		private int _Category;

		private string _ItemClass;

		private int _Amount;

		private string _CouponClass;

		private int _Price;

		private int _DisplayOrder;

		private string _CashShopType;

		private string _Feature;
	}
}
