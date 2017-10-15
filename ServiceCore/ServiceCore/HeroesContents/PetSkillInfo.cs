using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.PetSkillInfo")]
	public class PetSkillInfo
	{
		[Column(Storage = "_SkillID", DbType = "Int NOT NULL")]
		public int SkillID
		{
			get
			{
				return this._SkillID;
			}
			set
			{
				if (this._SkillID != value)
				{
					this._SkillID = value;
				}
			}
		}

		[Column(Storage = "_SkillKind", DbType = "Int NOT NULL")]
		public int SkillKind
		{
			get
			{
				return this._SkillKind;
			}
			set
			{
				if (this._SkillKind != value)
				{
					this._SkillKind = value;
				}
			}
		}

		[Column(Storage = "_SkillType", DbType = "NVarChar(256) NOT NULL", CanBeNull = false)]
		public string SkillType
		{
			get
			{
				return this._SkillType;
			}
			set
			{
				if (this._SkillType != value)
				{
					this._SkillType = value;
				}
			}
		}

		[Column(Storage = "_SkillLevel", DbType = "Int")]
		public int? SkillLevel
		{
			get
			{
				return this._SkillLevel;
			}
			set
			{
				if (this._SkillLevel != value)
				{
					this._SkillLevel = value;
				}
			}
		}

		[Column(Storage = "_SkillTarget", DbType = "Int")]
		public int? SkillTarget
		{
			get
			{
				return this._SkillTarget;
			}
			set
			{
				if (this._SkillTarget != value)
				{
					this._SkillTarget = value;
				}
			}
		}

		[Column(Storage = "_TargetRange", DbType = "Int")]
		public int? TargetRange
		{
			get
			{
				return this._TargetRange;
			}
			set
			{
				if (this._TargetRange != value)
				{
					this._TargetRange = value;
				}
			}
		}

		[Column(Storage = "_ActionType1", DbType = "NVarChar(256)")]
		public string ActionType1
		{
			get
			{
				return this._ActionType1;
			}
			set
			{
				if (this._ActionType1 != value)
				{
					this._ActionType1 = value;
				}
			}
		}

		[Column(Storage = "_Type1Arg1", DbType = "NVarChar(256)")]
		public string Type1Arg1
		{
			get
			{
				return this._Type1Arg1;
			}
			set
			{
				if (this._Type1Arg1 != value)
				{
					this._Type1Arg1 = value;
				}
			}
		}

		[Column(Storage = "_Type1Arg2", DbType = "NVarChar(256)")]
		public string Type1Arg2
		{
			get
			{
				return this._Type1Arg2;
			}
			set
			{
				if (this._Type1Arg2 != value)
				{
					this._Type1Arg2 = value;
				}
			}
		}

		[Column(Storage = "_Type1Arg3", DbType = "NVarChar(256)")]
		public string Type1Arg3
		{
			get
			{
				return this._Type1Arg3;
			}
			set
			{
				if (this._Type1Arg3 != value)
				{
					this._Type1Arg3 = value;
				}
			}
		}

		[Column(Storage = "_ActionType2", DbType = "NVarChar(256)")]
		public string ActionType2
		{
			get
			{
				return this._ActionType2;
			}
			set
			{
				if (this._ActionType2 != value)
				{
					this._ActionType2 = value;
				}
			}
		}

		[Column(Storage = "_Type2Arg1", DbType = "NVarChar(256)")]
		public string Type2Arg1
		{
			get
			{
				return this._Type2Arg1;
			}
			set
			{
				if (this._Type2Arg1 != value)
				{
					this._Type2Arg1 = value;
				}
			}
		}

		[Column(Storage = "_Type2Arg2", DbType = "NVarChar(256)")]
		public string Type2Arg2
		{
			get
			{
				return this._Type2Arg2;
			}
			set
			{
				if (this._Type2Arg2 != value)
				{
					this._Type2Arg2 = value;
				}
			}
		}

		[Column(Storage = "_Type2Arg3", DbType = "NVarChar(256)")]
		public string Type2Arg3
		{
			get
			{
				return this._Type2Arg3;
			}
			set
			{
				if (this._Type2Arg3 != value)
				{
					this._Type2Arg3 = value;
				}
			}
		}

		[Column(Storage = "_CoolTimeType", DbType = "Int")]
		public int? CoolTimeType
		{
			get
			{
				return this._CoolTimeType;
			}
			set
			{
				if (this._CoolTimeType != value)
				{
					this._CoolTimeType = value;
				}
			}
		}

		[Column(Storage = "_CoolTime", DbType = "Int")]
		public int? CoolTime
		{
			get
			{
				return this._CoolTime;
			}
			set
			{
				if (this._CoolTime != value)
				{
					this._CoolTime = value;
				}
			}
		}

		[Column(Storage = "_SequenceType", DbType = "Int")]
		public int? SequenceType
		{
			get
			{
				return this._SequenceType;
			}
			set
			{
				if (this._SequenceType != value)
				{
					this._SequenceType = value;
				}
			}
		}

		[Column(Storage = "_StartEFX", DbType = "NVarChar(256)")]
		public string StartEFX
		{
			get
			{
				return this._StartEFX;
			}
			set
			{
				if (this._StartEFX != value)
				{
					this._StartEFX = value;
				}
			}
		}

		[Column(Storage = "_StartEFXShot", DbType = "NVarChar(256)")]
		public string StartEFXShot
		{
			get
			{
				return this._StartEFXShot;
			}
			set
			{
				if (this._StartEFXShot != value)
				{
					this._StartEFXShot = value;
				}
			}
		}

		[Column(Storage = "_StartSound", DbType = "NVarChar(256)")]
		public string StartSound
		{
			get
			{
				return this._StartSound;
			}
			set
			{
				if (this._StartSound != value)
				{
					this._StartSound = value;
				}
			}
		}

		[Column(Storage = "_IconType", DbType = "NVarChar(256) NOT NULL", CanBeNull = false)]
		public string IconType
		{
			get
			{
				return this._IconType;
			}
			set
			{
				if (this._IconType != value)
				{
					this._IconType = value;
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

		private int _SkillID;

		private int _SkillKind;

		private string _SkillType;

		private int? _SkillLevel;

		private int? _SkillTarget;

		private int? _TargetRange;

		private string _ActionType1;

		private string _Type1Arg1;

		private string _Type1Arg2;

		private string _Type1Arg3;

		private string _ActionType2;

		private string _Type2Arg1;

		private string _Type2Arg2;

		private string _Type2Arg3;

		private int? _CoolTimeType;

		private int? _CoolTime;

		private int? _SequenceType;

		private string _StartEFX;

		private string _StartEFXShot;

		private string _StartSound;

		private string _IconType;

		private string _Feature;
	}
}
