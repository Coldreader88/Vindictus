using System;
using System.Data;
using System.Data.SqlClient;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase.DAO.Korea
{
	internal class GroupUserGetInfoSPWrapper : GroupSPWrapperBase<GroupUserGetInfoResult>
	{
		public GroupUserGetInfoSPWrapper()
		{
			this.SPName = "public_Group_User_GetInfo";
			this.IsRetrieveRecordset = false;
		}

		protected override void GenerateDataEntity(int TableIndex, SqlDataReader dataReader)
		{
		}

		protected override void GenerateOutputParameter()
		{
			base.Result.Info = new GroupMemberInfo
			{
				GuildSN = base["oidUser_group"].Parse(0),
				NexonSN = base["oidUser_user"].Parse(0),
				NexonID = base["strLocalID"].Parse(string.Empty),
				NameInGroup = base["strNameInGroup"].Parse(string.Empty),
				CharacterName = base["strCharacterName"].Parse(string.Empty),
				Intro = base["strIntro"].Parse(string.Empty),
				dtLastLoginDate = base["dateLastLoginTime"].Parse(DateTime.MinValue),
				emGroupUserType = base["codeGroupUserType"].Parse((GroupUserType)0),
				CharacterSN = (long)base["n8CharacterSN"].Parse(0)
			};
		}

		protected override void InitializeParameters()
		{
			base.AddParameter("maskGameCode_group", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("oidUser_group", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("maskGameCode_user", SqlDbType.Int, ParameterDirection.InputOutput);
			base.AddParameter("oidUser_user", SqlDbType.Int, ParameterDirection.InputOutput);
			base.AddParameter("strLocalID", SqlDbType.VarChar, 24, ParameterDirection.InputOutput);
			base.AddParameter("maskGameCode", SqlDbType.Int, ParameterDirection.InputOutput);
			base.AddParameter("strNameInGroup", SqlDbType.NVarChar, 50, ParameterDirection.InputOutput);
			base.AddParameter("strIntro", SqlDbType.NVarChar, 200, ParameterDirection.Output);
			base.AddParameter("dateCreate", SqlDbType.DateTime, ParameterDirection.Output);
			base.AddParameter("dateLastModified", SqlDbType.DateTime, ParameterDirection.Output);
			base.AddParameter("dateLastLoginTime", SqlDbType.DateTime, ParameterDirection.Output);
			base.AddParameter("datePrevLastLoginTime", SqlDbType.DateTime, ParameterDirection.Output);
			base.AddParameter("datePrevPrevLastLoginTime", SqlDbType.DateTime, ParameterDirection.Output);
			base.AddParameter("codeGroupUserType", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("codeGroupUserType_Prev", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("dateAccepted", SqlDbType.DateTime, ParameterDirection.Output);
			base.AddParameter("dateWaiting", SqlDbType.DateTime, ParameterDirection.Output);
			base.AddParameter("dateDismissed", SqlDbType.DateTime, ParameterDirection.Output);
			base.AddParameter("strDismissedReason", SqlDbType.NVarChar, 200, ParameterDirection.Output);
			base.AddParameter("strAnswer1", SqlDbType.NVarChar, 32, ParameterDirection.Output);
			base.AddParameter("strAnswer2", SqlDbType.NVarChar, 32, ParameterDirection.Output);
			base.AddParameter("strAnswer3", SqlDbType.NVarChar, 32, ParameterDirection.Output);
			base.AddParameter("strAnswer4", SqlDbType.NVarChar, 32, ParameterDirection.Output);
			base.AddParameter("strAnswer5", SqlDbType.NVarChar, 32, ParameterDirection.Output);
			base.AddParameter("strName", SqlDbType.NVarChar, 24, ParameterDirection.Output);
			base.AddParameter("codeSex", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("dateBirthday", SqlDbType.DateTime, ParameterDirection.Output);
			base.AddParameter("IsOpenName", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("IsOpenAge", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("IsOpenArea", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("IsOpenSchool", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("IsOpenPhone", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("IsOpenBirthday", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("n8WebExp", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4WebRanking", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4GameRanking", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalLoginCount", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalLoginCount_CurrentMonth", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalLoginCount_limit3PerDay", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalLoginCount_limit3PerDay_CurrentMonth", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalAbsenceCount", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalAbsenceCount_CurrentMonth", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalArticleCreateCount", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalArticleCreateCount_CurrentMonth", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalReadedArticleCount", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalReadedArticleCount_CurrentMonth", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalSketchCreateCount", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalSketchCreateCount_CurrentMonth", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n8TotalArticleReadedCount", SqlDbType.BigInt, ParameterDirection.Output);
			base.AddParameter("n8TotalArticleReadedCount_CurrentMonth", SqlDbType.BigInt, ParameterDirection.Output);
			base.AddParameter("n4TotalCommentCreateCount", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalCommentCreateCount_CurrentMonth", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalSimpleCommentCreateCount", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalSimpleCommentCreateCount_CurrentMonth", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n8GameExp", SqlDbType.BigInt, ParameterDirection.Output);
			base.AddParameter("n4GameSkillPoint", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n8GameMoney", SqlDbType.BigInt, ParameterDirection.Output);
			base.AddParameter("IsBookmarked", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("n1GameLevel", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("strCharacterName", SqlDbType.NVarChar, 50, ParameterDirection.Output);
			base.AddParameter("n8CharacterSN", SqlDbType.BigInt, ParameterDirection.InputOutput);
		}

		internal string CharacterName
		{
			set
			{
				base["strNameInGroup"] = value;
			}
		}

		internal int CharacterSN
		{
			set
			{
				base["n8CharacterSN"] = value;
			}
		}

		internal int GuildSN
		{
			set
			{
				base["oidUser_Group"] = value;
			}
		}

		internal int maskGameCode_group
		{
			set
			{
				base["maskGameCode_group"] = value;
			}
		}
	}
}
