using System;
using System.Data;
using System.Data.SqlClient;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase.DAO.Korea
{
	internal class UserGroupGetListSPWrapper : GroupSPWrapperBase<UserGroupGetListResult>
	{
		public UserGroupGetListSPWrapper()
		{
			this.SPName = "public_User_Group_GetList";
			this.IsRetrieveRecordset = true;
		}

		protected override void GenerateDataEntity(int TableIndex, SqlDataReader dataReader)
		{
			base.Result.InfoList.Add(new UserGroupInfo
			{
				GuildSN = dataReader["oidUser_group"].Parse(0),
				GuildID = dataReader["strLocalID_group"].Parse(string.Empty),
				GuildName = dataReader["strName_group"].Parse(string.Empty),
				Intro = dataReader["strIntro_group"].Parse(string.Empty),
				NeoxnSN_Master = dataReader["oidUser_master"].Parse(0),
				NexonID_Master = dataReader["strLocalID_master"].Parse(string.Empty),
				NameInGroup_Master = dataReader["strNameInGroup_master"].Parse(string.Empty),
				GroupUserType = dataReader["codeGroupUserType"].Parse((GroupUserType)0),
				RealUserCOunt = dataReader["n4RealUserCount"].Parse(0),
				dtLastContentUpdateDate = dataReader["dateLastModifiedMember"].Parse(DateTime.MinValue),
				NameInGroup = dataReader["strNameInGroup"].Parse(string.Empty),
				CharacterName = dataReader["strCharacterName"].Parse(string.Empty),
				dateCreate = dataReader["dateCreate"].Parse(DateTime.MinValue),
				CharacterSN = dataReader["n8CharacterSN"].Parse(0)
			});
		}

		protected override void GenerateOutputParameter()
		{
			base.Result.TotalRowCount = base["n4TotalRowCount"].Parse(0);
		}

		protected override void InitializeParameters()
		{
			base.AddParameter("maskGameCode_user", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("oidUser_user", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("n4PageNo", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("n1PageSize", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("maskGameCode_group_search", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("codeGroupUserType_search_begin", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("codeGroupUserType_search_end", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("isBookmarked_search", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("n4RowCount", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalRowCount", SqlDbType.Int, ParameterDirection.Output);
		}

		internal byte codeGroupUserType_search_begin
		{
			set
			{
				base["codeGroupUserType_search_begin"] = value;
			}
		}

		internal byte codeGroupUserType_search_end
		{
			set
			{
				base["codeGroupUserType_search_end"] = value;
			}
		}

		internal byte IsBookmarked_search
		{
			set
			{
				base["IsBookmarked_search"] = value;
			}
		}

		internal int maskGameCode_group_search
		{
			set
			{
				base["maskGameCode_group_search"] = value;
			}
		}

		internal int maskGameCode_user
		{
			set
			{
				base["maskGameCode_user"] = value;
			}
		}

		internal byte n1PageSize
		{
			set
			{
				base["n1PageSize"] = value;
			}
		}

		internal int n4PageNo
		{
			set
			{
				base["n4PageNo"] = value;
			}
		}

		internal int oidUser_user
		{
			set
			{
				base["oidUser_user"] = value;
			}
		}
	}
}
