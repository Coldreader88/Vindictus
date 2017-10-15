using System;
using System.Collections.Generic;
using GuildService.API;
using ServiceCore;
using ServiceCore.EndPointNetwork.GuildService;
using ServiceCore.GuildServiceOperations;
using UnifiedNetwork.Entity;
using Utility;

namespace GuildService.Processor
{
	internal class ChangeGuildMasterProcessor : EntityProcessor<ChangeGuildMaster, GuildEntity>
	{
		public ChangeGuildMasterProcessor(GuildService service, ChangeGuildMaster op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			if (base.Entity == null)
			{
				base.Finished = true;
				yield return ChangeGuildMaster.ResultCodeEnum.ChangeMaster_NotExistGuild;
			}
			else
			{
				GuildMember oldMaster = base.Entity.GetGuildMember(base.Operation.OldMasterName);
				if (oldMaster == null)
				{
					GuildLog.AddGuildLedger(new LogData((long)base.Entity.GuildSN, -1L, OperationType.ChangeMaster, GuildLedgerEventType.ChangeMaster_NotMatchOldMaster));
					base.Finished = true;
					yield return ChangeGuildMaster.ResultCodeEnum.ChangeMaster_NotMatchOldMaster;
				}
				else
				{
					GuildMember newMaster = base.Entity.GetGuildMember(base.Operation.NewMasterName);
					if (newMaster == null)
					{
						GuildLog.AddGuildLedger(new LogData((long)base.Entity.GuildSN, oldMaster.Key.CID, OperationType.ChangeMaster, GuildLedgerEventType.ChangeMaster_NotFoundNewMaster, base.Operation.NewMasterName));
						base.Finished = true;
						yield return ChangeGuildMaster.ResultCodeEnum.ChangeMaster_NotFoundNewMaster;
					}
					else if (oldMaster.Key.CID == newMaster.Key.CID)
					{
						Log<ChangeGuildMasterProcessor>.Logger.InfoFormat("EqualNew&OldID [ op.old: {0}, op.new: {1} ]", base.Operation.OldMasterName, base.Operation.NewMasterName);
						Log<ChangeGuildMasterProcessor>.Logger.InfoFormat("EqualNew&OldID [ old: {0}, new: {1} ]", oldMaster.Key.ToString(), newMaster.Key.ToString());
						GuildLog.AddGuildLedger(new LogData((long)base.Entity.GuildSN, oldMaster.Key.CID, OperationType.ChangeMaster, GuildLedgerEventType.ChangeMaster_EqualNewAndOldID));
						base.Finished = true;
						yield return ChangeGuildMaster.ResultCodeEnum.ChangeMaster_EqualNewAndOldID;
					}
					else if (!newMaster.Rank.IsOperator())
					{
						GuildLog.AddGuildLedger(new LogData((long)base.Entity.GuildSN, oldMaster.Key.CID, OperationType.ChangeMaster, GuildLedgerEventType.ChangeMaster_LowRankOldMaster));
						base.Finished = true;
						yield return ChangeGuildMaster.ResultCodeEnum.ChangeMaster_LowRankOldMaster;
					}
					else if (!base.Entity.ChangeGuildMaster(newMaster, oldMaster))
					{
						GuildLog.AddGuildLedger(new LogData((long)base.Entity.GuildSN, oldMaster.Key.CID, OperationType.ChangeMaster, GuildLedgerEventType.ChangeMaster_SystemError, base.Operation.NewMasterName));
						base.Finished = true;
						yield return ChangeGuildMaster.ResultCodeEnum.ChangeMaster_SystemError;
					}
					else
					{
						Func<bool> func = delegate
						{
							bool result;
							try
							{
								this.Entity.GuildInfo = null;
								this.Entity.GuildInfo = GuildAPI.GetAPI().GetGuildInfo(this.Entity.GuildSN).ToGuildInfo();
								this.Entity.ReportGuildInfoChanged();
								HeroesGuildMemberInfo memberInfo = GuildAPI.GetAPI().GetMemberInfo(this.Entity, oldMaster.Key);
								if (memberInfo == null)
								{
									result = false;
								}
								else
								{
									this.Entity.UpdateGroupMemberInfo(oldMaster.Key, memberInfo);
									HeroesGuildMemberInfo memberInfo2 = GuildAPI.GetAPI().GetMemberInfo(this.Entity, newMaster.Key);
									if (memberInfo2 == null)
									{
										result = false;
									}
									else
									{
										this.Entity.UpdateGroupMemberInfo(newMaster.Key, memberInfo2);
										result = true;
									}
								}
							}
							catch (Exception ex)
							{
								Log<ConnectGuildProcessor>.Logger.Warn(ex);
								result = false;
							}
							return result;
						};
						if (FeatureMatrix.IsEnable("GuildProcessorNotUseAsync"))
						{
							bool funcResult = func();
							if (funcResult)
							{
								base.Entity.Sync();
								GuildLog.AddGuildLedger(new LogData((long)base.Entity.GuildSN, oldMaster.Key.CID, OperationType.ChangeMaster, GuildLedgerEventType.ChangeMaster_Success, base.Operation.NewMasterName));
								base.Finished = true;
								yield return ChangeGuildMaster.ResultCodeEnum.ChangeMaster_Success;
							}
							else
							{
								GuildLog.AddGuildLedger(new LogData((long)base.Entity.GuildSN, oldMaster.Key.CID, OperationType.ChangeMaster, GuildLedgerEventType.ChangeMaster_SyncFailed, base.Operation.NewMasterName));
								base.Finished = true;
								yield return ChangeGuildMaster.ResultCodeEnum.ChangeMaster_SyncFailed;
							}
						}
						else
						{
							AsyncFuncSync<bool> sync = new AsyncFuncSync<bool>(func);
							yield return sync;
							if (sync.Result && sync.FuncResult)
							{
								base.Entity.Sync();
								GuildLog.AddGuildLedger(new LogData((long)base.Entity.GuildSN, oldMaster.Key.CID, OperationType.ChangeMaster, GuildLedgerEventType.ChangeMaster_Success, base.Operation.NewMasterName));
								base.Finished = true;
								yield return ChangeGuildMaster.ResultCodeEnum.ChangeMaster_Success;
							}
							else
							{
								GuildLog.AddGuildLedger(new LogData((long)base.Entity.GuildSN, oldMaster.Key.CID, OperationType.ChangeMaster, GuildLedgerEventType.ChangeMaster_SyncFailed, base.Operation.NewMasterName));
								base.Finished = true;
								yield return ChangeGuildMaster.ResultCodeEnum.ChangeMaster_SyncFailed;
							}
						}
					}
				}
			}
			yield break;
		}

		private GuildService service;
	}
}
