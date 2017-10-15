using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.CouponShopVersionInfo")]
	public class CouponShopVersionInfo
	{
		[Column(Storage = "_ShopVersion", DbType = "SmallInt NOT NULL")]
		public short ShopVersion
		{
			get
			{
				return this._ShopVersion;
			}
			set
			{
				if (this._ShopVersion != value)
				{
					this._ShopVersion = value;
				}
			}
		}

		[Column(Storage = "_ShopPeriodText", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
		public string ShopPeriodText
		{
			get
			{
				return this._ShopPeriodText;
			}
			set
			{
				if (this._ShopPeriodText != value)
				{
					this._ShopPeriodText = value;
				}
			}
		}

		[Column(Storage = "_PriceItemClass", DbType = "NVarChar(512) NOT NULL", CanBeNull = false)]
		public string PriceItemClass
		{
			get
			{
				return this._PriceItemClass;
			}
			set
			{
				if (this._PriceItemClass != value)
				{
					this._PriceItemClass = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "NVarChar(64)")]
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

		private short _ShopVersion;

		private string _ShopPeriodText;

		private string _PriceItemClass;

		private string _Feature;
	}
}
