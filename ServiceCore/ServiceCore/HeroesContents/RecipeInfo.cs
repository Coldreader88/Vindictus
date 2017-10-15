using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.RecipeInfo")]
	public class RecipeInfo
	{
		[Column(Storage = "_RecipeID", DbType = "NVarChar(256) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_AppearQuestID", DbType = "NVarChar(100)")]
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

		[Column(Storage = "_DisappearQuestID", DbType = "NVarChar(100)")]
		public string DisappearQuestID
		{
			get
			{
				return this._DisappearQuestID;
			}
			set
			{
				if (this._DisappearQuestID != value)
				{
					this._DisappearQuestID = value;
				}
			}
		}

		private string _RecipeID;

		private string _AppearQuestID;

		private string _DisappearQuestID;
	}
}
