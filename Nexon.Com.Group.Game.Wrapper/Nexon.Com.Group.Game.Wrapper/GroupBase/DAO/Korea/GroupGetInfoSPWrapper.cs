using System;
using System.Data;
using System.Data.SqlClient;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase.DAO.Korea
{
	internal class GroupGetInfoSPWrapper : GroupSPWrapperBase<GroupGetInfoResult>
	{
		public GroupGetInfoSPWrapper()
		{
			this.SPName = "public_Group_GetInfo";
			this.IsRetrieveRecordset = false;
		}

		protected override void GenerateDataEntity(int TableIndex, SqlDataReader dataReader)
		{
		}

		protected override void GenerateOutputParameter()
		{
			base.Result.Info = new GroupInfo
			{
				GuildSN = base["oidUser_group"].Parse(0),
				Intro = base["strIntro"].Parse(string.Empty),
				NameInGroup_Master = base["strNameInGroup_master"].Parse(string.Empty),
				GuildName = base["strName"].Parse(string.Empty),
				NexonID_Master = base["strLocalID_master"].Parse(string.Empty),
				GuildID = base["strLocalID"].Parse(string.Empty),
				RealUserCount = base["n4RealUserCount"].Parse(0),
				NexonSN_Master = base["oidUser_master"].Parse(0)
			};
		}

		protected override void InitializeParameters()
		{
			base.AddParameter("maskGameCode_group", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("oidUser_group", SqlDbType.Int, ParameterDirection.InputOutput);
			base.AddParameter("isClosed", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("strLocalID", SqlDbType.VarChar, 24, ParameterDirection.InputOutput);
			base.AddParameter("strIntro", SqlDbType.NVarChar, 200, ParameterDirection.Output);
			base.AddParameter("oidCoverImage", SqlDbType.BigInt, ParameterDirection.Output);
			base.AddParameter("strCoverIntro", SqlDbType.NVarChar, 500, ParameterDirection.Output);
			base.AddParameter("strNameInGroup_master", SqlDbType.NVarChar, 50, ParameterDirection.Output);
			base.AddParameter("strName", SqlDbType.NVarChar, 50, ParameterDirection.Output);
			base.AddParameter("strLocalID_master", SqlDbType.VarChar, 24, ParameterDirection.Output);
			base.AddParameter("n4RealUserCount", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("codeAdmissionType", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("n4TotalLoginCount_CurrentMonth", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalArticleCreateCount_CurrentMonth", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4CountOfWaitingUser", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("dateLastContentUpdated", SqlDbType.DateTime, ParameterDirection.Output);
			base.AddParameter("oidUser_master", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4GameRanking", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalRanking", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4WebRanking", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4GameRanking_prev", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4TotalRanking_prev", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4WebRanking_prev", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n4WebMemberCount", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("n2GameLevel", SqlDbType.SmallInt, ParameterDirection.Output);
			base.AddParameter("n2TotalLevel", SqlDbType.SmallInt, ParameterDirection.Output);
			base.AddParameter("n2WebLevel", SqlDbType.SmallInt, ParameterDirection.Output);
			base.AddParameter("maskGameCode", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("strQuiz_Answer", SqlDbType.NVarChar, 255, ParameterDirection.Output);
			base.AddParameter("n1Grade", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("isRequiredName", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("isRequiredAge", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("isRequiredArea", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("isRequiredSchool", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("isRequiredTel", SqlDbType.TinyInt, ParameterDirection.Output);
			base.AddParameter("isRequiredBirthday", SqlDbType.TinyInt, ParameterDirection.Output);
		}

		internal byte codeAdmissionType
		{
			set
			{
				base["codeAdmissionType"] = value;
			}
		}

		internal int maskGameCode_group
		{
			set
			{
				base["maskGameCode_group"] = value;
			}
		}

		internal int n4CountOfWaitingUser
		{
			set
			{
				base["n4CountOfWaitingUser"] = value;
			}
		}

		internal int n4TotalArticleCreateCount_CurrentMonth
		{
			set
			{
				base["n4TotalArticleCreateCount_CurrentMonth"] = value;
			}
		}

		internal int n4TotalLoginCount_CurrentMonth
		{
			set
			{
				base["n4TotalLoginCount_CurrentMonth"] = value;
			}
		}

		internal int oidCoverImage
		{
			set
			{
				base["oidCoverImage"] = value;
			}
		}

		internal int oidUser_group
		{
			set
			{
				base["oidUser_group"] = value;
			}
		}

		internal int oidUser_master
		{
			set
			{
				base["oidUser_master"] = value;
			}
		}

		internal string strLocalID
		{
			set
			{
				base["strLocalID"] = value;
			}
		}

		internal string strName
		{
			set
			{
				base["strName"] = value;
			}
		}

		internal int strNameInGroup_master
		{
			set
			{
				base["strNameInGroup_master"] = value;
			}
		}
	}
}
