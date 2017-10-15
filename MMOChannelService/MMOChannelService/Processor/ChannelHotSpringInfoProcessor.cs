using System;
using System.Collections.Generic;
using ServiceCore.MMOChannelServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using Utility;

namespace MMOChannelService.Processor
{
	internal class ChannelHotSpringInfoProcessor : EntityProcessor<ChannelHotSpringInfo, ChannelEntity>
	{
		public ChannelHotSpringInfoProcessor(MMOChannelService service, ChannelHotSpringInfo op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			ChannelMember member = base.Entity.FindMember(base.Operation.CID);
			if (member == null)
			{
				base.Finished = true;
				Log<ChannelHotSpringInfoProcessor>.Logger.ErrorFormat("character {0} is not in channel {1}", base.Operation.CID, base.Entity.Entity.ID);
				yield return new FailMessage("[ChannelHotSpringInfoProcessor] member")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			else
			{
				member.Client.Player.BroadCastHotSpringInfo(base.Operation.HotSpringPotionEffectInfos, base.Operation.TownID);
				base.Finished = true;
				yield return new OkMessage();
			}
			yield break;
		}

		private MMOChannelService service;
	}
}
