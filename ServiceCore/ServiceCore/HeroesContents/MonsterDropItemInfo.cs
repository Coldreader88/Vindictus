using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.MonsterDropItemInfo")]
	public class MonsterDropItemInfo
	{
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

		[Column(Storage = "_QuestID", DbType = "VarChar(100)")]
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

		[Column(Storage = "_ItemClassEX", DbType = "NVarChar(1024) NOT NULL", CanBeNull = false)]
		public string ItemClassEX
		{
			get
			{
				return this._ItemClassEX;
			}
			set
			{
				if (this._ItemClassEX != value)
				{
					this._ItemClassEX = value;
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

		[Column(Storage = "_IsAbsolute", DbType = "Bit NOT NULL")]
		public bool IsAbsolute
		{
			get
			{
				return this._IsAbsolute;
			}
			set
			{
				if (this._IsAbsolute != value)
				{
					this._IsAbsolute = value;
				}
			}
		}

		[Column(Storage = "_IsRequiredClass", DbType = "Bit NOT NULL")]
		public bool IsRequiredClass
		{
			get
			{
				return this._IsRequiredClass;
			}
			set
			{
				if (this._IsRequiredClass != value)
				{
					this._IsRequiredClass = value;
				}
			}
		}

		[Column(Storage = "_IsUniqueDrop", DbType = "Bit NOT NULL")]
		public bool IsUniqueDrop
		{
			get
			{
				return this._IsUniqueDrop;
			}
			set
			{
				if (this._IsUniqueDrop != value)
				{
					this._IsUniqueDrop = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "VarChar(100)")]
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

		private int _MonsterID;

		private string _QuestID;

		private int _Difficulty;

		private string _ItemClassEX;

		private int _Weight;

		private bool _IsAbsolute;

		private bool _IsRequiredClass;

		private bool _IsUniqueDrop;

		private string _Feature;
	}
}
