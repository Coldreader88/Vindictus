using System;
using System.Collections.Generic;
using ServiceCore.GuildServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace GuildService.Processor
{
	internal class InviteGuildProcessor : EntityProcessor<InviteGuild, GuildEntity>
	{
		public InviteGuildProcessor(GuildService service, InviteGuild op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			base.Finished = true;
			yield return new OkMessage();
			yield break;
		}

		private GuildService service;
	}
}
