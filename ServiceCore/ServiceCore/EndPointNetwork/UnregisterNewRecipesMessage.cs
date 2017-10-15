using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UnregisterNewRecipesMessage : IMessage
	{
		public ICollection<string> RecipeList { get; set; }

		public UnregisterNewRecipesMessage(ICollection<string> recipeList)
		{
			this.RecipeList = recipeList;
		}

		public override string ToString()
		{
			return string.Format("UnregisterNewRecipesMessage [ RecipeList x {0} ]", this.RecipeList.Count);
		}
	}
}
