using System;
using System.Data;
using System.Data.SqlClient;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase.DAO.OverSea
{
	internal class GroupMemberGetInfoSPWrapper : GroupSPWrapperBase<GroupUserGetInfoResult>
	{
		public GroupMemberGetInfoSPWrapper()
		{
			this.SPName = "gdp_MemberGetInfo";
			this.IsRetrieveRecordset = false;
		}

		protected override void GenerateDataEntity(int TableIndex, SqlDataReader dataReader)
		{
		}

		protected override void GenerateOutputParameter()
		{
			int num = base["codeMemberType"].Parse(0);
			base.Result.Info = new GroupMemberInfo
			{
				GuildSN = base["oidGuild"].Parse(0),
				CharacterName = base["strCharacterName"].Parse(string.Empty),
				emGroupUserType = ((num == 1) ? GroupUserType.master : ((num == 2) ? GroupUserType.member_lv1 : ((num == 4) ? GroupUserType.memberWaiting : ((num == 5) ? GroupUserType.sysop : GroupUserType.webmember)))),
				NexonSN = base["oidUser"].Parse(0),
				Intro = base["strIntroduction"].Parse(string.Empty),
				CharacterSN = (long)base["oidCharacter"].Parse(0),
				NameInGroup = base["strCharacterName"].Parse(string.Empty),
				dtLastLoginDate = base["dateLastModified"].Parse(DateTime.MinValue)
			};
		}

		protected override void InitializeParameters()
		{
			base.AddParameter("oidGuild", SqlDbType.Int, ParameterDirection.InputOutput);
			base.AddParameter("codeGame", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("codeServer", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("oidCharacter", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("oidUser", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("strGameCharacterName", SqlDbType.NVarChar, 50, ParameterDirection.Input);
			base.AddParameter("codeMemberType", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("codeMemberGroup", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("strIntroduction", SqlDbType.NVarChar, 2000, ParameterDirection.Output);
			base.AddParameter("dateCreated", SqlDbType.SmallDateTime, ParameterDirection.Output);
			base.AddParameter("dateLastModified", SqlDbType.SmallDateTime, ParameterDirection.Output);
			base.AddParameter("strCharacterName", SqlDbType.NVarChar, 50, ParameterDirection.Output);
		}

		internal string CharacterName
		{
			set
			{
				base["strGameCharacterName"] = value;
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
