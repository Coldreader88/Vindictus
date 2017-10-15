using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.TowerModuleSettingsInfo")]
	public class TowerModuleSettingsInfo
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

		[Column(Storage = "_SectorID", DbType = "Int")]
		public int? SectorID
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

		[Column(Name = "[Key]", Storage = "_Key", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Key
		{
			get
			{
				return this._Key;
			}
			set
			{
				if (this._Key != value)
				{
					this._Key = value;
				}
			}
		}

		[Column(Storage = "_FirstLevel", DbType = "Int NOT NULL")]
		public int FirstLevel
		{
			get
			{
				return this._FirstLevel;
			}
			set
			{
				if (this._FirstLevel != value)
				{
					this._FirstLevel = value;
				}
			}
		}

		[Column(Storage = "_FirstValue", DbType = "NVarChar(50)")]
		public string FirstValue
		{
			get
			{
				return this._FirstValue;
			}
			set
			{
				if (this._FirstValue != value)
				{
					this._FirstValue = value;
				}
			}
		}

		[Column(Storage = "_LastLevel", DbType = "Int NOT NULL")]
		public int LastLevel
		{
			get
			{
				return this._LastLevel;
			}
			set
			{
				if (this._LastLevel != value)
				{
					this._LastLevel = value;
				}
			}
		}

		[Column(Storage = "_LastValue", DbType = "NVarChar(50)")]
		public string LastValue
		{
			get
			{
				return this._LastValue;
			}
			set
			{
				if (this._LastValue != value)
				{
					this._LastValue = value;
				}
			}
		}

		private string _QuestID;

		private int? _SectorID;

		private string _Key;

		private int _FirstLevel;

		private string _FirstValue;

		private int _LastLevel;

		private string _LastValue;
	}
}
