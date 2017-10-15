using System;
using System.Data;
using System.Data.SqlClient;
using Nexon.Com.Group.Game.Wrapper.GroupBase.DataSource;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase.DAO.OverSea
{
	internal class WatingMemberGetInfoSPWrapper : GroupSPWrapperBase<WatingMemberGetInfoResult>
	{
		internal WatingMemberGetInfoSPWrapper()
		{
			this.CommandTimeout = GroupPlatform.ConnectionTimeout;
			this.SPName = "gdp_WaitingMemberGetInfo";
			this.IsRetrieveRecordset = true;
			this.SQLConnectionStringProvider = new GroupMasterConnectionStringProvider();
		}

		protected override void GenerateDataEntity(int TableIndex, SqlDataReader dataReader)
		{
		}

		protected override void GenerateOutputParameter()
		{
			base.Result.dtCreateDate = base["dateCreated"].Parse(DateTime.MinValue);
		}

		protected override void InitializeParameters()
		{
			base.AddParameter("oidCharacter", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("oidUser", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("codeGame", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("codeServer", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("oidGuild", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("strName", SqlDbType.NVarChar, 40, ParameterDirection.Output);
			base.AddParameter("dateCreated", SqlDbType.DateTime, ParameterDirection.Output);
		}

		internal int CharacterSN
		{
			set
			{
				base["oidCharacter"] = value;
			}
		}

		internal int GameCode
		{
			set
			{
				base["codeGame"] = value;
			}
		}

		internal int GuildSN
		{
			set
			{
				base["oidGuild"] = value;
			}
		}

		internal int NexonSN
		{
			set
			{
				base["oidUser"] = value;
			}
		}

		internal int ServerCode
		{
			set
			{
				base["codeServer"] = value;
			}
		}
	}
}
