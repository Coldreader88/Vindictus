using System;
using System.Data;
using System.Data.SqlClient;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase.DAO.OverSea
{
	internal class GroupCreateSPWrapper : GroupSPWrapperBase<GroupCreateResult>
	{
		public GroupCreateSPWrapper()
		{
			this.SPName = "gdp_GuildCreate";
			this.IsRetrieveRecordset = false;
		}

		protected override void GenerateDataEntity(int TableIndex, SqlDataReader dataReader)
		{
		}

		protected override void GenerateOutputParameter()
		{
			base.Result.GuildSN = base["oidGuild"].Parse(0);
		}

		protected override void InitializeParameters()
		{
			base.AddParameter("oidGuild", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("codeGame", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("codeServer", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("strName", SqlDbType.NVarChar, 50, ParameterDirection.Input);
			base.AddParameter("strID", SqlDbType.VarChar, 24, ParameterDirection.Input);
			base.AddParameter("strIntroduction", SqlDbType.NVarChar, 2000, ParameterDirection.Input);
			base.AddParameter("oidUser", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("oidCharacter", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("strCharacterName", SqlDbType.NVarChar, 50, ParameterDirection.Input);
			base.AddParameter("codeGuildType", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("codeMarkStatus", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("codeRegularMemberAdmissionType", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("codeAssociateMemberAdmissionType", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("strOperatorGroupName", SqlDbType.NVarChar, 50, ParameterDirection.Input);
			base.AddParameter("strLeaderGroupName", SqlDbType.NVarChar, 50, ParameterDirection.Input);
		}

		public byte AssociateMemberAdmissionType
		{
			set
			{
				base["codeAssociateMemberAdmissionType"] = value;
			}
		}

		public string CharacterName
		{
			set
			{
				base["strCharacterName"] = value;
			}
		}

		public int CharacterSN
		{
			set
			{
				base["oidCharacter"] = value;
			}
		}

		public byte codeGuildType
		{
			set
			{
				base["codeGuildType"] = value;
			}
		}

		public byte codeMarkStatus
		{
			set
			{
				base["codeMarkStatus"] = value;
			}
		}

		public int GameCode
		{
			set
			{
				base["codeGame"] = value;
			}
		}

		public string GuildID
		{
			set
			{
				base["strID"] = value;
			}
		}

		public string GuildName
		{
			set
			{
				base["strName"] = value;
			}
		}

		public string Introduction
		{
			set
			{
				base["strIntroduction"] = value;
			}
		}

		public string LeaderGroupName
		{
			set
			{
				base["strLeaderGroupName"] = value;
			}
		}

		public int NexonSN
		{
			set
			{
				base["oidUser"] = value;
			}
		}

		public string OperationGroupName
		{
			set
			{
				base["strOperatorGroupName"] = value;
			}
		}

		public byte RegularMemberAdmissionType
		{
			set
			{
				base["codeRegularMemberAdmissionType"] = value;
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
