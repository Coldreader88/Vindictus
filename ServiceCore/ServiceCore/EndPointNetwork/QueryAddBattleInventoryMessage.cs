using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryAddBattleInventoryMessage : IMessage
	{
		public int Tag { get; set; }

		public int SlotNum { get; set; }

		public bool IsFree { get; set; }

		public override string ToString()
		{
			return string.Format("QueryAddBattleInventoryMessage[]", new object[0]);
		}
	}
}
