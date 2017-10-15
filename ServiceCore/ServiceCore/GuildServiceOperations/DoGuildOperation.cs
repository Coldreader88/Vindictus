using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork.GuildService;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.GuildServiceOperations
{
	[Serializable]
	public sealed class DoGuildOperation : Operation
	{
		public GuildMemberKey GuildMemberKey { get; set; }

		public List<GuildOperationInfo> Operations { get; set; }

		public long GuildID
		{
			get
			{
				return this.guildID;
			}
			set
			{
				this.guildID = value;
			}
		}

		public DoGuildOperation(GuildMemberKey key, List<GuildOperationInfo> operations)
		{
			this.GuildMemberKey = key;
			this.Operations = operations;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}

		[NonSerialized]
		private long guildID;
	}
}
