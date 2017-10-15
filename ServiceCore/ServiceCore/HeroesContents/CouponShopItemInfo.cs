using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.CouponShopItemInfo")]
	public class CouponShopItemInfo : ShopTimeRestricted
	{
		public override short GetOrder()
		{
			return this.Order;
		}

		public override int GetRestrictCount()
		{
			if (this.RestrictCount == null)
			{
				return 0;
			}
			return this.RestrictCount.Value;
		}

		public override DateTime? GetResetTime()
		{
			return this.ResetTime;
		}

		public override int GetPeriodDay()
		{
			if (this.PeriodDay == null)
			{
				return 0;
			}
			return this.PeriodDay.Value;
		}

		public override short FirstUniqueKey()
		{
			return this.ShopVersion;
		}

		public override short SecondUniqueKey()
		{
			return this.Order;
		}

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

		[Column(Name = "[Order]", Storage = "_Order", DbType = "SmallInt NOT NULL")]
		public short Order
		{
			get
			{
				return this._Order;
			}
			set
			{
				if (this._Order != value)
				{
					this._Order = value;
				}
			}
		}

		[Column(Storage = "_RewardItemClass", DbType = "VarChar(512) NOT NULL", CanBeNull = false)]
		public string RewardItemClass
		{
			get
			{
				return this._RewardItemClass;
			}
			set
			{
				if (this._RewardItemClass != value)
				{
					this._RewardItemClass = value;
				}
			}
		}

		[Column(Storage = "_RewardItemCount", DbType = "Int NOT NULL")]
		public int RewardItemCount
		{
			get
			{
				return this._RewardItemCount;
			}
			set
			{
				if (this._RewardItemCount != value)
				{
					this._RewardItemCount = value;
				}
			}
		}

		[Column(Storage = "_PriceItemCount", DbType = "Int NOT NULL")]
		public int PriceItemCount
		{
			get
			{
				return this._PriceItemCount;
			}
			set
			{
				if (this._PriceItemCount != value)
				{
					this._PriceItemCount = value;
				}
			}
		}

		[Column(Storage = "_IsRare", DbType = "Bit NOT NULL")]
		public bool IsRare
		{
			get
			{
				return this._IsRare;
			}
			set
			{
				if (this._IsRare != value)
				{
					this._IsRare = value;
				}
			}
		}

		[Column(Storage = "_IsCharacterBind", DbType = "Bit NOT NULL")]
		public bool IsCharacterBind
		{
			get
			{
				return this._IsCharacterBind;
			}
			set
			{
				if (this._IsCharacterBind != value)
				{
					this._IsCharacterBind = value;
				}
			}
		}

		[Column(Storage = "_ResetTime", DbType = "DateTime")]
		public DateTime? ResetTime
		{
			get
			{
				return this._ResetTime;
			}
			set
			{
				if (this._ResetTime != value)
				{
					this._ResetTime = value;
				}
			}
		}

		[Column(Storage = "_PeriodDay", DbType = "Int")]
		public int? PeriodDay
		{
			get
			{
				return this._PeriodDay;
			}
			set
			{
				if (this._PeriodDay != value)
				{
					this._PeriodDay = value;
				}
			}
		}

		[Column(Storage = "_RestrictCount", DbType = "Int")]
		public int? RestrictCount
		{
			get
			{
				return this._RestrictCount;
			}
			set
			{
				if (this._RestrictCount != value)
				{
					this._RestrictCount = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "NVarChar(128)")]
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

		private short _Order;

		private string _RewardItemClass;

		private int _RewardItemCount;

		private int _PriceItemCount;

		private bool _IsRare;

		private bool _IsCharacterBind;

		private DateTime? _ResetTime;

		private int? _PeriodDay;

		private int? _RestrictCount;

		private string _Feature;
	}
}
