using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.AttendanceReward")]
	public class AttendanceReward
	{
		[Column(Name = "[INDEX]", Storage = "_INDEX", DbType = "Int NOT NULL")]
		public int INDEX
		{
			get
			{
				return this._INDEX;
			}
			set
			{
				if (this._INDEX != value)
				{
					this._INDEX = value;
				}
			}
		}

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

		[Column(Storage = "_ConditionID", DbType = "NVarChar(64)")]
		public string ConditionID
		{
			get
			{
				return this._ConditionID;
			}
			set
			{
				if (this._ConditionID != value)
				{
					this._ConditionID = value;
				}
			}
		}

		[Column(Storage = "_CharacterType", DbType = "Int")]
		public int? CharacterType
		{
			get
			{
				return this._CharacterType;
			}
			set
			{
				if (this._CharacterType != value)
				{
					this._CharacterType = value;
				}
			}
		}

		[Column(Storage = "_RewardItemClass", DbType = "NVarChar(250) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_Count", DbType = "Int")]
		public int? Count
		{
			get
			{
				return this._Count;
			}
			set
			{
				if (this._Count != value)
				{
					this._Count = value;
				}
			}
		}

		[Column(Storage = "_Color1", DbType = "Int")]
		public int? Color1
		{
			get
			{
				return this._Color1;
			}
			set
			{
				if (this._Color1 != value)
				{
					this._Color1 = value;
				}
			}
		}

		[Column(Storage = "_Color2", DbType = "Int")]
		public int? Color2
		{
			get
			{
				return this._Color2;
			}
			set
			{
				if (this._Color2 != value)
				{
					this._Color2 = value;
				}
			}
		}

		[Column(Storage = "_Color3", DbType = "Int")]
		public int? Color3
		{
			get
			{
				return this._Color3;
			}
			set
			{
				if (this._Color3 != value)
				{
					this._Color3 = value;
				}
			}
		}

		[Column(Storage = "_NotifyMessage", DbType = "TinyInt")]
		public byte? NotifyMessage
		{
			get
			{
				return this._NotifyMessage;
			}
			set
			{
				if (this._NotifyMessage != value)
				{
					this._NotifyMessage = value;
				}
			}
		}

		private int _INDEX;

		private int _AttendanceEventVer;

		private string _RewardID;

		private string _ConditionID;

		private int? _CharacterType;

		private string _RewardItemClass;

		private int? _Count;

		private int? _Color1;

		private int? _Color2;

		private int? _Color3;

		private byte? _NotifyMessage;
	}
}
