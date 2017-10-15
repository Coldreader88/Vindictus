using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.QuestPeriodInfo")]
	public class QuestPeriodInfo
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

		[Column(Storage = "_QuestID", DbType = "NVarChar(64) NOT NULL", CanBeNull = false)]
		public string QuestID
		{
			get
			{
				return this._QuestID;
			}
			set
			{
				if (this._QuestID != value)
				{
					this._QuestID = value;
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

		[Column(Storage = "_MaxPlayCount", DbType = "Int")]
		public int? MaxPlayCount
		{
			get
			{
				return this._MaxPlayCount;
			}
			set
			{
				if (this._MaxPlayCount != value)
				{
					this._MaxPlayCount = value;
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

		private int _RowID;

		private string _QuestID;

		private DateTime _DateFrom;

		private int _Period;

		private int? _MaxPlayCount;

		private string _Feature;
	}
}
