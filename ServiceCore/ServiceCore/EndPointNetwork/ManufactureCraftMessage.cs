using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ManufactureCraftMessage : IMessage
	{
		public string RecipeID { get; set; }

		public List<long> PartsIDList { get; set; }
	}
}
