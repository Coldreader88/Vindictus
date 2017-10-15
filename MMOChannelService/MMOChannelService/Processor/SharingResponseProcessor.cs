using System;
using System.Collections.Generic;
using ServiceCore.CharacterServiceOperations;
using ServiceCore.MMOChannelServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace MMOChannelService.Processor
{
	internal class SharingResponseProcessor : EntityProcessor<SharingResponse, ChannelEntity>
	{
		public SharingResponseProcessor(MMOChannelService service, SharingResponse op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			ChannelMember member = base.Entity.FindMember(base.Operation.CID);
			if (member == null || member.SharingInfo == null)
			{
				base.Finished = true;
				yield return new FailMessage("[SharingResponseProcessor] member.SharingInfo")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			else if (!base.Operation.Accept)
			{
				member.SharingInfo = null;
			}
			else
			{
				AddStatusEffect op = new AddStatusEffect
				{
					Type = member.SharingInfo.StatusEffect,
					Level = member.SharingInfo.EffectLevel,
					RemainTime = member.SharingInfo.DurationSec
				};
				member.CharacterConn.RequestOperation(op);
			}
			yield break;
		}

		private MMOChannelService service;
	}
}
