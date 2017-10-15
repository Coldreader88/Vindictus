using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork.GuildService;
using ServiceCore.GuildServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace GuildService.Processor
{
	internal class RequestNewbieRecommendGuildProcessor : EntityProcessor<RequestNewbieRecommendGuild, GuildEntity>
	{
		public RequestNewbieRecommendGuildProcessor(GuildService service, RequestNewbieRecommendGuild op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			base.Finished = true;
			if (!base.Entity.IsInitialized)
			{
				yield return new FailMessage("[RequestNewbieRecommendGuildProcessor] Entity.IsInitialized");
			}
			else if (base.Entity.OnlineMembers.Count != 0)
			{
				List<string> list = new List<string>();
				foreach (KeyValuePair<string, OnlineGuildMember> keyValuePair in base.Entity.OnlineMembers)
				{
					if (keyValuePair.Value.CharacterName != null)
					{
						keyValuePair.Value.SendMessage<NoticeNewbieRecommendMessage>(new NoticeNewbieRecommendMessage(base.Operation.UserName, base.Operation.ShipID));
						list.Add(keyValuePair.Value.CharacterName);
					}
				}
				yield return list;
			}
			else
			{
				yield return new FailMessage("[RequestNewbieRecommendGuildProcessor] Entity.OnlineMembers.Count");
			}
			yield break;
		}

		private GuildService service;
	}
}
