using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryNewRecipesMessage : IMessage
	{
		public override string ToString()
		{
			return "QueryNewRecipesMessage [ ]";
		}
	}
}
