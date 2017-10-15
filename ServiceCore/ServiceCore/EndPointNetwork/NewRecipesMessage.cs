using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class NewRecipesMessage : IMessage
	{
		public IDictionary<string, long> RecipeList { get; set; }

		public NewRecipesMessage(IDictionary<string, long> recipeList)
		{
			this.RecipeList = recipeList;
		}

		public override string ToString()
		{
			return string.Format("NewRecipesMessage [ RecipeList x {0} ]", this.RecipeList.Count);
		}
	}
}
