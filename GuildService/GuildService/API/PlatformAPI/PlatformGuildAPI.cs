using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Nexon.Com.Group.Game.Wrapper;
using ServiceCore;
using ServiceCore.EndPointNetwork.GuildService;
using ServiceCore.GuildServiceOperations;
using Utility;

namespace GuildService.API.PlatformAPI
{
	public class PlatformGuildAPI : IGuildCore
	{
		public GroupUserType ConvertToGroupUserType(HeroesGuildUserType type)
		{
			return (GroupUserType)type;
		}

		public HeroesGuildUserType ConvertToHeroesGuildUserType(GroupUserType type)
		{
			return (HeroesGuildUserType)type;
		}

		public HeroesGuildInfo GetGuildInfo(int guildSN)
		{
			Log<GuildAPI>.Logger.InfoFormat("GetGuildInfo: guildSN[{0}]", guildSN);
			GroupInfo info = HeroesGuild.GetInfo(GuildAPI.ServerCode, guildSN);
			return new HeroesGuildInfo(info);
		}

		public int OpenGuild(GuildMemberKey key, string guildName, string guildID, string guildIntro)
		{
			int result = 0;
			Log<GuildAPI>.Logger.InfoFormat("Create: GuildMemberKey[{0}], guildName[{1}], guildID[{2}], guildIntro[{3}]", new object[]
			{
				key,
				guildName,
				guildID,
				guildIntro
			});
			try
			{
				Log<PlatformGuildAPI>.Logger.Warn("Sending OpenGuild");
				result = HeroesGuild.Create(GuildAPI.ServerCode, key.NexonSN, (long)key.CharacterSN, key.CharacterName, guildName, guildID, guildIntro);
				Log<PlatformGuildAPI>.Logger.Warn("Done OpenGuild");
			}
			catch (GroupException ex)
			{
				if (ex.ErrorCode == 24)
				{
					result = -3;
				}
				else if (ex.ErrorCode == 1 || ex.ErrorCode == 9000)
				{
					result = -4;
				}
				else if (ex.ErrorCode == -1)
				{
					result = -1;
				}
				else
				{
					Log<PlatformGuildAPI>.Logger.ErrorFormat("GroupException : {0}", ex.ErrorCode);
					Log<PlatformGuildAPI>.Logger.Error("exception : ", ex);
					result = -3;
				}
			}
			return result;
		}

		public bool CloseGuild(GuildEntity guild, GuildMemberKey key)
		{
			Log<GuildAPI>.Logger.InfoFormat("Remove: GuildMemberKey[{0}]", key);
			try
			{
				HeroesGuild.Remove(GuildAPI.ServerCode, key.NexonSN, guild.GuildSN);
			}
			catch (GroupException ex)
			{
				if (ex.ErrorCode != 6)
				{
					Log<GuildService>.Logger.Error("Remove(CloseGuild) Error: ", ex);
				}
				return false;
			}
			catch (Exception ex2)
			{
				Log<GuildService>.Logger.Error("Remove(CloseGuild) Error: ", ex2);
				return false;
			}
			return true;
		}

		public void Join(GuildEntity guild, GuildMemberKey key)
		{
			Log<GuildAPI>.Logger.InfoFormat("UserJoin: GuildMemberKey[{0}]", key);
			HeroesGuild.UserJoin(GuildAPI.ServerCode, guild.GuildSN, key.NexonSN, (long)key.CharacterSN, key.CharacterName, string.Empty);
		}

		public void LeaveGuild(GuildEntity guild, GuildMemberKey key)
		{
			Log<GuildAPI>.Logger.InfoFormat("UserSecede: GuildMemberKey[{0}]", key);
			HeroesGuild.UserSecede(GuildAPI.ServerCode, guild.GuildSN, key.NexonSN, key.CharacterSN, key.CharacterName);
		}

		public bool AcceptJoin(GuildEntity guild, GuildMemberRank operatorRank, GuildMember targetMember, bool accept)
		{
			Log<GuildAPI>.Logger.InfoFormat("AcceptJoin: GuildMemberRank[{0}]", operatorRank);
			if (!operatorRank.HasAcceptJoinPermission(targetMember.Rank))
			{
				return false;
			}
			if (accept)
			{
				Log<GuildAPI>.Logger.InfoFormat("UserJoinApply: GuildSN[{0}], NexonSN[{1}], CharacterSN[{2}], CharacterName[{3}]", new object[]
				{
					guild.GuildSN,
					targetMember.Key.NexonSN,
					targetMember.Key.CharacterSN,
					targetMember.Key.CharacterName
				});
				HeroesGuild.UserJoinApply(GuildAPI.ServerCode, guild.GuildSN, targetMember.Key.NexonSN, targetMember.Key.CharacterSN, targetMember.Key.CharacterName);
				return true;
			}
			Log<GuildAPI>.Logger.InfoFormat("UserJoinReject: GuildSN[{0}], NexonSN[{1}], CharacterSN[{2}], CharacterName[{3}]", new object[]
			{
				guild.GuildSN,
				targetMember.Key.NexonSN,
				targetMember.Key.CharacterSN,
				targetMember.Key.CharacterName
			});
			HeroesGuild.UserJoinReject(GuildAPI.ServerCode, guild.GuildSN, targetMember.Key.NexonSN, targetMember.Key.CharacterSN, targetMember.Key.CharacterName);
			return true;
		}

		public bool ChangeMaster(GuildEntity guild, GuildMember newMaster, GuildMember oldMaster)
		{
			if (!FeatureMatrix.IsEnable("EnableChangeGuildMaster"))
			{
				return false;
			}
			GuildMemberKey key = newMaster.Key;
			Log<GuildAPI>.Logger.InfoFormat("ChangeMaster: Guild[{0}], newMasterKey[{1}], oldMasterKey[{2}]", guild.ToString(), key.ToString(), oldMaster.Key.ToString());
			Log<GuildAPI>.Logger.InfoFormat("ChangeMaster: serverCode[{0}] guildSn[{1}] NexonSN[{2}] C_SN[{3}] Name[{4}] Rank[{5}]", new object[]
			{
				GuildAPI.ServerCode,
				guild.GuildSN,
				key.NexonSN,
				key.CharacterSN,
				key.CharacterName,
				GuildMemberRank.Operator.ToGroupUserType()
			});
			HeroesGuildUserType type = GuildMemberRank.Operator.ToGroupUserType();
			return HeroesGuild.GroupChangeMaster(GuildAPI.ServerCode, guild.GuildSN, key.NexonSN, (long)key.CharacterSN, key.CharacterName, this.ConvertToGroupUserType(type));
		}

		public bool ChangeRank(GuildEntity guild, GuildMemberRank operatorRank, GuildMember targetMember, GuildMemberRank toRank)
		{
			Log<GuildAPI>.Logger.InfoFormat("ChangeRank: MemberRank[{0}], MemberSN[{1}], ToRank[{2}]", operatorRank, targetMember.Key.NexonSN, toRank);
			if (toRank == GuildMemberRank.Unknown)
			{
				if (operatorRank.HasSecedePermission(targetMember.Rank, toRank))
				{
					Log<GuildAPI>.Logger.InfoFormat("UserSecede: ServiceCode[{0}], GuildSN[{1}], MemberNexonSN[{2}]", GuildAPI.ServerCode, guild.GuildSN, targetMember.Key.NexonSN);
					HeroesGuild.UserSecede(GuildAPI.ServerCode, guild.GuildSN, targetMember.Key.NexonSN, targetMember.Key.CharacterSN, targetMember.Key.CharacterName);
					using (HeroesDataContext heroesDataContext = new HeroesDataContext())
					{
						heroesDataContext.UpdateGuildCharacterInfo(new long?(targetMember.Key.CID), new long?(0L));
						GuildLog.AddGuildLedger(new LogData((long)guild.GuildSN, targetMember.Key.CID, OperationType.GainGuildPoint, GuildLedgerEventType.UpdateGuildUserPoint, "0", "Secede"));
					}
					return true;
				}
			}
			else if (operatorRank.HasRankChangePermission(targetMember.Rank, toRank))
			{
				Log<GuildAPI>.Logger.InfoFormat("UserTypeModify: ServiceCode[{0}], GuildSN[{1}], MemberNexonSN[{2}]", GuildAPI.ServerCode, guild.GuildSN, targetMember.Key.NexonSN);
				HeroesGuild.UserTypeModify(GuildAPI.ServerCode, guild.GuildSN, targetMember.Key.NexonSN, targetMember.Key.CharacterSN, targetMember.Key.CharacterName, this.ConvertToGroupUserType(toRank.ToGroupUserType()));
				return true;
			}
			return false;
		}

		public List<HeroesGuildMemberInfo> GetMembers(int guildSN)
		{
			Log<GuildAPI>.Logger.InfoFormat("GetMembers: guildSN[{0}]", guildSN);
			List<HeroesGuildMemberInfo> list = new List<HeroesGuildMemberInfo>();
			foreach (int num in Enumerable.Range(1, 2))
			{
				Log<GuildAPI>.Logger.InfoFormat("AllMemberGetList: ServerCode[{0}], GuildSN[{1}], Page[{2}]", GuildAPI.ServerCode, guildSN, num);
				int num2;
				int num3;
				Collection<GroupMemberInfo> collection = HeroesGuild.AllMemberGetList(GuildAPI.ServerCode, guildSN, num, byte.MaxValue, true, out num2, out num3);
				foreach (GroupMemberInfo info in collection)
				{
					list.Add(new HeroesGuildMemberInfo(info));
				}
				if (list.Count >= num3)
				{
					return list;
				}
			}
			return list;
		}

		public HeroesGuildMemberInfo GetMemberInfo(GuildEntity guild, GuildMemberKey key)
		{
			HeroesGuildMemberInfo result;
			try
			{
				Log<GuildAPI>.Logger.InfoFormat("GroupMemberGetInfo: ServerCode[{0}], GuildSN[{1}], MemberNexonSN[{2}]", GuildAPI.ServerCode, guild.GuildSN, key.NexonSN);
				GroupMemberInfo groupMemberInfo = HeroesGuild.GroupMemberGetInfo(GuildAPI.ServerCode, guild.GuildSN, key.CharacterSN, key.CharacterName);
				HeroesGuildUserType value = this.ConvertToHeroesGuildUserType(groupMemberInfo.emGroupUserType);
				if (value.ToGuildMemberRank().IsInvalid())
				{
					result = null;
				}
				else
				{
					result = new HeroesGuildMemberInfo(groupMemberInfo);
				}
			}
			catch (GroupException ex)
			{
				if (ex.ErrorCode != 15)
				{
					Log<GuildService>.Logger.Error("GroupMemberGetInfo Error", ex);
				}
				result = null;
			}
			catch (Exception ex2)
			{
				Log<GuildService>.Logger.Error("GroupMemberGetInfo Error", ex2);
				result = null;
			}
			return result;
		}

        public ICollection<HeroesGuildInfo> SearchGuild(int searchType, byte orderType, int pageNo, byte pageSize, string searchKey, out int total_row_count)
        {
            ICollection<GroupInfo> groupInfos;
            object[] objArray = new object[] { searchType, orderType, pageNo, pageSize, searchKey };
            Log<GuildAPI>.Logger.InfoFormat("SearchGuild: SearchType[{0}], OrderType[{1}], PageNo[{2}], PageSize[{3}], SearchKey[{4}]", objArray);
            ICollection<HeroesGuildInfo> heroesGuildInfos = new List<HeroesGuildInfo>();
            int num = 0;
            if (searchType != 0)
            {
                object[] objArray1 = new object[] { orderType, pageNo, pageSize, searchKey };
                Log<GuildAPI>.Logger.InfoFormat("GroupGetListByGuildMaster: OrderType[{0}], PageNo[{1}], PageSize[{2}], SearchKey[{3}]", objArray1);
                groupInfos = HeroesGuild.GroupGetListByGuildMaster(GuildAPI.ServerCode, orderType, pageNo, pageSize, searchKey, out num);
            }
            else
            {
                try
                {
                    object[] serverCode = new object[] { GuildAPI.ServerCode, orderType, pageNo, pageSize, searchKey };
                    Log<GuildAPI>.Logger.InfoFormat("GroupSearchGetList: ServerCode[{0}], OrderType[{1}], PageNo[{2}], PageSize[{3}], SearchKey[{4}]", serverCode);
                    groupInfos = HeroesGuild.GroupSearchGetList(GuildAPI.ServerCode, pageNo, pageSize, orderType, searchKey, out num);
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    object[] objArray2 = new object[] { orderType, pageNo, pageSize, searchKey, exception.ToString() };
                    Log<GuildAPI>.Logger.InfoFormat("GroupGetListByGuildName: OrderType[{0}], PageNo[{1}], PageSize[{2}], SearchKey[{3}]\nException: {4}", objArray2);
                    groupInfos = HeroesGuild.GroupGetListByGuildName(GuildAPI.ServerCode, orderType, pageNo, pageSize, searchKey, out num);
                }
            }
            if (groupInfos != null)
            {
                foreach (GroupInfo groupInfo in groupInfos)
                {
                    heroesGuildInfos.Add(new HeroesGuildInfo(groupInfo));
                }
            }
            total_row_count = num;
            return heroesGuildInfos;
        }

        public List<HeroesUserGuildInfo> GetGuildInfo(GuildMemberKey key)
		{
			Log<GuildAPI>.Logger.InfoFormat("UserGroupGetList: ServerCode[{0}], MemberNexonSN[{1}], MaxValue[{2}]", GuildAPI.ServerCode, key.NexonSN, byte.MaxValue);
			int num;
			int num2;
			Collection<UserGroupInfo> collection = HeroesGuild.UserGroupGetList(GuildAPI.ServerCode, key.NexonSN, 1, byte.MaxValue, out num, out num2);
			List<HeroesUserGuildInfo> list = new List<HeroesUserGuildInfo>();
			foreach (UserGroupInfo userGroupInfo in collection)
			{
				if (userGroupInfo.CharacterName == key.CharacterName && userGroupInfo.CharacterSN == key.CharacterSN)
				{
					list.Add(new HeroesUserGuildInfo(userGroupInfo));
				}
			}
			return list;
		}

		public bool CheckGuild(long CharacterSN, int NexonSN, string CharacterName)
		{
			Log<GuildAPI>.Logger.InfoFormat("GroupUserTryJoin: ServerCode[{0}], CharacterSN[{1}], NexonSN[{2}], CharacterName[{3}]", new object[]
			{
				GuildAPI.ServerCode,
				CharacterSN,
				NexonSN,
				CharacterName
			});
			return HeroesGuild.GroupUserTryJoin(GuildAPI.ServerCode, CharacterSN, NexonSN, CharacterName);
		}

		public GroupNameCheckResult CheckGroupName(string guildName)
		{
			GroupNameCheckResult groupNameCheckResult = GuildAPI._CheckGroupName(guildName);
			if (groupNameCheckResult == GroupNameCheckResult.Succeed)
			{
				try
				{
					Log<GuildAPI>.Logger.InfoFormat("CheckGroupName: ServerCode[{0}], GuildName[{1}]", GuildAPI.ServerCode, guildName);
					HeroesGuild.CheckGroupName(GuildAPI.ServerCode, guildName);
					return GroupNameCheckResult.Succeed;
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
			GroupIDCheckResult result = GroupIDCheckResult.Succeed;
			try
			{
				Log<GuildAPI>.Logger.InfoFormat("CheckGroupID: ServerCode[{0}], GuildName[{1}]", GuildAPI.ServerCode, guildID);
				HeroesGuild.CheckGroupID(GuildAPI.ServerCode, guildID);
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
			if (key == null || string.IsNullOrEmpty(newName))
			{
				return;
			}
			HeroesGuild.UserNameModify(GuildAPI.ServerCode, key.NexonSN, key.CharacterSN, newName);
		}
	}
}
