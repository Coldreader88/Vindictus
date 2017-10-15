using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.AttendanceEvent")]
	public class AttendanceEvent
	{
		[Column(Storage = "_AttendanceEventVer", DbType = "Int NOT NULL")]
		public int AttendanceEventVer
		{
			get
			{
				return this._AttendanceEventVer;
			}
			set
			{
				if (this._AttendanceEventVer != value)
				{
					this._AttendanceEventVer = value;
				}
			}
		}

		[Column(Storage = "_ResetVer", DbType = "NVarChar(32) NOT NULL", CanBeNull = false)]
		public string ResetVer
		{
			get
			{
				return this._ResetVer;
			}
			set
			{
				if (this._ResetVer != value)
				{
					this._ResetVer = value;
				}
			}
		}

		[Column(Storage = "_ConditionType", DbType = "NVarChar(64)")]
		public string ConditionType
		{
			get
			{
				return this._ConditionType;
			}
			set
			{
				if (this._ConditionType != value)
				{
					this._ConditionType = value;
				}
			}
		}

		[Column(Storage = "_UseSavePoint", DbType = "NVarChar(32)")]
		public string UseSavePoint
		{
			get
			{
				return this._UseSavePoint;
			}
			set
			{
				if (this._UseSavePoint != value)
				{
					this._UseSavePoint = value;
				}
			}
		}

		[Column(Storage = "_Title", DbType = "NVarChar(512) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_EventPeriodText", DbType = "NVarChar(64) NOT NULL", CanBeNull = false)]
		public string EventPeriodText
		{
			get
			{
				return this._EventPeriodText;
			}
			set
			{
				if (this._EventPeriodText != value)
				{
					this._EventPeriodText = value;
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

		[Column(Storage = "_AttendanceTotal", DbType = "Int NOT NULL")]
		public int AttendanceTotal
		{
			get
			{
				return this._AttendanceTotal;
			}
			set
			{
				if (this._AttendanceTotal != value)
				{
					this._AttendanceTotal = value;
				}
			}
		}

		private int _AttendanceEventVer;

		private string _ResetVer;

		private string _ConditionType;

		private string _UseSavePoint;

		private string _Title;

		private string _EventPeriodText;

		private DateTime _DateFrom;

		private DateTime _DateUntil;

		private int _Period;

		private int _AttendanceTotal;
	}
}
