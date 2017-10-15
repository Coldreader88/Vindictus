using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.TowerModuleSectorInfo")]
	public class TowerModuleSectorInfo
	{
		[Column(Storage = "_QuestID", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_SectorID", DbType = "Int NOT NULL")]
		public int SectorID
		{
			get
			{
				return this._SectorID;
			}
			set
			{
				if (this._SectorID != value)
				{
					this._SectorID = value;
				}
			}
		}

		[Column(Storage = "_BSPname", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string BSPname
		{
			get
			{
				return this._BSPname;
			}
			set
			{
				if (this._BSPname != value)
				{
					this._BSPname = value;
				}
			}
		}

		[Column(Storage = "_usage", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string usage
		{
			get
			{
				return this._usage;
			}
			set
			{
				if (this._usage != value)
				{
					this._usage = value;
				}
			}
		}

		[Column(Storage = "_minLevel", DbType = "Int NOT NULL")]
		public int minLevel
		{
			get
			{
				return this._minLevel;
			}
			set
			{
				if (this._minLevel != value)
				{
					this._minLevel = value;
				}
			}
		}

		[Column(Storage = "_maxLevel", DbType = "Int NOT NULL")]
		public int maxLevel
		{
			get
			{
				return this._maxLevel;
			}
			set
			{
				if (this._maxLevel != value)
				{
					this._maxLevel = value;
				}
			}
		}

		private string _QuestID;

		private int _SectorID;

		private string _BSPname;

		private string _usage;

		private int _minLevel;

		private int _maxLevel;
	}
}
