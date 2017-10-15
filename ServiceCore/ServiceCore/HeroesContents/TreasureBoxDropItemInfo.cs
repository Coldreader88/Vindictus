using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.TreasureBoxDropItemInfo")]
	public class TreasureBoxDropItemInfo
	{
		[Column(Storage = "_EvilCoreName", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string EvilCoreName
		{
			get
			{
				return this._EvilCoreName;
			}
			set
			{
				if (this._EvilCoreName != value)
				{
					this._EvilCoreName = value;
				}
			}
		}

		[Column(Storage = "_QuestID", DbType = "VarChar(50)")]
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

		[Column(Storage = "_ItemClassEx", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string ItemClassEx
		{
			get
			{
				return this._ItemClassEx;
			}
			set
			{
				if (this._ItemClassEx != value)
				{
					this._ItemClassEx = value;
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

		private string _EvilCoreName;

		private string _QuestID;

		private int _Difficulty;

		private string _ItemClassEx;

		private int _Weight;

		private bool _IsAbsolute;

		private string _Feature;
	}
}
