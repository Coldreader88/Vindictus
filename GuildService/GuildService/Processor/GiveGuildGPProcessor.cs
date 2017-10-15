using System;
using System.Collections.Generic;
using GuildService.API;
using ServiceCore;
using ServiceCore.EndPointNetwork.GuildService;
using ServiceCore.GuildServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using Utility;

namespace GuildService.Processor
{
	internal class GiveGuildGPProcessor : EntityProcessor<GiveGuildGP, GuildEntity>
	{
		public GiveGuildGPProcessor(GuildService service, GiveGuildGP op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			base.Finished = true;
			if (!FeatureMatrix.IsEnable("GuildLevel"))
			{
				yield return new FailMessage("[GiveGuildGPProcessor] GuildLevel")
				{
					Reason = FailMessage.ReasonCode.NotSupportedOperation
				};
			}
			else
			{
				using (HeroesDataContext context = new HeroesDataContext())
				{
					bool gpAdded = false;
					foreach (KeyValuePair<long, int> keyValuePair in base.Operation.GiveDict)
					{
						long key = keyValuePair.Key;
						int value = keyValuePair.Value;
						OnlineGuildMember onlineGuildMember = null;
						foreach (KeyValuePair<string, OnlineGuildMember> keyValuePair2 in base.Entity.OnlineMembers)
						{
							if (keyValuePair2.Value.CID == key)
							{
								onlineGuildMember = keyValuePair2.Value;
								break;
							}
						}
						if (onlineGuildMember != null && onlineGuildMember.GuildMember.Rank.IsRegularMember())
						{
							int num = base.Entity.GiveGP(value, base.Operation.GainType, key);
							gpAdded |= (num != 0);
							try
							{
								onlineGuildMember.GuildMember.AddPoint(num);
								long point = onlineGuildMember.GuildMember.GetPoint();
								base.Entity.ReportGuildMemberChanged(onlineGuildMember.GuildMember.Key);
								context.UpdateGuildCharacterInfo(new long?(key), new long?(point));
								GuildLog.AddGuildLedger(new LogData((long)base.Entity.GuildSN, key, OperationType.GainGuildPoint, GuildLedgerEventType.UpdateGuildUserPoint, point.ToString()));
							}
							catch (Exception ex)
							{
								Log<GiveGuildGP>.Logger.ErrorFormat("Error while GiveGuildGPProcessor, Step UpdateGuildCharacterInfo : {0}", ex.ToString());
							}
						}
					}
					if (!gpAdded)
					{
						yield return new FailMessage("[GiveGuildGPProcessor] gpAdded");
						yield break;
					}
					try
					{
						context.UpdateGuildInfo(new int?(FeatureMatrix.GameCode), new int?(GuildAPI.ServerCode), new int?(base.Entity.GuildSN), null, null, new int?(base.Entity.GuildInfo.GuildLevel), new long?(base.Entity.GuildInfo.GuildPoint), null, new int?(FeatureMatrix.GetInteger("InGameGuild_MaxMember")), null);
					}
					catch (Exception ex2)
					{
						Log<GiveGuildGP>.Logger.ErrorFormat("Error while GiveGuildGPProcessor, Step UpdateGuildInfo : {0}", ex2.ToString());
					}
				}
				base.Entity.ReportGuildInfoChanged();
				base.Entity.Sync();
				yield return new OkMessage();
			}
			yield break;
		}

		private GuildService service;
	}
}
