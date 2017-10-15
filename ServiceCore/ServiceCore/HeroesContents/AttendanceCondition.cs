using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.AttendanceCondition")]
	public class AttendanceCondition
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

		[Column(Storage = "_RewardID", DbType = "NVarChar(32) NOT NULL", CanBeNull = false)]
		public string RewardID
		{
			get
			{
				return this._RewardID;
			}
			set
			{
				if (this._RewardID != value)
				{
					this._RewardID = value;
				}
			}
		}

		[Column(Storage = "_IsSavePoint", DbType = "TinyInt NOT NULL")]
		public byte IsSavePoint
		{
			get
			{
				return this._IsSavePoint;
			}
			set
			{
				if (this._IsSavePoint != value)
				{
					this._IsSavePoint = value;
				}
			}
		}

		[Column(Storage = "_IsRibbon", DbType = "TinyInt NOT NULL")]
		public byte IsRibbon
		{
			get
			{
				return this._IsRibbon;
			}
			set
			{
				if (this._IsRibbon != value)
				{
					this._IsRibbon = value;
				}
			}
		}

		private int _AttendanceEventVer;

		private string _RewardID;

		private byte _IsSavePoint;

		private byte _IsRibbon;
	}
}
