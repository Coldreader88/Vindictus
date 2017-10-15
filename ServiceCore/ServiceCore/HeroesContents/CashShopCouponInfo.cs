using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.CashShopCouponInfo")]
	public class CashShopCouponInfo
	{
		[Column(Storage = "_CouponCode", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string CouponCode
		{
			get
			{
				return this._CouponCode;
			}
			set
			{
				if (this._CouponCode != value)
				{
					this._CouponCode = value;
				}
			}
		}

		[Column(Storage = "_CouponItemClass", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string CouponItemClass
		{
			get
			{
				return this._CouponItemClass;
			}
			set
			{
				if (this._CouponItemClass != value)
				{
					this._CouponItemClass = value;
				}
			}
		}

		[Column(Storage = "_TargetItem", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string TargetItem
		{
			get
			{
				return this._TargetItem;
			}
			set
			{
				if (this._TargetItem != value)
				{
					this._TargetItem = value;
				}
			}
		}

		[Column(Storage = "_DiscountValue", DbType = "Float NOT NULL")]
		public double DiscountValue
		{
			get
			{
				return this._DiscountValue;
			}
			set
			{
				if (this._DiscountValue != value)
				{
					this._DiscountValue = value;
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

		private string _CouponCode;

		private string _CouponItemClass;

		private string _TargetItem;

		private double _DiscountValue;

		private string _Feature;
	}
}
