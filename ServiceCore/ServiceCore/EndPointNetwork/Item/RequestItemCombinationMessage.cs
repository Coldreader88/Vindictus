using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork.Item
{
	[Serializable]
	public sealed class RequestItemCombinationMessage : IMessage
	{
		public string combinedEquipItemClass { get; private set; }

		public List<long> partsIDList { get; private set; }
	}
}
