using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.SectorGroupInfo")]
	public class SectorGroupInfo
	{
		[Column(Storage = "_QuestID", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_FromGroup", DbType = "Int NOT NULL")]
		public int FromGroup
		{
			get
			{
				return this._FromGroup;
			}
			set
			{
				if (this._FromGroup != value)
				{
					this._FromGroup = value;
				}
			}
		}

		[Column(Storage = "_FromSector", DbType = "Int")]
		public int? FromSector
		{
			get
			{
				return this._FromSector;
			}
			set
			{
				if (this._FromSector != value)
				{
					this._FromSector = value;
				}
			}
		}

		[Column(Storage = "_FromTrigger", DbType = "NVarChar(128)")]
		public string FromTrigger
		{
			get
			{
				return this._FromTrigger;
			}
			set
			{
				if (this._FromTrigger != value)
				{
					this._FromTrigger = value;
				}
			}
		}

		[Column(Storage = "_ToGroup", DbType = "Int NOT NULL")]
		public int ToGroup
		{
			get
			{
				return this._ToGroup;
			}
			set
			{
				if (this._ToGroup != value)
				{
					this._ToGroup = value;
				}
			}
		}

		[Column(Storage = "_ToSpawnPoint", DbType = "NVarChar(128)")]
		public string ToSpawnPoint
		{
			get
			{
				return this._ToSpawnPoint;
			}
			set
			{
				if (this._ToSpawnPoint != value)
				{
					this._ToSpawnPoint = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "NVarChar(128)")]
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

		private int _FromGroup;

		private int? _FromSector;

		private string _FromTrigger;

		private int _ToGroup;

		private string _ToSpawnPoint;

		private string _Feature;
	}
}
