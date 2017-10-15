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
    internal class JoinGuildProcessor : EntityProcessor<JoinGuild, GuildEntity>
    {
        public JoinGuildProcessor(GuildService service, JoinGuild op) : base(op)
        {
            this.service = service;
        }

        public override IEnumerable<object> Run()
        {
            if (!this.Entity.IsInitialized)
            {
                Log<JoinGuildProcessor>.Logger.WarnFormat("JoinGuildProcessor Set TriggerSync");
                TriggerSync triggerSync = new TriggerSync(20000);
                this.Entity.InitializCompleted += new Action(triggerSync.Trigger);
                this.Entity.QueryGuildInfo();
                yield return triggerSync;
            }
            bool checkResult = false;
            bool checkFuncResult = false;
            if (FeatureMatrix.IsEnable("GuildProcessorNotUseAsync"))
            {
                checkResult = true;
                checkFuncResult = FeatureMatrix.IsEnable("koKR") ? GuildAPI.GetAPI().CheckGuild((long)this.Operation.Key.CharacterSN, this.Operation.Key.NexonSN, this.Operation.Key.CharacterName) : GuildAPI.GetAPI().GetGuildInfo(this.Operation.Key).Count == 0;
                if (!checkResult)
                {
                    Log<JoinGuildProcessor>.Logger.ErrorFormat("Failed to join the guild. Cannot see my guild.{0}", (object)this.Operation.Key.ToString());
                    this.Finished = true;
                    yield return "GuildCheckFail";
                }
                if (!checkFuncResult)
                {
                    Log<JoinGuildProcessor>.Logger.InfoFormat("Failed to join the guild. Already belong to another guild.. {0}", (object)this.Operation.Key.ToString());
                    this.Finished = true;
                    yield return "HasGuild";

                    if (this.Entity.GetRegularMemberCount() >= this.Entity.GetMaxMemberCount())
                    {
                        Log<JoinGuildProcessor>.Logger.InfoFormat("Failed to join the guild. Guild members are full.{0} >= {1}", (object)this.Entity.GetRegularMemberCount(), (object)this.Entity.GetMaxMemberCount());
                        this.Finished = true;
                        yield return "GuildFull";
                    }
                    bool joinResult = false;
                    HeroesGuildMemberInfo joinFuncResult = (HeroesGuildMemberInfo)null;
                    if (FeatureMatrix.IsEnable("GuildProcessorNotUseAsync"))
                    {
                        joinResult = true;
                        try
                        {
                            GuildAPI.GetAPI().Join(this.Entity, this.Operation.Key);
                            joinFuncResult = GuildAPI.GetAPI().GetMemberInfo(this.Entity, this.Operation.Key);
                        }
                        catch (Exception ex)
                        {
                            Log<JoinGuildProcessor>.Logger.Warn((object)ex);
                        }
                        this.Finished = true;
                        if (joinResult && joinFuncResult != null && joinFuncResult.emGroupUserType.ToGuildMemberRank() == GuildMemberRank.Wait)
                        {
                            using (HeroesDataContext heroesDataContext = new HeroesDataContext())
                            {
                                heroesDataContext.UpdateGuildCharacterInfo(new long?(this.Operation.Key.CID), new long?(0L));
                                GuildLog.AddGuildLedger(new LogData((long)this.Entity.GuildSN, this.Operation.Key.CID, OperationType.GainGuildPoint, GuildLedgerEventType.UpdateGuildUserPoint, "0", "JoinGuild"));
                            }
                            this.Entity.UpdateGroupMemberInfo(this.Operation.Key, joinFuncResult);
                            this.Entity.Sync();
                            yield return base.Entity.GuildID;
                        }
                        yield return new FailMessage("[JoinGuildProcessor] ToGuildMemberRank")
                        {
                            Reason = FailMessage.ReasonCode.LogicalFail
                        };
                    }
                    AsyncFuncSync<HeroesGuildMemberInfo> sync = new AsyncFuncSync<HeroesGuildMemberInfo>((Func<HeroesGuildMemberInfo>)(() =>
                    {
                        try
                        {
                            GuildAPI.GetAPI().Join(this.Entity, this.Operation.Key);
                            return GuildAPI.GetAPI().GetMemberInfo(this.Entity, this.Operation.Key);
                        }
                        catch (Exception ex)
                        {
                            Log<JoinGuildProcessor>.Logger.Warn((object)ex);
                            return (HeroesGuildMemberInfo)null;
                        }
                    }));
                    yield return sync;
                }
                AsyncFuncSync<bool> checkSync = new AsyncFuncSync<bool>((Func<bool>)(() =>
                {
                    if (!FeatureMatrix.IsEnable("koKR"))
                        return GuildAPI.GetAPI().GetGuildInfo(this.Operation.Key).Count == 0;
                    return GuildAPI.GetAPI().CheckGuild((long)this.Operation.Key.CharacterSN, this.Operation.Key.NexonSN, this.Operation.Key.CharacterName);
                }));
                yield return checkSync;
            }
        }

        private GuildService service;
    }
}
