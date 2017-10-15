using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.TodayMissionGoalInfo")]
	public class TodayMissionGoalInfo
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

		[Column(Storage = "_MissionGoal", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string MissionGoal
		{
			get
			{
				return this._MissionGoal;
			}
			set
			{
				if (this._MissionGoal != value)
				{
					this._MissionGoal = value;
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

		private string _MissionGoal;

		private bool _IsParty;
	}
}
