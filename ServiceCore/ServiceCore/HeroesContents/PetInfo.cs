using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.PetInfo")]
	public class PetInfo
	{
		[Column(Storage = "_PetID", DbType = "Int NOT NULL")]
		public int PetID
		{
			get
			{
				return this._PetID;
			}
			set
			{
				if (this._PetID != value)
				{
					this._PetID = value;
				}
			}
		}

		[Column(Storage = "_DefaultLevel", DbType = "Int NOT NULL")]
		public int DefaultLevel
		{
			get
			{
				return this._DefaultLevel;
			}
			set
			{
				if (this._DefaultLevel != value)
				{
					this._DefaultLevel = value;
				}
			}
		}

		[Column(Storage = "_StatBalanceType", DbType = "Int NOT NULL")]
		public int StatBalanceType
		{
			get
			{
				return this._StatBalanceType;
			}
			set
			{
				if (this._StatBalanceType != value)
				{
					this._StatBalanceType = value;
				}
			}
		}

		[Column(Storage = "_MaxActiveTime", DbType = "Int NOT NULL")]
		public int MaxActiveTime
		{
			get
			{
				return this._MaxActiveTime;
			}
			set
			{
				if (this._MaxActiveTime != value)
				{
					this._MaxActiveTime = value;
				}
			}
		}

		[Column(Storage = "_DeactiveTime", DbType = "Int NOT NULL")]
		public int DeactiveTime
		{
			get
			{
				return this._DeactiveTime;
			}
			set
			{
				if (this._DeactiveTime != value)
				{
					this._DeactiveTime = value;
				}
			}
		}

		[Column(Storage = "_ItemClass", DbType = "NVarChar(256) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_MaxHunger", DbType = "Int NOT NULL")]
		public int MaxHunger
		{
			get
			{
				return this._MaxHunger;
			}
			set
			{
				if (this._MaxHunger != value)
				{
					this._MaxHunger = value;
				}
			}
		}

		[Column(Storage = "_HungerTime", DbType = "Int NOT NULL")]
		public int HungerTime
		{
			get
			{
				return this._HungerTime;
			}
			set
			{
				if (this._HungerTime != value)
				{
					this._HungerTime = value;
				}
			}
		}

		[Column(Storage = "_HungerDecrease", DbType = "Int NOT NULL")]
		public int HungerDecrease
		{
			get
			{
				return this._HungerDecrease;
			}
			set
			{
				if (this._HungerDecrease != value)
				{
					this._HungerDecrease = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "NVarChar(32)")]
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

		private int _PetID;

		private int _DefaultLevel;

		private int _StatBalanceType;

		private int _MaxActiveTime;

		private int _DeactiveTime;

		private string _ItemClass;

		private int _MaxHunger;

		private int _HungerTime;

		private int _HungerDecrease;

		private string _Feature;
	}
}
