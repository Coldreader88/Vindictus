using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryCharacterViewInfoMessage : IMessage
	{
		public long QueryID { get; set; }

		public string name { get; set; }

		public override string ToString()
		{
			return string.Format("QueryCharacterViewInfoMessage[ {0} / {1} ]", this.QueryID, this.name);
		}
	}
}
