using System;
using System.Collections.Generic;
using GuildService.API;
using ServiceCore;
using ServiceCore.GuildServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace GuildService.Processor
{
	internal class EditGuildNoticeProcessor : EntityProcessor<EditGuildNotice, GuildEntity>
	{
		public EditGuildNoticeProcessor(GuildService service, EditGuildNotice op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			HeroesDataContext context = new HeroesDataContext();
			context.UpdateGuildInfo(new int?(FeatureMatrix.GameCode), new int?(GuildAPI.ServerCode), new int?(base.Entity.GuildSN), null, null, null, null, base.Operation.Text, new int?(FeatureMatrix.GetInteger("InGameGuild_MaxMember")), null);
			base.Entity.GuildInfo.GuildNotice = base.Operation.Text;
			base.Entity.ReportGuildInfoChanged();
			base.Entity.Sync();
			base.Finished = true;
			yield return new OkMessage();
			yield break;
		}

		private GuildService service;
	}
}
