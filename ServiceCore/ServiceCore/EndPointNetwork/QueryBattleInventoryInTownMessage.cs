using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryBattleInventoryInTownMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("QueryBattleInventoryInTownMessage[]", new object[0]);
		}
	}
}
