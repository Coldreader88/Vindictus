using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.BriefDropItemInfo")]
	public class BriefDropItemInfo
	{
		[Column(Storage = "_MonsterID", DbType = "Int NOT NULL")]
		public int MonsterID
		{
			get
			{
				return this._MonsterID;
			}
			set
			{
				if (this._MonsterID != value)
				{
					this._MonsterID = value;
				}
			}
		}

		[Column(Storage = "_QuestID", DbType = "VarChar(100)")]
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

		[Column(Storage = "_ItemClassEX", DbType = "NVarChar(1024) NOT NULL", CanBeNull = false)]
		public string ItemClassEX
		{
			get
			{
				return this._ItemClassEX;
			}
			set
			{
				if (this._ItemClassEX != value)
				{
					this._ItemClassEX = value;
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

		private int _MonsterID;

		private string _QuestID;

		private int _Difficulty;

		private string _ItemClassEX;

		private string _Feature;
	}
}
