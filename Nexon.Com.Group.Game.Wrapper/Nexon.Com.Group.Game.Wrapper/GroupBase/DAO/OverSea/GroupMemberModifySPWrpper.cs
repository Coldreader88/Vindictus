using System;
using System.Data;
using System.Data.SqlClient;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase.DAO.OverSea
{
	internal class GroupMemberModifySPWrpper : GroupSPWrapperBase<GroupMemberModifyResult>
	{
		public GroupMemberModifySPWrpper()
		{
			this.CommandTimeout = GroupPlatform.ConnectionTimeout;
			this.SPName = "gdp_MemberModify";
		}

		protected override void GenerateDataEntity(int TableIndex, SqlDataReader dataReader)
		{
		}

		protected override void GenerateOutputParameter()
		{
		}

		protected override void InitializeParameters()
		{
			base.AddParameter("oidGuild", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("oidCharacter", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("codeGame", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("codeServer", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("oidUser", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("codeMemberType", SqlDbType.TinyInt, ParameterDirection.Input);
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

		internal byte MemberType
		{
			set
			{
				base["codeMemberType"] = value;
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
