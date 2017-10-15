using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.HuntingAutoProbabilityInfo")]
	public class HuntingAutoProbabilityInfo
	{
		[Column(Storage = "_QuestID", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_MonsterID", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
		public string MonsterID
		{
			get
			{
				return this._MonsterID;
			}
			set
			{
				if (this._MonsterID != value)
				{
					this._MonsterID = value;
				}
			}
		}

		[Column(Storage = "_ItemClass", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
		public string ItemClass
		{
			get
			{
				return this._ItemClass;
			}
			set
			{
				if (this._ItemClass != value)
				{
					this._ItemClass = value;
				}
			}
		}

		[Column(Storage = "_ProbabilityHit", DbType = "Int NOT NULL")]
		public int ProbabilityHit
		{
			get
			{
				return this._ProbabilityHit;
			}
			set
			{
				if (this._ProbabilityHit != value)
				{
					this._ProbabilityHit = value;
				}
			}
		}

		[Column(Storage = "_ProbabilityMiss", DbType = "Int NOT NULL")]
		public int ProbabilityMiss
		{
			get
			{
				return this._ProbabilityMiss;
			}
			set
			{
				if (this._ProbabilityMiss != value)
				{
					this._ProbabilityMiss = value;
				}
			}
		}

		[Column(Storage = "_ProbabilityAuto", DbType = "Int NOT NULL")]
		public int ProbabilityAuto
		{
			get
			{
				return this._ProbabilityAuto;
			}
			set
			{
				if (this._ProbabilityAuto != value)
				{
					this._ProbabilityAuto = value;
				}
			}
		}

		private string _QuestID;

		private string _MonsterID;

		private string _ItemClass;

		private int _ProbabilityHit;

		private int _ProbabilityMiss;

		private int _ProbabilityAuto;
	}
}
