using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using ServiceCore.EndPointNetwork.GuildService;
using ServiceCore.GuildServiceOperations;
using Utility;

namespace GuildService.API.HeroesAPI
{
	public class HeroesGuildAPI : IGuildCore
	{
		public HeroesGuildInfo GetGuildInfo(int guildSN)
		{
			HeroesGuildInfo result = null;
			try
			{
				HeroesGuildDBDataContext heroesGuildDBDataContext = new HeroesGuildDBDataContext();
				ISingleResult<GuildGetInfoBySnResult> source = heroesGuildDBDataContext.GuildGetInfoBySn(new int?(GuildAPI.ServerCode), new int?(guildSN));
				GuildGetInfoBySnResult guildGetInfoBySnResult = source.SingleOrDefault<GuildGetInfoBySnResult>();
				if (guildGetInfoBySnResult != null)
				{
					result = new HeroesGuildInfo(guildGetInfoBySnResult.dtCreateDate, guildGetInfoBySnResult.GuildID, guildGetInfoBySnResult.GuildName, guildGetInfoBySnResult.GuildSN, guildGetInfoBySnResult.Intro, guildGetInfoBySnResult.NameInGroup_Master, guildGetInfoBySnResult.NexonSN_Master, guildGetInfoBySnResult.RealUserCount);
				}
			}
			catch (Exception ex)
			{
				Log<HeroesGuildAPI>.Logger.Error("Error in GetGuildInfo(int guildSN)", ex);
			}
			return result;
		}

		public int OpenGuild(GuildMemberKey key, string guildName, string guildID, string guildIntro)
		{
			int num = 0;
			Log<HeroesGuildAPI>.Logger.InfoFormat("Create: GuildMemberKey[{0}], guildName[{1}], guildID[{2}], guildIntro[{3}]", new object[]
			{
				key,
				guildName,
				guildID,
				guildIntro
			});
			try
			{
				Log<HeroesGuildAPI>.Logger.Warn("Sending OpenGuild");
				HeroesGuildDBDataContext heroesGuildDBDataContext = new HeroesGuildDBDataContext();
				num = heroesGuildDBDataContext.GuildCreate(new int?(GuildAPI.ServerCode), new int?(key.NexonSN), new long?((long)key.CharacterSN), key.CharacterName, guildName, guildID, guildIntro);
				Log<HeroesGuildAPI>.Logger.Warn("Done OpenGuild");
			}
			catch (Exception ex)
			{
				Log<HeroesGuildAPI>.Logger.Error("Error in Create", ex);
			}
			if (num >= 0)
			{
				GuildLog.AddGuildLedger(new LogData((long)num, key.CID, OperationType.HeroesCore_OpenGuild, GuildLedgerEventType.Success, string.Format("GuildName: {0}, GuildID: {1}", guildName, guildID)));
			}
			else
			{
				GuildLog.AddGuildLedger(new LogData((long)num, key.CID, OperationType.HeroesCore_OpenGuild, GuildLedgerEventType.DatabaseFail, string.Format("GuildName: {0}, GuildID: {1}", guildName, guildID)));
			}
			return num;
		}

		public bool CloseGuild(GuildEntity guild, GuildMemberKey key)
		{
			if (guild == null || key == null)
			{
				Log<HeroesGuildAPI>.Logger.ErrorFormat("parameter is null in CloseGuild()", new object[0]);
				return false;
			}
			int num = -1;
			try
			{
				Log<HeroesGuildAPI>.Logger.InfoFormat("Remove: GuildMemberKey[{0}]", key);
				HeroesGuildDBDataContext heroesGuildDBDataContext = new HeroesGuildDBDataContext();
				num = heroesGuildDBDataContext.GuildRemove(new int?(GuildAPI.ServerCode), new int?(key.NexonSN), new int?(guild.GuildSN));
			}
			catch (Exception ex)
			{
				Log<HeroesGuildAPI>.Logger.Error("Remove(CloseGuild) Error: ", ex);
			}
			if (num == 0)
			{
				GuildLog.AddGuildLedger(new LogData((long)guild.GuildSN, key.CID, OperationType.HeroesCore_CloseGuild, GuildLedgerEventType.Success, string.Format("GuildSn: {0}, GuildToString: {1}", guild.GuildSN, guild.ToString())));
			}
			else
			{
				GuildLog.AddGuildLedger(new LogData((long)guild.GuildSN, key.CID, OperationType.HeroesCore_CloseGuild, GuildLedgerEventType.DatabaseFail, string.Format("GuildSn: {0}, GuildToString: {1}", guild.GuildSN, guild.ToString())));
			}
			return num == 0;
		}

		public void Join(GuildEntity guild, GuildMemberKey key)
		{
			int num = -1;
			try
			{
				HeroesGuildDBDataContext heroesGuildDBDataContext = new HeroesGuildDBDataContext();
				num = heroesGuildDBDataContext.GuildUserJoin(new int?(GuildAPI.ServerCode), new int?(guild.GuildSN), new int?(key.NexonSN), new long?((long)key.CharacterSN), key.CharacterName, string.Empty);
			}
			catch (Exception ex)
			{
				Log<HeroesGuildAPI>.Logger.Error("Error in Join(GuildEntity guild, GuildMemberKey key)", ex);
			}
			if (num == 0)
			{
				GuildLog.AddGuildLedger(new LogData((long)guild.GuildSN, key.CID, OperationType.HeroesCore_RequestJoin, GuildLedgerEventType.Success, string.Format("GuildSn: {0}, GuildToString: {1}", guild.GuildSN, guild.ToString())));
				return;
			}
			GuildLog.AddGuildLedger(new LogData((long)guild.GuildSN, key.CID, OperationType.HeroesCore_RequestJoin, GuildLedgerEventType.DatabaseFail, string.Format("GuildSn: {0}, GuildToString: {1}", guild.GuildSN, guild.ToString())));
		}

		public void LeaveGuild(GuildEntity guild, GuildMemberKey key)
		{
			int num = -1;
			try
			{
				HeroesGuildDBDataContext heroesGuildDBDataContext = new HeroesGuildDBDataContext();
				num = heroesGuildDBDataContext.GuildUserSecede(new int?(GuildAPI.ServerCode), new int?(guild.GuildSN), new int?(key.NexonSN), new long?((long)key.CharacterSN), key.CharacterName);
			}
			catch (Exception ex)
			{
				Log<HeroesGuildAPI>.Logger.Error("Error in LeaveGuild(GuildEntity guild, GuildMemberKey key)", ex);
			}
			if (num == 0)
			{
				GuildLog.AddGuildLedger(new LogData((long)guild.GuildSN, key.CID, OperationType.HeroesCore_LeaveGuild, GuildLedgerEventType.Success, string.Format("GuildSn: {0}, GuildToString: {1}", guild.GuildSN, guild.ToString(), key.ToString())));
				return;
			}
			GuildLog.AddGuildLedger(new LogData((long)guild.GuildSN, key.CID, OperationType.HeroesCore_LeaveGuild, GuildLedgerEventType.DatabaseFail, string.Format("GuildSn: {0}, GuildToString: {1}", guild.GuildSN, guild.ToString(), key.ToString())));
		}

		public bool AcceptJoin(GuildEntity guild, GuildMemberRank operatorRank, GuildMember targetMember, bool accept)
		{
			int num = -1;
			try
			{
				if (operatorRank.HasAcceptJoinPermission(targetMember.Rank))
				{
					HeroesGuildDBDataContext heroesGuildDBDataContext = new HeroesGuildDBDataContext();
					if (accept)
					{
						num = heroesGuildDBDataContext.GuildUserJoinApply(new int?(GuildAPI.ServerCode), new int?(guild.GuildSN), new int?(targetMember.Key.NexonSN), new long?((long)targetMember.Key.CharacterSN), targetMember.Key.CharacterName);
					}
					else
					{
						num = heroesGuildDBDataContext.GuildUserJoinReject(new int?(GuildAPI.ServerCode), new int?(guild.GuildSN), new int?(targetMember.Key.NexonSN), new long?((long)targetMember.Key.CharacterSN), targetMember.Key.CharacterName);
					}
				}
			}
			catch (Exception ex)
			{
				Log<HeroesGuildAPI>.Logger.Error("Error in AcceptJoin(GuildEntity guild, GuildMemberRank operatorRank, GuildMember targetMember, bool accept)", ex);
			}
			GuildLedgerEventType eventType;
			if (num == 0)
			{
				eventType = GuildLedgerEventType.Success;
			}
			else
			{
				eventType = GuildLedgerEventType.DatabaseFail;
			}
			GuildLog.AddGuildLedger(new LogData((long)guild.GuildSN, targetMember.Key.CID, OperationType.HeroesCore_ResponseJoin, eventType, string.Format("GuildSn: {0}, isAccept: {1}", guild.GuildSN, accept.ToString()), targetMember.Key.ToString()));
			return num == 0;
		}

		public bool ChangeMaster(GuildEntity guild, GuildMember newMaster, GuildMember oldMaster)
		{
			GuildMemberKey key = newMaster.Key;
			HeroesGuildUserType value = GuildMemberRank.Operator.ToGroupUserType();
			int num = -1;
			try
			{
				HeroesGuildDBDataContext heroesGuildDBDataContext = new HeroesGuildDBDataContext();
				num = heroesGuildDBDataContext.GuildGroupChangeMaster(new int?(GuildAPI.ServerCode), new int?(guild.GuildSN), new int?(key.NexonSN), new long?((long)key.CharacterSN), key.CharacterName, new int?((int)value));
			}
			catch (Exception ex)
			{
				Log<HeroesGuildAPI>.Logger.Error("Error in ChangeMaster", ex);
			}
			if (num == 0)
			{
				GuildLog.AddGuildLedger(new LogData((long)guild.GuildSN, newMaster.Key.CID, OperationType.HeroesCore_ChangeMaster, GuildLedgerEventType.Success, guild.GuildInfo.MasterName, newMaster.Key.CharacterName));
				return true;
			}
			GuildLog.AddGuildLedger(new LogData((long)guild.GuildSN, newMaster.Key.CID, OperationType.HeroesCore_ChangeMaster, GuildLedgerEventType.DatabaseFail, guild.GuildInfo.MasterName, newMaster.Key.CharacterName));
			return false;
		}

		public bool ChangeRank(GuildEntity guild, GuildMemberRank operatorRank, GuildMember targetMember, GuildMemberRank toRank)
		{
			int num = -1;
			HeroesGuildDBDataContext heroesGuildDBDataContext = new HeroesGuildDBDataContext();
			if (toRank == GuildMemberRank.Unknown)
			{
				if (operatorRank.HasSecedePermission(targetMember.Rank, toRank))
				{
					try
					{
						num = heroesGuildDBDataContext.GuildUserSecede(new int?(GuildAPI.ServerCode), new int?(guild.GuildSN), new int?(targetMember.Key.NexonSN), new long?((long)targetMember.Key.CharacterSN), targetMember.Key.CharacterName);
					}
					catch
					{
						Log<GuildAPI>.Logger.ErrorFormat("GuildUserSecede fail in ChangeRank : ServiceCode[{0}], GuildSN[{1}], MemberNexonSN[{2}]", GuildAPI.ServerCode, guild.GuildSN, targetMember.Key.NexonSN);
					}
					if (num == 0)
					{
						GuildLog.AddGuildLedger(new LogData((long)guild.GuildSN, targetMember.Key.CID, OperationType.GainGuildPoint, GuildLedgerEventType.UpdateGuildUserPoint, "0", "Secede"));
						int num2 = -1;
						try
						{
							using (HeroesDataContext heroesDataContext = new HeroesDataContext())
							{
								num2 = heroesDataContext.UpdateGuildCharacterInfo(new long?(targetMember.Key.CID), new long?(0L));
							}
						}
						catch
						{
							Log<GuildAPI>.Logger.ErrorFormat("UpdateGuildCharacterInfo fail in ChangeRank : ServiceCode[{0}], GuildSN[{1}], MemberNexonSN[{2}], toRank[{3}]", new object[]
							{
								GuildAPI.ServerCode,
								guild.GuildSN,
								targetMember.Key.NexonSN,
								toRank
							});
						}
						if (num2 == 0)
						{
							GuildLog.AddGuildLedger(new LogData((long)guild.GuildSN, targetMember.Key.CID, OperationType.HeroesCore_LeaveGuild, GuildLedgerEventType.Success, string.Format("GuildSn: {0}, GuildToString: {1}", guild.GuildSN, guild.ToString(), targetMember.Key.ToString())));
						}
					}
					else
					{
						GuildLog.AddGuildLedger(new LogData((long)guild.GuildSN, targetMember.Key.CID, OperationType.HeroesCore_LeaveGuild, GuildLedgerEventType.DatabaseFail, string.Format("GuildSn: {0}, GuildToString: {1}", guild.GuildSN, guild.ToString(), targetMember.Key.ToString())));
					}
				}
			}
			else if (operatorRank.HasRankChangePermission(targetMember.Rank, toRank))
			{
				Log<GuildAPI>.Logger.InfoFormat("UserTypeModify: ServiceCode[{0}], GuildSN[{1}], MemberNexonSN[{2}]", GuildAPI.ServerCode, guild.GuildSN, targetMember.Key.NexonSN);
				try
				{
					num = heroesGuildDBDataContext.GuildUserTypeModify(new int?(GuildAPI.ServerCode), new int?(guild.GuildSN), new int?(targetMember.Key.NexonSN), new int?(targetMember.Key.CharacterSN), targetMember.Key.CharacterName, new int?((int)toRank.ToGroupUserType()));
				}
				catch (Exception ex)
				{
					Log<HeroesGuildAPI>.Logger.Error("Error in ChangeRank(GuildEntity guild, GuildMemberRank operatorRank, GuildMember targetMember, GuildMemberRank toRank)", ex);
				}
				if (num == 0)
				{
					GuildLog.AddGuildLedger(new LogData((long)guild.GuildSN, targetMember.Key.CID, OperationType.HeroesCore_ChangeRank, GuildLedgerEventType.Success, string.Format("GuildSn: {0}, GuildToString: {1}", guild.GuildSN, guild.ToString(), targetMember.Key.ToString())));
				}
				else
				{
					GuildLog.AddGuildLedger(new LogData((long)guild.GuildSN, targetMember.Key.CID, OperationType.HeroesCore_ChangeRank, GuildLedgerEventType.DatabaseFail, string.Format("GuildSn: {0}, GuildToString: {1}", guild.GuildSN, guild.ToString(), targetMember.Key.ToString())));
				}
			}
			return num == 0;
		}

		public List<HeroesGuildMemberInfo> GetMembers(int guildSN)
		{
			List<HeroesGuildMemberInfo> list = new List<HeroesGuildMemberInfo>();
			try
			{
				HeroesGuildDBDataContext heroesGuildDBDataContext = new HeroesGuildDBDataContext();
				int num = 0;
				for (int i = 1; i <= 10; i++)
				{
					int? num2 = new int?(0);
					int? num3 = new int?(0);
					ISingleResult<GuildAllMemberGetListResult> singleResult = heroesGuildDBDataContext.GuildAllMemberGetList(new int?(GuildAPI.ServerCode), new int?(guildSN), new int?(i), new byte?(byte.MaxValue), new byte?(1), ref num2, ref num3);
					foreach (GuildAllMemberGetListResult guildAllMemberGetListResult in singleResult)
					{
						list.Add(new HeroesGuildMemberInfo(guildAllMemberGetListResult.CharacterName, (long)guildAllMemberGetListResult.CharacterSN, (HeroesGuildUserType)guildAllMemberGetListResult.emGroupUserType, guildAllMemberGetListResult.GuildSN, guildAllMemberGetListResult.Intro, guildAllMemberGetListResult.NameInGroup, guildAllMemberGetListResult.NexonSN));
					}
					if (list.Count >= num3 || num == list.Count)
					{
						break;
					}
					num = list.Count;
				}
			}
			catch (Exception ex)
			{
				Log<HeroesGuildAPI>.Logger.Error("Error in GetMembers(int guildSN)", ex);
			}
			return list;
		}

		public HeroesGuildMemberInfo GetMemberInfo(GuildEntity guild, GuildMemberKey key)
		{
			try
			{
				HeroesGuildDBDataContext heroesGuildDBDataContext = new HeroesGuildDBDataContext();
				List<GuildGroupMemberGetInfoResult> source = heroesGuildDBDataContext.GuildGroupMemberGetInfo(new int?(GuildAPI.ServerCode), new int?(guild.GuildSN), new int?(key.CharacterSN), key.CharacterName).ToList<GuildGroupMemberGetInfoResult>();
				GuildGroupMemberGetInfoResult guildGroupMemberGetInfoResult = source.FirstOrDefault<GuildGroupMemberGetInfoResult>();
				if (source.Count<GuildGroupMemberGetInfoResult>() == 1 && guildGroupMemberGetInfoResult != null)
				{
					return new HeroesGuildMemberInfo(guildGroupMemberGetInfoResult.CharacterName, (long)guildGroupMemberGetInfoResult.CharacterSN, (HeroesGuildUserType)guildGroupMemberGetInfoResult.emGroupUserType, guildGroupMemberGetInfoResult.GuildSN, guildGroupMemberGetInfoResult.Intro, guildGroupMemberGetInfoResult.NameInGroup, guildGroupMemberGetInfoResult.NexonSN);
				}
			}
			catch (Exception ex)
			{
				Log<HeroesGuildAPI>.Logger.Error("Error in GetMemberInfo(GuildEntity guild, GuildMemberKey key)", ex);
			}
			return null;
		}

        public ICollection<HeroesGuildInfo> SearchGuild(int searchType, byte orderType, int pageNo, byte pageSize, string searchKey, out int total_row_count)
        {
            ICollection<HeroesGuildInfo> heroesGuildInfos = new List<HeroesGuildInfo>();
            int? nullable = new int?(0);
            if (searchType != 0)
            {
                object[] objArray = new object[] { orderType, pageNo, pageSize, searchKey };
                Log<GuildAPI>.Logger.InfoFormat("GroupGetListByGuildMaster: OrderType[{0}], PageNo[{1}], PageSize[{2}], SearchKey[{3}]", objArray);
                try
                {
                    HeroesGuildDBDataContext heroesGuildDBDataContext = new HeroesGuildDBDataContext();
                    ISingleResult<GuildGroupGetListByGuildMasterResult> guildGroupGetListByGuildMasterResults = heroesGuildDBDataContext.GuildGroupGetListByGuildMaster(new int?(GuildAPI.ServerCode), new byte?(orderType), new int?(pageNo), new byte?(pageSize), searchKey, ref nullable);
                    foreach (GuildGroupGetListByGuildMasterResult guildGroupGetListByGuildMasterResult in guildGroupGetListByGuildMasterResults)
                    {
                        if (guildGroupGetListByGuildMasterResult == null)
                        {
                            continue;
                        }
                        heroesGuildInfos.Add(new HeroesGuildInfo(guildGroupGetListByGuildMasterResult.dtCreateDate, guildGroupGetListByGuildMasterResult.GuildID, guildGroupGetListByGuildMasterResult.GuildName, guildGroupGetListByGuildMasterResult.GuildSN, guildGroupGetListByGuildMasterResult.Intro, guildGroupGetListByGuildMasterResult.NameInGroup_Master, guildGroupGetListByGuildMasterResult.NexonSN_Master, guildGroupGetListByGuildMasterResult.RealUserCount));
                    }
                }
                catch (Exception exception)
                {
                    Log<HeroesGuildAPI>.Logger.Error("Error in SearchGuild", exception);
                }
            }
            else
            {
                try
                {
                    object[] serverCode = new object[] { GuildAPI.ServerCode, orderType, pageNo, pageSize, searchKey };
                    Log<GuildAPI>.Logger.InfoFormat("GroupSearchGetList: ServerCode[{0}], OrderType[{1}], PageNo[{2}], PageSize[{3}], SearchKey[{4}]", serverCode);
                    HeroesGuildDBDataContext heroesGuildDBDataContext1 = new HeroesGuildDBDataContext();
                    List<GuildGroupGetListByGuildNameResult> list = heroesGuildDBDataContext1.GuildGroupGetListByGuildName(new int?(GuildAPI.ServerCode), new byte?(orderType), new int?(pageNo), new byte?(pageSize), searchKey, ref nullable).ToList<GuildGroupGetListByGuildNameResult>();
                    if (list != null)
                    {
                        foreach (GuildGroupGetListByGuildNameResult guildGroupGetListByGuildNameResult in list)
                        {
                            if (guildGroupGetListByGuildNameResult == null)
                            {
                                continue;
                            }
                            heroesGuildInfos.Add(new HeroesGuildInfo(guildGroupGetListByGuildNameResult.dtCreateDate, guildGroupGetListByGuildNameResult.GuildID, guildGroupGetListByGuildNameResult.GuildName, guildGroupGetListByGuildNameResult.GuildSN, guildGroupGetListByGuildNameResult.Intro, guildGroupGetListByGuildNameResult.NameInGroup_Master, guildGroupGetListByGuildNameResult.NexonSN_Master, guildGroupGetListByGuildNameResult.RealUserCount));
                        }
                    }
                }
                catch (Exception exception2)
                {
                    Exception exception1 = exception2;
                    object[] objArray1 = new object[] { orderType, pageNo, pageSize, searchKey, exception1.ToString() };
                    Log<GuildAPI>.Logger.InfoFormat("GroupGetListByGuildName: OrderType[{0}], PageNo[{1}], PageSize[{2}], SearchKey[{3}]\nException: {4}", objArray1);
                    Log<HeroesGuildAPI>.Logger.Error("Error in SearchGuild", exception1);
                }
            }
            total_row_count = (nullable.HasValue ? nullable.Value : 0);
            return heroesGuildInfos;
        }

		public List<HeroesUserGuildInfo> GetGuildInfo(GuildMemberKey key)
		{
			int? num = new int?(0);
			int? num2 = new int?(0);
			List<HeroesUserGuildInfo> list = new List<HeroesUserGuildInfo>();
			try
			{
				HeroesGuildDBDataContext heroesGuildDBDataContext = new HeroesGuildDBDataContext();
				ISingleResult<GuildUserGroupGetListResult> source = heroesGuildDBDataContext.GuildUserGroupGetList(new int?(GuildAPI.ServerCode), new int?(key.NexonSN), new int?(1), new byte?(byte.MaxValue), ref num, ref num2);
				foreach (GuildUserGroupGetListResult guildUserGroupGetListResult in source.ToList<GuildUserGroupGetListResult>())
				{
					if (guildUserGroupGetListResult.CharacterName == key.CharacterName && guildUserGroupGetListResult.CharacterSN == key.CharacterSN)
					{
						list.Add(new HeroesUserGuildInfo(guildUserGroupGetListResult.CharacterName, guildUserGroupGetListResult.CharacterSN, guildUserGroupGetListResult.dateCreate, (HeroesGuildUserType)guildUserGroupGetListResult.GroupUserType, guildUserGroupGetListResult.GuildID, guildUserGroupGetListResult.GuildName, guildUserGroupGetListResult.GuildSN, guildUserGroupGetListResult.Intro, guildUserGroupGetListResult.NameInGroup, guildUserGroupGetListResult.NameInGroup_Master, guildUserGroupGetListResult.NexonSN_Master, guildUserGroupGetListResult.RealUserCount));
					}
				}
			}
			catch (Exception ex)
			{
				Log<HeroesGuildAPI>.Logger.Error("Error in GetGuildInfo(GuildMemberKey key)", ex);
			}
			return list;
		}

		public bool CheckGuild(long CharacterSN, int NexonSN, string CharacterName)
		{
			try
			{
				HeroesGuildDBDataContext heroesGuildDBDataContext = new HeroesGuildDBDataContext();
				int num = heroesGuildDBDataContext.GuildGroupUserTryJoin(new int?(GuildAPI.ServerCode), new long?(CharacterSN), new int?(NexonSN), CharacterName);
				return num == 0;
			}
			catch (Exception ex)
			{
				Log<HeroesGuildAPI>.Logger.Error("Error in CheckGuild(long CharacterSN, int NexonSN, string CharacterName)", ex);
			}
			return false;
		}

		public GroupNameCheckResult CheckGroupName(string guildName)
		{
			GroupNameCheckResult groupNameCheckResult = GuildAPI._CheckGroupName(guildName);
			if (groupNameCheckResult == GroupNameCheckResult.Succeed)
			{
				if (GuildContents.HasForbiddenWords(guildName))
				{
					return GroupNameCheckResult.NotMatchedNamingRule;
				}
				try
				{
					HeroesGuildDBDataContext heroesGuildDBDataContext = new HeroesGuildDBDataContext();
					Log<GuildAPI>.Logger.InfoFormat("CheckGroupName: ServerCode[{0}], GuildName[{1}]", GuildAPI.ServerCode, guildName);
					if (heroesGuildDBDataContext.GuildCheckGroupName(new int?(GuildAPI.ServerCode), guildName) == 0)
					{
						return GroupNameCheckResult.Succeed;
					}
					return GroupNameCheckResult.DuplicatedName;
				}
				catch (Exception ex)
				{
					Log<GuildService>.Logger.Error("잘못된 길드명입니다", ex);
					groupNameCheckResult = GroupNameCheckResult.DuplicatedName;
				}
				return groupNameCheckResult;
			}
			return groupNameCheckResult;
		}

		public GroupIDCheckResult CheckGroupID(string guildID)
		{
			if (guildID == null || guildID.Length == 0)
			{
				return GroupIDCheckResult.IDNotSupplied;
			}
			if (GuildContents.HasForbiddenWords(guildID))
			{
				return GroupIDCheckResult.InvalidCharacter;
			}
			GroupIDCheckResult result = GroupIDCheckResult.Succeed;
			try
			{
				Log<GuildAPI>.Logger.InfoFormat("CheckGroupID: ServerCode[{0}], GuildName[{1}]", GuildAPI.ServerCode, guildID);
				HeroesGuildDBDataContext heroesGuildDBDataContext = new HeroesGuildDBDataContext();
				if (heroesGuildDBDataContext.GuildCheckGroupID(new int?(GuildAPI.ServerCode), guildID) == 0)
				{
					return GroupIDCheckResult.Succeed;
				}
				return GroupIDCheckResult.DuplicatedID;
			}
			catch (ArgumentException ex)
			{
				if (ex.Message == "Exists Guid ID.")
				{
					result = GroupIDCheckResult.DuplicatedID;
				}
				else
				{
					if (ex.Message != "Guild ID has special character")
					{
						Log<GuildAPI>.Logger.ErrorFormat("Unknown CheckGroupID exception message: {0}", ex.Message);
					}
					result = GroupIDCheckResult.InvalidCharacter;
				}
			}
			catch (Exception ex2)
			{
				Log<GuildService>.Logger.Error("CheckGroupID failed [" + guildID + "]", ex2);
				result = GroupIDCheckResult.InvalidCharacter;
			}
			return result;
		}

		public void UserNameModify(GuildMemberKey key, string newName)
		{
			int num = -1;
			try
			{
				HeroesGuildDBDataContext heroesGuildDBDataContext = new HeroesGuildDBDataContext();
				num = heroesGuildDBDataContext.GuildUserNameModify(new int?(GuildAPI.ServerCode), new int?(key.NexonSN), new long?((long)key.CharacterSN), newName);
			}
			catch (Exception ex)
			{
				Log<HeroesGuildAPI>.Logger.Error("Error in UserNameModify(GuildMemberKey key, string newName)", ex);
			}
			if (num == 0)
			{
				GuildLog.AddGuildLedger(new LogData(0L, key.CID, OperationType.HeroesCore_UserNameModify, GuildLedgerEventType.Success, string.Format("Prev: {0}", key.ToString()), string.Format("New: {0}", newName)));
			}
			else
			{
				Log<HeroesGuildAPI>.Logger.ErrorFormat("ErrorCode return in UserNameModify(GuildMemberKey key, string newName): {0}", num);
				GuildLog.AddGuildLedger(new LogData(0L, key.CID, OperationType.HeroesCore_UserNameModify, GuildLedgerEventType.DatabaseFail, string.Format("Prev: {0}", key.ToString()), string.Format("New: {0}", newName)));
			}
		}
	}
}
