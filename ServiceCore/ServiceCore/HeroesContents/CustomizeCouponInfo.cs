using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.CustomizeCouponInfo")]
	public class CustomizeCouponInfo
	{
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

		[Column(Storage = "_Category", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string Category
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

		private string _ItemClass;

		private string _Category;

		private int _Price;

		private string _CashShopType;

		private string _Feature;
	}
}
