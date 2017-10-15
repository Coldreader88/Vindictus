using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.WeakpointRewardInfo")]
	public class WeakpointRewardInfo
	{
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

		[Column(Storage = "_MonsterID", DbType = "Int NOT NULL")]
		public int MonsterID
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

		[Column(Storage = "_PartIndex", DbType = "Int")]
		public int? PartIndex
		{
			get
			{
				return this._PartIndex;
			}
			set
			{
				if (this._PartIndex != value)
				{
					this._PartIndex = value;
				}
			}
		}

		[Column(Storage = "_Difficulty", DbType = "Int NOT NULL")]
		public int Difficulty
		{
			get
			{
				return this._Difficulty;
			}
			set
			{
				if (this._Difficulty != value)
				{
					this._Difficulty = value;
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

		private int _Identifier;

		private int _IsGarbage;

		private string _ItemClass;

		private int _MonsterID;

		private int _Probability;

		private int? _PartIndex;

		private int _Difficulty;

		private string _Feature;
	}
}
