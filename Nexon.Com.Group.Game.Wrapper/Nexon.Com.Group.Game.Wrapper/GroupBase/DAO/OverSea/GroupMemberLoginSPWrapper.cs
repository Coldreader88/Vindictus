using System;
using System.Data;
using System.Data.SqlClient;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase.DAO.OverSea
{
	internal class GroupMemberLoginSPWrapper : GroupSPWrapperBase<GroupMemberLoginResult>
	{
		internal GroupMemberLoginSPWrapper()
		{
			this.CommandTimeout = GroupPlatform.ConnectionTimeout;
			this.SPName = "gdp_MemberLogin";
		}

		protected override void InitializeParameters()
		{
			base.AddParameter("oidGuild", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("oidCharacter", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("codeGame", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("codeServer", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("oidUser", SqlDbType.Int, ParameterDirection.Input);
		}

		internal int GuildSN
		{
			set
			{
				base["oidGuild"] = value;
			}
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

		internal int ServerCode
		{
			set
			{
				base["codeServer"] = value;
			}
		}

		internal int NexonSN
		{
			set
			{
				base["oidUser"] = value;
			}
		}

		protected override void GenerateDataEntity(int TableIndex, SqlDataReader dataReader)
		{
		}

		protected override void GenerateOutputParameter()
		{
		}
	}
}
