using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public sealed class GuildOperationMessage : IMessage
	{
		public List<GuildOperationInfo> Operations { get; set; }

		public override string ToString()
		{
			return string.Format("GuildOperationMessage[ Operations x  {0} ]", this.Operations.Count);
		}
	}
}
