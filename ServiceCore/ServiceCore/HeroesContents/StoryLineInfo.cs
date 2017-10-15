using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.StoryLineInfo")]
	public class StoryLineInfo
	{
		[Column(Storage = "_StoryLineID", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string StoryLineID
		{
			get
			{
				return this._StoryLineID;
			}
			set
			{
				if (this._StoryLineID != value)
				{
					this._StoryLineID = value;
				}
			}
		}

		[Column(Storage = "_StoryLineName", DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
		public string StoryLineName
		{
			get
			{
				return this._StoryLineName;
			}
			set
			{
				if (this._StoryLineName != value)
				{
					this._StoryLineName = value;
				}
			}
		}

		[Column(Storage = "_XmlPath", DbType = "NVarChar(50)")]
		public string XmlPath
		{
			get
			{
				return this._XmlPath;
			}
			set
			{
				if (this._XmlPath != value)
				{
					this._XmlPath = value;
				}
			}
		}

		[Column(Storage = "_Npc", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Npc
		{
			get
			{
				return this._Npc;
			}
			set
			{
				if (this._Npc != value)
				{
					this._Npc = value;
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

		[Column(Storage = "_Stage", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Stage
		{
			get
			{
				return this._Stage;
			}
			set
			{
				if (this._Stage != value)
				{
					this._Stage = value;
				}
			}
		}

		[Column(Storage = "_RewardGold", DbType = "Int NOT NULL")]
		public int RewardGold
		{
			get
			{
				return this._RewardGold;
			}
			set
			{
				if (this._RewardGold != value)
				{
					this._RewardGold = value;
				}
			}
		}

		[Column(Storage = "_RewardExp", DbType = "Int NOT NULL")]
		public int RewardExp
		{
			get
			{
				return this._RewardExp;
			}
			set
			{
				if (this._RewardExp != value)
				{
					this._RewardExp = value;
				}
			}
		}

		[Column(Storage = "_RewardAp", DbType = "Int NOT NULL")]
		public int RewardAp
		{
			get
			{
				return this._RewardAp;
			}
			set
			{
				if (this._RewardAp != value)
				{
					this._RewardAp = value;
				}
			}
		}

		[Column(Storage = "_RewardTitle", DbType = "Int NOT NULL")]
		public int RewardTitle
		{
			get
			{
				return this._RewardTitle;
			}
			set
			{
				if (this._RewardTitle != value)
				{
					this._RewardTitle = value;
				}
			}
		}

		[Column(Storage = "_RewardItem", DbType = "NVarChar(128)")]
		public string RewardItem
		{
			get
			{
				return this._RewardItem;
			}
			set
			{
				if (this._RewardItem != value)
				{
					this._RewardItem = value;
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

		[Column(Storage = "_RewardSkill", DbType = "NVarChar(50)")]
		public string RewardSkill
		{
			get
			{
				return this._RewardSkill;
			}
			set
			{
				if (this._RewardSkill != value)
				{
					this._RewardSkill = value;
				}
			}
		}

		[Column(Storage = "_RewardSkillRank", DbType = "Int NOT NULL")]
		public int RewardSkillRank
		{
			get
			{
				return this._RewardSkillRank;
			}
			set
			{
				if (this._RewardSkillRank != value)
				{
					this._RewardSkillRank = value;
				}
			}
		}

		[Column(Storage = "_AutoGiveLevel", DbType = "Int NOT NULL")]
		public int AutoGiveLevel
		{
			get
			{
				return this._AutoGiveLevel;
			}
			set
			{
				if (this._AutoGiveLevel != value)
				{
					this._AutoGiveLevel = value;
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

		[Column(Storage = "_AutoProcess", DbType = "Bit NOT NULL")]
		public bool AutoProcess
		{
			get
			{
				return this._AutoProcess;
			}
			set
			{
				if (this._AutoProcess != value)
				{
					this._AutoProcess = value;
				}
			}
		}

		[Column(Storage = "_AutoReveal", DbType = "Bit NOT NULL")]
		public bool AutoReveal
		{
			get
			{
				return this._AutoReveal;
			}
			set
			{
				if (this._AutoReveal != value)
				{
					this._AutoReveal = value;
				}
			}
		}

		[Column(Storage = "_Season", DbType = "Int NOT NULL")]
		public int Season
		{
			get
			{
				return this._Season;
			}
			set
			{
				if (this._Season != value)
				{
					this._Season = value;
				}
			}
		}

		[Column(Storage = "_Episode", DbType = "NVarChar(16) NOT NULL", CanBeNull = false)]
		public string Episode
		{
			get
			{
				return this._Episode;
			}
			set
			{
				if (this._Episode != value)
				{
					this._Episode = value;
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

		private string _StoryLineID;

		private string _StoryLineName;

		private string _XmlPath;

		private string _Npc;

		private string _Category;

		private string _Stage;

		private int _RewardGold;

		private int _RewardExp;

		private int _RewardAp;

		private int _RewardTitle;

		private string _RewardItem;

		private int _RewardItemCount;

		private string _RewardSkill;

		private int _RewardSkillRank;

		private int _AutoGiveLevel;

		private int _ClassRestriction;

		private bool _AutoProcess;

		private bool _AutoReveal;

		private int _Season;

		private string _Episode;

		private string _Feature;
	}
}
