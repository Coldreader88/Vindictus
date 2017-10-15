using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.DormantReward")]
	public class DormantReward
	{
		[Column(Storage = "_RewardItemClass", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_Num", DbType = "Int NOT NULL")]
		public int Num
		{
			get
			{
				return this._Num;
			}
			set
			{
				if (this._Num != value)
				{
					this._Num = value;
				}
			}
		}

		[Column(Storage = "_MinLevel", DbType = "Int NOT NULL")]
		public int MinLevel
		{
			get
			{
				return this._MinLevel;
			}
			set
			{
				if (this._MinLevel != value)
				{
					this._MinLevel = value;
				}
			}
		}

		[Column(Storage = "_MaxLevel", DbType = "Int NOT NULL")]
		public int MaxLevel
		{
			get
			{
				return this._MaxLevel;
			}
			set
			{
				if (this._MaxLevel != value)
				{
					this._MaxLevel = value;
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

		private string _RewardItemClass;

		private int _Num;

		private int _MinLevel;

		private int _MaxLevel;

		private string _Feature;
	}
}
