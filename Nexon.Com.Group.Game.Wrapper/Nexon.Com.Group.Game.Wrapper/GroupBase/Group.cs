using Nexon.Com.DAO;
using Nexon.Com.Group.Game.Wrapper.GroupBase.DAO;
using Nexon.Com.Group.Game.Wrapper.GroupBase.DAO.Korea;
using Nexon.Com.Group.Game.Wrapper.GroupBase.DAO.OverSea;
using Nexon.Com.Log;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase
{
    internal static class Group
    {
        internal static void CheckGroupID(int n4ServerCode, string GuildID)
        {
            if (GroupPlatform.isOverSea)
                return;
            new Code().Code_Group(GroupPlatform.GameCode, n4ServerCode);
            string empty = string.Empty;
            try
            {
                int ErrorCode = 0;
                string strErrorMessage = string.Empty;
                using (Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes heroes = new Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes())
                    ErrorCode = heroes.CheckGroupID(n4ServerCode, GuildID, out strErrorMessage);
                if (ErrorCode != 0)
                {
                    if (ErrorCode == 1)
                        throw new ArgumentException(strErrorMessage);
                    throw new GroupException(ErrorCode, strErrorMessage);
                }
            }
            catch (GroupException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new GroupException(9000, ex.Message);
            }
        }

        internal static void CheckGroupName(int n4ServerCode, string GuildName)
        {
            if (GroupPlatform.isOverSea)
                return;
            new Code().Code_Group(GroupPlatform.GameCode, n4ServerCode);
            string empty = string.Empty;
            try
            {
                int ErrorCode = 0;
                string strErrorMessage = string.Empty;
                using (Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes heroes = new Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes())
                    ErrorCode = heroes.CheckGroupName(n4ServerCode, GuildName, out strErrorMessage);
                if (ErrorCode != 0)
                {
                    if (ErrorCode == 1)
                        throw new ArgumentException(strErrorMessage);
                    throw new GroupException(ErrorCode, strErrorMessage);
                }
            }
            catch (GroupException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new GroupException(9000, ex.Message);
            }
        }

        internal static void Close(int n4ServerCode, int NexonSN, int GuildSN)
        {
            if (GroupPlatform.isOverSea)
            {
                try
                {
                    new Nexon.Com.Group.Game.Wrapper.GroupBase.DAO.OverSea.GroupRemoveSPWrapper()
                    {
                        GameCode = GroupPlatform.GameCode,
                        ServerCode = n4ServerCode,
                        GuildSN = GuildSN,
                        GuildStatus = (byte)3
                    }.Execute();
                }
                catch (SPFatalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, ex.Message);
                }
                catch (SPLogicalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, ex.Message);
                }
                catch (GroupException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new GroupException(9000, ex.Message);
                }
            }
            else
            {
                new Code().Code_Group(GroupPlatform.GameCode, n4ServerCode);
                try
                {
                    int ErrorCode = 0;
                    string strErrorMessage = string.Empty;
                    using (Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes heroes = new Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes())
                        ErrorCode = heroes.Remove(n4ServerCode, NexonSN, GuildSN, out strErrorMessage);
                    if (ErrorCode != 0)
                    {
                        if (ErrorCode == 1)
                            throw new ArgumentException(strErrorMessage);
                        throw new GroupException(ErrorCode, strErrorMessage);
                    }
                    new GroupNMLinkSoapWrapper(NexonSN).Execute();
                }
                catch (SPFatalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, ex.Message);
                }
                catch (SPLogicalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, ex.Message);
                }
                catch (GroupException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new GroupException(9000, ex.Message);
                }
            }
        }

        internal static int Create(int n4ServerCode, int NexonSN, long CharacterSN, string CharacterName, string GuildName, string GuildID, string GuildIntro, GroupCreateMemberType MemberType, GroupCreateMemberJoin MemberJoinType, GroupCreateMemberJoin MemberJoinType_Web, GroupMenuDefaultMenu DefaultMenu, GroupCreateSkin Skin)
        {
            if (GroupPlatform.isOverSea)
            {
                try
                {
                    if (MemberJoinType == null)
                        throw new NotImplementedException("NotImplementedException : MemberJoin Type is null");
                    GroupCreateResult groupCreateResult = new Nexon.Com.Group.Game.Wrapper.GroupBase.DAO.OverSea.GroupCreateSPWrapper()
                    {
                        GameCode = GroupPlatform.GameCode,
                        ServerCode = n4ServerCode,
                        GuildName = GuildName,
                        GuildID = string.Format("{0}{1}", (object)DateTime.Now.ToString("yyyyMMddhhmm"), (object)GuildID),
                        Introduction = GuildIntro,
                        NexonSN = NexonSN,
                        CharacterSN = ((int)CharacterSN),
                        CharacterName = CharacterName,
                        RegularMemberAdmissionType = MemberJoinType.codeAdmission,
                        AssociateMemberAdmissionType = (byte)2,
                        codeMarkStatus = (byte)3,
                        codeGuildType = (byte)1
                    }.Execute();
                    if (GroupPlatform.Logging)
                        FileLog.CreateLog("", string.Format("GroupDataLog_{0}.log", (object)DateTime.Today.ToString("yyyyMMddHHMM")), new List<string>()
            {
              "Create",
              groupCreateResult.AlreadyJoinGroupSN.ToString(),
              groupCreateResult.GuildSN.ToString(),
              "----------------------------"
            });
                    return groupCreateResult.GuildSN;
                }
                catch (SPFatalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, ex.Message);
                }
                catch (SPLogicalException ex)
                {
                    if (ex.SPErrorCode == 24)
                        throw new GroupException(ex.SPErrorCode, "The guild contains a prohibited word.");
                    throw new GroupException(ex.SPErrorCode, "A system error has occurred.");
                }
                catch (GroupException ex)
                {
                    throw ex;
                }
                catch (ArgumentException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new GroupException(9000, ex.Message);
                }
            }
            else
            {
                int GroupSN = 0;
                int ErrorCode = 0;
                string strErrorMessage = string.Empty;
                try
                {
                    using (Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes heroes = new Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes())
                        ErrorCode = heroes.Create(n4ServerCode, NexonSN, CharacterSN, CharacterName, GuildName, GuildID, GuildIntro, out GroupSN, out strErrorMessage);
                    if (ErrorCode != 0)
                    {
                        if (ErrorCode == 1)
                            throw new ArgumentException(strErrorMessage);
                        throw new GroupException(ErrorCode, strErrorMessage);
                    }
                    new GroupNMLinkSoapWrapper(NexonSN).Execute();
                    return GroupSN;
                }
                catch (SPFatalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, ex.Message);
                }
                catch (SPLogicalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, ex.Message);
                }
                catch (GroupException ex)
                {
                    throw ex;
                }
                catch (ArgumentException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new GroupException(9000, ex.Message);
                }
            }
        }

        internal static Nexon.Com.Group.Game.Wrapper.GroupInfo GetInfo(int n4ServerCode, int? GuildSN, string GuildName)
        {
            if (GroupPlatform.isOverSea)
            {
                try
                {
                    Nexon.Com.Group.Game.Wrapper.GroupBase.DAO.OverSea.GroupGetInfoSPWrapper getInfoSpWrapper = new Nexon.Com.Group.Game.Wrapper.GroupBase.DAO.OverSea.GroupGetInfoSPWrapper()
                    {
                        GameCode = GroupPlatform.GameCode,
                        ServerCode = n4ServerCode
                    };
                    if (GuildSN.HasValue)
                    {
                        getInfoSpWrapper.GuildSN = GuildSN.Value;
                    }
                    else
                    {
                        if (GuildName == null || GuildName.Length <= 0)
                            throw new ArgumentException("ArgumentException : GuildSN is null or GuildName is String.Empty or null");
                        getInfoSpWrapper.GuildName = GuildName;
                    }
                    GroupGetInfoResult groupGetInfoResult = getInfoSpWrapper.Execute();
                    if (GroupPlatform.Logging)
                        FileLog.CreateLog("", string.Format("GroupDataLog_{0}.log", (object)DateTime.Today.ToString("yyyyMMddHHMM")), new List<string>()
            {
              "GetInfo",
              groupGetInfoResult.Info.GuildID,
              groupGetInfoResult.Info.GuildSN.ToString(),
              "----------------------------"
            });
                    return groupGetInfoResult.Info;
                }
                catch (SPFatalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, ex.Message);
                }
                catch (SPLogicalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, ex.Message);
                }
                catch (GroupException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new GroupException(9000, ex.Message);
                }
            }
            else
            {
                new Code().Code_Group(GroupPlatform.GameCode, n4ServerCode);
                try
                {
                    int ErrorCode = 0;
                    string strErrorMessage = string.Empty;
                    Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.GroupInfo Info;
                    using (Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes heroes = new Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes())
                    {
                        if (GuildSN.HasValue)
                        {
                            ErrorCode = heroes.GroupGetInfoByGuildSN(n4ServerCode, GuildSN.Value, out strErrorMessage, out Info);
                        }
                        else
                        {
                            if (GuildName == null || GuildName.Length <= 0)
                                throw new ArgumentException("ArgumentException : GuildSN is null or GuildName is String.Empty or null");
                            ErrorCode = heroes.GroupGetInfoByGuildName(n4ServerCode, GuildName, out strErrorMessage, out Info);
                        }
                    }
                    if (ErrorCode != 0)
                    {
                        if (ErrorCode == 1)
                            throw new ArgumentException(strErrorMessage);
                        throw new GroupException(ErrorCode, strErrorMessage);
                    }
                    Nexon.Com.Group.Game.Wrapper.GroupInfo groupInfo = new Nexon.Com.Group.Game.Wrapper.GroupInfo();
                    if (Info != null)
                    {
                        groupInfo.CharacterSN_Master = Info.CharacterSN_Master;
                        groupInfo.dtCreateDate = Info.dtCreateDate;
                        groupInfo.GuildID = Info.GuildID;
                        groupInfo.GuildName = Info.GuildName;
                        groupInfo.GuildSN = Info.GuildSN;
                        groupInfo.Intro = Info.Intro;
                        groupInfo.NameInGroup_Master = Info.NameInGroup_Master;
                        groupInfo.NexonID_Master = Info.NexonID_Master;
                        groupInfo.NexonSN_Master = Info.NexonSN_Master;
                        groupInfo.RealUserCount = Info.RealUserCount;
                    }
                    return groupInfo;
                }
                catch (ArgumentException ex)
                {
                    throw ex;
                }
                catch (SPFatalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, ex.Message);
                }
                catch (SPLogicalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, ex.Message);
                }
                catch (GroupException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new GroupException(9000, ex.Message);
                }
            }
        }

        internal static Collection<Nexon.Com.Group.Game.Wrapper.GroupInfo> GetList(int n4ServerCode, byte OrderType, int n4PageNo, byte n1PageSize, string strName, string strNameInGroup_Master_Search, out int n4TotalRowCount)
        {
            if (GroupPlatform.isOverSea)
            {
                try
                {
                    n4TotalRowCount = 0;
                    Nexon.Com.Group.Game.Wrapper.GroupBase.DAO.OverSea.GroupGetListSPWrapper getListSpWrapper = new Nexon.Com.Group.Game.Wrapper.GroupBase.DAO.OverSea.GroupGetListSPWrapper()
                    {
                        GameCode = GroupPlatform.GameCode,
                        ServerCode = n4ServerCode,
                        n4PageNo = n4PageNo,
                        n1PageSize = n1PageSize
                    };
                    if (!string.IsNullOrEmpty(strNameInGroup_Master_Search))
                        getListSpWrapper.strMasterCharacterName_Search = strNameInGroup_Master_Search;
                    if (!string.IsNullOrEmpty(strName))
                        getListSpWrapper.GuildName_Search = strName;
                    GroupGetListResult groupGetListResult = getListSpWrapper.Execute();
                    if (GroupPlatform.Logging)
                    {
                        List<string> arrLogElements = new List<string>()
            {
              "GetList"
            };
                        foreach (Nexon.Com.Group.Game.Wrapper.GroupInfo group in groupGetListResult.GroupList)
                        {
                            arrLogElements.Add(group.GuildID);
                            arrLogElements.Add(group.GuildSN.ToString());
                            arrLogElements.Add("----------------------------");
                        }
                        FileLog.CreateLog("", string.Format("GroupDataLog_{0}.log", (object)DateTime.Today.ToString("yyyyMMddHHMM")), arrLogElements);
                    }
                    n4TotalRowCount = groupGetListResult.TotalRowCount;
                    return groupGetListResult.GroupList;
                }
                catch (SPFatalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, ex.Message);
                }
                catch (SPLogicalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, ex.Message);
                }
                catch (GroupException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new GroupException(9000, ex.Message);
                }
            }
            else
            {
                new Code().Code_Group(GroupPlatform.GameCode, n4ServerCode);
                try
                {
                    int ErrorCode = 0;
                    string strErrorMessage = string.Empty;
                    Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.GroupInfo[] GroupList;
                    using (Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes heroes = new Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes())
                    {
                        if (strNameInGroup_Master_Search != null && strNameInGroup_Master_Search != string.Empty)
                        {
                            ErrorCode = heroes.GroupGetListByGuildMaster(n4ServerCode, OrderType, n4PageNo, n1PageSize, strNameInGroup_Master_Search, out strErrorMessage, out GroupList, out n4TotalRowCount);
                        }
                        else
                        {
                            if (strName == null || !(strName != string.Empty))
                                throw new ArgumentException("ArgumentException : strNameInGroup_Master_Search or strName is null or String.Empty");
                            ErrorCode = heroes.GroupGetListByGuildName(n4ServerCode, OrderType, n4PageNo, n1PageSize, strName, out strErrorMessage, out GroupList, out n4TotalRowCount);
                        }
                    }
                    if (ErrorCode != 0)
                    {
                        if (ErrorCode == 1)
                            throw new ArgumentException(strErrorMessage);
                        throw new GroupException(ErrorCode, strErrorMessage);
                    }
                    Collection<Nexon.Com.Group.Game.Wrapper.GroupInfo> collection = new Collection<Nexon.Com.Group.Game.Wrapper.GroupInfo>();
                    if (GroupList != null)
                    {
                        foreach (Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.GroupInfo groupInfo in GroupList)
                            collection.Add(new Nexon.Com.Group.Game.Wrapper.GroupInfo()
                            {
                                CharacterSN_Master = groupInfo.CharacterSN_Master,
                                dtCreateDate = groupInfo.dtCreateDate,
                                RealUserCount = groupInfo.RealUserCount,
                                NexonSN_Master = groupInfo.NexonSN_Master,
                                NexonID_Master = groupInfo.NexonID_Master,
                                NameInGroup_Master = groupInfo.NameInGroup_Master,
                                Intro = groupInfo.Intro,
                                GuildSN = groupInfo.GuildSN,
                                GuildName = groupInfo.GuildName,
                                GuildID = groupInfo.GuildID
                            });
                    }
                    return collection;
                }
                catch (SPFatalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, ex.Message);
                }
                catch (SPLogicalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, ex.Message);
                }
                catch (GroupException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new GroupException(9000, ex.Message);
                }
            }
        }

        internal static Collection<Nexon.Com.Group.Game.Wrapper.GroupMemberInfo> GroupMemberGetList(int n4ServerCode, int GuildSN, int PageNo, byte PageSize, Nexon.Com.Group.Game.Wrapper.GroupUserType StartUserType, Nexon.Com.Group.Game.Wrapper.GroupUserType EndUserType, bool isAsc, out int RowCount, out int TotalRowCount)
        {
            RowCount = 0;
            TotalRowCount = 0;
            if (GroupPlatform.isOverSea)
            {
                try
                {
                    GroupUserGetListResult userGetListResult = new GroupMemberGetListSPWrapper()
                    {
                        GuildSN = GuildSN,
                        GameCode = GroupPlatform.GameCode,
                        ServerCode = n4ServerCode,
                        PageNo = PageNo,
                        PageSize = PageSize
                    }.Execute();
                    RowCount = userGetListResult.RowCount;
                    TotalRowCount = userGetListResult.TotalRowCount;
                    if (GroupPlatform.Logging)
                    {
                        List<string> arrLogElements = new List<string>()
            {
              "GroupMemberGetList"
            };
                        foreach (Nexon.Com.Group.Game.Wrapper.GroupMemberInfo userInfo in userGetListResult.UserInfoList)
                        {
                            arrLogElements.Add(userInfo.CharacterName);
                            arrLogElements.Add(userInfo.GuildSN.ToString());
                            arrLogElements.Add("----------------------------");
                        }
                        FileLog.CreateLog("", string.Format("GroupDataLog_{0}.log", (object)DateTime.Today.ToString("yyyyMMddHHMM")), arrLogElements);
                    }
                    return userGetListResult.UserInfoList;
                }
                catch (SPFatalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, ex.Message);
                }
                catch (SPLogicalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, ex.Message);
                }
                catch (GroupException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new GroupException(9000, ex.Message);
                }
            }
            else
            {
                new Code().Code_Group(GroupPlatform.GameCode, n4ServerCode);
                try
                {
                    int ErrorCode = 0;
                    string strErrorMessage = string.Empty;
                    Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.GroupMemberInfo[] MemberList;
                    using (Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes heroes = new Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes())
                        ErrorCode = heroes.AllMemberGetList(n4ServerCode, GuildSN, PageNo, PageSize, isAsc, out strErrorMessage, out MemberList, out RowCount, out TotalRowCount);
                    if (ErrorCode != 0)
                    {
                        if (ErrorCode == 1)
                            throw new ArgumentException(strErrorMessage);
                        throw new GroupException(ErrorCode, strErrorMessage);
                    }
                    Collection<Nexon.Com.Group.Game.Wrapper.GroupMemberInfo> collection = new Collection<Nexon.Com.Group.Game.Wrapper.GroupMemberInfo>();
                    if (MemberList != null)
                    {
                        foreach (Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.GroupMemberInfo groupMemberInfo in MemberList)
                            collection.Add(new Nexon.Com.Group.Game.Wrapper.GroupMemberInfo()
                            {
                                CharacterName = groupMemberInfo.CharacterName,
                                CharacterSN = groupMemberInfo.CharacterSN,
                                dtLastLoginDate = groupMemberInfo.dtLastLoginDate,
                                emGroupUserType = (Nexon.Com.Group.Game.Wrapper.GroupUserType)Enum.Parse(typeof(Nexon.Com.Group.Game.Wrapper.GroupUserType), groupMemberInfo.emGroupUserType.ToString(), true),
                                GuildSN = groupMemberInfo.GuildSN,
                                Intro = groupMemberInfo.Intro,
                                NameInGroup = groupMemberInfo.NameInGroup,
                                NexonID = groupMemberInfo.NexonID,
                                NexonSN = groupMemberInfo.NexonSN
                            });
                    }
                    return collection;
                }
                catch (SPFatalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, ex.Message);
                }
                catch (SPLogicalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, ex.Message);
                }
                catch (GroupException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new GroupException(9000, ex.Message);
                }
            }
        }

        public static Nexon.Com.Group.Game.Wrapper.GroupMemberInfo GroupMemberInfo(int n4ServerCode, int GuildSN, int CharacterSN, string CharacterName)
        {
            if (GroupPlatform.isOverSea)
            {
                try
                {
                    GroupUserGetInfoResult userGetInfoResult = new GroupMemberGetInfoSPWrapper()
                    {
                        GameCode = GroupPlatform.GameCode,
                        ServerCode = n4ServerCode,
                        GuildSN = GuildSN,
                        CharacterSN = CharacterSN,
                        CharacterName = CharacterName
                    }.Execute();
                    if (GroupPlatform.Logging)
                        FileLog.CreateLog("", string.Format("GroupDataLog_{0}.log", (object)DateTime.Today.ToString("yyyyMMddHHMM")), new List<string>()
            {
              "GroupMemberInfo",
              userGetInfoResult.Info.CharacterName,
              userGetInfoResult.Info.GuildSN.ToString(),
              "----------------------------"
            });
                    return userGetInfoResult.Info;
                }
                catch (SPFatalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, ex.Message);
                }
                catch (SPLogicalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, ex.Message);
                }
                catch (GroupException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new GroupException(9000, ex.Message);
                }
            }
            else
            {
                new Code().Code_Group(GroupPlatform.GameCode, n4ServerCode);
                try
                {
                    int ErrorCode = 0;
                    string strErrorMessage = string.Empty;
                    Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.GroupMemberInfo MemberInfo;
                    using (Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes heroes = new Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes())
                        ErrorCode = heroes.GroupMemberGetInfo(n4ServerCode, GuildSN, CharacterSN, CharacterName, out strErrorMessage, out MemberInfo);
                    if (ErrorCode != 0)
                    {
                        if (ErrorCode == 1)
                            throw new ArgumentException(strErrorMessage);
                        throw new GroupException(ErrorCode, strErrorMessage);
                    }
                    if (MemberInfo == null)
                        return new Nexon.Com.Group.Game.Wrapper.GroupMemberInfo();
                    return new Nexon.Com.Group.Game.Wrapper.GroupMemberInfo()
                    {
                        CharacterName = MemberInfo.CharacterName,
                        CharacterSN = MemberInfo.CharacterSN,
                        dtLastLoginDate = MemberInfo.dtLastLoginDate,
                        emGroupUserType = (Nexon.Com.Group.Game.Wrapper.GroupUserType)Enum.Parse(typeof(Nexon.Com.Group.Game.Wrapper.GroupUserType), MemberInfo.emGroupUserType.ToString(), true),
                        GuildSN = MemberInfo.GuildSN,
                        Intro = MemberInfo.Intro,
                        NameInGroup = MemberInfo.NameInGroup,
                        NexonID = MemberInfo.NexonID,
                        NexonSN = MemberInfo.NexonSN
                    };
                }
                catch (SPFatalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, ex.Message);
                }
                catch (SPLogicalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, ex.Message);
                }
                catch (GroupException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new GroupException(9000, ex.Message);
                }
            }
        }

        internal static Nexon.Com.Group.Game.Wrapper.GroupUserInfo GroupUserGetInfo(int n4ServerCode, int NexonSN)
        {
            if (GroupPlatform.isOverSea)
                return new Nexon.Com.Group.Game.Wrapper.GroupUserInfo();
            new Code().Code_Group(GroupPlatform.GameCode, n4ServerCode);
            try
            {
                int ErrorCode = 0;
                string strErrorMessage = string.Empty;
                Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.GroupUserInfo UserInfo;
                using (Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes heroes = new Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes())
                    ErrorCode = heroes.GroupUserGetInfo(n4ServerCode, NexonSN, out strErrorMessage, out UserInfo);
                if (ErrorCode != 0)
                {
                    if (ErrorCode == 1)
                        throw new ArgumentException(strErrorMessage);
                    throw new GroupException(ErrorCode, strErrorMessage);
                }
                if (UserInfo == null)
                    return new Nexon.Com.Group.Game.Wrapper.GroupUserInfo();
                return new Nexon.Com.Group.Game.Wrapper.GroupUserInfo()
                {
                    CharacterName = UserInfo.CharacterName,
                    dtLastLoginTimeDate = UserInfo.dtLastLoginTimeDate,
                    emGroupUserType = (Nexon.Com.Group.Game.Wrapper.GroupUserType)Enum.Parse(typeof(Nexon.Com.Group.Game.Wrapper.GroupUserType), UserInfo.emGroupUserType.ToString(), true),
                    GuildID = UserInfo.GuildID,
                    GuildIntro = UserInfo.GuildIntro,
                    GuildName = UserInfo.GuildName,
                    GuildSN = UserInfo.GuildSN,
                    NameInGroup_Master = UserInfo.NameInGroup_Master,
                    NameInGroup_User = UserInfo.NameInGroup_User,
                    NexonSN = UserInfo.NexonSN,
                    NexonSN_Master = UserInfo.NexonSN_Master,
                    RealUserCount = UserInfo.RealUserCount
                };
            }
            catch (SPFatalException ex)
            {
                throw new GroupException(ex.SPErrorCode, "Group User GetInfo Fail");
            }
            catch (SPLogicalException ex)
            {
                throw new GroupException(ex.SPErrorCode, "Group User GetInfo Fail");
            }
            catch (GroupException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new GroupException(9000, ex.Message);
            }
        }

        internal static void UserApply(int n4ServerCode, int GuildSN, int NexonSN, int CharacterSN, string CharacterName, int maskGameCode)
        {
            if (GroupPlatform.isOverSea)
            {
                try
                {
                    DateTime dtCreateDate = new WatingMemberGetInfoSPWrapper()
                    {
                        ServerCode = n4ServerCode,
                        NexonSN = NexonSN,
                        GameCode = GroupPlatform.GameCode,
                        GuildSN = GuildSN,
                        CharacterSN = CharacterSN
                    }.Execute().dtCreateDate;
                    new WatingMemberJoinSPWrapper()
                    {
                        GuildSN = GuildSN,
                        GameCode = GroupPlatform.GameCode,
                        CharacterSN = CharacterSN.Parse<int>(0),
                        NexonSN = NexonSN,
                        ServerCode = n4ServerCode,
                        CreateDate = dtCreateDate
                    }.Execute();
                }
                catch (SPFatalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, "Group UserJoin Fail");
                }
                catch (SPLogicalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, "Group UserJoin Fail");
                }
                catch (GroupException ex)
                {
                    throw ex;
                }
                catch (ArgumentException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new GroupException(9000, ex.Message);
                }
            }
            else
            {
                try
                {
                    int ErrorCode = 0;
                    string strErrorMessage = string.Empty;
                    using (Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes heroes = new Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes())
                        ErrorCode = heroes.UserJoinApply(n4ServerCode, GuildSN, NexonSN, CharacterSN, CharacterName, out strErrorMessage);
                    if (ErrorCode != 0)
                    {
                        if (ErrorCode == 1)
                            throw new ArgumentException(strErrorMessage);
                        throw new GroupException(ErrorCode, strErrorMessage);
                    }
                    new GroupNMLinkSoapWrapper(NexonSN).Execute();
                }
                catch (SPFatalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, "Group UserApply Fail");
                }
                catch (SPLogicalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, "Group UserApply Fail");
                }
                catch (GroupException ex)
                {
                    throw ex;
                }
                catch (ArgumentException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new GroupException(9000, ex.Message);
                }
            }
        }

        internal static Collection<Nexon.Com.Group.Game.Wrapper.UserGroupInfo> UserGroupGetList(int n4ServerCode, int NexonSN, int PageNo, byte PageSize, Nexon.Com.Group.Game.Wrapper.GroupUserType StartUserType, Nexon.Com.Group.Game.Wrapper.GroupUserType EndUserType, out int RowCount, out int TotalRowCount)
        {
            RowCount = 0;
            TotalRowCount = 0;
            if (GroupPlatform.isOverSea)
            {
                try
                {
                    UserGroupGetListResult groupGetListResult = new UserGetGuildListSPWrapper()
                    {
                        n4NexonSN = NexonSN
                    }.Execute();
                    RowCount = groupGetListResult.RowCount;
                    TotalRowCount = groupGetListResult.TotalRowCount;
                    if (GroupPlatform.Logging)
                    {
                        List<string> arrLogElements = new List<string>()
            {
              "UserGroupGetList"
            };
                        foreach (Nexon.Com.Group.Game.Wrapper.UserGroupInfo info in groupGetListResult.InfoList)
                        {
                            arrLogElements.Add(info.GuildID);
                            arrLogElements.Add(info.GuildSN.ToString());
                            arrLogElements.Add("----------------------------");
                        }
                        FileLog.CreateLog("", string.Format("GroupDataLog_{0}.log", (object)DateTime.Today.ToString("yyyyMMddHHMM")), arrLogElements);
                    }
                    return groupGetListResult.InfoList;
                }
                catch (SPFatalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, ex.Message);
                }
                catch (SPLogicalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, ex.Message);
                }
                catch (GroupException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new GroupException(9000, ex.Message);
                }
            }
            else
            {
                new Code().Code_Group(GroupPlatform.GameCode, n4ServerCode);
                try
                {
                    int ErrorCode = 0;
                    string strErrorMessage = string.Empty;
                    Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.UserGroupInfo[] UserGroupList;
                    using (Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes heroes = new Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes())
                        ErrorCode = heroes.UserGroupGetList(n4ServerCode, NexonSN, PageNo, PageSize, out strErrorMessage, out UserGroupList, out RowCount, out TotalRowCount);
                    if (ErrorCode != 0)
                    {
                        if (ErrorCode == 1)
                            throw new ArgumentException(strErrorMessage);
                        throw new GroupException(ErrorCode, strErrorMessage);
                    }
                    Collection<Nexon.Com.Group.Game.Wrapper.UserGroupInfo> collection = new Collection<Nexon.Com.Group.Game.Wrapper.UserGroupInfo>();
                    if (UserGroupList == null || UserGroupList.Length == 0)
                        return collection;
                    foreach (Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.UserGroupInfo userGroupInfo in UserGroupList)
                        collection.Add(new Nexon.Com.Group.Game.Wrapper.UserGroupInfo()
                        {
                            CharacterName = userGroupInfo.CharacterName,
                            CharacterSN = userGroupInfo.CharacterSN,
                            dateCreate = userGroupInfo.dateCreate,
                            dtLastContentUpdateDate = userGroupInfo.dtLastContentUpdateDate,
                            GroupUserType = (Nexon.Com.Group.Game.Wrapper.GroupUserType)Enum.Parse(typeof(Nexon.Com.Group.Game.Wrapper.GroupUserType), userGroupInfo.GroupUserType.ToString(), true),
                            GuildID = userGroupInfo.GuildID,
                            GuildName = userGroupInfo.GuildName,
                            GuildSN = userGroupInfo.GuildSN,
                            Intro = userGroupInfo.Intro,
                            NameInGroup = userGroupInfo.NameInGroup,
                            NameInGroup_Master = userGroupInfo.NameInGroup_Master,
                            NeoxnSN_Master = userGroupInfo.NeoxnSN_Master,
                            NexonID_Master = userGroupInfo.NexonID_Master,
                            RealUserCOunt = userGroupInfo.RealUserCOunt
                        });
                    return collection;
                }
                catch (SPFatalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, ex.Message);
                }
                catch (SPLogicalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, ex.Message);
                }
                catch (GroupException ex)
                {
                    throw ex;
                }
                catch (ArgumentException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new GroupException(9000, ex.Message);
                }
            }
        }

        internal static void UserJoin(int n4ServerCode, int GuildSN, int NexonSN, int maskGameCode, long CharacterSN, string CharacterName, bool OpenAge, bool OpenArea, bool OpenBirthDay, bool OpenName, bool OpenPhoneNumber, bool OpenSchool, string Answer1, string Answer2, string Answer3, string Answer4, string Answer5, string Intro, string QuizAnswer)
        {
            if (GroupPlatform.isOverSea)
            {
                try
                {
                    try
                    {
                        FileLog.CreateLog("", string.Format("GroupJoin_{0}.log", (object)DateTime.Today.ToString("yyyyMMddHHMM")), new List<string>()
            {
              string.Format("GuildSN={0}", (object) GuildSN),
              string.Format("GameCode={0}", (object) GroupPlatform.GameCode),
              string.Format("CharacterSN={0}", (object) CharacterSN.Parse<int>(0)),
              string.Format("CharacterName={0}", (object) CharacterName),
              string.Format("NexonSN={0}", (object) NexonSN),
              string.Format("Intro={0}", (object) Intro),
              string.Format("n4ServerCode={0}", (object) n4ServerCode)
            });
                    }
                    catch (Exception)
                    {
                    }
                    new WatingMemberCreateSPWrapper()
                    {
                        GuildSN = GuildSN,
                        GameCode = GroupPlatform.GameCode,
                        CharacterSN = CharacterSN.Parse<int>(0),
                        CharacterName = CharacterName,
                        NexonSN = NexonSN,
                        Introduction = Intro,
                        ServerCode = n4ServerCode,
                        MemberType = (byte)2
                    }.Execute();
                }
                catch (SPFatalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, "Group UserJoin Fail");
                }
                catch (SPLogicalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, "Group UserJoin Fail");
                }
                catch (GroupException ex)
                {
                    throw ex;
                }
                catch (ArgumentException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new GroupException(9000, ex.Message);
                }
            }
            else
            {
                new Code().Code_Group(GroupPlatform.GameCode, n4ServerCode);
                try
                {
                    int ErrorCode = 0;
                    string strErrorMessage = string.Empty;
                    using (Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes heroes = new Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes())
                        ErrorCode = heroes.UserJoin(n4ServerCode, GuildSN, NexonSN, CharacterSN, CharacterName, Intro, out strErrorMessage);
                    if (ErrorCode != 0)
                    {
                        if (ErrorCode == 1)
                            throw new ArgumentException(strErrorMessage);
                        throw new GroupException(ErrorCode, strErrorMessage);
                    }
                    new GroupNMLinkSoapWrapper(NexonSN).Execute();
                }
                catch (SPFatalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, ex.Message);
                }
                catch (SPLogicalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, ex.Message);
                }
                catch (GroupException ex)
                {
                    throw ex;
                }
                catch (ArgumentException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new GroupException(9000, ex.Message);
                }
            }
        }

        internal static void UserReject(int n4ServerCode, int GuildSN, int NexonSN, int CharacterSN, string CharacterName, int maskGameCode)
        {
            if (GroupPlatform.isOverSea)
            {
                try
                {
                    Nexon.Com.Group.Game.Wrapper.GroupBase.Group.UserReject(n4ServerCode, GuildSN, NexonSN, CharacterSN, CharacterName, maskGameCode, DateTime.MinValue);
                }
                catch (SPFatalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, "Group UserReject Fail");
                }
                catch (SPLogicalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, "Group UserReject Fail");
                }
                catch (GroupException ex)
                {
                    throw ex;
                }
                catch (ArgumentException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new GroupException(9000, ex.Message);
                }
            }
            else
            {
                try
                {
                    int ErrorCode = 0;
                    string strErrorMessage = string.Empty;
                    using (Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes heroes = new Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes())
                        ErrorCode = heroes.UserJoinReject(n4ServerCode, GuildSN, NexonSN, CharacterSN, CharacterName, out strErrorMessage);
                    if (ErrorCode != 0)
                    {
                        if (ErrorCode == 1)
                            throw new ArgumentException(strErrorMessage);
                        throw new GroupException(ErrorCode, strErrorMessage);
                    }
                    new GroupNMLinkSoapWrapper(NexonSN).Execute();
                }
                catch (SPFatalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, "Group UserReject Fail");
                }
                catch (SPLogicalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, "Group UserReject Fail");
                }
                catch (GroupException ex)
                {
                    throw ex;
                }
                catch (ArgumentException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new GroupException(9000, ex.Message);
                }
            }
        }

        private static void UserReject(int n4ServerCode, int GuildSN, int NexonSN, int CharacterSN, string CharacterName, int maskGameCode, DateTime WaitingDate)
        {
            if (GroupPlatform.isOverSea)
            {
                try
                {
                    if (WaitingDate == DateTime.MinValue || WaitingDate == DateTime.MaxValue)
                        WaitingDate = new WatingMemberGetInfoSPWrapper()
                        {
                            ServerCode = n4ServerCode,
                            NexonSN = NexonSN,
                            GameCode = GroupPlatform.GameCode,
                            GuildSN = GuildSN,
                            CharacterSN = CharacterSN
                        }.Execute().dtCreateDate;
                    new WatingMemberRejectSPWrapper()
                    {
                        GuildSN = GuildSN,
                        CharacterSN = CharacterSN,
                        NexonSN = NexonSN,
                        GameCode = GroupPlatform.GameCode,
                        ServerCode = n4ServerCode,
                        dtCraeteDate = WaitingDate
                    }.Execute();
                }
                catch (SPFatalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, "Group UserReject Fail");
                }
                catch (SPLogicalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, "Group UserReject Fail");
                }
                catch (GroupException ex)
                {
                    throw ex;
                }
                catch (ArgumentException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new GroupException(9000, ex.Message);
                }
            }
            else
            {
                try
                {
                    int ErrorCode = 0;
                    string strErrorMessage = string.Empty;
                    using (Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes heroes = new Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes())
                        ErrorCode = heroes.UserTypeModify(n4ServerCode, GuildSN, NexonSN, CharacterSN, CharacterName, Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.GroupUserType.memberSeceded, out strErrorMessage);
                    if (ErrorCode != 0)
                    {
                        if (ErrorCode == 1)
                            throw new ArgumentException(strErrorMessage);
                        throw new GroupException(ErrorCode, strErrorMessage);
                    }
                    new GroupNMLinkSoapWrapper(NexonSN).Execute();
                }
                catch (SPFatalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, "Group UserReject Fail");
                }
                catch (SPLogicalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, "Group UserReject Fail");
                }
                catch (GroupException ex)
                {
                    throw ex;
                }
                catch (ArgumentException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new GroupException(9000, ex.Message);
                }
            }
        }

        internal static void UserSecede(int n4ServerCode, int GuildSN, int NexonSN, int CharacterSN, string CharacterName, int maskGameCode)
        {
            if (GroupPlatform.isOverSea)
            {
                try
                {
                    DateTime WaitingDate = new WatingMemberGetInfoSPWrapper()
                    {
                        ServerCode = n4ServerCode,
                        NexonSN = NexonSN,
                        GameCode = GroupPlatform.GameCode,
                        GuildSN = GuildSN,
                        CharacterSN = CharacterSN
                    }.Execute().dtCreateDate;
                    if (WaitingDate == DateTime.MaxValue)
                        WaitingDate = DateTime.MinValue;
                    if (WaitingDate != DateTime.MinValue)
                        Nexon.Com.Group.Game.Wrapper.GroupBase.Group.UserReject(n4ServerCode, GuildSN, NexonSN, CharacterSN, CharacterName, maskGameCode, WaitingDate);
                    else
                        new GroupMemberRemoveSPWrapper()
                        {
                            GuildSN = GuildSN,
                            GameCode = GroupPlatform.GameCode,
                            CharacterSN = CharacterSN.Parse<int>(0),
                            NexonSN = NexonSN,
                            ServerCode = n4ServerCode
                        }.Execute();
                }
                catch (SPFatalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, "Group UserSecede Fail");
                }
                catch (SPLogicalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, "Group UserSecede Fail");
                }
                catch (GroupException ex)
                {
                    throw ex;
                }
                catch (ArgumentException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new GroupException(9000, ex.Message);
                }
            }
            else
            {
                try
                {
                    int ErrorCode = 0;
                    string strErrorMessage = string.Empty;
                    using (Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes heroes = new Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes())
                        ErrorCode = heroes.UserTypeModify(n4ServerCode, GuildSN, NexonSN, CharacterSN, CharacterName, Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.GroupUserType.memberSeceded, out strErrorMessage);
                    if (ErrorCode != 0)
                    {
                        if (ErrorCode == 1)
                            throw new ArgumentException(strErrorMessage);
                        throw new GroupException(ErrorCode, strErrorMessage);
                    }
                    new GroupNMLinkSoapWrapper(NexonSN).Execute();
                }
                catch (SPFatalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, "Group Secede Fail");
                }
                catch (SPLogicalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, "Group Secede Fail");
                }
                catch (GroupException ex)
                {
                    throw ex;
                }
                catch (ArgumentException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new GroupException(9000, ex.Message);
                }
            }
        }

        internal static void UserTypeModify(int n4ServerCode, int GuildSN, int NexonSN, int CharacterSN, string CharacterName, int maskGameCode, Nexon.Com.Group.Game.Wrapper.GroupUserType emGroupUserType)
        {
            if (GroupPlatform.isOverSea)
            {
                try
                {
                    new GroupMemberModifySPWrpper()
                    {
                        CharacterSN = CharacterSN,
                        GameCode = GroupPlatform.GameCode,
                        ServerCode = n4ServerCode,
                        GuildSN = GuildSN,
                        NexonSN = NexonSN,
                        MemberType = (emGroupUserType == Nexon.Com.Group.Game.Wrapper.GroupUserType.master ? (byte)1 : (emGroupUserType == Nexon.Com.Group.Game.Wrapper.GroupUserType.member_lv1 ? (byte)2 : (emGroupUserType == Nexon.Com.Group.Game.Wrapper.GroupUserType.sysop ? (byte)5 : (byte)3)))
                    }.Execute();
                }
                catch (SPFatalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, string.Format("Group Modify {0} Fail", (object)emGroupUserType));
                }
                catch (SPLogicalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, string.Format("Group Modify {0} Fail", (object)emGroupUserType));
                }
                catch (GroupException ex)
                {
                    throw ex;
                }
                catch (ArgumentException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new GroupException(9000, ex.Message);
                }
            }
            else
            {
                new Code().Code_Group(GroupPlatform.GameCode, n4ServerCode);
                try
                {
                    int ErrorCode = 0;
                    string strErrorMessage = string.Empty;
                    Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.GroupUserType emGroupUserType1 = (Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.GroupUserType)Enum.Parse(typeof(Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.GroupUserType), emGroupUserType.ToString(), true);
                    using (Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes heroes = new Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes())
                        ErrorCode = heroes.UserTypeModify(n4ServerCode, GuildSN, NexonSN, CharacterSN, CharacterName, emGroupUserType1, out strErrorMessage);
                    if (ErrorCode != 0)
                    {
                        if (ErrorCode == 1)
                            throw new ArgumentException(strErrorMessage);
                        throw new GroupException(ErrorCode, strErrorMessage);
                    }
                    new GroupNMLinkSoapWrapper(NexonSN).Execute();
                }
                catch (SPFatalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, string.Format("Group Modify {0} Fail", (object)emGroupUserType));
                }
                catch (SPLogicalException ex)
                {
                    throw new GroupException(ex.SPErrorCode, string.Format("Group Modify {0} Fail", (object)emGroupUserType));
                }
                catch (GroupException ex)
                {
                    throw ex;
                }
                catch (ArgumentException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new GroupException(9000, ex.Message);
                }
            }
        }

        internal static bool UserNameModify(int n4ServerCode, int? GuildSN, int NexonSN, int CharacterSN, string newCharacterName)
        {
            if (!GroupPlatform.isOverSea)
                throw new GroupException(104, "The method \"UserNameModify\" is not supported in korea.");
            try
            {
                if (GuildSN.HasValue)
                {
                    GroupChangeCharacterNameResult characterNameResult = new GroupChangeCharacterNameSPWrapper()
                    {
                        GuildSN = GuildSN.Value,
                        CharacterSN = CharacterSN,
                        GameCode = GroupPlatform.GameCode,
                        ServerCode = n4ServerCode,
                        NexonSN = NexonSN,
                        CharacterName = newCharacterName
                    }.Execute();
                    if (GroupPlatform.Logging)
                        FileLog.CreateLog("", string.Format("GroupDataLog_{0}.log", (object)DateTime.Today.ToString("yyyyMMddHHMM")), new List<string>()
            {
              "UserNameModify(Guild)",
              "----------------------------"
            });
                    return characterNameResult.SPReturnValue == 0;
                }
                GroupCharacterNameChange_HeroesBackOfficeResult backOfficeResult = new GroupCharacterNameChange_HeroesBackOfficeSPWrapper()
                {
                    CharacterSN = CharacterSN,
                    GameCode = GroupPlatform.GameCode,
                    ServerCode = n4ServerCode,
                    NexonSN = NexonSN,
                    NewCharacterName = newCharacterName
                }.Execute();
                if (GroupPlatform.Logging)
                    FileLog.CreateLog("", string.Format("GroupDataLog_{0}.log", (object)DateTime.Today.ToString("yyyyMMddHHMM")), new List<string>()
          {
            "UserNameModify(All)",
            "----------------------------"
          });
                return backOfficeResult.SPReturnValue == 0;
            }
            catch (SPFatalException ex)
            {
                throw new GroupException(ex.SPErrorCode, ex.Message);
            }
            catch (SPLogicalException ex)
            {
                throw new GroupException(ex.SPErrorCode, ex.Message);
            }
            catch (GroupException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new GroupException(9000, ex.Message);
            }
        }

        internal static void MemberLogin(int n4ServerCode, int GuildSN, int NexonSN, int CharacterSN)
        {
            if (!GroupPlatform.isOverSea)
                return;
            try
            {
                new GroupMemberLoginSPWrapper()
                {
                    GuildSN = GuildSN,
                    GameCode = GroupPlatform.GameCode,
                    CharacterSN = CharacterSN,
                    NexonSN = NexonSN,
                    ServerCode = n4ServerCode
                }.Execute();
            }
            catch (SPFatalException ex)
            {
                throw new GroupException(ex.SPErrorCode, "Group Login Fail");
            }
            catch (SPLogicalException ex)
            {
                throw new GroupException(ex.SPErrorCode, "Group Login Fail");
            }
            catch (GroupException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new GroupException(9000, ex.Message);
            }
        }

        internal static bool GroupUserTryJoin(int ServerCode, long CharacterSN, long NexonSN, string CharacterName)
        {
            bool flag = true;
            if (GroupPlatform.isOverSea)
                throw new GroupException(104, "지원하지 않는 기능입니다.");
            try
            {
                using (Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes heroes = new Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes())
                {
                    string strErrorMessage = string.Empty;
                    if (heroes.GroupUserTryJoin(ServerCode, CharacterSN, NexonSN, CharacterName, out strErrorMessage) > 0)
                        flag = false;
                }
            }
            catch (GroupException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Exception exception = ex;
                exception = exception = (Exception)null;
                flag = false;
            }
            return flag;
        }

        internal static bool GroupChangeMaster(int ServerCode, int GuildSN, int NexonSN_master, long CharacterSN_master, string CharacterName_master, Nexon.Com.Group.Game.Wrapper.GroupUserType emGroupUserType_oldMaster)
        {
            bool flag = true;
            if (GroupPlatform.isOverSea)
                throw new GroupException(104, "지원하지 않는 기능입니다.");
            try
            {
                using (Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes heroes = new Nexon.Com.Group.Game.Wrapper.HeroesGameGuild.heroes())
                {
                    string strErrorMessage = string.Empty;
                    new Code().Code_Group(GroupPlatform.GameCode, ServerCode);
                    byte codeGroupUserType_oldMaster;
                    try
                    {
                        codeGroupUserType_oldMaster = (byte)emGroupUserType_oldMaster;
                    }
                    catch
                    {
                        codeGroupUserType_oldMaster = (byte)125;
                    }
                    if (heroes.GroupChangeMaster(ServerCode, GuildSN, (long)NexonSN_master, CharacterSN_master, CharacterName_master, codeGroupUserType_oldMaster, out strErrorMessage) != 0)
                        flag = false;
                }
            }
            catch (GroupException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Exception exception = ex;
                exception = exception = (Exception)null;
                flag = false;
            }
            return flag;
        }

        internal static Collection<Nexon.Com.Group.Game.Wrapper.GroupInfo> GroupSearchGetList(int ServerCode, int PageNo, byte PageSize, byte OrderingType, string GroupName, byte SearchType, out int TotalRowCount)
        {
            TotalRowCount = 0;
            if (GroupPlatform.isOverSea)
                throw new GroupException(104, "지원하지 않는 기능입니다.");
            DataSet ds = new DataSet();
            Collection<Nexon.Com.Group.Game.Wrapper.GroupInfo> collection = new Collection<Nexon.Com.Group.Game.Wrapper.GroupInfo>();
            using (Nexon.Com.Group.Game.Wrapper.hereoes.heroes heroes = new Nexon.Com.Group.Game.Wrapper.hereoes.heroes())
            {
                int maskGameCode_group = new Code().Code_Group(GroupPlatform.GameCode, ServerCode);
                if (heroes.GuildSearchGetList(maskGameCode_group, PageNo, PageSize, GroupName, OrderingType, SearchType, out ds, out TotalRowCount) == 0)
                {
                    if (ds.Tables[0] != null)
                    {
                        foreach (DataRow row in (InternalDataCollectionBase)ds.Tables[0].Rows)
                            collection.Add(new Nexon.Com.Group.Game.Wrapper.GroupInfo()
                            {
                                GuildSN = row["oidUser_group"].Parse<int>(0),
                                GuildID = row["strLocalID"].Parse<string>(string.Empty),
                                RealUserCount = row["n4RealUserCount"].Parse<int>(0),
                                dtCreateDate = row["dateCreate"].Parse<DateTime>(DateTime.MinValue),
                                GuildName = row["strName"].Parse<string>(string.Empty),
                                Intro = row["strIntro"].Parse<string>(string.Empty),
                                NameInGroup_Master = row["strNameInGroup_master"].Parse<string>(string.Empty),
                                NexonID_Master = string.Empty,
                                CharacterSN_Master = 0,
                                NexonSN_Master = row["oidUser_master"].Parse<int>(0)
                            });
                    }
                }
            }
            return collection;
        }
    }
}
