using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.EventDropItemInfo")]
	public class EventDropItemInfo
	{
		public bool HasTimeCondition()
		{
			return this.StartTime != null && this.Period != null;
		}

		public bool IsOnTime(DateTime time)
		{
			return !this.HasTimeCondition() || (this.StartTime.Value <= time && this.StartTime.Value + new TimeSpan(0, this.Period.Value, 0) > time);
		}

		[Column(Storage = "_Identifier", DbType = "Int NOT NULL")]
		public int Identifier
		{
			get
			{
				return this._Identifier;
			}
			set
			{
				if (this._Identifier != value)
				{
					this._Identifier = value;
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

		[Column(Storage = "_Condition", DbType = "NVarChar(50)")]
		public string Condition
		{
			get
			{
				return this._Condition;
			}
			set
			{
				if (this._Condition != value)
				{
					this._Condition = value;
				}
			}
		}

		[Column(Storage = "_IsGarbage", DbType = "Int NOT NULL")]
		public int IsGarbage
		{
			get
			{
				return this._IsGarbage;
			}
			set
			{
				if (this._IsGarbage != value)
				{
					this._IsGarbage = value;
				}
			}
		}

		[Column(Storage = "_ItemClass", DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_MonsterID", DbType = "Int")]
		public int? MonsterID
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

		[Column(Storage = "_Probability", DbType = "Int NOT NULL")]
		public int Probability
		{
			get
			{
				return this._Probability;
			}
			set
			{
				if (this._Probability != value)
				{
					this._Probability = value;
				}
			}
		}

		[Column(Storage = "_StartTime", DbType = "DateTime2")]
		public DateTime? StartTime
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

		[Column(Storage = "_Period", DbType = "Int")]
		public int? Period
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

		private int _Identifier;

		private string _Feature;

		private string _Condition;

		private int _IsGarbage;

		private string _ItemClass;

		private int? _MonsterID;

		private int _Probability;

		private DateTime? _StartTime;

		private int? _Period;
	}
}
