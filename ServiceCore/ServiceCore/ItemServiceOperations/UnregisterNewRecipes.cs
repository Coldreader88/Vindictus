using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class UnregisterNewRecipes : Operation
	{
		public ICollection<string> RecipeList { get; set; }

		public UnregisterNewRecipes(ICollection<string> recipeList)
		{
			this.RecipeList = recipeList;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
