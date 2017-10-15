using System;
using System.Data;
using System.Data.SqlClient;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase.DAO.Korea
{
	internal class GroupUserSetSPWrapper : GroupSPWrapperBase<GroupUserSetResult>
	{
		public GroupUserSetSPWrapper()
		{
			this.SPName = "public_Group_User_Set";
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
				Name = base["strNameInGroup"].Parse(string.Empty),
				CharacterName = base["strNameInGroup"].Parse(string.Empty),
				emGroupUserType = base["codeGroupUserType_new"].Parse((GroupUserType)0)
			};
		}

		protected override void InitializeParameters()
		{
			base.AddParameter("maskGameCode_group", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("oidUser_group", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("maskGameCode_user", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("oidUser_user", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("maskGameCode", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("strLocalID", SqlDbType.VarChar, 24, ParameterDirection.Input);
			base.AddParameter("strNameInGroup", SqlDbType.NVarChar, 50, ParameterDirection.Input);
			base.AddParameter("strNameInGroup_new", SqlDbType.NVarChar, 50, ParameterDirection.Input);
			base.AddParameter("n8CharacterSN", SqlDbType.BigInt, ParameterDirection.Input);
			base.AddParameter("n4ServerCode", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("codeGroupUserType_new", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("strIntro_new", SqlDbType.NVarChar, 200, ParameterDirection.Input);
			base.AddParameter("strDismissedReason_new", SqlDbType.NVarChar, 200, ParameterDirection.Input);
			base.AddParameter("strAnswer1_new", SqlDbType.NVarChar, 32, ParameterDirection.Input);
			base.AddParameter("strAnswer2_new", SqlDbType.NVarChar, 32, ParameterDirection.Input);
			base.AddParameter("strAnswer3_new", SqlDbType.NVarChar, 32, ParameterDirection.Input);
			base.AddParameter("strAnswer4_new", SqlDbType.NVarChar, 32, ParameterDirection.Input);
			base.AddParameter("strAnswer5_new", SqlDbType.NVarChar, 32, ParameterDirection.Input);
			base.AddParameter("IsOpenName_new", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("IsOpenAge_new", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("IsOpenArea_new", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("IsOpenSchool_new", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("IsOpenPhone_new", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("IsOpenBirthday_new", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("n8GameExp_new", SqlDbType.BigInt, ParameterDirection.Input);
			base.AddParameter("n4GameSkillPoint_new", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("n8GameMoney_new", SqlDbType.BigInt, ParameterDirection.Input);
			base.AddParameter("IsBookmarked_new", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("n1GameLevel", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("strName", SqlDbType.NVarChar, 24, ParameterDirection.InputOutput);
			base.AddParameter("codeSex", SqlDbType.TinyInt, ParameterDirection.InputOutput);
			base.AddParameter("dateBirthday", SqlDbType.DateTime, ParameterDirection.InputOutput);
			base.AddParameter("strNameInGroup_old", SqlDbType.NVarChar, 50, ParameterDirection.Output);
			base.AddParameter("strIntro_old", SqlDbType.NVarChar, 200, ParameterDirection.Output);
			base.AddParameter("codeGroupUserType_old", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("codeGroupUserType_Prev_old", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("dateWaiting_old", SqlDbType.DateTime, ParameterDirection.Output);
			base.AddParameter("dateAccepted_old", SqlDbType.DateTime, ParameterDirection.Output);
			base.AddParameter("dateLastLoginTime_old", SqlDbType.DateTime, ParameterDirection.Output);
			base.AddParameter("datePrevLastLoginTime_old", SqlDbType.DateTime, ParameterDirection.Output);
			base.AddParameter("datePrevPrevLastLoginTime_old", SqlDbType.DateTime, ParameterDirection.Output);
			base.AddParameter("dateDismissed_old", SqlDbType.DateTime, ParameterDirection.Output);
			base.AddParameter("strDismissedReason_old", SqlDbType.NVarChar, 200, ParameterDirection.Output);
			base.AddParameter("strAnswer1_old", SqlDbType.NVarChar, 32, ParameterDirection.Output);
			base.AddParameter("strAnswer2_old", SqlDbType.NVarChar, 32, ParameterDirection.Output);
			base.AddParameter("strAnswer3_old", SqlDbType.NVarChar, 32, ParameterDirection.Output);
			base.AddParameter("strAnswer4_old", SqlDbType.NVarChar, 32, ParameterDirection.Output);
			base.AddParameter("strAnswer5_old", SqlDbType.NVarChar, 32, ParameterDirection.Output);
			base.AddParameter("IsOpenName_old", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("IsOpenAge_old", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("IsOpenArea_old", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("IsOpenSchool_old", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("IsOpenPhone_old", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("IsOpenBirthday_old", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("n8WebExp_old", SqlDbType.BigInt, ParameterDirection.Output);
			base.AddParameter("n4WebRanking_old", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4GameRanking_old", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalLoginCount_old", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalLoginCount_CurrentMonth_old", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalLoginCount_limit3PerDay_old", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalLoginCount_limit3PerDay_CurrentMonth_old", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalAbsenceCount_old", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalAbsenceCount_CurrentMonth_old", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalArticleCreateCount_old", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalArticleCreateCount_CurrentMonth_old", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalReadedArticleCount_old", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalReadedArticleCount_CurrentMonth_old", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalSketchCreateCount_old", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalSketchCreateCount_CurrentMonth_old", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n8TotalArticleReadedCount_old", SqlDbType.BigInt, ParameterDirection.Output);
			base.AddParameter("n8TotalArticleReadedCount_CurrentMonth_old", SqlDbType.BigInt, ParameterDirection.Output);
			base.AddParameter("n4TotalCommentCreateCount_old", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalCommentCreateCount_CurrentMonth_old", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalSimpleCommentCreateCount_old", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalSimpleCommentCreateCount_CurrentMonth_old", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("strName_old", SqlDbType.NVarChar, 50, ParameterDirection.Output);
			base.AddParameter("codeSex_old", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("dateBirthday_old", SqlDbType.DateTime, ParameterDirection.Output);
			base.AddParameter("n8GameExp_old", SqlDbType.BigInt, ParameterDirection.Output);
			base.AddParameter("n4GameSkillPoint_old", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n8GameMoney_old", SqlDbType.BigInt, ParameterDirection.Output);
			base.AddParameter("IsBookmarked_old", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("dateCreate_old", SqlDbType.DateTime, ParameterDirection.Output);
			base.AddParameter("dateLastModified_old", SqlDbType.DateTime, ParameterDirection.Output);
			base.AddParameter("oidUser_alreadyJoinedGroup", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("isNewMember", SqlDbType.TinyInt, ParameterDirection.Output);
		}

		internal byte __forOnlyGroupCreate
		{
			set
			{
				base["__forOnlyGroupCreate"] = value;
			}
		}

		internal byte codeGroupUserType_new
		{
			set
			{
				base["codeGroupUserType_new"] = value;
			}
		}

		internal byte codeSex
		{
			set
			{
				base["codeSex"] = value;
			}
		}

		internal DateTime dateBirthday
		{
			set
			{
				base["dateBirthday"] = value;
			}
		}

		internal byte IsBookmarked_new
		{
			set
			{
				base["IsBookmarked_new"] = value;
			}
		}

		internal byte IsOpenAge_new
		{
			set
			{
				base["IsOpenAge_new"] = value;
			}
		}

		internal byte IsOpenArea_new
		{
			set
			{
				base["IsOpenArea_new"] = value;
			}
		}

		internal byte IsOpenBirthday_new
		{
			set
			{
				base["IsOpenBirthday_new"] = value;
			}
		}

		internal byte IsOpenName_new
		{
			set
			{
				base["IsOpenName_new"] = value;
			}
		}

		internal byte IsOpenPhone_new
		{
			set
			{
				base["IsOpenPhone_new"] = value;
			}
		}

		internal byte IsOpenSchool_new
		{
			set
			{
				base["IsOpenSchool_new"] = value;
			}
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

		internal byte n1GameLevel
		{
			set
			{
				base["n1GameLevel"] = value;
			}
		}

		internal int n4GameSkillPoint_new
		{
			set
			{
				base["n4GameSkillPoint_new"] = value;
			}
		}

		internal int n4ServerCode
		{
			set
			{
				base["n4ServerCode"] = value;
			}
		}

		internal long n8CharacterSN
		{
			set
			{
				base["n8CharacterSN"] = value;
			}
		}

		internal long n8GameExp_new
		{
			set
			{
				base["n8GameExp_new"] = value;
			}
		}

		internal long n8GameMoney_new
		{
			set
			{
				base["n8GameMoney_new"] = value;
			}
		}

		internal int oidUser_group
		{
			set
			{
				base["oidUser_group"] = value;
			}
		}

		internal int oidUser_user
		{
			set
			{
				base["oidUser_user"] = value;
			}
		}

		internal string strAnswer1_new
		{
			set
			{
				base["strAnswer1_new"] = value;
			}
		}

		internal string strAnswer2_new
		{
			set
			{
				base["strAnswer2_new"] = value;
			}
		}

		internal string strAnswer3_new
		{
			set
			{
				base["strAnswer3_new"] = value;
			}
		}

		internal string strAnswer4_new
		{
			set
			{
				base["strAnswer4_new"] = value;
			}
		}

		internal string strAnswer5_new
		{
			set
			{
				base["strAnswer5_new"] = value;
			}
		}

		internal string strDismissedReason_new
		{
			set
			{
				base["strDismissedReason_new"] = value;
			}
		}

		internal string strIntro_new
		{
			set
			{
				base["strIntro_new"] = value;
			}
		}

		internal string strLocalID
		{
			set
			{
				base["strLocalID"] = value.Trim();
			}
		}

		internal string strName
		{
			set
			{
				base["strName"] = value;
			}
		}

		internal string strNameInGroup
		{
			set
			{
				base["strNameInGroup"] = value;
			}
		}

		internal string strNameInGroup_new
		{
			set
			{
				base["strNameInGroup_new"] = value;
			}
		}
	}
}
