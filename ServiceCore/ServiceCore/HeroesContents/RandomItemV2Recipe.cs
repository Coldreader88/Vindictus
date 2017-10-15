using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.RandomItemV2Recipe")]
	public class RandomItemV2Recipe
	{
		[Column(Storage = "_RandomItemID", DbType = "NVarChar(1024) NOT NULL", CanBeNull = false)]
		public string RandomItemID
		{
			get
			{
				return this._RandomItemID;
			}
			set
			{
				if (this._RandomItemID != value)
				{
					this._RandomItemID = value;
				}
			}
		}

		[Column(Storage = "_ItemClass", DbType = "NVarChar(1024)")]
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

		[Column(Storage = "_Count", DbType = "Int NOT NULL")]
		public int Count
		{
			get
			{
				return this._Count;
			}
			set
			{
				if (this._Count != value)
				{
					this._Count = value;
				}
			}
		}

		[Column(Storage = "_RecipeType", DbType = "TinyInt NOT NULL")]
		public byte RecipeType
		{
			get
			{
				return this._RecipeType;
			}
			set
			{
				if (this._RecipeType != value)
				{
					this._RecipeType = value;
				}
			}
		}

		[Column(Storage = "_Probability", DbType = "Float NOT NULL")]
		public double Probability
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

		[Column(Storage = "_RareItemID", DbType = "Int")]
		public int? RareItemID
		{
			get
			{
				return this._RareItemID;
			}
			set
			{
				if (this._RareItemID != value)
				{
					this._RareItemID = value;
				}
			}
		}

		[Column(Storage = "_RareItemLimit", DbType = "Int")]
		public int? RareItemLimit
		{
			get
			{
				return this._RareItemLimit;
			}
			set
			{
				if (this._RareItemLimit != value)
				{
					this._RareItemLimit = value;
				}
			}
		}

		[Column(Storage = "_Cond_Character", DbType = "NVarChar(1024)")]
		public string Cond_Character
		{
			get
			{
				return this._Cond_Character;
			}
			set
			{
				if (this._Cond_Character != value)
				{
					this._Cond_Character = value;
				}
			}
		}

		[Column(Storage = "_Cond_Item", DbType = "NVarChar(1024)")]
		public string Cond_Item
		{
			get
			{
				return this._Cond_Item;
			}
			set
			{
				if (this._Cond_Item != value)
				{
					this._Cond_Item = value;
				}
			}
		}

		[Column(Storage = "_Cond_Feature", DbType = "NVarChar(1024)")]
		public string Cond_Feature
		{
			get
			{
				return this._Cond_Feature;
			}
			set
			{
				if (this._Cond_Feature != value)
				{
					this._Cond_Feature = value;
				}
			}
		}

		[Column(Storage = "_Color1", DbType = "Int NOT NULL")]
		public int Color1
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

		[Column(Storage = "_Color2", DbType = "Int NOT NULL")]
		public int Color2
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

		[Column(Storage = "_Color3", DbType = "Int NOT NULL")]
		public int Color3
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

		[Column(Storage = "_CharacterBind", DbType = "Bit NOT NULL")]
		public bool CharacterBind
		{
			get
			{
				return this._CharacterBind;
			}
			set
			{
				if (this._CharacterBind != value)
				{
					this._CharacterBind = value;
				}
			}
		}

		[Column(Storage = "_ExpireBySec", DbType = "Int")]
		public int? ExpireBySec
		{
			get
			{
				return this._ExpireBySec;
			}
			set
			{
				if (this._ExpireBySec != value)
				{
					this._ExpireBySec = value;
				}
			}
		}

		[Column(Storage = "_Announce", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
		public string Announce
		{
			get
			{
				return this._Announce;
			}
			set
			{
				if (this._Announce != value)
				{
					this._Announce = value;
				}
			}
		}

		[Column(Storage = "_AnnounceTelepathyEnable", DbType = "NVarChar(50)")]
		public string AnnounceTelepathyEnable
		{
			get
			{
				return this._AnnounceTelepathyEnable;
			}
			set
			{
				if (this._AnnounceTelepathyEnable != value)
				{
					this._AnnounceTelepathyEnable = value;
				}
			}
		}

		[Column(Storage = "_AnnounceUIEnable", DbType = "NVarChar(50)")]
		public string AnnounceUIEnable
		{
			get
			{
				return this._AnnounceUIEnable;
			}
			set
			{
				if (this._AnnounceUIEnable != value)
				{
					this._AnnounceUIEnable = value;
				}
			}
		}

		[Column(Storage = "_Message", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
		public string Message
		{
			get
			{
				return this._Message;
			}
			set
			{
				if (this._Message != value)
				{
					this._Message = value;
				}
			}
		}

		private string _RandomItemID;

		private string _ItemClass;

		private int _Count;

		private byte _RecipeType;

		private double _Probability;

		private int? _RareItemID;

		private int? _RareItemLimit;

		private string _Cond_Character;

		private string _Cond_Item;

		private string _Cond_Feature;

		private int _Color1;

		private int _Color2;

		private int _Color3;

		private bool _CharacterBind;

		private int? _ExpireBySec;

		private string _Announce;

		private string _AnnounceTelepathyEnable;

		private string _AnnounceUIEnable;

		private string _Message;
	}
}
