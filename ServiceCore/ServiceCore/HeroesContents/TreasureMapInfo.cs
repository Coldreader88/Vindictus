using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.TreasureMapInfo")]
	public class TreasureMapInfo
	{
		[Column(Storage = "_ID", DbType = "VarChar(64) NOT NULL", CanBeNull = false)]
		public string ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if (this._ID != value)
				{
					this._ID = value;
				}
			}
		}

		[Column(Storage = "_BossDropProbability", DbType = "Int NOT NULL")]
		public int BossDropProbability
		{
			get
			{
				return this._BossDropProbability;
			}
			set
			{
				if (this._BossDropProbability != value)
				{
					this._BossDropProbability = value;
				}
			}
		}

		[Column(Storage = "_ZaccoDropProbability", DbType = "Int NOT NULL")]
		public int ZaccoDropProbability
		{
			get
			{
				return this._ZaccoDropProbability;
			}
			set
			{
				if (this._ZaccoDropProbability != value)
				{
					this._ZaccoDropProbability = value;
				}
			}
		}

		[Column(Storage = "_BossDropCount", DbType = "Int NOT NULL")]
		public int BossDropCount
		{
			get
			{
				return this._BossDropCount;
			}
			set
			{
				if (this._BossDropCount != value)
				{
					this._BossDropCount = value;
				}
			}
		}

		[Column(Storage = "_ZaccoDropCount", DbType = "Int NOT NULL")]
		public int ZaccoDropCount
		{
			get
			{
				return this._ZaccoDropCount;
			}
			set
			{
				if (this._ZaccoDropCount != value)
				{
					this._ZaccoDropCount = value;
				}
			}
		}

		[Column(Storage = "_MaxDropCount", DbType = "Int NOT NULL")]
		public int MaxDropCount
		{
			get
			{
				return this._MaxDropCount;
			}
			set
			{
				if (this._MaxDropCount != value)
				{
					this._MaxDropCount = value;
				}
			}
		}

		private string _ID;

		private int _BossDropProbability;

		private int _ZaccoDropProbability;

		private int _BossDropCount;

		private int _ZaccoDropCount;

		private int _MaxDropCount;
	}
}
