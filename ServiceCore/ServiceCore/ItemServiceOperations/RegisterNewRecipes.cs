using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class RegisterNewRecipes : Operation
	{
		public IDictionary<string, long> RecipeList { get; set; }

		public RegisterNewRecipes(IDictionary<string, long> recipeList)
		{
			this.RecipeList = recipeList;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
