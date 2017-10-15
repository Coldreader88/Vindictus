using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.AvatarSetBonusInfo")]
	public class AvatarSetBonusInfo
	{
		[Column(Storage = "_SetID", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string SetID
		{
			get
			{
				return this._SetID;
			}
			set
			{
				if (this._SetID != value)
				{
					this._SetID = value;
				}
			}
		}

		[Column(Storage = "_PartsCount", DbType = "Int NOT NULL")]
		public int PartsCount
		{
			get
			{
				return this._PartsCount;
			}
			set
			{
				if (this._PartsCount != value)
				{
					this._PartsCount = value;
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

		[Column(Storage = "_StatusEffect", DbType = "NVarChar(50)")]
		public string StatusEffect
		{
			get
			{
				return this._StatusEffect;
			}
			set
			{
				if (this._StatusEffect != value)
				{
					this._StatusEffect = value;
				}
			}
		}

		[Column(Storage = "_Description", DbType = "NVarChar(256)")]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if (this._Description != value)
				{
					this._Description = value;
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

		private string _SetID;

		private int _PartsCount;

		private int _STR;

		private int _DEX;

		private int _INT;

		private int _WILL;

		private int _LUCK;

		private int _HP;

		private int _STAMINA;

		private int _ATK;

		private int _DEF;

		private int _MATK;

		private string _StatusEffect;

		private string _Description;

		private string _Feature;
	}
}
