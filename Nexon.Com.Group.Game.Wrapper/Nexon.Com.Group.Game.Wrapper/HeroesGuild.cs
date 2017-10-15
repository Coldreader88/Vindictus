using System;
using System.Collections.ObjectModel;
using Nexon.Com.Group.Game.Wrapper.GroupBase;

namespace Nexon.Com.Group.Game.Wrapper
{
	public static class HeroesGuild
	{
		public static Collection<GroupMemberInfo> AllMemberGetList(int ServerCode, int GuildSN, int PageNo, byte PageSize, bool isAscending, out int RowCount, out int TotalRowCount)
		{
			return GroupBase.Group.GroupMemberGetList(ServerCode, GuildSN, PageNo, PageSize, GroupUserType.master, GroupUserType.unknown, isAscending, out RowCount, out TotalRowCount);
		}

		public static void CheckGroupID(int ServerCode, string GuildID)
		{
            GroupBase.Group.CheckGroupID(ServerCode, GuildID);
		}

		public static void CheckGroupName(int ServerCode, string GuildName)
		{
            GroupBase.Group.CheckGroupName(ServerCode, GuildName);
		}

		public static int Create(int ServerCode, int NexonSN, long CharacterSN, string CharacterName, string GuildName, string GuildID, string GuildIntro)
		{
			GroupCreateMemberType memberType = new GroupCreateMemberType(false)
			{
				Master = "마스터",
				Manager = "운영진",
				Level1 = "일반회원"
			};
			GroupMenuDefaultMenu defaultMenu = new GroupMenuDefaultMenu
			{
				GameNotice = "공지사항",
				GameUpdate = "업데이트",
				GameNotice_BoardSN = 0,
				GameUpdate_BoardSN = 0
			};
			GroupCreateSkin skin = new GroupCreateSkin(false)
			{
				GroupSkinSyle = 4,
				IsFullGroupSkin = false,
				IsDefinedGroupSkin = false,
				BBSTitleColor = "#B8B8B8",
				BGColor = "#404040",
				BGImage = "",
				LeftMenuColor = "#EAEAEA",
				LoginBottomColor = "#FFFFFF",
				LoginBoxColor = "#777777",
				LoginBoxUpperColor = "#B8B8B8",
				MidBarColor = "#CACACA",
				TitleColor = "#FFFFFF",
				UpperColor = "#89B13F",
				UpperImage = "gr_top10.gif"
			};
			GroupCreateMemberJoin groupCreateMemberJoin = new GroupCreateMemberJoin
			{
				Answer = string.Empty,
				codeAdmission = 3,
				IsRequiredAge = false,
				IsRequiredArea = false,
				IsRequiredBirthday = false,
				IsRequiredName = false,
				IsRequiredSchool = false,
				IsRequiredTel = false,
				Question = string.Empty,
				UseNote = false
			};
			return GroupBase.Group.Create(ServerCode, NexonSN, CharacterSN, CharacterName, GuildName, GuildID, GuildIntro, memberType, groupCreateMemberJoin, groupCreateMemberJoin, defaultMenu, skin);
		}

		public static GroupInfo GetInfo(int ServerCode, int GuildSN)
		{
			return GroupBase.Group.GetInfo(ServerCode, new int?(GuildSN), null);
		}

		public static GroupInfo GetInfo(int ServerCode, string GuildName)
		{
			return GroupBase.Group.GetInfo(ServerCode, null, GuildName);
		}

		public static Collection<GroupInfo> GroupGetListByGuildMaster(int ServerCode, byte OrderType, int PageNo, byte PageSize, string NameInGroup_Master_Search, out int TotalRowCount)
		{
			return GroupBase.Group.GetList(ServerCode, OrderType, PageNo, PageSize, null, NameInGroup_Master_Search, out TotalRowCount);
		}

		public static Collection<GroupInfo> GroupGetListByGuildName(int ServerCode, byte OrderType, int PageNo, byte PageSize, string GuildName, out int TotalRowCount)
		{
			return GroupBase.Group.GetList(ServerCode, OrderType, PageNo, PageSize, GuildName, null, out TotalRowCount);
		}

		public static Collection<GroupInfo> GroupSearchGetList(int ServerCode, int PageNo, byte PageSize, byte OrderingType, string GroupName, out int TotalRowCount)
		{
			return GroupBase.Group.GroupSearchGetList(ServerCode, PageNo, PageSize, OrderingType, GroupName, 1, out TotalRowCount);
		}

		public static GroupMemberInfo GroupMemberGetInfo(int ServerCode, int GuildSN, int CharacterSN, string CharacterName)
		{
			return GroupBase.Group.GroupMemberInfo(ServerCode, GuildSN, CharacterSN, CharacterName);
		}

		public static GroupUserInfo GroupUserGetInfo(int ServerCode, int NexonSN)
		{
			return GroupBase.Group.GroupUserGetInfo(ServerCode, NexonSN);
		}

		public static void Remove(int ServerCode, int NexonSN, int GuildSN)
		{
            GroupBase.Group.Close(ServerCode, NexonSN, GuildSN);
		}

		public static Collection<UserGroupInfo> UserGroupGetList(int ServerCode, int NexonSN, int PageNo, byte PageSize, out int RowCount, out int TotalRowCount)
		{
			return GroupBase.Group.UserGroupGetList(ServerCode, NexonSN, PageNo, PageSize, GroupUserType.master, GroupUserType.webmemberWaiting, out RowCount, out TotalRowCount);
		}

		public static void UserJoin(int ServerCode, int GuildSN, int NexonSN, long CharacterSN, string CharacterName, string Intro)
		{
            GroupBase.Group.UserJoin(ServerCode, GuildSN, NexonSN, 65536, CharacterSN, CharacterName, false, false, false, false, false, false, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
		}

		public static void UserJoinApply(int ServerCode, int GuildSN, int NexonSN, int CharacterSN, string CharacterName)
		{
            GroupBase.Group.UserApply(ServerCode, GuildSN, NexonSN, CharacterSN, CharacterName, 65536);
		}

		public static void UserJoinReject(int ServerCode, int GuildSN, int NexonSN, int CharacterSN, string CharacterName)
		{
            GroupBase.Group.UserReject(ServerCode, GuildSN, NexonSN, CharacterSN, CharacterName, 65536);
		}

		public static void UserSecede(int ServerCode, int GuildSN, int NexonSN, int CharacterSN, string CharacterName)
		{
            GroupBase.Group.UserSecede(ServerCode, GuildSN, NexonSN, CharacterSN, CharacterName, 65536);
		}

		public static void UserTypeModify(int ServerCode, int GuildSN, int NexonSN, int CharacterSN, string CharacterName, GroupUserType emGroupUserType)
		{
            GroupBase.Group.UserTypeModify(ServerCode, GuildSN, NexonSN, CharacterSN, CharacterName, 65536, emGroupUserType);
		}

		public static bool UserNameModify(int ServerCode, int NexonSN, int CharacterSN, string newCharacterName)
		{
			return GroupBase.Group.UserNameModify(ServerCode, null, NexonSN, CharacterSN, newCharacterName);
		}

		public static void MemberLogin(int ServerCode, int GuildSN, int NexonSN, int CharacterSN)
		{
            GroupBase.Group.MemberLogin(ServerCode, GuildSN, NexonSN, CharacterSN);
		}

		public static bool GroupUserTryJoin(int ServerCode, long CharacterSN, int NexonSN, string CharacterName)
		{
			return GroupBase.Group.GroupUserTryJoin(ServerCode, CharacterSN, (long)NexonSN, CharacterName);
		}

		public static bool GroupChangeMaster(int ServerCode, int GuildSN, int newNexonSN, long newCharacterSN, string newCharacterName, GroupUserType emGroupUserType_oldMaster)
		{
			return GroupBase.Group.GroupChangeMaster(ServerCode, GuildSN, newNexonSN, newCharacterSN, newCharacterName, emGroupUserType_oldMaster);
		}
	}
}
