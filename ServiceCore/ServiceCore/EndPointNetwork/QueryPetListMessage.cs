using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryPetListMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format(" [ QueryPetListMessage ] ", new object[0]);
		}
	}
}
