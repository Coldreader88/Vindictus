using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.ItemClassInfo")]
	public class ItemClassInfo
	{
		[Column(Storage = "_ItemClass", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_Icon", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Icon
		{
			get
			{
				return this._Icon;
			}
			set
			{
				if (this._Icon != value)
				{
					this._Icon = value;
				}
			}
		}

		[Column(Storage = "_IconBG", DbType = "NVarChar(50)")]
		public string IconBG
		{
			get
			{
				return this._IconBG;
			}
			set
			{
				if (this._IconBG != value)
				{
					this._IconBG = value;
				}
			}
		}

		[Column(Storage = "_Category", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_TradeCategory", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string TradeCategory
		{
			get
			{
				return this._TradeCategory;
			}
			set
			{
				if (this._TradeCategory != value)
				{
					this._TradeCategory = value;
				}
			}
		}

		[Column(Storage = "_TradeCategorySub", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string TradeCategorySub
		{
			get
			{
				return this._TradeCategorySub;
			}
			set
			{
				if (this._TradeCategorySub != value)
				{
					this._TradeCategorySub = value;
				}
			}
		}

		[Column(Storage = "_InventoryFilterName", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string InventoryFilterName
		{
			get
			{
				return this._InventoryFilterName;
			}
			set
			{
				if (this._InventoryFilterName != value)
				{
					this._InventoryFilterName = value;
				}
			}
		}

		[Column(Storage = "_SoundClass", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string SoundClass
		{
			get
			{
				return this._SoundClass;
			}
			set
			{
				if (this._SoundClass != value)
				{
					this._SoundClass = value;
				}
			}
		}

		[Column(Storage = "_SortOrder", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string SortOrder
		{
			get
			{
				return this._SortOrder;
			}
			set
			{
				if (this._SortOrder != value)
				{
					this._SortOrder = value;
				}
			}
		}

		[Column(Storage = "_Storage", DbType = "TinyInt NOT NULL")]
		public byte Storage
		{
			get
			{
				return this._Storage;
			}
			set
			{
				if (this._Storage != value)
				{
					this._Storage = value;
				}
			}
		}

		[Column(Storage = "_MaxStack", DbType = "Int NOT NULL")]
		public int MaxStack
		{
			get
			{
				return this._MaxStack;
			}
			set
			{
				if (this._MaxStack != value)
				{
					this._MaxStack = value;
				}
			}
		}

		[Column(Storage = "_QuickSlotMaxStack", DbType = "Int NOT NULL")]
		public int QuickSlotMaxStack
		{
			get
			{
				return this._QuickSlotMaxStack;
			}
			set
			{
				if (this._QuickSlotMaxStack != value)
				{
					this._QuickSlotMaxStack = value;
				}
			}
		}

		[Column(Storage = "_Draft", DbType = "Int NOT NULL")]
		public int Draft
		{
			get
			{
				return this._Draft;
			}
			set
			{
				if (this._Draft != value)
				{
					this._Draft = value;
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

		[Column(Storage = "_SellPrice", DbType = "Int NOT NULL")]
		public int SellPrice
		{
			get
			{
				return this._SellPrice;
			}
			set
			{
				if (this._SellPrice != value)
				{
					this._SellPrice = value;
				}
			}
		}

		[Column(Storage = "_ExpireIn", DbType = "Int")]
		public int? ExpireIn
		{
			get
			{
				return this._ExpireIn;
			}
			set
			{
				if (this._ExpireIn != value)
				{
					this._ExpireIn = value;
				}
			}
		}

		[Column(Storage = "_Rarity", DbType = "Int NOT NULL")]
		public int Rarity
		{
			get
			{
				return this._Rarity;
			}
			set
			{
				if (this._Rarity != value)
				{
					this._Rarity = value;
				}
			}
		}

		[Column(Name = "[Level]", Storage = "_Level", DbType = "Int NOT NULL")]
		public int Level
		{
			get
			{
				return this._Level;
			}
			set
			{
				if (this._Level != value)
				{
					this._Level = value;
				}
			}
		}

		[Column(Storage = "_RequiredLevel", DbType = "Int NOT NULL")]
		public int RequiredLevel
		{
			get
			{
				return this._RequiredLevel;
			}
			set
			{
				if (this._RequiredLevel != value)
				{
					this._RequiredLevel = value;
				}
			}
		}

		[Column(Storage = "_LevelUpperBound", DbType = "Int NOT NULL")]
		public int LevelUpperBound
		{
			get
			{
				return this._LevelUpperBound;
			}
			set
			{
				if (this._LevelUpperBound != value)
				{
					this._LevelUpperBound = value;
				}
			}
		}

		[Column(Storage = "_RequiredSkill", DbType = "NVarChar(50)")]
		public string RequiredSkill
		{
			get
			{
				return this._RequiredSkill;
			}
			set
			{
				if (this._RequiredSkill != value)
				{
					this._RequiredSkill = value;
				}
			}
		}

		[Column(Storage = "_RequiredSkillRank", DbType = "Int NOT NULL")]
		public int RequiredSkillRank
		{
			get
			{
				return this._RequiredSkillRank;
			}
			set
			{
				if (this._RequiredSkillRank != value)
				{
					this._RequiredSkillRank = value;
				}
			}
		}

		[Column(Storage = "_ClassRestriction", DbType = "Int NOT NULL")]
		public int ClassRestriction
		{
			get
			{
				return this._ClassRestriction;
			}
			set
			{
				if (this._ClassRestriction != value)
				{
					this._ClassRestriction = value;
				}
			}
		}

		[Column(Name = "[Unique]", Storage = "_Unique", DbType = "Int NOT NULL")]
		public int Unique
		{
			get
			{
				return this._Unique;
			}
			set
			{
				if (this._Unique != value)
				{
					this._Unique = value;
				}
			}
		}

		[Column(Storage = "_IsCafeOnly", DbType = "Int NOT NULL")]
		public int IsCafeOnly
		{
			get
			{
				return this._IsCafeOnly;
			}
			set
			{
				if (this._IsCafeOnly != value)
				{
					this._IsCafeOnly = value;
				}
			}
		}

		[Column(Storage = "_Indestructible", DbType = "Int NOT NULL")]
		public int Indestructible
		{
			get
			{
				return this._Indestructible;
			}
			set
			{
				if (this._Indestructible != value)
				{
					this._Indestructible = value;
				}
			}
		}

		[Column(Storage = "_MaxAntiBind", DbType = "Int NOT NULL")]
		public int MaxAntiBind
		{
			get
			{
				return this._MaxAntiBind;
			}
			set
			{
				if (this._MaxAntiBind != value)
				{
					this._MaxAntiBind = value;
				}
			}
		}

		[Column(Storage = "_UseInTown", DbType = "SmallInt NOT NULL")]
		public short UseInTown
		{
			get
			{
				return this._UseInTown;
			}
			set
			{
				if (this._UseInTown != value)
				{
					this._UseInTown = value;
				}
			}
		}

		[Column(Storage = "_UseInQuest", DbType = "SmallInt NOT NULL")]
		public short UseInQuest
		{
			get
			{
				return this._UseInQuest;
			}
			set
			{
				if (this._UseInQuest != value)
				{
					this._UseInQuest = value;
				}
			}
		}

		[Column(Storage = "_Bind", DbType = "VarChar(256) NOT NULL", CanBeNull = false)]
		public string Bind
		{
			get
			{
				return this._Bind;
			}
			set
			{
				if (this._Bind != value)
				{
					this._Bind = value;
				}
			}
		}

		[Column(Storage = "_TradeOnBind", DbType = "VarChar(256) NOT NULL", CanBeNull = false)]
		public string TradeOnBind
		{
			get
			{
				return this._TradeOnBind;
			}
			set
			{
				if (this._TradeOnBind != value)
				{
					this._TradeOnBind = value;
				}
			}
		}

		[Column(Storage = "_TradeRestirction", DbType = "NVarChar(50)")]
		public string TradeRestirction
		{
			get
			{
				return this._TradeRestirction;
			}
			set
			{
				if (this._TradeRestirction != value)
				{
					this._TradeRestirction = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "NVarChar(256)")]
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

		[Column(Storage = "_ExtraData", DbType = "NVarChar(256)")]
		public string ExtraData
		{
			get
			{
				return this._ExtraData;
			}
			set
			{
				if (this._ExtraData != value)
				{
					this._ExtraData = value;
				}
			}
		}

		[Column(Storage = "_RequiredEnhanceLevel", DbType = "Int NOT NULL")]
		public int RequiredEnhanceLevel
		{
			get
			{
				return this._RequiredEnhanceLevel;
			}
			set
			{
				if (this._RequiredEnhanceLevel != value)
				{
					this._RequiredEnhanceLevel = value;
				}
			}
		}

		[Column(Storage = "_RequiredEnchantLevel", DbType = "Int NOT NULL")]
		public int RequiredEnchantLevel
		{
			get
			{
				return this._RequiredEnchantLevel;
			}
			set
			{
				if (this._RequiredEnchantLevel != value)
				{
					this._RequiredEnchantLevel = value;
				}
			}
		}

		[Column(Storage = "_ExpanderClassName", DbType = "NVarChar(50)")]
		public string ExpanderClassName
		{
			get
			{
				return this._ExpanderClassName;
			}
			set
			{
				if (this._ExpanderClassName != value)
				{
					this._ExpanderClassName = value;
				}
			}
		}

		private string _ItemClass;

		private string _Icon;

		private string _IconBG;

		private string _Category;

		private string _TradeCategory;

		private string _TradeCategorySub;

		private string _InventoryFilterName;

		private string _SoundClass;

		private string _SortOrder;

		private byte _Storage;

		private int _MaxStack;

		private int _QuickSlotMaxStack;

		private int _Draft;

		private int _Price;

		private int _SellPrice;

		private int? _ExpireIn;

		private int _Rarity;

		private int _Level;

		private int _RequiredLevel;

		private int _LevelUpperBound;

		private string _RequiredSkill;

		private int _RequiredSkillRank;

		private int _ClassRestriction;

		private int _Unique;

		private int _IsCafeOnly;

		private int _Indestructible;

		private int _MaxAntiBind;

		private short _UseInTown;

		private short _UseInQuest;

		private string _Bind;

		private string _TradeOnBind;

		private string _TradeRestirction;

		private string _Feature;

		private string _ExtraData;

		private int _RequiredEnhanceLevel;

		private int _RequiredEnchantLevel;

		private string _ExpanderClassName;
	}
}
