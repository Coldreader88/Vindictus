using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.CraftShopInfo")]
	public class CraftShopInfo
	{
		[Column(Storage = "_ShopID", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string ShopID
		{
			get
			{
				return this._ShopID;
			}
			set
			{
				if (this._ShopID != value)
				{
					this._ShopID = value;
				}
			}
		}

		[Column(Name = "[Order]", Storage = "_Order", DbType = "Int NOT NULL")]
		public int Order
		{
			get
			{
				return this._Order;
			}
			set
			{
				if (this._Order != value)
				{
					this._Order = value;
				}
			}
		}

		[Column(Storage = "_RecipeID", DbType = "VarChar(100) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_AppearQuestID", DbType = "VarChar(50)")]
		public string AppearQuestID
		{
			get
			{
				return this._AppearQuestID;
			}
			set
			{
				if (this._AppearQuestID != value)
				{
					this._AppearQuestID = value;
				}
			}
		}

		[Column(Storage = "_AppearQuestDifficulty", DbType = "Int NOT NULL")]
		public int AppearQuestDifficulty
		{
			get
			{
				return this._AppearQuestDifficulty;
			}
			set
			{
				if (this._AppearQuestDifficulty != value)
				{
					this._AppearQuestDifficulty = value;
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

		private string _ShopID;

		private int _Order;

		private string _RecipeID;

		private string _AppearQuestID;

		private int _AppearQuestDifficulty;

		private string _Feature;
	}
}
