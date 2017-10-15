using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.QuestDifficultyInfo")]
	public class QuestDifficultyInfo
	{
		[Column(Storage = "_QuestID", DbType = "VarChar(64) NOT NULL", CanBeNull = false)]
		public string QuestID
		{
			get
			{
				return this._QuestID;
			}
			set
			{
				if (this._QuestID != value)
				{
					this._QuestID = value;
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

		[Column(Storage = "_GameMode", DbType = "TinyInt NOT NULL")]
		public byte GameMode
		{
			get
			{
				return this._GameMode;
			}
			set
			{
				if (this._GameMode != value)
				{
					this._GameMode = value;
				}
			}
		}

		[Column(Storage = "_ShowBossHp", DbType = "Bit NOT NULL")]
		public bool ShowBossHp
		{
			get
			{
				return this._ShowBossHp;
			}
			set
			{
				if (this._ShowBossHp != value)
				{
					this._ShowBossHp = value;
				}
			}
		}

		[Column(Storage = "_EvilCoreCount", DbType = "Int NOT NULL")]
		public int EvilCoreCount
		{
			get
			{
				return this._EvilCoreCount;
			}
			set
			{
				if (this._EvilCoreCount != value)
				{
					this._EvilCoreCount = value;
				}
			}
		}

		[Column(Storage = "_MinLevel", DbType = "Int NOT NULL")]
		public int MinLevel
		{
			get
			{
				return this._MinLevel;
			}
			set
			{
				if (this._MinLevel != value)
				{
					this._MinLevel = value;
				}
			}
		}

		[Column(Storage = "_MaxLevel", DbType = "Int NOT NULL")]
		public int MaxLevel
		{
			get
			{
				return this._MaxLevel;
			}
			set
			{
				if (this._MaxLevel != value)
				{
					this._MaxLevel = value;
				}
			}
		}

		[Column(Storage = "_GoalExpRatio", DbType = "Int NOT NULL")]
		public int GoalExpRatio
		{
			get
			{
				return this._GoalExpRatio;
			}
			set
			{
				if (this._GoalExpRatio != value)
				{
					this._GoalExpRatio = value;
				}
			}
		}

		[Column(Storage = "_GoalGoldRatio", DbType = "Int NOT NULL")]
		public int GoalGoldRatio
		{
			get
			{
				return this._GoalGoldRatio;
			}
			set
			{
				if (this._GoalGoldRatio != value)
				{
					this._GoalGoldRatio = value;
				}
			}
		}

		[Column(Storage = "_BossHPRatio", DbType = "Float NOT NULL")]
		public double BossHPRatio
		{
			get
			{
				return this._BossHPRatio;
			}
			set
			{
				if (this._BossHPRatio != value)
				{
					this._BossHPRatio = value;
				}
			}
		}

		[Column(Storage = "_BossATKRatio", DbType = "Float NOT NULL")]
		public double BossATKRatio
		{
			get
			{
				return this._BossATKRatio;
			}
			set
			{
				if (this._BossATKRatio != value)
				{
					this._BossATKRatio = value;
				}
			}
		}

		[Column(Storage = "_BossDefRatio", DbType = "Float NOT NULL")]
		public double BossDefRatio
		{
			get
			{
				return this._BossDefRatio;
			}
			set
			{
				if (this._BossDefRatio != value)
				{
					this._BossDefRatio = value;
				}
			}
		}

		[Column(Storage = "_BossDownGaugeRatio", DbType = "Float NOT NULL")]
		public double BossDownGaugeRatio
		{
			get
			{
				return this._BossDownGaugeRatio;
			}
			set
			{
				if (this._BossDownGaugeRatio != value)
				{
					this._BossDownGaugeRatio = value;
				}
			}
		}

		[Column(Storage = "_MonsterAttackDamageRatio", DbType = "Float NOT NULL")]
		public double MonsterAttackDamageRatio
		{
			get
			{
				return this._MonsterAttackDamageRatio;
			}
			set
			{
				if (this._MonsterAttackDamageRatio != value)
				{
					this._MonsterAttackDamageRatio = value;
				}
			}
		}

		[Column(Storage = "_MonsterPlaybackrateRatio", DbType = "Float NOT NULL")]
		public double MonsterPlaybackrateRatio
		{
			get
			{
				return this._MonsterPlaybackrateRatio;
			}
			set
			{
				if (this._MonsterPlaybackrateRatio != value)
				{
					this._MonsterPlaybackrateRatio = value;
				}
			}
		}

		[Column(Storage = "_AttackReadyPlaybackRate", DbType = "Float NOT NULL")]
		public double AttackReadyPlaybackRate
		{
			get
			{
				return this._AttackReadyPlaybackRate;
			}
			set
			{
				if (this._AttackReadyPlaybackRate != value)
				{
					this._AttackReadyPlaybackRate = value;
				}
			}
		}

		[Column(Storage = "_TauntProbabilityRate", DbType = "Float NOT NULL")]
		public double TauntProbabilityRate
		{
			get
			{
				return this._TauntProbabilityRate;
			}
			set
			{
				if (this._TauntProbabilityRate != value)
				{
					this._TauntProbabilityRate = value;
				}
			}
		}

		[Column(Storage = "_EliteProbability", DbType = "Float NOT NULL")]
		public double EliteProbability
		{
			get
			{
				return this._EliteProbability;
			}
			set
			{
				if (this._EliteProbability != value)
				{
					this._EliteProbability = value;
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

		private string _QuestID;

		private int _Difficulty;

		private byte _GameMode;

		private bool _ShowBossHp;

		private int _EvilCoreCount;

		private int _MinLevel;

		private int _MaxLevel;

		private int _GoalExpRatio;

		private int _GoalGoldRatio;

		private double _BossHPRatio;

		private double _BossATKRatio;

		private double _BossDefRatio;

		private double _BossDownGaugeRatio;

		private double _MonsterAttackDamageRatio;

		private double _MonsterPlaybackrateRatio;

		private double _AttackReadyPlaybackRate;

		private double _TauntProbabilityRate;

		private double _EliteProbability;

		private string _Feature;
	}
}
