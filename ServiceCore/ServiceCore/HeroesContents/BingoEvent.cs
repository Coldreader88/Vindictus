using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.BingoEvent")]
	public class BingoEvent
	{
		[Column(Storage = "_BingoEventVer", DbType = "Int NOT NULL")]
		public int BingoEventVer
		{
			get
			{
				return this._BingoEventVer;
			}
			set
			{
				if (this._BingoEventVer != value)
				{
					this._BingoEventVer = value;
				}
			}
		}

		[Column(Storage = "_Title", DbType = "NVarChar(512)")]
		public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				if (this._Title != value)
				{
					this._Title = value;
				}
			}
		}

		[Column(Storage = "_StartDate", DbType = "NVarChar(32)")]
		public string StartDate
		{
			get
			{
				return this._StartDate;
			}
			set
			{
				if (this._StartDate != value)
				{
					this._StartDate = value;
				}
			}
		}

		[Column(Storage = "_EndDate", DbType = "NVarChar(32)")]
		public string EndDate
		{
			get
			{
				return this._EndDate;
			}
			set
			{
				if (this._EndDate != value)
				{
					this._EndDate = value;
				}
			}
		}

		[Column(Storage = "_Length", DbType = "Int NOT NULL")]
		public int Length
		{
			get
			{
				return this._Length;
			}
			set
			{
				if (this._Length != value)
				{
					this._Length = value;
				}
			}
		}

		[Column(Storage = "_NumberLimit", DbType = "Int NOT NULL")]
		public int NumberLimit
		{
			get
			{
				return this._NumberLimit;
			}
			set
			{
				if (this._NumberLimit != value)
				{
					this._NumberLimit = value;
				}
			}
		}

		private int _BingoEventVer;

		private string _Title;

		private string _StartDate;

		private string _EndDate;

		private int _Length;

		private int _NumberLimit;
	}
}
