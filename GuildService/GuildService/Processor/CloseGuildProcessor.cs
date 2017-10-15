using System;
using System.Collections.Generic;
using GuildService.API;
using ServiceCore;
using ServiceCore.GuildServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using Utility;

namespace GuildService.Processor
{
	internal class CloseGuildProcessor : EntityProcessor<CloseGuild, GuildEntity>
	{
		public CloseGuildProcessor(GuildService service, CloseGuild op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			if (base.Entity.OnlineMembers.TryGetValue(base.Operation.Key.CharacterName) == null)
			{
				base.Finished = true;
				yield return new FailMessage("[CloseGuildProcessor] member");
			}
			else if (base.Entity.GetMemberCount() != 1)
			{
				base.Finished = true;
				yield return "HasMember";
			}
			else
			{
				bool closeResult = false;
				if (FeatureMatrix.IsEnable("GuildProcessorNotUseAsync"))
				{
					closeResult = GuildAPI.GetAPI().CloseGuild(base.Entity, base.Operation.Key);
				}
				else
				{
					AsyncFuncSync<bool> sync = new AsyncFuncSync<bool>(delegate
					{
						GuildAPI.GetAPI().CloseGuild(base.Entity, base.Operation.Key);
						return true;
					});
					yield return sync;
					closeResult = sync.Result;
				}
				base.Finished = true;
				if (closeResult)
				{
					base.Entity.SyncCloseGuild();
					yield return new OkMessage();
				}
				else
				{
					yield return new FailMessage("[CloseGuildProcessor] closeResult");
				}
			}
			yield break;
		}

		private GuildService service;
	}
}
