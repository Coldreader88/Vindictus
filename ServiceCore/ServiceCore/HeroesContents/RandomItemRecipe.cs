using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.RandomItemRecipe")]
	public class RandomItemRecipe
	{
		[Column(Storage = "_RandomItemID", DbType = "Int NOT NULL")]
		public int RandomItemID
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

		[Column(Storage = "_RecipeClassName", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
		public string RecipeClassName
		{
			get
			{
				return this._RecipeClassName;
			}
			set
			{
				if (this._RecipeClassName != value)
				{
					this._RecipeClassName = value;
				}
			}
		}

		[Column(Storage = "_CharacterType", DbType = "Int")]
		public int? CharacterType
		{
			get
			{
				return this._CharacterType;
			}
			set
			{
				if (this._CharacterType != value)
				{
					this._CharacterType = value;
				}
			}
		}

		[Column(Storage = "_ItemClass", DbType = "NVarChar(1024) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_Number", DbType = "Int NOT NULL")]
		public int Number
		{
			get
			{
				return this._Number;
			}
			set
			{
				if (this._Number != value)
				{
					this._Number = value;
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

		[Column(Storage = "_NotifyMessage", DbType = "NVarChar(256)")]
		public string NotifyMessage
		{
			get
			{
				return this._NotifyMessage;
			}
			set
			{
				if (this._NotifyMessage != value)
				{
					this._NotifyMessage = value;
				}
			}
		}

		[Column(Storage = "_IsVariable", DbType = "Int NOT NULL")]
		public int IsVariable
		{
			get
			{
				return this._IsVariable;
			}
			set
			{
				if (this._IsVariable != value)
				{
					this._IsVariable = value;
				}
			}
		}

		[Column(Storage = "_HaveItem", DbType = "Float NOT NULL")]
		public double HaveItem
		{
			get
			{
				return this._HaveItem;
			}
			set
			{
				if (this._HaveItem != value)
				{
					this._HaveItem = value;
				}
			}
		}

		[Column(Storage = "_DontHaveItem", DbType = "Float NOT NULL")]
		public double DontHaveItem
		{
			get
			{
				return this._DontHaveItem;
			}
			set
			{
				if (this._DontHaveItem != value)
				{
					this._DontHaveItem = value;
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

		private int _RandomItemID;

		private string _RecipeClassName;

		private int? _CharacterType;

		private string _ItemClass;

		private int _Number;

		private double _Probability;

		private int? _Color1;

		private int? _Color2;

		private int? _Color3;

		private string _NotifyMessage;

		private int _IsVariable;

		private double _HaveItem;

		private double _DontHaveItem;

		private string _Feature;
	}
}
