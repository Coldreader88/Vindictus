using System;
using System.Collections.Generic;
using ServiceCore;
using ServiceCore.GuildServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace GuildService.Processor
{
	internal class JoinGuildChatRoomProcessor : EntityProcessor<JoinGuildChatRoom, GuildEntity>
	{
		public JoinGuildChatRoomProcessor(GuildService service, JoinGuildChatRoom op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			base.Finished = true;
			if (FeatureMatrix.IsEnable("UseHeroesGuildChatServer") && this.service.JoinChatRoomFromGame(base.Entity, base.Operation.Key))
			{
				yield return new OkMessage();
			}
			else
			{
				yield return new FailMessage("[JoinGuildChatRoomProcessor] UseHeroesGuildChatServer")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			yield break;
		}

		private GuildService service;
	}
}
