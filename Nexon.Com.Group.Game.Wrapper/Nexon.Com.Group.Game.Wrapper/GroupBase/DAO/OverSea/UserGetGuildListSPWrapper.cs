using System;
using System.Data;
using System.Data.SqlClient;
using Nexon.Com.Group.Game.Wrapper.GroupBase.DataSource;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase.DAO.OverSea
{
	internal class UserGetGuildListSPWrapper : GroupSPWrapperBase<UserGroupGetListResult>
	{
		internal UserGetGuildListSPWrapper()
		{
			this.CommandTimeout = GroupPlatform.ConnectionTimeout;
			this.SPName = "gdp_UserGetGuildList";
			this.IsRetrieveRecordset = true;
			this.SQLConnectionStringProvider = new GroupMasterConnectionStringProvider();
		}

		protected override void GenerateDataEntity(int TableIndex, SqlDataReader dataReader)
		{
			int num = dataReader["codeMemberType"].Parse(0);
			base.Result.InfoList.Add(new UserGroupInfo
			{
				GuildSN = dataReader["oidGuild"].Parse(0),
				GuildName = dataReader["strName"].Parse(string.Empty),
				GuildID = dataReader["strID"].Parse(string.Empty),
				dateCreate = dataReader["dateCreated"].Parse(DateTime.MinValue),
				dtLastContentUpdateDate = dataReader["dateCreated"].Parse(DateTime.MinValue),
				NeoxnSN_Master = dataReader["oidUser_master"].Parse(0),
				CharacterName = dataReader["strCharacterName"].Parse(string.Empty),
				RealUserCOunt = dataReader["n4TotalRegularMember"].Parse(0),
				NameInGroup = dataReader["strCharacterName"].Parse(string.Empty),
				NameInGroup_Master = dataReader["strCharacterName_master"].Parse(string.Empty),
				GroupUserType = ((num == 1) ? GroupUserType.master : ((num == 2) ? GroupUserType.member_lv1 : ((num == 4) ? GroupUserType.memberWaiting : ((num == 5) ? GroupUserType.sysop : GroupUserType.webmember)))),
				CharacterSN = dataReader["oidCharacter"].Parse(0)
			});
		}

		protected override void GenerateOutputParameter()
		{
			base.Result.RowCount = base.Result.InfoList.Count;
			base.Result.TotalRowCount = base.Result.InfoList.Count;
		}

		protected override void InitializeParameters()
		{
			base.AddParameter("oidUser", SqlDbType.Int, ParameterDirection.Input);
		}

		internal int n4NexonSN
		{
			set
			{
				base["oidUser"] = value;
			}
		}
	}
}
