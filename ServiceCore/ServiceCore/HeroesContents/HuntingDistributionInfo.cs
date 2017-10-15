using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.HuntingDistributionInfo")]
	public class HuntingDistributionInfo
	{
		[Column(Storage = "_rowID", DbType = "Int NOT NULL")]
		public int rowID
		{
			get
			{
				return this._rowID;
			}
			set
			{
				if (this._rowID != value)
				{
					this._rowID = value;
				}
			}
		}

		[Column(Storage = "_QuestID", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_MonsterID", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_SizeMin", DbType = "Int NOT NULL")]
		public int SizeMin
		{
			get
			{
				return this._SizeMin;
			}
			set
			{
				if (this._SizeMin != value)
				{
					this._SizeMin = value;
				}
			}
		}

		[Column(Storage = "_SizeMax", DbType = "Int NOT NULL")]
		public int SizeMax
		{
			get
			{
				return this._SizeMax;
			}
			set
			{
				if (this._SizeMax != value)
				{
					this._SizeMax = value;
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

		[Column(Storage = "_IsAutoFish", DbType = "TinyInt NOT NULL")]
		public byte IsAutoFish
		{
			get
			{
				return this._IsAutoFish;
			}
			set
			{
				if (this._IsAutoFish != value)
				{
					this._IsAutoFish = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "VarChar(50)")]
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

		private int _rowID;

		private string _QuestID;

		private string _MonsterID;

		private int _SizeMin;

		private int _SizeMax;

		private int _Probability;

		private byte _IsAutoFish;

		private string _Feature;
	}
}
