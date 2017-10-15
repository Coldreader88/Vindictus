using System;
using System.Data;
using System.Data.SqlClient;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase.DAO.Korea
{
	internal class UserGroupGetInfoSPWrapper : GroupSPWrapperBase<UserGroupGetInfoResult>
	{
		public UserGroupGetInfoSPWrapper()
		{
			this.SPName = "public_User_Group_GetInfo";
			this.IsRetrieveRecordset = false;
		}

		protected override void GenerateDataEntity(int TableIndex, SqlDataReader dataReader)
		{
		}

		protected override void GenerateOutputParameter()
		{
			base.Result.UserInfo = new GroupUserInfo
			{
				NexonSN = base["oidUser_user"].Parse(0),
				NameInGroup_User = base["strNameInGroup_user"].Parse(string.Empty),
				GuildSN = base["oidUser_group"].Parse(0),
				GuildID = base["strLocalID_group"].Parse(string.Empty),
				GuildName = base["strName_group"].Parse(string.Empty),
				GuildIntro = base["strIntro_group"].Parse(string.Empty),
				NexonSN_Master = base["oidUser_master"].Parse(0),
				NameInGroup_Master = base["strNameInGroup_master"].Parse(string.Empty),
				emGroupUserType = base["codeGroupUserType"].Parse((GroupUserType)0),
				RealUserCount = base["n4RealUserCount"].Parse(0),
				dtLastLoginTimeDate = base["dateLastLoginTime"].Parse(DateTime.MinValue),
				CharacterName = base["strCharacterName"].Parse(string.Empty)
			};
		}

		protected override void InitializeParameters()
		{
			base.AddParameter("maskGameCode_user", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("oidUser_user", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("maskGameCode_group", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("maskGameCode", SqlDbType.Int, ParameterDirection.InputOutput);
			base.AddParameter("strNameInGroup_user", SqlDbType.NVarChar, 50, ParameterDirection.InputOutput);
			base.AddParameter("oidUser_group", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("strLocalID_group", SqlDbType.VarChar, 24, ParameterDirection.Output);
			base.AddParameter("strName_group", SqlDbType.NVarChar, 50, ParameterDirection.Output);
			base.AddParameter("strIntro_group", SqlDbType.NVarChar, 200, ParameterDirection.Output);
			base.AddParameter("maskGameCode_master", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("oidUser_master", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("strLocalID_master", SqlDbType.VarChar, 24, ParameterDirection.Output);
			base.AddParameter("strNameInGroup_master", SqlDbType.NVarChar, 50, ParameterDirection.Output);
			base.AddParameter("codeGroupUserType", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("n1Grade_group", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("n4RealUserCount", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("dateLastContentUpdated", SqlDbType.DateTime, ParameterDirection.Output);
			base.AddParameter("n4WebRanking", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4GameRanking", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("dateAccepted", SqlDbType.DateTime, ParameterDirection.Output);
			base.AddParameter("dateLastLoginTime", SqlDbType.DateTime, ParameterDirection.Output);
			base.AddParameter("strAnswer1", SqlDbType.NVarChar, 32, ParameterDirection.Output);
			base.AddParameter("strAnswer2", SqlDbType.NVarChar, 32, ParameterDirection.Output);
			base.AddParameter("strAnswer3", SqlDbType.NVarChar, 32, ParameterDirection.Output);
			base.AddParameter("strAnswer4", SqlDbType.NVarChar, 32, ParameterDirection.Output);
			base.AddParameter("strAnswer5", SqlDbType.NVarChar, 32, ParameterDirection.Output);
			base.AddParameter("IsOpenName", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("IsOpenAge", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("IsOpenArea", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("IsOpenSchool", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("IsOpenPhone", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("IsOpenBirthday", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("strIntro", SqlDbType.NVarChar, 200, ParameterDirection.Output);
			base.AddParameter("strCharacterName", SqlDbType.NVarChar, 50, ParameterDirection.Output);
		}

		internal int maskGameCode
		{
			set
			{
				base["maskGameCode"] = value;
			}
		}

		internal int maskGameCode_group
		{
			set
			{
				base["maskGameCode_group"] = value;
			}
		}

		internal int maskGameCode_user
		{
			set
			{
				base["maskGameCode_user"] = value;
			}
		}

		internal int oidUser_user
		{
			set
			{
				base["oidUser_user"] = value;
			}
		}

		internal string strNameInGroup_user
		{
			set
			{
				base["strNameInGroup_user"] = value;
			}
		}
	}
}
