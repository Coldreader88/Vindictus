using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.RankPeriodInfo")]
	public class RankPeriodInfo
	{
		[Column(Storage = "_PeriodType", DbType = "Int NOT NULL")]
		public int PeriodType
		{
			get
			{
				return this._PeriodType;
			}
			set
			{
				if (this._PeriodType != value)
				{
					this._PeriodType = value;
				}
			}
		}

		[Column(Storage = "_DateForm", DbType = "DateTime NOT NULL")]
		public DateTime DateForm
		{
			get
			{
				return this._DateForm;
			}
			set
			{
				if (this._DateForm != value)
				{
					this._DateForm = value;
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

		[Column(Storage = "_Feature", DbType = "NVarChar(50)")]
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

		private int _PeriodType;

		private DateTime _DateForm;

		private DateTime _DateUntil;

		private int _Period;

		private string _Feature;
	}
}
