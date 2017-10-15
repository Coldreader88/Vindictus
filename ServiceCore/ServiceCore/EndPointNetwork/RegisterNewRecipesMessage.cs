using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RegisterNewRecipesMessage : IMessage
	{
		public IDictionary<string, long> RecipeList { get; set; }

		public RegisterNewRecipesMessage(IDictionary<string, long> recipeList)
		{
			this.RecipeList = recipeList;
		}

		public override string ToString()
		{
			return string.Format("RegisterNewRecipesMessage [ RecipeList x {0} ]", this.RecipeList.Count);
		}
	}
}
