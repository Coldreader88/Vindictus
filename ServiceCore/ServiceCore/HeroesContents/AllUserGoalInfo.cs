using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.AllUserGoalInfo")]
	public class AllUserGoalInfo
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

		[Column(Storage = "_SlotID", DbType = "Int NOT NULL")]
		public int SlotID
		{
			get
			{
				return this._SlotID;
			}
			set
			{
				if (this._SlotID != value)
				{
					this._SlotID = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "NVarChar(256)")]
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

		[Column(Storage = "_ServerCode", DbType = "NVarChar(128)")]
		public string ServerCode
		{
			get
			{
				return this._ServerCode;
			}
			set
			{
				if (this._ServerCode != value)
				{
					this._ServerCode = value;
				}
			}
		}

		[Column(Storage = "_GoalType", DbType = "Int NOT NULL")]
		public int GoalType
		{
			get
			{
				return this._GoalType;
			}
			set
			{
				if (this._GoalType != value)
				{
					this._GoalType = value;
				}
			}
		}

		[Column(Storage = "_Goal", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
		public string Goal
		{
			get
			{
				return this._Goal;
			}
			set
			{
				if (this._Goal != value)
				{
					this._Goal = value;
				}
			}
		}

		[Column(Storage = "_GoalCount", DbType = "Int NOT NULL")]
		public int GoalCount
		{
			get
			{
				return this._GoalCount;
			}
			set
			{
				if (this._GoalCount != value)
				{
					this._GoalCount = value;
				}
			}
		}

		[Column(Storage = "_RewardItemClass", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
		public string RewardItemClass
		{
			get
			{
				return this._RewardItemClass;
			}
			set
			{
				if (this._RewardItemClass != value)
				{
					this._RewardItemClass = value;
				}
			}
		}

		[Column(Storage = "_RewardNum", DbType = "Int NOT NULL")]
		public int RewardNum
		{
			get
			{
				return this._RewardNum;
			}
			set
			{
				if (this._RewardNum != value)
				{
					this._RewardNum = value;
				}
			}
		}

		[Column(Storage = "_StartTime", DbType = "DateTime NOT NULL")]
		public DateTime StartTime
		{
			get
			{
				return this._StartTime;
			}
			set
			{
				if (this._StartTime != value)
				{
					this._StartTime = value;
				}
			}
		}

		[Column(Storage = "_EndTime", DbType = "DateTime NOT NULL")]
		public DateTime EndTime
		{
			get
			{
				return this._EndTime;
			}
			set
			{
				if (this._EndTime != value)
				{
					this._EndTime = value;
				}
			}
		}

		[Column(Storage = "_GoalName", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
		public string GoalName
		{
			get
			{
				return this._GoalName;
			}
			set
			{
				if (this._GoalName != value)
				{
					this._GoalName = value;
				}
			}
		}

		[Column(Storage = "_GoalDesc", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
		public string GoalDesc
		{
			get
			{
				return this._GoalDesc;
			}
			set
			{
				if (this._GoalDesc != value)
				{
					this._GoalDesc = value;
				}
			}
		}

		[Column(Storage = "_GoalInfo", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
		public string GoalInfo
		{
			get
			{
				return this._GoalInfo;
			}
			set
			{
				if (this._GoalInfo != value)
				{
					this._GoalInfo = value;
				}
			}
		}

		private int _ID;

		private int _SlotID;

		private string _Feature;

		private string _ServerCode;

		private int _GoalType;

		private string _Goal;

		private int _GoalCount;

		private string _RewardItemClass;

		private int _RewardNum;

		private DateTime _StartTime;

		private DateTime _EndTime;

		private string _GoalName;

		private string _GoalDesc;

		private string _GoalInfo;
	}
}
