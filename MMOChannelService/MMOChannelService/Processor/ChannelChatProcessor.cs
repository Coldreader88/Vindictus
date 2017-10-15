using System;
using System.Collections.Generic;
using ServiceCore.MMOChannelServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using Utility;

namespace MMOChannelService.Processor
{
	internal class ChannelChatProcessor : EntityProcessor<ChannelChat, ChannelEntity>
	{
		public ChannelChatProcessor(MMOChannelService service, ChannelChat op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			ChannelMember member = base.Entity.FindMember(base.Operation.CID);
			if (member == null)
			{
				base.Finished = true;
				Log<ChannelChatProcessor>.Logger.ErrorFormat("character {0} is not in channel {1}", base.Operation.CID, base.Entity.Entity.ID);
				yield return new FailMessage("[ChannelChatProcessor] member")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			else
			{
				member.Client.Player.BroadCastChat(base.Operation.Message);
				base.Finished = true;
				yield return new OkMessage();
			}
			yield break;
		}

		private MMOChannelService service;
	}
}
