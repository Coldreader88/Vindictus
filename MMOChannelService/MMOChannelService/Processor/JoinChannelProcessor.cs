using System;
using System.Collections.Generic;
using Devcat.Core.Threading;
using ServiceCore.FrontendServiceOperations;
using ServiceCore.MMOChannelServiceOperations;
using UnifiedNetwork.Entity;
using Utility;

namespace MMOChannelService.Processor
{
	internal class JoinChannelProcessor : EntityProcessor<JoinChannel, ChannelEntity>
	{
		public JoinChannelProcessor(MMOChannelService service, JoinChannel op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			if (base.Connection.RemoteCategory != "FrontendServiceCore.FrontendService")
			{
				base.Finished = true;
				yield return JoinChannel.FailReasonEnum.NotFrontEnd;
			}
			else if (base.Entity.Count >= this.service.Capacity || this.service.CurrentLoad > (long)LoadManager.MaxLoad)
			{
				base.Finished = true;
				Log<JoinChannelProcessor>.Logger.WarnFormat("load or capacity exceed : {0}, {1}", base.Entity.Count, this.service.CurrentLoad);
				base.Finished = true;
				yield return JoinChannel.FailReasonEnum.MaxCapacityOver;
			}
			else
			{
				Log<JoinChannelProcessor>.Logger.InfoFormat("load or capacity not exceed : {0}, {1}", base.Entity.Count, this.service.CurrentLoad);
				ChannelMember member = new ChannelMember(base.Operation.CID, base.Connection, base.Operation.PartitionID, base.Operation.Action)
				{
					ChannelJoined = base.Entity
				};
				if (!this.service.AddWaitingMember(base.Operation.CID, member))
				{
					Log<JoinChannelProcessor>.Logger.WarnFormat("duplicate join channel waiting members : [{0} -> {1}]", base.Operation.CID, base.Entity.Entity.ID);
					base.Finished = true;
					yield return JoinChannel.FailReasonEnum.HasWaitingMemeber;
				}
				else
				{
					int key = this.service.Server.KeyGen(base.Operation.CID);
					Scheduler.Schedule(this.service.Thread, Job.Create(delegate
					{
						IEntity entity = member.ChannelJoined.Entity;
						if (this.service.RemoveWaitingMember(member.CID, member))
						{
							Log<JoinChannelProcessor>.Logger.ErrorFormat("Cannot Connect to client(peer) : [{0} -> {1}]", member.CID, this.Entity.Entity.ID);
							this.service.Server.RemoveKey(member.CID);
							member.Close();
							this.service.RequestOperation(entity, new Location(member.FID, "FrontendServiceCore.FrontendService"), new NotifyEnterChannelResult
							{
								ResultEnum = EnterChannelResult.Timeout
							});
						}
					}), 60000);
					base.Finished = true;
					yield return this.service.Server.Address;
					yield return this.service.Server.Port;
					yield return base.Entity.Entity.ID;
					yield return key;
				}
			}
			yield break;
		}

		private MMOChannelService service;
	}
}
