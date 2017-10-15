using System;
using System.Data;
using System.Data.SqlClient;
using Nexon.Com.Group.Game.Wrapper.GroupBase.DataSource;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase.DAO.OverSea
{
	internal class GroupGetListSPWrapper : GroupSPWrapperBase<GroupGetListResult>
	{
		public GroupGetListSPWrapper()
		{
			this.SPName = "gdp_GuildGetList2";
			this.IsRetrieveRecordset = true;
			this.SQLConnectionStringProvider = new GroupMasterConnectionStringProvider();
		}

		protected override void GenerateDataEntity(int TableIndex, SqlDataReader dataReader)
		{
			if (TableIndex == 0)
			{
				base.Result.GroupList.Add(new GroupInfo
				{
					GuildSN = dataReader["oidGuild"].Parse(0),
					GuildName = dataReader["strName"].Parse(string.Empty),
					GuildID = dataReader["strID"].Parse(string.Empty),
					NexonSN_Master = dataReader["oidUser_master"].Parse(0),
					CharacterSN_Master = dataReader["oidCharacter_master"].Parse(0),
					NameInGroup_Master = dataReader["strCharacterName_master"].Parse(string.Empty),
					RealUserCount = dataReader["n4TotalRegularMember"].Parse(0),
					dtCreateDate = dataReader["dateCreated"].Parse(DateTime.MinValue)
				});
			}
		}

		protected override void GenerateOutputParameter()
		{
			base.Result.TotalRowCount = base["n4TotalRowCount"].Parse(0);
		}

		protected override void InitializeParameters()
		{
			base.AddParameter("codeGame", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("codeServer", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("strGuildName_Search", SqlDbType.NVarChar, 50, ParameterDirection.Input);
			base.AddParameter("strMasterCharacterName_Search", SqlDbType.NVarChar, 50, ParameterDirection.Input);
			base.AddParameter("n1PageSize", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("n4PageNo", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("n4TotalRowCount", SqlDbType.Int, ParameterDirection.Output);
		}

		internal int GameCode
		{
			set
			{
				base["codeGame"] = value;
			}
		}

		internal int ServerCode
		{
			set
			{
				base["codeServer"] = value;
			}
		}

		internal string GuildName_Search
		{
			set
			{
				base["strGuildName_Search"] = value;
			}
		}

		internal string strMasterCharacterName_Search
		{
			set
			{
				base["strMasterCharacterName_Search"] = value;
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
	}
}
