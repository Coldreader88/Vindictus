using System;
using System.Data;
using System.Data.SqlClient;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase.DAO.OverSea
{
	internal class GroupMemberGetListSPWrapper : GroupSPWrapperBase<GroupUserGetListResult>
	{
		public GroupMemberGetListSPWrapper()
		{
			this.CommandTimeout = GroupPlatform.ConnectionTimeout;
			this.SPName = "gdp_MemberGetList";
			this.IsRetrieveRecordset = true;
		}

		protected override void GenerateDataEntity(int TableIndex, SqlDataReader dataReader)
		{
			int num = dataReader["codeMemberType"].Parse(0);
			base.Result.UserInfoList.Add(new GroupMemberInfo
			{
				GuildSN = dataReader["oidGuild"].Parse(0),
				CharacterSN = (long)dataReader["oidCharacter"].Parse(0),
				CharacterName = dataReader["strCharacterName"].Parse(string.Empty),
				NameInGroup = dataReader["strCharacterName"].Parse(string.Empty),
				emGroupUserType = ((num == 1) ? GroupUserType.master : ((num == 2) ? GroupUserType.member_lv1 : ((num == 4) ? GroupUserType.memberWaiting : ((num == 5) ? GroupUserType.sysop : GroupUserType.webmember)))),
				NexonSN = dataReader["oidUser"].Parse(0),
				Intro = dataReader["strIntroduction"].Parse(string.Empty),
				dtLastLoginDate = dataReader["dateLastModified"].Parse(DateTime.MinValue)
			});
		}

		protected override void GenerateOutputParameter()
		{
			base.Result.RowCount = base.Result.UserInfoList.Count;
			base.Result.TotalRowCount = base["n4TotalRowCount"].Parse(0);
		}

		protected override void InitializeParameters()
		{
			base.AddParameter("oidGuild", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("codeGame", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("codeServer", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("codeMemberType_search", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("strCharacterName_search", SqlDbType.NVarChar, 50, ParameterDirection.Input);
			base.AddParameter("n1PageSize", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("n4PageNo", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("n4TotalRowCount", SqlDbType.Int, ParameterDirection.Output);
		}

		public int GameCode
		{
			set
			{
				base["codeGame"] = value;
			}
		}

		public int GuildSN
		{
			set
			{
				base["oidGuild"] = value;
			}
		}

		public byte MemberType
		{
			set
			{
				base["codeMemberType_search"] = value;
			}
		}

		public int PageNo
		{
			set
			{
				base["n4PageNo"] = value;
			}
		}

		public byte PageSize
		{
			set
			{
				base["n1PageSize"] = value;
			}
		}

		public int ServerCode
		{
			set
			{
				base["codeServer"] = value;
			}
		}
	}
}
