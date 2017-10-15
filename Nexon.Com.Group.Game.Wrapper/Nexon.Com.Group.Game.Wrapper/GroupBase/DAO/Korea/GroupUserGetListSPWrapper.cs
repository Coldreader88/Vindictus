using System;
using System.Data;
using System.Data.SqlClient;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase.DAO.Korea
{
	internal class GroupUserGetListSPWrapper : GroupSPWrapperBase<GroupUserGetListResult>
	{
		internal GroupUserGetListSPWrapper()
		{
			this.SPName = "public_Group_User_GetList";
			this.IsRetrieveRecordset = true;
		}

		protected override void GenerateDataEntity(int TableIndex, SqlDataReader dataReader)
		{
			base.Result.UserInfoList.Add(new GroupMemberInfo
			{
				GuildSN = dataReader["oidUser_group"].Parse(0),
				NexonSN = dataReader["oidUser_user"].Parse(0),
				NexonID = dataReader["strLocalID"].Parse(string.Empty),
				NameInGroup = dataReader["strNameInGroup"].Parse(string.Empty),
				CharacterSN = dataReader["n8CharacterSN"].Parse(0L),
				CharacterName = dataReader["strCharacterName"].Parse(string.Empty),
				Intro = dataReader["strIntro"].Parse(string.Empty),
				dtLastLoginDate = dataReader["dateLastLoginTime"].Parse(DateTime.MinValue),
				emGroupUserType = dataReader["codeGroupUserType"].Parse((GroupUserType)0)
			});
		}

		protected override void GenerateOutputParameter()
		{
			base.Result.RowCount = base["n4RowCount"].Parse(0);
			base.Result.TotalRowCount = base["n4TotalRowCount"].Parse(0);
		}

		protected override void InitializeParameters()
		{
			base.AddParameter("maskGameCode_group", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("oidUser_group", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("n1PageSize", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("n4PageNo", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("strLocalID_search", SqlDbType.VarChar, 24, ParameterDirection.Input);
			base.AddParameter("strNameInGroup_search", SqlDbType.NVarChar, 50, ParameterDirection.Input);
			base.AddParameter("strName_search", SqlDbType.NVarChar, 24, ParameterDirection.Input);
			base.AddParameter("codeGroupUserTypeBegin_search", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("codeGroupUserTypeEnd_search", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("codeOrderingMethod", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("IsDescOrdering", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("n4RowCount", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalRowCount", SqlDbType.Int, ParameterDirection.Output);
		}

		internal byte codeGroupUserTypeBegin_search
		{
			set
			{
				base["codeGroupUserTypeBegin_search"] = value;
			}
		}

		internal byte codeGroupUserTypeEnd_search
		{
			set
			{
				base["codeGroupUserTypeEnd_search"] = value;
			}
		}

		internal byte isDescOrdering
		{
			set
			{
				base["IsDescOrdering"] = value;
			}
		}

		internal int maskGameCode_group
		{
			set
			{
				base["maskGameCode_Group"] = value;
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

		internal int oidUser_group
		{
			set
			{
				base["oidUser_Group"] = value;
			}
		}

		internal string strLocalID_search
		{
			set
			{
				base["strLocalID_search"] = value;
			}
		}

		internal string strName_search
		{
			set
			{
				base["strName_search"] = value;
			}
		}

		internal string strNameInGroup_search
		{
			set
			{
				base["strNameInGroup_search"] = value;
			}
		}
	}
}
