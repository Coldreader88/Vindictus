using System;
using ServiceCore.EndPointNetwork.GuildService;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.FrontendServiceOperations
{
	[Serializable]
	public sealed class GuildKicked : Operation
	{
		public GuildResultEnum GuildResultEnum { get; set; }

		public GuildKicked(GuildResultEnum value)
		{
			this.GuildResultEnum = value;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
