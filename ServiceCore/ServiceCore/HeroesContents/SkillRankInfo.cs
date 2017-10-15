using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.SkillRankInfo")]
	public class SkillRankInfo
	{
		[Column(Storage = "_RowID", DbType = "Int NOT NULL")]
		public int RowID
		{
			get
			{
				return this._RowID;
			}
			set
			{
				if (this._RowID != value)
				{
					this._RowID = value;
				}
			}
		}

		[Column(Storage = "_SkillID", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string SkillID
		{
			get
			{
				return this._SkillID;
			}
			set
			{
				if (this._SkillID != value)
				{
					this._SkillID = value;
				}
			}
		}

		[Column(Storage = "_Rank", DbType = "Int NOT NULL")]
		public int Rank
		{
			get
			{
				return this._Rank;
			}
			set
			{
				if (this._Rank != value)
				{
					this._Rank = value;
				}
			}
		}

		[Column(Storage = "_LevelConstraint", DbType = "Int NOT NULL")]
		public int LevelConstraint
		{
			get
			{
				return this._LevelConstraint;
			}
			set
			{
				if (this._LevelConstraint != value)
				{
					this._LevelConstraint = value;
				}
			}
		}

		[Column(Storage = "_RequiredAP", DbType = "Int NOT NULL")]
		public int RequiredAP
		{
			get
			{
				return this._RequiredAP;
			}
			set
			{
				if (this._RequiredAP != value)
				{
					this._RequiredAP = value;
				}
			}
		}

		[Column(Storage = "_RequireUnlock", DbType = "Bit NOT NULL")]
		public bool RequireUnlock
		{
			get
			{
				return this._RequireUnlock;
			}
			set
			{
				if (this._RequireUnlock != value)
				{
					this._RequireUnlock = value;
				}
			}
		}

		[Column(Storage = "_UnlockStoryID", DbType = "NVarChar(50)")]
		public string UnlockStoryID
		{
			get
			{
				return this._UnlockStoryID;
			}
			set
			{
				if (this._UnlockStoryID != value)
				{
					this._UnlockStoryID = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "NVarChar(50)")]
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

		[Column(Storage = "_Description", DbType = "NVarChar(70) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_StatusEffectType_01", DbType = "NVarChar(50)")]
		public string StatusEffectType_01
		{
			get
			{
				return this._StatusEffectType_01;
			}
			set
			{
				if (this._StatusEffectType_01 != value)
				{
					this._StatusEffectType_01 = value;
				}
			}
		}

		[Column(Storage = "_StatusEffectRank_01", DbType = "Int")]
		public int? StatusEffectRank_01
		{
			get
			{
				return this._StatusEffectRank_01;
			}
			set
			{
				if (this._StatusEffectRank_01 != value)
				{
					this._StatusEffectRank_01 = value;
				}
			}
		}

		[Column(Storage = "_StatusEffectType_02", DbType = "NVarChar(50)")]
		public string StatusEffectType_02
		{
			get
			{
				return this._StatusEffectType_02;
			}
			set
			{
				if (this._StatusEffectType_02 != value)
				{
					this._StatusEffectType_02 = value;
				}
			}
		}

		[Column(Storage = "_StatusEffectRank_02", DbType = "Int")]
		public int? StatusEffectRank_02
		{
			get
			{
				return this._StatusEffectRank_02;
			}
			set
			{
				if (this._StatusEffectRank_02 != value)
				{
					this._StatusEffectRank_02 = value;
				}
			}
		}

		[Column(Storage = "_StatusEffectType_03", DbType = "NVarChar(50)")]
		public string StatusEffectType_03
		{
			get
			{
				return this._StatusEffectType_03;
			}
			set
			{
				if (this._StatusEffectType_03 != value)
				{
					this._StatusEffectType_03 = value;
				}
			}
		}

		[Column(Storage = "_StatusEffectRank_03", DbType = "Int")]
		public int? StatusEffectRank_03
		{
			get
			{
				return this._StatusEffectRank_03;
			}
			set
			{
				if (this._StatusEffectRank_03 != value)
				{
					this._StatusEffectRank_03 = value;
				}
			}
		}

		private int _RowID;

		private string _SkillID;

		private int _Rank;

		private int _LevelConstraint;

		private int _RequiredAP;

		private bool _RequireUnlock;

		private string _UnlockStoryID;

		private string _Feature;

		private string _Description;

		private string _StatusEffectType_01;

		private int? _StatusEffectRank_01;

		private string _StatusEffectType_02;

		private int? _StatusEffectRank_02;

		private string _StatusEffectType_03;

		private int? _StatusEffectRank_03;
	}
}
