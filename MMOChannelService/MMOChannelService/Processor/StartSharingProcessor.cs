using System;
using System.Collections.Generic;
using ServiceCore.CharacterServiceOperations;
using ServiceCore.EndPointNetwork;
using ServiceCore.FrontendServiceOperations;
using ServiceCore.MMOChannelServiceOperations;
using UnifiedNetwork.Entity;
using Utility;

namespace MMOChannelService.Processor
{
	internal class StartSharingProcessor : EntityProcessor<StartSharing, ChannelEntity>
	{
		public StartSharingProcessor(MMOChannelService service, StartSharing op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			ChannelMember channelMember = base.Entity.FindMember(base.Operation.CID);
			if (channelMember == null)
			{
				Log<StartSharingProcessor>.Logger.Error("member is null");
			}
			else if (base.Operation.TargetsCID == null)
			{
				Log<StartSharingProcessor>.Logger.Error("TargetsCID is null");
			}
			else if (base.Operation.SharingInfo == null)
			{
				Log<StartSharingProcessor>.Logger.Error("SharingInfo is null");
			}
			else
			{
				if (base.Operation.SharingInfo.ItemClassName.Equals("dancing_pipe"))
				{
					channelMember.SharingInfo = base.Operation.SharingInfo;
					ServiceCore.CharacterServiceOperations.AddStatusEffect op = new ServiceCore.CharacterServiceOperations.AddStatusEffect
					{
						Type = channelMember.SharingInfo.StatusEffect,
						Level = channelMember.SharingInfo.EffectLevel,
						RemainTime = channelMember.SharingInfo.DurationSec
					};
					channelMember.CharacterConn.RequestOperation(op);
				}
				using (List<long>.Enumerator enumerator = base.Operation.TargetsCID.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						long cid = enumerator.Current;
						ChannelMember channelMember2 = base.Entity.FindMember(cid);
						if (channelMember2 != null)
						{
							channelMember2.SharingInfo = base.Operation.SharingInfo;
							if (base.Operation.SharingInfo.ItemClassName.Equals("dancing_pipe"))
							{
								ServiceCore.CharacterServiceOperations.AddStatusEffect op2 = new ServiceCore.CharacterServiceOperations.AddStatusEffect
								{
									Type = channelMember2.SharingInfo.StatusEffect,
									Level = channelMember2.SharingInfo.EffectLevel,
									RemainTime = channelMember2.SharingInfo.DurationSec
								};
								channelMember2.CharacterConn.RequestOperation(op2);
							}
							else
							{
								channelMember2.FrontendConn.RequestOperation(SendPacket.Create<SharingCheckMessage>(new SharingCheckMessage
								{
									SharingCharacterName = channelMember.Look.CharacterID,
									ItemClassName = base.Operation.SharingInfo.ItemClassName,
									StatusEffect = base.Operation.SharingInfo.StatusEffect,
									EffectLevel = base.Operation.SharingInfo.EffectLevel,
									DurationSec = base.Operation.SharingInfo.DurationSec
								}));
							}
						}
					}
					yield break;
				}
			}
			yield break;
		}

		private MMOChannelService service;
	}
}
