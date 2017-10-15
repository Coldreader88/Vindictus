using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class ManufactureCraftItem : Operation
	{
		public string RecipeID { get; private set; }

		public int Count { get; private set; }

		public List<long> PartsIDList { get; private set; }

		public ManufactureCraftItem(string recipeID, int count, List<long> partsIDList)
		{
			this.RecipeID = recipeID;
			this.Count = count;
			this.PartsIDList = partsIDList;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
