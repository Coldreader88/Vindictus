using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.TitleGoalInfo")]
	public class TitleGoalInfo
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

		[Column(Storage = "_Description", DbType = "VarChar(128) NOT NULL", CanBeNull = false)]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if (this._Description != value)
				{
					this._Description = value;
				}
			}
		}

		[Column(Storage = "_Target", DbType = "Text NOT NULL", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
		public string Target
		{
			get
			{
				return this._Target;
			}
			set
			{
				if (this._Target != value)
				{
					this._Target = value;
				}
			}
		}

		[Column(Storage = "_TargetCount", DbType = "Int NOT NULL")]
		public int TargetCount
		{
			get
			{
				return this._TargetCount;
			}
			set
			{
				if (this._TargetCount != value)
				{
					this._TargetCount = value;
				}
			}
		}

		[Column(Storage = "_IsPositive", DbType = "Bit NOT NULL")]
		public bool IsPositive
		{
			get
			{
				return this._IsPositive;
			}
			set
			{
				if (this._IsPositive != value)
				{
					this._IsPositive = value;
				}
			}
		}

		[Column(Storage = "_IsParty", DbType = "Bit NOT NULL")]
		public bool IsParty
		{
			get
			{
				return this._IsParty;
			}
			set
			{
				if (this._IsParty != value)
				{
					this._IsParty = value;
				}
			}
		}

		private int _ID;

		private int _TitleID;

		private string _Description;

		private string _Target;

		private int _TargetCount;

		private bool _IsPositive;

		private bool _IsParty;
	}
}
