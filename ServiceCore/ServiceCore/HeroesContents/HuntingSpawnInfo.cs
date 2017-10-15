using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.HuntingSpawnInfo")]
	public class HuntingSpawnInfo
	{
		[Column(Storage = "_rowID", DbType = "Int NOT NULL")]
		public int rowID
		{
			get
			{
				return this._rowID;
			}
			set
			{
				if (this._rowID != value)
				{
					this._rowID = value;
				}
			}
		}

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

		[Column(Storage = "_SpawnPoint", DbType = "NVarChar(10) NOT NULL", CanBeNull = false)]
		public string SpawnPoint
		{
			get
			{
				return this._SpawnPoint;
			}
			set
			{
				if (this._SpawnPoint != value)
				{
					this._SpawnPoint = value;
				}
			}
		}

		[Column(Storage = "_IntervalMin", DbType = "Int NOT NULL")]
		public int IntervalMin
		{
			get
			{
				return this._IntervalMin;
			}
			set
			{
				if (this._IntervalMin != value)
				{
					this._IntervalMin = value;
				}
			}
		}

		[Column(Storage = "_IntervalMax", DbType = "Int NOT NULL")]
		public int IntervalMax
		{
			get
			{
				return this._IntervalMax;
			}
			set
			{
				if (this._IntervalMax != value)
				{
					this._IntervalMax = value;
				}
			}
		}

		[Column(Storage = "_GroupMin", DbType = "Int NOT NULL")]
		public int GroupMin
		{
			get
			{
				return this._GroupMin;
			}
			set
			{
				if (this._GroupMin != value)
				{
					this._GroupMin = value;
				}
			}
		}

		[Column(Storage = "_GroupMax", DbType = "Int NOT NULL")]
		public int GroupMax
		{
			get
			{
				return this._GroupMax;
			}
			set
			{
				if (this._GroupMax != value)
				{
					this._GroupMax = value;
				}
			}
		}

		private int _rowID;

		private string _QuestID;

		private string _SpawnPoint;

		private int _IntervalMin;

		private int _IntervalMax;

		private int _GroupMin;

		private int _GroupMax;
	}
}
