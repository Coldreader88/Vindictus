using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.ShopInfo")]
	public class ShopInfo : ShopTimeRestricted
	{
		public override short GetOrder()
		{
			return this.Order;
		}

		public override int GetRestrictCount()
		{
			return this.RestrictionCount;
		}

		public override DateTime? GetResetTime()
		{
			return this.RestrictionResetTime;
		}

		public override int GetPeriodDay()
		{
			return this.RestrictionPeriodDay;
		}

		public override short FirstUniqueKey()
		{
			return 0;
		}

		public override short SecondUniqueKey()
		{
			if (this.ID == null)
			{
				return 0;
			}
			return this.ID.Value;
		}

		[Column(Storage = "_ShopID", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string ShopID
		{
			get
			{
				return this._ShopID;
			}
			set
			{
				if (this._ShopID != value)
				{
					this._ShopID = value;
				}
			}
		}

		[Column(Name = "[Order]", Storage = "_Order", DbType = "Int NOT NULL")]
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

		[Column(Storage = "_ItemClass", DbType = "VarChar(100) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_Count", DbType = "Int NOT NULL")]
		public int Count
		{
			get
			{
				return this._Count;
			}
			set
			{
				if (this._Count != value)
				{
					this._Count = value;
				}
			}
		}

		[Column(Storage = "_PriceItemClass", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_PriceCount", DbType = "Int NOT NULL")]
		public int PriceCount
		{
			get
			{
				return this._PriceCount;
			}
			set
			{
				if (this._PriceCount != value)
				{
					this._PriceCount = value;
				}
			}
		}

		[Column(Storage = "_AppearQuestID", DbType = "VarChar(50)")]
		public string AppearQuestID
		{
			get
			{
				return this._AppearQuestID;
			}
			set
			{
				if (this._AppearQuestID != value)
				{
					this._AppearQuestID = value;
				}
			}
		}

		[Column(Storage = "_CharacterBind", DbType = "Bit NOT NULL")]
		public bool CharacterBind
		{
			get
			{
				return this._CharacterBind;
			}
			set
			{
				if (this._CharacterBind != value)
				{
					this._CharacterBind = value;
				}
			}
		}

		[Column(Storage = "_ID", DbType = "Int")]
		public short? ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if (this._ID != value)
				{
					this._ID = value;
				}
			}
		}

		[Column(Storage = "_RestrictionResetTime", DbType = "DateTime")]
		public DateTime? RestrictionResetTime
		{
			get
			{
				return this._RestrictionResetTime;
			}
			set
			{
				if (this._RestrictionResetTime != value)
				{
					this._RestrictionResetTime = value;
				}
			}
		}

		[Column(Storage = "_RestrictionPeriodDay", DbType = "Int NOT NULL")]
		public int RestrictionPeriodDay
		{
			get
			{
				return this._RestrictionPeriodDay;
			}
			set
			{
				if (this._RestrictionPeriodDay != value)
				{
					this._RestrictionPeriodDay = value;
				}
			}
		}

		[Column(Storage = "_RestrictionCount", DbType = "Int NOT NULL")]
		public int RestrictionCount
		{
			get
			{
				return this._RestrictionCount;
			}
			set
			{
				if (this._RestrictionCount != value)
				{
					this._RestrictionCount = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
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

		private string _ShopID;

		private short _Order;

		private string _ItemClass;

		private int _Count;

		private string _PriceItemClass;

		private int _PriceCount;

		private string _AppearQuestID;

		private bool _CharacterBind;

		private short? _ID;

		private DateTime? _RestrictionResetTime;

		private int _RestrictionPeriodDay;

		private int _RestrictionCount;

		private string _Feature;
	}
}
