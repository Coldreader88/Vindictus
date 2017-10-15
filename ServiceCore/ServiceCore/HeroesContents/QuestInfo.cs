using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.QuestInfo")]
	public class QuestInfo
	{
		public int? EventAppliedMaxPlayCount(string questID)
		{
			if (ServiceCore.FeatureMatrix.IsEnable("Event_PlayCount_Unlimited"))
			{
				return null;
			}
			if (this.MaxPlayCount == null)
			{
				return null;
			}
			int num = 0;
			string @string = ServiceCore.FeatureMatrix.GetString("AddDeparture");
			if (@string.Length > 0)
			{
				foreach (string value in @string.Split(new char[]
				{
					','
				}))
				{
					if (questID.Equals(value))
					{
						num = ServiceCore.FeatureMatrix.GetInteger("AddDepartureCount");
						break;
					}
				}
			}
			return new int?(this.MaxPlayCount.Value + ServiceCore.FeatureMatrix.GetInteger("Event_PlayCount_Plus") + num);
		}

		[Column(Storage = "_ID", DbType = "VarChar(64) NOT NULL", CanBeNull = false)]
		public string ID
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

		[Column(Storage = "_Name", DbType = "VarChar(64) NOT NULL", CanBeNull = false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if (this._Name != value)
				{
					this._Name = value;
				}
			}
		}

		[Column(Storage = "_Client", DbType = "VarChar(16) NOT NULL", CanBeNull = false)]
		public string Client
		{
			get
			{
				return this._Client;
			}
			set
			{
				if (this._Client != value)
				{
					this._Client = value;
				}
			}
		}

		[Column(Storage = "_QuestGrade", DbType = "VarChar(16) NOT NULL", CanBeNull = false)]
		public string QuestGrade
		{
			get
			{
				return this._QuestGrade;
			}
			set
			{
				if (this._QuestGrade != value)
				{
					this._QuestGrade = value;
				}
			}
		}

		[Column(Storage = "_Description", DbType = "VarChar(64) NOT NULL", CanBeNull = false)]
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

		[Column(Name = "[Level]", Storage = "_Level", DbType = "Int NOT NULL")]
		public int Level
		{
			get
			{
				return this._Level;
			}
			set
			{
				if (this._Level != value)
				{
					this._Level = value;
				}
			}
		}

		[Column(Storage = "_AutoGiveLevel", DbType = "Int NOT NULL")]
		public int AutoGiveLevel
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

		[Column(Storage = "_DropItemCountMax", DbType = "Int NOT NULL")]
		public int DropItemCountMax
		{
			get
			{
				return this._DropItemCountMax;
			}
			set
			{
				if (this._DropItemCountMax != value)
				{
					this._DropItemCountMax = value;
				}
			}
		}

		[Column(Storage = "_DropItemCountMin", DbType = "Int NOT NULL")]
		public int DropItemCountMin
		{
			get
			{
				return this._DropItemCountMin;
			}
			set
			{
				if (this._DropItemCountMin != value)
				{
					this._DropItemCountMin = value;
				}
			}
		}

		[Column(Storage = "_TimeLimit", DbType = "Int NOT NULL")]
		public int TimeLimit
		{
			get
			{
				return this._TimeLimit;
			}
			set
			{
				if (this._TimeLimit != value)
				{
					this._TimeLimit = value;
				}
			}
		}

		[Column(Storage = "_STAGE", DbType = "VarChar(64) NOT NULL", CanBeNull = false)]
		public string STAGE
		{
			get
			{
				return this._STAGE;
			}
			set
			{
				if (this._STAGE != value)
				{
					this._STAGE = value;
				}
			}
		}

		[Column(Storage = "_PropDropTableName", DbType = "VarChar(32) NOT NULL", CanBeNull = false)]
		public string PropDropTableName
		{
			get
			{
				return this._PropDropTableName;
			}
			set
			{
				if (this._PropDropTableName != value)
				{
					this._PropDropTableName = value;
				}
			}
		}

		[Column(Storage = "_HomeAfterArrival", DbType = "VarChar(8) NOT NULL", CanBeNull = false)]
		public string HomeAfterArrival
		{
			get
			{
				return this._HomeAfterArrival;
			}
			set
			{
				if (this._HomeAfterArrival != value)
				{
					this._HomeAfterArrival = value;
				}
			}
		}

		[Column(Storage = "_QuestSet", DbType = "Int NOT NULL")]
		public int QuestSet
		{
			get
			{
				return this._QuestSet;
			}
			set
			{
				if (this._QuestSet != value)
				{
					this._QuestSet = value;
				}
			}
		}

		[Column(Storage = "_QuestGroupID", DbType = "VarChar(32)")]
		public string QuestGroupID
		{
			get
			{
				return this._QuestGroupID;
			}
			set
			{
				if (this._QuestGroupID != value)
				{
					this._QuestGroupID = value;
				}
			}
		}

		[Column(Storage = "_FreeQuest", DbType = "Bit NOT NULL")]
		public bool FreeQuest
		{
			get
			{
				return this._FreeQuest;
			}
			set
			{
				if (this._FreeQuest != value)
				{
					this._FreeQuest = value;
				}
			}
		}

		[Column(Storage = "_HuntingQuest", DbType = "Bit NOT NULL")]
		public bool HuntingQuest
		{
			get
			{
				return this._HuntingQuest;
			}
			set
			{
				if (this._HuntingQuest != value)
				{
					this._HuntingQuest = value;
				}
			}
		}

		[Column(Storage = "_ServiceCoin", DbType = "Int NOT NULL")]
		public int ServiceCoin
		{
			get
			{
				return this._ServiceCoin;
			}
			set
			{
				if (this._ServiceCoin != value)
				{
					this._ServiceCoin = value;
				}
			}
		}

		[Column(Storage = "_MaxMemberCount", DbType = "Int NOT NULL")]
		public int MaxMemberCount
		{
			get
			{
				return this._MaxMemberCount;
			}
			set
			{
				if (this._MaxMemberCount != value)
				{
					this._MaxMemberCount = value;
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

		[Column(Storage = "_EnableTodayQuest", DbType = "VarChar(256)")]
		public string EnableTodayQuest
		{
			get
			{
				return this._EnableTodayQuest;
			}
			set
			{
				if (this._EnableTodayQuest != value)
				{
					this._EnableTodayQuest = value;
				}
			}
		}

		[Column(Storage = "_Season3TodayGroup", DbType = "VarChar(256)")]
		public string Season3TodayGroup
		{
			get
			{
				return this._Season3TodayGroup;
			}
			set
			{
				if (this._Season3TodayGroup != value)
				{
					this._Season3TodayGroup = value;
				}
			}
		}

		[Column(Storage = "_AutoReveal", DbType = "Bit NOT NULL")]
		public bool AutoReveal
		{
			get
			{
				return this._AutoReveal;
			}
			set
			{
				if (this._AutoReveal != value)
				{
					this._AutoReveal = value;
				}
			}
		}

		[Column(Storage = "_DisableUserShip", DbType = "Bit NOT NULL")]
		public bool DisableUserShip
		{
			get
			{
				return this._DisableUserShip;
			}
			set
			{
				if (this._DisableUserShip != value)
				{
					this._DisableUserShip = value;
				}
			}
		}

		[Column(Storage = "_DurabilityRatio", DbType = "Float NOT NULL")]
		public double DurabilityRatio
		{
			get
			{
				return this._DurabilityRatio;
			}
			set
			{
				if (this._DurabilityRatio != value)
				{
					this._DurabilityRatio = value;
				}
			}
		}

		[Column(Storage = "_RequiredItem", DbType = "NVarChar(50)")]
		public string RequiredItem
		{
			get
			{
				return this._RequiredItem;
			}
			set
			{
				if (this._RequiredItem != value)
				{
					this._RequiredItem = value;
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

		[Column(Storage = "_MinClearTime", DbType = "Int")]
		public int? MinClearTime
		{
			get
			{
				return this._MinClearTime;
			}
			set
			{
				if (this._MinClearTime != value)
				{
					this._MinClearTime = value;
				}
			}
		}

		[Column(Storage = "_EnableDSServer", DbType = "NVarChar(256)")]
		public string EnableDSServer
		{
			get
			{
				return this._EnableDSServer;
			}
			set
			{
				if (this._EnableDSServer != value)
				{
					this._EnableDSServer = value;
				}
			}
		}

		[Column(Storage = "_EnableDSServer_Constraint", DbType = "NVarChar(256)")]
		public string EnableDSServer_Constraint
		{
			get
			{
				return this._EnableDSServer_Constraint;
			}
			set
			{
				if (this._EnableDSServer_Constraint != value)
				{
					this._EnableDSServer_Constraint = value;
				}
			}
		}

		[Column(Storage = "_FatiguePointCount", DbType = "Int")]
		public int? FatiguePointCount
		{
			get
			{
				return this._FatiguePointCount;
			}
			set
			{
				if (this._FatiguePointCount != value)
				{
					this._FatiguePointCount = value;
				}
			}
		}

		[Column(Storage = "_RequiredStoryLineID", DbType = "NVarChar(256)")]
		public string RequiredStoryLineID
		{
			get
			{
				return this._RequiredStoryLineID;
			}
			set
			{
				if (this._RequiredStoryLineID != value)
				{
					this._RequiredStoryLineID = value;
				}
			}
		}

		[Column(Storage = "_RequiredPhase", DbType = "NVarChar(256)")]
		public string RequiredPhase
		{
			get
			{
				return this._RequiredPhase;
			}
			set
			{
				if (this._RequiredPhase != value)
				{
					this._RequiredPhase = value;
				}
			}
		}

		[Column(Storage = "_IsSeason2", DbType = "Bit NOT NULL")]
		public bool IsSeason2
		{
			get
			{
				return this._IsSeason2;
			}
			set
			{
				if (this._IsSeason2 != value)
				{
					this._IsSeason2 = value;
				}
			}
		}

		[Column(Storage = "_InitGameTime", DbType = "Int")]
		public int? InitGameTime
		{
			get
			{
				return this._InitGameTime;
			}
			set
			{
				if (this._InitGameTime != value)
				{
					this._InitGameTime = value;
				}
			}
		}

		[Column(Storage = "_SectorMoveGameTime", DbType = "Int")]
		public int? SectorMoveGameTime
		{
			get
			{
				return this._SectorMoveGameTime;
			}
			set
			{
				if (this._SectorMoveGameTime != value)
				{
					this._SectorMoveGameTime = value;
				}
			}
		}

		[Column(Storage = "_GatheringRockMax", DbType = "Int NOT NULL")]
		public int GatheringRockMax
		{
			get
			{
				return this._GatheringRockMax;
			}
			set
			{
				if (this._GatheringRockMax != value)
				{
					this._GatheringRockMax = value;
				}
			}
		}

		[Column(Storage = "_EnablePracticeMode", DbType = "NVarChar(256)")]
		public string EnablePracticeMode
		{
			get
			{
				return this._EnablePracticeMode;
			}
			set
			{
				if (this._EnablePracticeMode != value)
				{
					this._EnablePracticeMode = value;
				}
			}
		}

		[Column(Storage = "_IsHardcore", DbType = "Bit NOT NULL")]
		public bool IsHardcore
		{
			get
			{
				return this._IsHardcore;
			}
			set
			{
				if (this._IsHardcore != value)
				{
					this._IsHardcore = value;
				}
			}
		}

		[Column(Storage = "_EnableUserDSMode", DbType = "Bit NOT NULL")]
		public bool EnableUserDSMode
		{
			get
			{
				return this._EnableUserDSMode;
			}
			set
			{
				if (this._EnableUserDSMode != value)
				{
					this._EnableUserDSMode = value;
				}
			}
		}

		private string _ID;

		private string _Name;

		private string _Client;

		private string _QuestGrade;

		private string _Description;

		private int _Level;

		private int _AutoGiveLevel;

		private int _DropItemCountMax;

		private int _DropItemCountMin;

		private int _TimeLimit;

		private string _STAGE;

		private string _PropDropTableName;

		private string _HomeAfterArrival;

		private int _QuestSet;

		private string _QuestGroupID;

		private bool _FreeQuest;

		private bool _HuntingQuest;

		private int _ServiceCoin;

		private int _MaxMemberCount;

		private int? _MaxPlayCount;

		private string _EnableTodayQuest;

		private string _Season3TodayGroup;

		private bool _AutoReveal;

		private bool _DisableUserShip;

		private double _DurabilityRatio;

		private string _RequiredItem;

		private string _Feature;

		private int? _MinClearTime;

		private string _EnableDSServer;

		private string _EnableDSServer_Constraint;

		private int? _FatiguePointCount;

		private string _RequiredStoryLineID;

		private string _RequiredPhase;

		private bool _IsSeason2;

		private int? _InitGameTime;

		private int? _SectorMoveGameTime;

		private int _GatheringRockMax;

		private string _EnablePracticeMode;

		private bool _IsHardcore;

		private bool _EnableUserDSMode;
	}
}
