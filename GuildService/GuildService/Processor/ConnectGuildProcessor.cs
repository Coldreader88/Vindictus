using System;
using System.Collections.Generic;
using GuildService.API;
using ServiceCore;
using ServiceCore.GuildServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using Utility;

namespace GuildService.Processor
{
	internal class ConnectGuildProcessor : EntityProcessor<ConnectGuild, GuildEntity>
	{
		public ConnectGuildProcessor(GuildService service, ConnectGuild op) : base(op)
		{
			this.service = service;
		}

        public override IEnumerable<object> Run()
        {
            if (!this.Entity.IsInitialized)
            {
                Log<ConnectGuildProcessor>.Logger.WarnFormat("ConnectGuildProcessor Set TriggerSync");
                TriggerSync triggerSync = new TriggerSync(20000);
                this.Entity.InitializCompleted += new Action(triggerSync.Trigger);
                this.Entity.QueryGuildInfo();
                yield return (object)triggerSync;
                if (!triggerSync.Result)
                {
                    this.Finished = true;
                    bool isInitGroupInfo = this.Entity.GuildInfo != null;
                    bool isInitGroupMemberInfo = this.Entity.GuildMemberDict != null;
                    Log<ConnectGuildProcessor>.Logger.ErrorFormat("Failed to connect to guild. Guild initialization failed. Queried = {0}, value = {1} GuildSN : {2}, IsInitialized: {3}, GuildInfo: {4}, MemberInfo: {5}", (object)this.Entity.IsQueriedGuildInfo, (object)this.Operation.Key.ToString(), (object)this.Entity.GuildSN, (object)this.Entity.IsInitialized, (object)isInitGroupInfo, (object)isInitGroupMemberInfo);
                    yield return (object)new FailMessage("[ConnectGuildProcessor] triggerSync.Result");
                    yield break;
                }
            }
            this.Entity.Connect(this.Operation.Key, this.Connection.RemoteID);
            if (FeatureMatrix.IsEnable("GuildProcessorNotUseAsync"))
            {
                HeroesGuildMemberInfo info = (HeroesGuildMemberInfo)null;
                try
                {
                    info = GuildAPI.GetAPI().GetMemberInfo(this.Entity, this.Operation.Key);
                }
                catch (Exception ex)
                {
                    Log<ConnectGuildProcessor>.Logger.Warn((object)ex);
                }
                if (info != null)
                {
                    this.Entity.UpdateGroupMemberInfo(this.Operation.Key, info);
                    this.Entity.Sync();
                }
            }
            else
            {
                AsyncFuncSync<HeroesGuildMemberInfo> sync = new AsyncFuncSync<HeroesGuildMemberInfo>((Func<HeroesGuildMemberInfo>)(() =>
                {
                    try
                    {
                        return GuildAPI.GetAPI().GetMemberInfo(this.Entity, this.Operation.Key);
                    }
                    catch (Exception ex)
                    {
                        Log<ConnectGuildProcessor>.Logger.Warn((object)ex);
                        return (HeroesGuildMemberInfo)null;
                    }
                }));
                yield return (object)sync;
                if (sync.Result && sync.FuncResult != null)
                {
                    this.Entity.UpdateGroupMemberInfo(this.Operation.Key, sync.FuncResult);
                    this.Entity.Sync();
                }
            }
            this.Finished = true;
            yield return (object)new OkMessage();
        }

        private GuildService service;
	}
}
