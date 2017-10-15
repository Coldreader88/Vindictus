using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.StoryLinePeriodInfo")]
	public class StoryLinePeriodInfo
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

		[Column(Storage = "_StoryLineID", DbType = "NVarChar(70) NOT NULL", CanBeNull = false)]
		public string StoryLineID
		{
			get
			{
				return this._StoryLineID;
			}
			set
			{
				if (this._StoryLineID != value)
				{
					this._StoryLineID = value;
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

		[Column(Storage = "_TargetPhaseID", DbType = "NVarChar(50)")]
		public string TargetPhaseID
		{
			get
			{
				return this._TargetPhaseID;
			}
			set
			{
				if (this._TargetPhaseID != value)
				{
					this._TargetPhaseID = value;
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

		private int _ID;

		private string _StoryLineID;

		private DateTime _DateFrom;

		private int _Period;

		private string _TargetPhaseID;

		private string _Feature;
	}
}
