using System;

namespace ServiceCore.EndPointNetwork.Item
{
	[Serializable]
	public sealed class RequestPartChangingMessage : IMessage
	{
		public long combinedEquipItemID { get; private set; }

		public int targetIndex { get; private set; }

		public long partID { get; private set; }
	}
}
