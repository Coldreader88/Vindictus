using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.EquipItemInfo")]
	public class EquipItemInfo
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

		[Column(Storage = "_EquipClass", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string EquipClass
		{
			get
			{
				return this._EquipClass;
			}
			set
			{
				if (this._EquipClass != value)
				{
					this._EquipClass = value;
				}
			}
		}

		[Column(Storage = "_CostumeType", DbType = "Int NOT NULL")]
		public int CostumeType
		{
			get
			{
				return this._CostumeType;
			}
			set
			{
				if (this._CostumeType != value)
				{
					this._CostumeType = value;
				}
			}
		}

		[Column(Storage = "_MaxDurability", DbType = "Int NOT NULL")]
		public int MaxDurability
		{
			get
			{
				return this._MaxDurability;
			}
			set
			{
				if (this._MaxDurability != value)
				{
					this._MaxDurability = value;
				}
			}
		}

		[Column(Storage = "_ArmorHP", DbType = "Int NOT NULL")]
		public int ArmorHP
		{
			get
			{
				return this._ArmorHP;
			}
			set
			{
				if (this._ArmorHP != value)
				{
					this._ArmorHP = value;
				}
			}
		}

		[Column(Storage = "_ColorOverride", DbType = "TinyInt NOT NULL")]
		public byte ColorOverride
		{
			get
			{
				return this._ColorOverride;
			}
			set
			{
				if (this._ColorOverride != value)
				{
					this._ColorOverride = value;
				}
			}
		}

		[Column(Storage = "_Material1", DbType = "NVarChar(50)")]
		public string Material1
		{
			get
			{
				return this._Material1;
			}
			set
			{
				if (this._Material1 != value)
				{
					this._Material1 = value;
				}
			}
		}

		[Column(Storage = "_Material2", DbType = "NVarChar(50)")]
		public string Material2
		{
			get
			{
				return this._Material2;
			}
			set
			{
				if (this._Material2 != value)
				{
					this._Material2 = value;
				}
			}
		}

		[Column(Storage = "_Material3", DbType = "NVarChar(50)")]
		public string Material3
		{
			get
			{
				return this._Material3;
			}
			set
			{
				if (this._Material3 != value)
				{
					this._Material3 = value;
				}
			}
		}

		[Column(Storage = "_ATK", DbType = "Int NOT NULL")]
		public int ATK
		{
			get
			{
				return this._ATK;
			}
			set
			{
				if (this._ATK != value)
				{
					this._ATK = value;
				}
			}
		}

		[Column(Storage = "_ATK_Speed", DbType = "Int NOT NULL")]
		public int ATK_Speed
		{
			get
			{
				return this._ATK_Speed;
			}
			set
			{
				if (this._ATK_Speed != value)
				{
					this._ATK_Speed = value;
				}
			}
		}

		[Column(Storage = "_Critical", DbType = "Int NOT NULL")]
		public int Critical
		{
			get
			{
				return this._Critical;
			}
			set
			{
				if (this._Critical != value)
				{
					this._Critical = value;
				}
			}
		}

		[Column(Storage = "_Balance", DbType = "Int NOT NULL")]
		public int Balance
		{
			get
			{
				return this._Balance;
			}
			set
			{
				if (this._Balance != value)
				{
					this._Balance = value;
				}
			}
		}

		[Column(Storage = "_MATK", DbType = "Int NOT NULL")]
		public int MATK
		{
			get
			{
				return this._MATK;
			}
			set
			{
				if (this._MATK != value)
				{
					this._MATK = value;
				}
			}
		}

		[Column(Storage = "_DEF", DbType = "Int NOT NULL")]
		public int DEF
		{
			get
			{
				return this._DEF;
			}
			set
			{
				if (this._DEF != value)
				{
					this._DEF = value;
				}
			}
		}

		[Column(Storage = "_Res_Critical", DbType = "Int NOT NULL")]
		public int Res_Critical
		{
			get
			{
				return this._Res_Critical;
			}
			set
			{
				if (this._Res_Critical != value)
				{
					this._Res_Critical = value;
				}
			}
		}

		[Column(Storage = "_PVP_ATK", DbType = "Int NOT NULL")]
		public int PVP_ATK
		{
			get
			{
				return this._PVP_ATK;
			}
			set
			{
				if (this._PVP_ATK != value)
				{
					this._PVP_ATK = value;
				}
			}
		}

		[Column(Storage = "_PVP_MATK", DbType = "Int NOT NULL")]
		public int PVP_MATK
		{
			get
			{
				return this._PVP_MATK;
			}
			set
			{
				if (this._PVP_MATK != value)
				{
					this._PVP_MATK = value;
				}
			}
		}

		[Column(Storage = "_PVP_DEF", DbType = "Int NOT NULL")]
		public int PVP_DEF
		{
			get
			{
				return this._PVP_DEF;
			}
			set
			{
				if (this._PVP_DEF != value)
				{
					this._PVP_DEF = value;
				}
			}
		}

		[Column(Storage = "_STR", DbType = "Int NOT NULL")]
		public int STR
		{
			get
			{
				return this._STR;
			}
			set
			{
				if (this._STR != value)
				{
					this._STR = value;
				}
			}
		}

		[Column(Storage = "_DEX", DbType = "Int NOT NULL")]
		public int DEX
		{
			get
			{
				return this._DEX;
			}
			set
			{
				if (this._DEX != value)
				{
					this._DEX = value;
				}
			}
		}

		[Column(Storage = "_INT", DbType = "Int NOT NULL")]
		public int INT
		{
			get
			{
				return this._INT;
			}
			set
			{
				if (this._INT != value)
				{
					this._INT = value;
				}
			}
		}

		[Column(Storage = "_WILL", DbType = "Int NOT NULL")]
		public int WILL
		{
			get
			{
				return this._WILL;
			}
			set
			{
				if (this._WILL != value)
				{
					this._WILL = value;
				}
			}
		}

		[Column(Storage = "_LUCK", DbType = "Int NOT NULL")]
		public int LUCK
		{
			get
			{
				return this._LUCK;
			}
			set
			{
				if (this._LUCK != value)
				{
					this._LUCK = value;
				}
			}
		}

		[Column(Storage = "_HP", DbType = "Int NOT NULL")]
		public int HP
		{
			get
			{
				return this._HP;
			}
			set
			{
				if (this._HP != value)
				{
					this._HP = value;
				}
			}
		}

		[Column(Storage = "_STAMINA", DbType = "Int NOT NULL")]
		public int STAMINA
		{
			get
			{
				return this._STAMINA;
			}
			set
			{
				if (this._STAMINA != value)
				{
					this._STAMINA = value;
				}
			}
		}

		[Column(Storage = "_TOWN_SPEED", DbType = "Int NOT NULL")]
		public int TOWN_SPEED
		{
			get
			{
				return this._TOWN_SPEED;
			}
			set
			{
				if (this._TOWN_SPEED != value)
				{
					this._TOWN_SPEED = value;
				}
			}
		}

		[Column(Storage = "_Weight", DbType = "Float NOT NULL")]
		public double Weight
		{
			get
			{
				return this._Weight;
			}
			set
			{
				if (this._Weight != value)
				{
					this._Weight = value;
				}
			}
		}

		[Column(Storage = "_EnhanceType", DbType = "VarChar(50)")]
		public string EnhanceType
		{
			get
			{
				return this._EnhanceType;
			}
			set
			{
				if (this._EnhanceType != value)
				{
					this._EnhanceType = value;
				}
			}
		}

		[Column(Storage = "_QualityType", DbType = "NVarChar(50)")]
		public string QualityType
		{
			get
			{
				return this._QualityType;
			}
			set
			{
				if (this._QualityType != value)
				{
					this._QualityType = value;
				}
			}
		}

		[Column(Storage = "_Synthesizable", DbType = "TinyInt NOT NULL")]
		public byte Synthesizable
		{
			get
			{
				return this._Synthesizable;
			}
			set
			{
				if (this._Synthesizable != value)
				{
					this._Synthesizable = value;
				}
			}
		}

		[Column(Storage = "_Enchantable", DbType = "TinyInt NOT NULL")]
		public byte Enchantable
		{
			get
			{
				return this._Enchantable;
			}
			set
			{
				if (this._Enchantable != value)
				{
					this._Enchantable = value;
				}
			}
		}

		[Column(Storage = "_Dyeable", DbType = "TinyInt NOT NULL")]
		public byte Dyeable
		{
			get
			{
				return this._Dyeable;
			}
			set
			{
				if (this._Dyeable != value)
				{
					this._Dyeable = value;
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

		private string _EquipClass;

		private int _CostumeType;

		private int _MaxDurability;

		private int _ArmorHP;

		private byte _ColorOverride;

		private string _Material1;

		private string _Material2;

		private string _Material3;

		private int _ATK;

		private int _ATK_Speed;

		private int _Critical;

		private int _Balance;

		private int _MATK;

		private int _DEF;

		private int _Res_Critical;

		private int _PVP_ATK;

		private int _PVP_MATK;

		private int _PVP_DEF;

		private int _STR;

		private int _DEX;

		private int _INT;

		private int _WILL;

		private int _LUCK;

		private int _HP;

		private int _STAMINA;

		private int _TOWN_SPEED;

		private double _Weight;

		private string _EnhanceType;

		private string _QualityType;

		private byte _Synthesizable;

		private byte _Enchantable;

		private byte _Dyeable;

		private string _Feature;
	}
}
