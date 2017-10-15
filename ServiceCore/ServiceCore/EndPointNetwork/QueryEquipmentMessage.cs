using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryEquipmentMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("QueryEquipmentMessage[]", new object[0]);
		}
	}
}
