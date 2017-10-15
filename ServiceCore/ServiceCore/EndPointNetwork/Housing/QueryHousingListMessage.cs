using System;

namespace ServiceCore.EndPointNetwork.Housing
{
	[Serializable]
	public sealed class QueryHousingListMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("QueryHousingListMessage[]", new object[0]);
		}
	}
}
