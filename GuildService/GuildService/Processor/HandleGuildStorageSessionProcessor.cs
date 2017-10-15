using System;
using System.Collections.Generic;
using ServiceCore;
using ServiceCore.GuildServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace GuildService.Processor
{
	internal class HandleGuildStorageSessionProcessor : EntityProcessor<HandleGuildStorageSession, GuildEntity>
	{
		public HandleGuildStorageSessionProcessor(GuildService service, HandleGuildStorageSession op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			GuildStorageManager storage = base.Entity.Storage;
			if (!base.Entity.Storage.Valid || !FeatureMatrix.IsEnable("GuildStorage") || base.Entity.Storage.Processing)
			{
				base.Finished = true;
				yield return new FailMessage("[HandleGuildStorageSessionProcessor] Entity.Storage.Processing")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			else
			{
				OnlineGuildMember member = base.Entity.GetOnlineMember(base.Operation.RequestingCID);
				if (member != null)
				{
					member.IsGuildStorageListening = base.Operation.IsStarting;
					if (member.IsGuildStorageListening)
					{
						member.SendGuildStorageInfoMessage();
						member.SendGuildStorageLogsMessage(true);
						member.SendGuildStorageLogsMessage(false);
					}
				}
				base.Finished = true;
				yield return new OkMessage();
			}
			yield break;
		}

		private GuildService service;
	}
}
