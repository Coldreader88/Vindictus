using System;
using System.Data.Linq.Mapping;
using Utility;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.QuestGoalInfo")]
	public class QuestGoalInfo
	{
		public int GetAP(byte clearCount, byte regenedAP, DateTime lastClearTime)
		{
			return this.GetAP(DateTime.Now, clearCount, regenedAP, lastClearTime);
		}

		public int GetAP(DateTime now, byte clearCount, byte regenedAP, DateTime lastClearTime)
		{
			int num = this.MaxAP - this.StepAP * (int)clearCount;
			if (num < this.MinAP)
			{
				num = this.MinAP;
			}
			int num2 = 0;
			if (this.APRegenTime > 0)
			{
				num2 = Math.Max((int)regenedAP + (int)((now - lastClearTime).TotalMinutes / (double)this.APRegenTime), 0);
				if (num + num2 > this.APRegenMax)
				{
					num2 = Math.Max(this.APRegenMax - num, 0);
				}
			}
			Log<QuestGoalInfo>.Logger.WarnFormat("QuestGoalInfo.GetAP : {0} + {1}", num, num2);
			return num + num2;
		}

		public void AddClearCount(DateTime goalClearTime, ref byte clearCount, ref byte regenedAP, ref DateTime lastClearTime)
		{
			TimeSpan t = new TimeSpan(0L);
			if (this.APRegenTime > 0 && goalClearTime > lastClearTime)
			{
				int num = Math.Max((int)((goalClearTime - lastClearTime).TotalMinutes / (double)this.APRegenTime), 0);
				t = goalClearTime - lastClearTime - TimeSpan.FromMinutes((double)(num * this.APRegenTime));
				if (t.Ticks < 0L)
				{
					t = new TimeSpan(0L);
				}
				if (num + (int)regenedAP > 255)
				{
					regenedAP = byte.MaxValue;
				}
				else
				{
					regenedAP += (byte)num;
				}
			}
			lastClearTime = goalClearTime - t;
			int num2 = this.MaxAP - this.StepAP * (int)clearCount;
			if (num2 <= this.MinAP)
			{
				num2 = this.MinAP;
			}
			if (this.APRegenMax < (int)regenedAP + num2)
			{
				if (num2 > this.APRegenMax)
				{
					regenedAP = 0;
				}
				else
				{
					regenedAP = (byte)(this.APRegenMax - num2);
				}
			}
			if (num2 <= this.MinAP)
			{
				regenedAP = (byte)(Math.Max((int)regenedAP, this.StepAP) - this.StepAP);
				return;
			}
			clearCount += 1;
		}

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

		[Column(Storage = "_Description", DbType = "Text NOT NULL", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
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

		[Column(Storage = "_Relation", DbType = "Int NOT NULL")]
		public int Relation
		{
			get
			{
				return this._Relation;
			}
			set
			{
				if (this._Relation != value)
				{
					this._Relation = value;
				}
			}
		}

		[Column(Storage = "_Weight", DbType = "Int NOT NULL")]
		public int Weight
		{
			get
			{
				return this._Weight;
			}
			set
			{
				if (this._Weight != value)
				{
					this._Weight = value;
				}
			}
		}

		[Column(Storage = "_BossImg", DbType = "NVarChar(50)")]
		public string BossImg
		{
			get
			{
				return this._BossImg;
			}
			set
			{
				if (this._BossImg != value)
				{
					this._BossImg = value;
				}
			}
		}

		[Column(Storage = "_BossName", DbType = "NVarChar(50)")]
		public string BossName
		{
			get
			{
				return this._BossName;
			}
			set
			{
				if (this._BossName != value)
				{
					this._BossName = value;
				}
			}
		}

		[Column(Storage = "_GameSetting", DbType = "NVarChar(50)")]
		public string GameSetting
		{
			get
			{
				return this._GameSetting;
			}
			set
			{
				if (this._GameSetting != value)
				{
					this._GameSetting = value;
				}
			}
		}

		[Column(Storage = "_GameSettingArg", DbType = "Int")]
		public int? GameSettingArg
		{
			get
			{
				return this._GameSettingArg;
			}
			set
			{
				if (this._GameSettingArg != value)
				{
					this._GameSettingArg = value;
				}
			}
		}

		[Column(Storage = "_Target", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
		public string Target
		{
			get
			{
				return this._Target;
			}
			set
			{
				if (this._Target != value)
				{
					this._Target = value;
				}
			}
		}

		[Column(Storage = "_TargetCount", DbType = "Int")]
		public int? TargetCount
		{
			get
			{
				return this._TargetCount;
			}
			set
			{
				if (this._TargetCount != value)
				{
					this._TargetCount = value;
				}
			}
		}

		[Column(Storage = "_IsPositive", DbType = "Int NOT NULL")]
		public int IsPositive
		{
			get
			{
				return this._IsPositive;
			}
			set
			{
				if (this._IsPositive != value)
				{
					this._IsPositive = value;
				}
			}
		}

		[Column(Storage = "_IsParty", DbType = "Int NOT NULL")]
		public int IsParty
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

		[Column(Storage = "_Gold", DbType = "Int NOT NULL")]
		public int Gold
		{
			get
			{
				return this._Gold;
			}
			set
			{
				if (this._Gold != value)
				{
					this._Gold = value;
				}
			}
		}

		[Column(Storage = "_XP", DbType = "Int NOT NULL")]
		public int XP
		{
			get
			{
				return this._XP;
			}
			set
			{
				if (this._XP != value)
				{
					this._XP = value;
				}
			}
		}

		[Column(Storage = "_MaxAP", DbType = "Int NOT NULL")]
		public int MaxAP
		{
			get
			{
				return this._MaxAP;
			}
			set
			{
				if (this._MaxAP != value)
				{
					this._MaxAP = value;
				}
			}
		}

		[Column(Storage = "_MinAP", DbType = "Int NOT NULL")]
		public int MinAP
		{
			get
			{
				return this._MinAP;
			}
			set
			{
				if (this._MinAP != value)
				{
					this._MinAP = value;
				}
			}
		}

		[Column(Storage = "_StepAP", DbType = "Int NOT NULL")]
		public int StepAP
		{
			get
			{
				return this._StepAP;
			}
			set
			{
				if (this._StepAP != value)
				{
					this._StepAP = value;
				}
			}
		}

		[Column(Storage = "_APRegenTime", DbType = "Int NOT NULL")]
		public int APRegenTime
		{
			get
			{
				return this._APRegenTime;
			}
			set
			{
				if (this._APRegenTime != value)
				{
					this._APRegenTime = value;
				}
			}
		}

		[Column(Storage = "_APRegenMax", DbType = "Int NOT NULL")]
		public int APRegenMax
		{
			get
			{
				return this._APRegenMax;
			}
			set
			{
				if (this._APRegenMax != value)
				{
					this._APRegenMax = value;
				}
			}
		}

		[Column(Storage = "_Item", DbType = "Text NOT NULL", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
		public string Item
		{
			get
			{
				return this._Item;
			}
			set
			{
				if (this._Item != value)
				{
					this._Item = value;
				}
			}
		}

		[Column(Storage = "_ItemNum", DbType = "Int NOT NULL")]
		public int ItemNum
		{
			get
			{
				return this._ItemNum;
			}
			set
			{
				if (this._ItemNum != value)
				{
					this._ItemNum = value;
				}
			}
		}

		[Column(Storage = "_ItemMultiplierFeature", DbType = "NVarChar(256) NOT NULL", CanBeNull = false)]
		public string ItemMultiplierFeature
		{
			get
			{
				return this._ItemMultiplierFeature;
			}
			set
			{
				if (this._ItemMultiplierFeature != value)
				{
					this._ItemMultiplierFeature = value;
				}
			}
		}

		[Column(Storage = "_ItemMultiplie", DbType = "Int NOT NULL")]
		public int ItemMultiplier
		{
			get
			{
				return this._ItemMultiplie;
			}
			set
			{
				if (this._ItemMultiplie != value)
				{
					this._ItemMultiplie = value;
				}
			}
		}

		private int _ID;

		private string _QuestID;

		private string _Description;

		private int _Relation;

		private int _Weight;

		private string _BossImg;

		private string _BossName;

		private string _GameSetting;

		private int? _GameSettingArg;

		private string _Target;

		private int? _TargetCount;

		private int _IsPositive;

		private int _IsParty;

		private int _Gold;

		private int _XP;

		private int _MaxAP;

		private int _MinAP;

		private int _StepAP;

		private int _APRegenTime;

		private int _APRegenMax;

		private string _Item;

		private int _ItemNum;

		private string _ItemMultiplierFeature;

		private int _ItemMultiplie;
	}
}
