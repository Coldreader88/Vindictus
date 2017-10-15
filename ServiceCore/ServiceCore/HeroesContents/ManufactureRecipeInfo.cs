using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.ManufactureRecipeInfo")]
	public class ManufactureRecipeInfo
	{
		[Column(Storage = "_ManufactureID", DbType = "NVarChar(16) NOT NULL", CanBeNull = false)]
		public string ManufactureID
		{
			get
			{
				return this._ManufactureID;
			}
			set
			{
				if (this._ManufactureID != value)
				{
					this._ManufactureID = value;
				}
			}
		}

		[Column(Storage = "_RecipeID", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
		public string RecipeID
		{
			get
			{
				return this._RecipeID;
			}
			set
			{
				if (this._RecipeID != value)
				{
					this._RecipeID = value;
				}
			}
		}

		[Column(Storage = "_ExperienceRequired", DbType = "Int NOT NULL")]
		public int ExperienceRequired
		{
			get
			{
				return this._ExperienceRequired;
			}
			set
			{
				if (this._ExperienceRequired != value)
				{
					this._ExperienceRequired = value;
				}
			}
		}

		[Column(Storage = "_GradeRequired", DbType = "Int NOT NULL")]
		public int GradeRequired
		{
			get
			{
				return this._GradeRequired;
			}
			set
			{
				if (this._GradeRequired != value)
				{
					this._GradeRequired = value;
				}
			}
		}

		[Column(Storage = "_BaseRewardEXP", DbType = "Int NOT NULL")]
		public int BaseRewardEXP
		{
			get
			{
				return this._BaseRewardEXP;
			}
			set
			{
				if (this._BaseRewardEXP != value)
				{
					this._BaseRewardEXP = value;
				}
			}
		}

		[Column(Storage = "_RecipeItemClass", DbType = "NVarChar(128)")]
		public string RecipeItemClass
		{
			get
			{
				return this._RecipeItemClass;
			}
			set
			{
				if (this._RecipeItemClass != value)
				{
					this._RecipeItemClass = value;
				}
			}
		}

		[Column(Storage = "_AutoGiveQuest", DbType = "NVarChar(128)")]
		public string AutoGiveQuest
		{
			get
			{
				return this._AutoGiveQuest;
			}
			set
			{
				if (this._AutoGiveQuest != value)
				{
					this._AutoGiveQuest = value;
				}
			}
		}

		[Column(Storage = "_AlwaysListed", DbType = "TinyInt NOT NULL")]
		public byte AlwaysListed
		{
			get
			{
				return this._AlwaysListed;
			}
			set
			{
				if (this._AlwaysListed != value)
				{
					this._AlwaysListed = value;
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

		private string _ManufactureID;

		private string _RecipeID;

		private int _ExperienceRequired;

		private int _GradeRequired;

		private int _BaseRewardEXP;

		private string _RecipeItemClass;

		private string _AutoGiveQuest;

		private byte _AlwaysListed;

		private string _Feature;
	}
}
