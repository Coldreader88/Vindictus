using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.MonsterInfo")]
	public class MonsterInfo
	{
		public string MonsterType
		{
			get
			{
				return string.Format("{0} {1}", this.Class, this.Variation);
			}
		}

		[Column(Storage = "_Identifier", DbType = "Int NOT NULL")]
		public int Identifier
		{
			get
			{
				return this._Identifier;
			}
			set
			{
				if (this._Identifier != value)
				{
					this._Identifier = value;
				}
			}
		}

		[Column(Storage = "_Class", DbType = "NVarChar(30) NOT NULL", CanBeNull = false)]
		public string Class
		{
			get
			{
				return this._Class;
			}
			set
			{
				if (this._Class != value)
				{
					this._Class = value;
				}
			}
		}

		[Column(Storage = "_Variation", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Variation
		{
			get
			{
				return this._Variation;
			}
			set
			{
				if (this._Variation != value)
				{
					this._Variation = value;
				}
			}
		}

		[Column(Storage = "_Difficulty", DbType = "Int NOT NULL")]
		public int Difficulty
		{
			get
			{
				return this._Difficulty;
			}
			set
			{
				if (this._Difficulty != value)
				{
					this._Difficulty = value;
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

		[Column(Storage = "_Exp", DbType = "Int NOT NULL")]
		public int Exp
		{
			get
			{
				return this._Exp;
			}
			set
			{
				if (this._Exp != value)
				{
					this._Exp = value;
				}
			}
		}

		[Column(Storage = "_HP1", DbType = "Int NOT NULL")]
		public int HP1
		{
			get
			{
				return this._HP1;
			}
			set
			{
				if (this._HP1 != value)
				{
					this._HP1 = value;
				}
			}
		}

		[Column(Storage = "_HP2", DbType = "Int NOT NULL")]
		public int HP2
		{
			get
			{
				return this._HP2;
			}
			set
			{
				if (this._HP2 != value)
				{
					this._HP2 = value;
				}
			}
		}

		[Column(Storage = "_HP3", DbType = "Int NOT NULL")]
		public int HP3
		{
			get
			{
				return this._HP3;
			}
			set
			{
				if (this._HP3 != value)
				{
					this._HP3 = value;
				}
			}
		}

		[Column(Storage = "_HP4", DbType = "Int NOT NULL")]
		public int HP4
		{
			get
			{
				return this._HP4;
			}
			set
			{
				if (this._HP4 != value)
				{
					this._HP4 = value;
				}
			}
		}

		[Column(Storage = "_HP5", DbType = "Int NOT NULL")]
		public int HP5
		{
			get
			{
				return this._HP5;
			}
			set
			{
				if (this._HP5 != value)
				{
					this._HP5 = value;
				}
			}
		}

		[Column(Storage = "_HP_Hero_Percent", DbType = "Int NOT NULL")]
		public int HP_Hero_Percent
		{
			get
			{
				return this._HP_Hero_Percent;
			}
			set
			{
				if (this._HP_Hero_Percent != value)
				{
					this._HP_Hero_Percent = value;
				}
			}
		}

		[Column(Storage = "_HP_Normal_Percent", DbType = "Int NOT NULL")]
		public int HP_Normal_Percent
		{
			get
			{
				return this._HP_Normal_Percent;
			}
			set
			{
				if (this._HP_Normal_Percent != value)
				{
					this._HP_Normal_Percent = value;
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

		[Column(Storage = "_CriticalDmgFactor", DbType = "Int NOT NULL")]
		public int CriticalDmgFactor
		{
			get
			{
				return this._CriticalDmgFactor;
			}
			set
			{
				if (this._CriticalDmgFactor != value)
				{
					this._CriticalDmgFactor = value;
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

		[Column(Storage = "_WeakpointReward", DbType = "Int NOT NULL")]
		public int WeakpointReward
		{
			get
			{
				return this._WeakpointReward;
			}
			set
			{
				if (this._WeakpointReward != value)
				{
					this._WeakpointReward = value;
				}
			}
		}

		[Column(Storage = "_ATK_Bonus_Hero", DbType = "Int NOT NULL")]
		public int ATK_Bonus_Hero
		{
			get
			{
				return this._ATK_Bonus_Hero;
			}
			set
			{
				if (this._ATK_Bonus_Hero != value)
				{
					this._ATK_Bonus_Hero = value;
				}
			}
		}

		[Column(Storage = "_DEF_Bonus_Hero", DbType = "Int NOT NULL")]
		public int DEF_Bonus_Hero
		{
			get
			{
				return this._DEF_Bonus_Hero;
			}
			set
			{
				if (this._DEF_Bonus_Hero != value)
				{
					this._DEF_Bonus_Hero = value;
				}
			}
		}

		private int _Identifier;

		private string _Class;

		private string _Variation;

		private int _Difficulty;

		private int _Level;

		private int _Exp;

		private int _HP1;

		private int _HP2;

		private int _HP3;

		private int _HP4;

		private int _HP5;

		private int _HP_Hero_Percent;

		private int _HP_Normal_Percent;

		private int _ATK;

		private int _Critical;

		private int _CriticalDmgFactor;

		private int _DEF;

		private int _Res_Critical;

		private int _WeakpointReward;

		private int _ATK_Bonus_Hero;

		private int _DEF_Bonus_Hero;
	}
}
