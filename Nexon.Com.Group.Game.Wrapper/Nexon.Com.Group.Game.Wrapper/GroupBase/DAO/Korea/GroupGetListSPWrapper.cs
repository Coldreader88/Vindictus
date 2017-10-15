using System;
using System.Data;
using System.Data.SqlClient;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase.DAO.Korea
{
	internal class GroupGetListSPWrapper : GroupSPWrapperBase<GroupGetListResult>
	{
		internal GroupGetListSPWrapper()
		{
			this.SPName = "public_Group_GetList";
			this.IsRetrieveRecordset = true;
		}

		protected override void GenerateDataEntity(int TableIndex, SqlDataReader dataReader)
		{
			base.Result.GroupList.Add(new GroupInfo
			{
				GuildSN = dataReader["oidUser_group"].Parse(0),
				GuildID = dataReader["strLocalID"].Parse(string.Empty),
				GuildName = dataReader["strName"].Parse(string.Empty),
				Intro = dataReader["strIntro"].Parse(string.Empty),
				NexonSN_Master = dataReader["oidUser_master"].Parse(0),
				NexonID_Master = dataReader["strLocalID_master"].Parse(string.Empty),
				NameInGroup_Master = dataReader["strNameInGroup_master"].Parse(string.Empty),
				RealUserCount = dataReader["n4RealUserCount"].Parse(0),
				dtCreateDate = dataReader["dateCreate"].Parse(DateTime.MinValue)
			});
		}

		protected override void GenerateOutputParameter()
		{
			base.Result.TotalRowCount = base["n4TotalRowCount"].Parse(0);
		}

		protected override void InitializeParameters()
		{
			base.AddParameter("maskGameCode_group", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("codeOrderingMethod", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("n4PageNo", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("n1PageSize", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("strNameInGroup_master_search", SqlDbType.VarChar, 50, ParameterDirection.Input);
			base.AddParameter("strName_search", SqlDbType.VarChar, 50, ParameterDirection.Input);
			base.AddParameter("n4TotalRowCount", SqlDbType.Int, ParameterDirection.Output);
		}

		internal byte codeOrderingMethod
		{
			set
			{
				base["codeOrderingMethod"] = value;
			}
		}

		internal string GuildName_search
		{
			set
			{
				base["strName_search"] = value;
			}
		}

		internal int maskGameCode_group
		{
			set
			{
				base["maskGameCode_group"] = value;
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

		internal string strNameInGroup_master
		{
			set
			{
				base["strNameInGroup_master_search"] = value;
			}
		}

		internal string strSearchString_search
		{
			set
			{
				base["strSearchString_search"] = value;
			}
		}
	}
}
