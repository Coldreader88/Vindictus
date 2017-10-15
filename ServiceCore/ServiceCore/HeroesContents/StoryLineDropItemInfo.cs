using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.StoryLineDropItemInfo")]
	public class StoryLineDropItemInfo
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

		[Column(Storage = "_StoryLineID", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
		public string StoryLineID
		{
			get
			{
				return this._StoryLineID;
			}
			set
			{
				if (this._StoryLineID != value)
				{
					this._StoryLineID = value;
				}
			}
		}

		[Column(Storage = "_Phase", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
		public string Phase
		{
			get
			{
				return this._Phase;
			}
			set
			{
				if (this._Phase != value)
				{
					this._Phase = value;
				}
			}
		}

		[Column(Storage = "_QuestID", DbType = "NVarChar(128)")]
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

		[Column(Storage = "_ObjectType", DbType = "NVarChar(126) NOT NULL", CanBeNull = false)]
		public string ObjectType
		{
			get
			{
				return this._ObjectType;
			}
			set
			{
				if (this._ObjectType != value)
				{
					this._ObjectType = value;
				}
			}
		}

		[Column(Storage = "_ObjectRegex", DbType = "NVarChar(1024) NOT NULL", CanBeNull = false)]
		public string ObjectRegex
		{
			get
			{
				return this._ObjectRegex;
			}
			set
			{
				if (this._ObjectRegex != value)
				{
					this._ObjectRegex = value;
				}
			}
		}

		[Column(Storage = "_ItemClass", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_Amount", DbType = "Int NOT NULL")]
		public int Amount
		{
			get
			{
				return this._Amount;
			}
			set
			{
				if (this._Amount != value)
				{
					this._Amount = value;
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

		[Column(Storage = "_MaxTry", DbType = "Int NOT NULL")]
		public int MaxTry
		{
			get
			{
				return this._MaxTry;
			}
			set
			{
				if (this._MaxTry != value)
				{
					this._MaxTry = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "NVarChar(128)")]
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

		private string _StoryLineID;

		private string _Phase;

		private string _QuestID;

		private string _ObjectType;

		private string _ObjectRegex;

		private string _ItemClass;

		private int _Amount;

		private int _Probability;

		private int _MaxTry;

		private string _Feature;
	}
}
