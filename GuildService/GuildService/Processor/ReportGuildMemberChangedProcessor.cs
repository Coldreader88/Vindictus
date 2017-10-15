using System;
using System.Collections.Generic;
using ServiceCore.GuildServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace GuildService.Processor
{
	internal class ReportGuildMemberChangedProcessor : EntityProcessor<ReportGuildMemberChanged, GuildEntity>
	{
		public ReportGuildMemberChangedProcessor(GuildService service, ReportGuildMemberChanged op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			base.Finished = true;
			if (!base.Entity.IsInitialized)
			{
				yield return new OkMessage();
			}
			else
			{
				GuildMember member = base.Entity.GetGuildMember(base.Operation.CharacterName);
				if (member != null)
				{
					member.SetLevel(base.Operation.Level);
					base.Entity.ReportGuildMemberChanged(member.Key);
					base.Entity.Sync();
					yield return new OkMessage();
				}
				else
				{
					yield return new FailMessage("[ReportGuildMemberChangedProcessor] member");
				}
			}
			yield break;
		}

		private GuildService service;
	}
}
