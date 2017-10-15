using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryCharacterCommonInfoMessage : IMessage
	{
		public long QueryID { get; set; }

		public long CID { get; set; }

		public override string ToString()
		{
			return string.Format("QueryCharacterCommonInfoMessage[]", new object[0]);
		}
	}
}
