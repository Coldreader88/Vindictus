using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.TitleStatInfo")]
	public class TitleStatInfo
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

		[Column(Storage = "_TitleID", DbType = "Int NOT NULL")]
		public int TitleID
		{
			get
			{
				return this._TitleID;
			}
			set
			{
				if (this._TitleID != value)
				{
					this._TitleID = value;
				}
			}
		}

		[Column(Storage = "_Stat", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Stat
		{
			get
			{
				return this._Stat;
			}
			set
			{
				if (this._Stat != value)
				{
					this._Stat = value;
				}
			}
		}

		[Column(Storage = "_Amount", DbType = "Int NOT NULL")]
		public int Amount
		{
			get
			{
				return this._Amount;
			}
			set
			{
				if (this._Amount != value)
				{
					this._Amount = value;
				}
			}
		}

		private int _ID;

		private int _TitleID;

		private string _Stat;

		private int _Amount;
	}
}
