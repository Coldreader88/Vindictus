using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.PetStatusBalanceInfo")]
	public class PetStatusBalanceInfo
	{
		[Column(Storage = "_StatType", DbType = "Int NOT NULL")]
		public int StatType
		{
			get
			{
				return this._StatType;
			}
			set
			{
				if (this._StatType != value)
				{
					this._StatType = value;
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

		[Column(Storage = "_RequiredExp", DbType = "Int NOT NULL")]
		public int RequiredExp
		{
			get
			{
				return this._RequiredExp;
			}
			set
			{
				if (this._RequiredExp != value)
				{
					this._RequiredExp = value;
				}
			}
		}

		[Column(Storage = "_MaxExp", DbType = "Int NOT NULL")]
		public int MaxExp
		{
			get
			{
				return this._MaxExp;
			}
			set
			{
				if (this._MaxExp != value)
				{
					this._MaxExp = value;
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

		[Column(Storage = "_ResDamage", DbType = "Int NOT NULL")]
		public int ResDamage
		{
			get
			{
				return this._ResDamage;
			}
			set
			{
				if (this._ResDamage != value)
				{
					this._ResDamage = value;
				}
			}
		}

		[Column(Storage = "_HPRecovery", DbType = "Int NOT NULL")]
		public int HPRecovery
		{
			get
			{
				return this._HPRecovery;
			}
			set
			{
				if (this._HPRecovery != value)
				{
					this._HPRecovery = value;
				}
			}
		}

		[Column(Storage = "_DefBreak", DbType = "Int NOT NULL")]
		public int DefBreak
		{
			get
			{
				return this._DefBreak;
			}
			set
			{
				if (this._DefBreak != value)
				{
					this._DefBreak = value;
				}
			}
		}

		[Column(Storage = "_AtkBalance", DbType = "Int NOT NULL")]
		public int AtkBalance
		{
			get
			{
				return this._AtkBalance;
			}
			set
			{
				if (this._AtkBalance != value)
				{
					this._AtkBalance = value;
				}
			}
		}

		[Column(Storage = "_Atk", DbType = "Int NOT NULL")]
		public int Atk
		{
			get
			{
				return this._Atk;
			}
			set
			{
				if (this._Atk != value)
				{
					this._Atk = value;
				}
			}
		}

		[Column(Storage = "_Def", DbType = "Int NOT NULL")]
		public int Def
		{
			get
			{
				return this._Def;
			}
			set
			{
				if (this._Def != value)
				{
					this._Def = value;
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

		[Column(Storage = "_ResCritical", DbType = "Int NOT NULL")]
		public int ResCritical
		{
			get
			{
				return this._ResCritical;
			}
			set
			{
				if (this._ResCritical != value)
				{
					this._ResCritical = value;
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

		private int _StatType;

		private int _Level;

		private int _RequiredExp;

		private int _MaxExp;

		private int _HP;

		private int _ResDamage;

		private int _HPRecovery;

		private int _DefBreak;

		private int _AtkBalance;

		private int _Atk;

		private int _Def;

		private int _Critical;

		private int _ResCritical;

		private string _Feature;
	}
}
