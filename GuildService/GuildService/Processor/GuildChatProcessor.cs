using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork.GuildService;
using ServiceCore.GuildServiceOperations;
using UnifiedNetwork.Entity;

namespace GuildService.Processor
{
	internal class GuildChatProcessor : EntityProcessor<GuildChat, GuildEntity>
	{
		public GuildChatProcessor(GuildService service, GuildChat op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			base.Finished = true;
			OnlineGuildMember member = base.Entity.GetOnlineMember(base.Operation.CID);
			if (member != null)
			{
				if (member.GuildMember.Rank.IsRegularMember())
				{
					this.service.GuildChat(member, base.Operation.Message);
					yield return GuildChat.ErrorCode.Success;
				}
				else
				{
					yield return GuildChat.ErrorCode.NotEnoughPermission;
				}
			}
			yield break;
		}

		private GuildService service;
	}
}
