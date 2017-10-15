using System;
using System.Data;
using System.Data.SqlClient;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase.DAO.OverSea
{
	internal class GroupGetInfoSPWrapper : GroupSPWrapperBase<GroupGetInfoResult>
	{
		public GroupGetInfoSPWrapper()
		{
			this.SPName = "gdp_GuildGetInfo";
			this.IsRetrieveRecordset = false;
		}

		protected override void GenerateDataEntity(int TableIndex, SqlDataReader dataReader)
		{
		}

		protected override void GenerateOutputParameter()
		{
			base.Result.Info = new GroupInfo
			{
				GuildSN = base["oidGuild"].Parse(0),
				GuildName = base["strName"].Parse(string.Empty),
				GuildID = base["strID"].Parse(string.Empty),
				Intro = base["strIntroduction"].Parse(string.Empty),
				NexonSN_Master = base["oidUser"].Parse(0),
				CharacterSN_Master = base["oidCharacter"].Parse(0),
				RealUserCount = base["n4TotalRegularMember"].Parse(0),
				dtCreateDate = base["dateCreated"].Parse(DateTime.MinValue),
				NameInGroup_Master = base["strCharacterName"].Parse(string.Empty)
			};
		}

		protected override void InitializeParameters()
		{
			base.AddParameter("codeGame", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("codeServer", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("oidGuild", SqlDbType.Int, ParameterDirection.InputOutput);
			base.AddParameter("strName", SqlDbType.NVarChar, 50, ParameterDirection.InputOutput);
			base.AddParameter("strID", SqlDbType.VarChar, 24, ParameterDirection.InputOutput);
			base.AddParameter("codeGuildStatus", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("strNotice", SqlDbType.NVarChar, 2000, ParameterDirection.Output);
			base.AddParameter("strIntroduction", SqlDbType.NVarChar, 2000, ParameterDirection.Output);
			base.AddParameter("dateCreated", SqlDbType.DateTime, ParameterDirection.Output);
			base.AddParameter("dateLastModified", SqlDbType.DateTime, ParameterDirection.Output);
			base.AddParameter("dateLastVisited", SqlDbType.DateTime, ParameterDirection.Output);
			base.AddParameter("codeMarkStatus", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("oidUser", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("oidCharacter", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("strCharacterName", SqlDbType.NVarChar, 50, ParameterDirection.Output);
			base.AddParameter("n4TotalRegularMember", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalAssociateMember", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("codeRegularMemberAdmissionType", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("codeAssociateMemberAdmissionType", SqlDbType.TinyInt, ParameterDirection.Output);
		}

		internal int GameCode
		{
			set
			{
				base["codeGame"] = value;
			}
		}

		internal string GuildID
		{
			set
			{
				base["strID"] = value;
			}
		}

		internal string GuildName
		{
			set
			{
				base["strName"] = value;
			}
		}

		internal int GuildSN
		{
			set
			{
				base["oidGuild"] = value;
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
