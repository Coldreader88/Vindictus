using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.TodayMissionInfo")]
	public class TodayMissionInfo
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

		[Column(Storage = "_RewardItem", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string RewardItem
		{
			get
			{
				return this._RewardItem;
			}
			set
			{
				if (this._RewardItem != value)
				{
					this._RewardItem = value;
				}
			}
		}

		[Column(Storage = "_RewardItemCount", DbType = "Int NOT NULL")]
		public int RewardItemCount
		{
			get
			{
				return this._RewardItemCount;
			}
			set
			{
				if (this._RewardItemCount != value)
				{
					this._RewardItemCount = value;
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

		private int _ID;

		private int _GoalCount;

		private string _RewardItem;

		private int _RewardItemCount;

		private int? _Color1;

		private int? _Color2;

		private int? _Color3;

		private string _Feature;
	}
}
