using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.SpiritInjectionDiceInfo")]
	public class SpiritInjectionDiceInfo
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

		[Column(Storage = "_EnhanceExistStat", DbType = "SmallInt NOT NULL")]
		public short EnhanceExistStat
		{
			get
			{
				return this._EnhanceExistStat;
			}
			set
			{
				if (this._EnhanceExistStat != value)
				{
					this._EnhanceExistStat = value;
				}
			}
		}

		[Column(Storage = "_EnhanceNoneExistStat", DbType = "SmallInt NOT NULL")]
		public short EnhanceNoneExistStat
		{
			get
			{
				return this._EnhanceNoneExistStat;
			}
			set
			{
				if (this._EnhanceNoneExistStat != value)
				{
					this._EnhanceNoneExistStat = value;
				}
			}
		}

		[Column(Storage = "_ReduceExistStat", DbType = "SmallInt NOT NULL")]
		public short ReduceExistStat
		{
			get
			{
				return this._ReduceExistStat;
			}
			set
			{
				if (this._ReduceExistStat != value)
				{
					this._ReduceExistStat = value;
				}
			}
		}

		[Column(Storage = "_ATK", DbType = "SmallInt NOT NULL")]
		public short ATK
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

		[Column(Storage = "_ATK_Speed", DbType = "SmallInt NOT NULL")]
		public short ATK_Speed
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

		[Column(Storage = "_Critical", DbType = "SmallInt NOT NULL")]
		public short Critical
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

		[Column(Storage = "_Balance", DbType = "SmallInt NOT NULL")]
		public short Balance
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

		[Column(Storage = "_MATK", DbType = "SmallInt NOT NULL")]
		public short MATK
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

		[Column(Storage = "_DEF", DbType = "SmallInt NOT NULL")]
		public short DEF
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

		[Column(Storage = "_Res_Critical", DbType = "SmallInt NOT NULL")]
		public short Res_Critical
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

		[Column(Storage = "_STR", DbType = "SmallInt NOT NULL")]
		public short STR
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

		[Column(Storage = "_DEX", DbType = "SmallInt NOT NULL")]
		public short DEX
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

		[Column(Storage = "_INT", DbType = "SmallInt NOT NULL")]
		public short INT
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

		[Column(Storage = "_WILL", DbType = "SmallInt NOT NULL")]
		public short WILL
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

		[Column(Storage = "_HP", DbType = "SmallInt NOT NULL")]
		public short HP
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

		[Column(Storage = "_STAMINA", DbType = "SmallInt NOT NULL")]
		public short STAMINA
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

		private string _ItemClass;

		private short _EnhanceExistStat;

		private short _EnhanceNoneExistStat;

		private short _ReduceExistStat;

		private short _ATK;

		private short _ATK_Speed;

		private short _Critical;

		private short _Balance;

		private short _MATK;

		private short _DEF;

		private short _Res_Critical;

		private short _STR;

		private short _DEX;

		private short _INT;

		private short _WILL;

		private short _HP;

		private short _STAMINA;
	}
}
