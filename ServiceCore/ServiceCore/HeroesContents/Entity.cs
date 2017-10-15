using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.Entity")]
	public class Entity
	{
		[Column(Storage = "_ID", DbType = "Int NOT NULL")]
		public int ID
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

		private int _ID;

		private int _SectorID;
	}
}
