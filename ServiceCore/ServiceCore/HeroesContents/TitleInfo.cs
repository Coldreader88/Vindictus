using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.TitleInfo")]
	public class TitleInfo
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

		[Column(Storage = "_Description", DbType = "NVarChar(4000) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_GoalDescription", DbType = "NVarChar(4000) NOT NULL", CanBeNull = false)]
		public string GoalDescription
		{
			get
			{
				return this._GoalDescription;
			}
			set
			{
				if (this._GoalDescription != value)
				{
					this._GoalDescription = value;
				}
			}
		}

		[Column(Storage = "_Category", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Category
		{
			get
			{
				return this._Category;
			}
			set
			{
				if (this._Category != value)
				{
					this._Category = value;
				}
			}
		}

		[Column(Storage = "_HideUntil", DbType = "Int NOT NULL")]
		public int HideUntil
		{
			get
			{
				return this._HideUntil;
			}
			set
			{
				if (this._HideUntil != value)
				{
					this._HideUntil = value;
				}
			}
		}

		[Column(Storage = "_AutoGiveLevel", DbType = "Int")]
		public int? AutoGiveLevel
		{
			get
			{
				return this._AutoGiveLevel;
			}
			set
			{
				if (this._AutoGiveLevel != value)
				{
					this._AutoGiveLevel = value;
				}
			}
		}

		[Column(Storage = "_RequiredLevel", DbType = "Int NOT NULL")]
		public int RequiredLevel
		{
			get
			{
				return this._RequiredLevel;
			}
			set
			{
				if (this._RequiredLevel != value)
				{
					this._RequiredLevel = value;
				}
			}
		}

		[Column(Storage = "_ClassRestriction", DbType = "Int NOT NULL")]
		public int ClassRestriction
		{
			get
			{
				return this._ClassRestriction;
			}
			set
			{
				if (this._ClassRestriction != value)
				{
					this._ClassRestriction = value;
				}
			}
		}

		[Column(Storage = "_AllowShare", DbType = "Bit NOT NULL")]
		public bool AllowShare
		{
			get
			{
				return this._AllowShare;
			}
			set
			{
				if (this._AllowShare != value)
				{
					this._AllowShare = value;
				}
			}
		}

		[Column(Storage = "_Episode", DbType = "NVarChar(16) NOT NULL", CanBeNull = false)]
		public string Episode
		{
			get
			{
				return this._Episode;
			}
			set
			{
				if (this._Episode != value)
				{
					this._Episode = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "VarChar(1024)")]
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

		private int _ID;

		private string _Description;

		private string _GoalDescription;

		private string _Category;

		private int _HideUntil;

		private int? _AutoGiveLevel;

		private int _RequiredLevel;

		private int _ClassRestriction;

		private bool _AllowShare;

		private string _Episode;

		private string _Feature;
	}
}
