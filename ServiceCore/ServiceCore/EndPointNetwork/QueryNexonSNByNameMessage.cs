using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryNexonSNByNameMessage : IMessage
	{
		public long QueryID { get; set; }

		public string cName { get; set; }

		public override string ToString()
		{
			return string.Format("QueryNexonSNByNameMessage[{0}, cname {1}]", this.QueryID, this.cName);
		}
	}
}
