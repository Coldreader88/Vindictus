using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.DailyItemProcessInfo")]
	public class DailyItemProcessInfo
	{
		[Column(Storage = "_DailyProcessID", DbType = "Int NOT NULL")]
		public int DailyProcessID
		{
			get
			{
				return this._DailyProcessID;
			}
			set
			{
				if (this._DailyProcessID != value)
				{
					this._DailyProcessID = value;
				}
			}
		}

		[Column(Storage = "_DateFrom", DbType = "DateTime NOT NULL")]
		public DateTime DateFrom
		{
			get
			{
				return this._DateFrom;
			}
			set
			{
				if (this._DateFrom != value)
				{
					this._DateFrom = value;
				}
			}
		}

		[Column(Storage = "_DateUntil", DbType = "DateTime NOT NULL")]
		public DateTime DateUntil
		{
			get
			{
				return this._DateUntil;
			}
			set
			{
				if (this._DateUntil != value)
				{
					this._DateUntil = value;
				}
			}
		}

		[Column(Storage = "_Period", DbType = "Int NOT NULL")]
		public int Period
		{
			get
			{
				return this._Period;
			}
			set
			{
				if (this._Period != value)
				{
					this._Period = value;
				}
			}
		}

		[Column(Storage = "_CafeLevel", DbType = "Int NOT NULL")]
		public int CafeLevel
		{
			get
			{
				return this._CafeLevel;
			}
			set
			{
				if (this._CafeLevel != value)
				{
					this._CafeLevel = value;
				}
			}
		}

		[Column(Storage = "_CafeType", DbType = "Int NOT NULL")]
		public int CafeType
		{
			get
			{
				return this._CafeType;
			}
			set
			{
				if (this._CafeType != value)
				{
					this._CafeType = value;
				}
			}
		}

		[Column(Storage = "_LevelMin", DbType = "Int")]
		public int? LevelMin
		{
			get
			{
				return this._LevelMin;
			}
			set
			{
				if (this._LevelMin != value)
				{
					this._LevelMin = value;
				}
			}
		}

		[Column(Storage = "_LevelMax", DbType = "Int")]
		public int? LevelMax
		{
			get
			{
				return this._LevelMax;
			}
			set
			{
				if (this._LevelMax != value)
				{
					this._LevelMax = value;
				}
			}
		}

		[Column(Storage = "_QueryString", DbType = "VarChar(256) NOT NULL", CanBeNull = false)]
		public string QueryString
		{
			get
			{
				return this._QueryString;
			}
			set
			{
				if (this._QueryString != value)
				{
					this._QueryString = value;
				}
			}
		}

		[Column(Storage = "_Message", DbType = "VarChar(1024)")]
		public string Message
		{
			get
			{
				return this._Message;
			}
			set
			{
				if (this._Message != value)
				{
					this._Message = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "VarChar(50)")]
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

		private int _DailyProcessID;

		private DateTime _DateFrom;

		private DateTime _DateUntil;

		private int _Period;

		private int _CafeLevel;

		private int _CafeType;

		private int? _LevelMin;

		private int? _LevelMax;

		private string _QueryString;

		private string _Message;

		private string _Feature;
	}
}
