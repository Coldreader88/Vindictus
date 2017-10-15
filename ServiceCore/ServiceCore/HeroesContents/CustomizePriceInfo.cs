using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.CustomizePriceInfo")]
	public class CustomizePriceInfo
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

		[Column(Storage = "_Duration", DbType = "Int NOT NULL")]
		public int Duration
		{
			get
			{
				return this._Duration;
			}
			set
			{
				if (this._Duration != value)
				{
					this._Duration = value;
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

		private int _Duration;

		private int _Price;

		private string _CashShopType;

		private string _Feature;
	}
}
