using System;
using System.Collections.Generic;
using ServiceCore.MMOChannelServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace MMOChannelService.Processor
{
	internal class LeaveChannelProcessor : EntityProcessor<LeaveChannel, ChannelEntity>
	{
		public LeaveChannelProcessor(MMOChannelService service, LeaveChannel op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			if (base.Connection.RemoteCategory != "FrontendServiceCore.FrontendService")
			{
				base.Finished = true;
				yield return new FailMessage("[LeaveChannelProcessor] Connection.RemoteCategory")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			else
			{
				ChannelMember member = base.Connection.Tag as ChannelMember;
				if (member == null)
				{
					base.Finished = true;
					yield return new FailMessage("[LeaveChannelProcessor] member")
					{
						Reason = FailMessage.ReasonCode.LogicalFail
					};
				}
				else
				{
					base.Finished = true;
					yield return new OkMessage();
					member.Close();
				}
			}
			yield break;
		}

		private MMOChannelService service;
	}
}
