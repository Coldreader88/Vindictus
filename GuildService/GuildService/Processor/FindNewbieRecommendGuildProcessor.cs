using System;
using System.Collections.Generic;
using ServiceCore.GuildServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace GuildService.Processor
{
	internal class FindNewbieRecommendGuildProcessor : OperationProcessor<FindNewbieRecommendGuild>
	{
		public FindNewbieRecommendGuildProcessor(GuildService service, FindNewbieRecommendGuild op) : base(op)
		{
			this.service = service;
		}

        public override IEnumerable<object> Run()
        {
            this.Finished = true;
            for (int i = 0; i < 3; ++i)
            {
                if (this.service.NewbieRecommendGuild.Count == 0)
                {
                    yield return (object)new FailMessage("[FindNewbieRecommendGuildProcessor] service.NewbieRecommendGuild.Count");
                    yield break;
                }
                else
                {
                    int selectedIndex = FindNewbieRecommendGuildProcessor.randomIndex.Next(this.service.NewbieRecommendGuild.Count - 1);
                    long guildID = this.service.NewbieRecommendGuild[selectedIndex];
                    IEntity entity = this.service.GetEntityByID(guildID);
                    if (entity != null)
                    {
                        GuildEntity guild = entity.Tag as GuildEntity;
                        if (guild != null && guild.GuildInfo != null && (guild.GuildInfo.IsNewbieRecommend && guild.OnlineMembers.Count != 0))
                        {
                            yield return (object)guild.GuildInfo;
                            yield break;
                        }
                        else
                            this.service.NewbieRecommendGuild.Remove(guildID);
                    }
                }
            }
            yield return (object)new FailMessage("[FindNewbieRecommendGuildProcessor] fail");
        }

        private const int RetryCount = 3;

		private static Random randomIndex = new Random();

		private GuildService service;
	}
}
