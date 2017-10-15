using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.ForbiddenNameInfo")]
	public class ForbiddenNameInfo
	{
		[Column(Storage = "_RowID", DbType = "Int NOT NULL")]
		public int RowID
		{
			get
			{
				return this._RowID;
			}
			set
			{
				if (this._RowID != value)
				{
					this._RowID = value;
				}
			}
		}

		[Column(Storage = "_ForbiddenName", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string ForbiddenName
		{
			get
			{
				return this._ForbiddenName;
			}
			set
			{
				if (this._ForbiddenName != value)
				{
					this._ForbiddenName = value;
				}
			}
		}

		[Column(Storage = "_IsFullMatch", DbType = "Bit NOT NULL")]
		public bool IsFullMatch
		{
			get
			{
				return this._IsFullMatch;
			}
			set
			{
				if (this._IsFullMatch != value)
				{
					this._IsFullMatch = value;
				}
			}
		}

		[Column(Storage = "_Language", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Language
		{
			get
			{
				return this._Language;
			}
			set
			{
				if (this._Language != value)
				{
					this._Language = value;
				}
			}
		}

		private int _RowID;

		private string _ForbiddenName;

		private bool _IsFullMatch;

		private string _Language;
	}
}
