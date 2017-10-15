using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.RankDescription")]
	public class RankDescription
	{
		[Column(Storage = "_RankID", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string RankID
		{
			get
			{
				return this._RankID;
			}
			set
			{
				if (this._RankID != value)
				{
					this._RankID = value;
				}
			}
		}

		[Column(Storage = "_CategoryID", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string CategoryID
		{
			get
			{
				return this._CategoryID;
			}
			set
			{
				if (this._CategoryID != value)
				{
					this._CategoryID = value;
				}
			}
		}

		[Column(Storage = "_EventID", DbType = "Int NOT NULL")]
		public int EventID
		{
			get
			{
				return this._EventID;
			}
			set
			{
				if (this._EventID != value)
				{
					this._EventID = value;
				}
			}
		}

		[Column(Storage = "_ScoreType", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string ScoreType
		{
			get
			{
				return this._ScoreType;
			}
			set
			{
				if (this._ScoreType != value)
				{
					this._ScoreType = value;
				}
			}
		}

		[Column(Storage = "_SortType", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string SortType
		{
			get
			{
				return this._SortType;
			}
			set
			{
				if (this._SortType != value)
				{
					this._SortType = value;
				}
			}
		}

		[Column(Storage = "_ScoreBasisType", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string ScoreBasisType
		{
			get
			{
				return this._ScoreBasisType;
			}
			set
			{
				if (this._ScoreBasisType != value)
				{
					this._ScoreBasisType = value;
				}
			}
		}

		[Column(Storage = "_ScoreResultType", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string ScoreResultType
		{
			get
			{
				return this._ScoreResultType;
			}
			set
			{
				if (this._ScoreResultType != value)
				{
					this._ScoreResultType = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_ScoreLimit", DbType = "Int NOT NULL")]
		public int ScoreLimit
		{
			get
			{
				return this._ScoreLimit;
			}
			set
			{
				if (this._ScoreLimit != value)
				{
					this._ScoreLimit = value;
				}
			}
		}

		[Column(Storage = "_IsGuild", DbType = "Bit NOT NULL")]
		public bool IsGuild
		{
			get
			{
				return this._IsGuild;
			}
			set
			{
				if (this._IsGuild != value)
				{
					this._IsGuild = value;
				}
			}
		}

		[Column(Storage = "_InGameBannerPeriodType", DbType = "Int NOT NULL")]
		public int InGameBannerPeriodType
		{
			get
			{
				return this._InGameBannerPeriodType;
			}
			set
			{
				if (this._InGameBannerPeriodType != value)
				{
					this._InGameBannerPeriodType = value;
				}
			}
		}

		[Column(Storage = "_InGameBannerPosition", DbType = "Int NOT NULL")]
		public int InGameBannerPosition
		{
			get
			{
				return this._InGameBannerPosition;
			}
			set
			{
				if (this._InGameBannerPosition != value)
				{
					this._InGameBannerPosition = value;
				}
			}
		}

		private string _RankID;

		private string _CategoryID;

		private int _EventID;

		private string _ScoreType;

		private string _SortType;

		private string _ScoreBasisType;

		private string _ScoreResultType;

		private string _Feature;

		private int _ScoreLimit;

		private bool _IsGuild;

		private int _InGameBannerPeriodType;

		private int _InGameBannerPosition;
	}
}
