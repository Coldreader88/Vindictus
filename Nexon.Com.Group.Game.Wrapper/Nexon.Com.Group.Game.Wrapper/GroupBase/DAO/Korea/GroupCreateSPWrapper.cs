using System;
using System.Data;
using System.Data.SqlClient;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase.DAO.Korea
{
	internal class GroupCreateSPWrapper : GroupSPWrapperBase<GroupCreateResult>
	{
		internal GroupCreateSPWrapper()
		{
			this.SPName = "public_Group_Create";
			this.IsRetrieveRecordset = false;
		}

		protected override void GenerateDataEntity(int TableIndex, SqlDataReader dataReader)
		{
		}

		protected override void GenerateOutputParameter()
		{
			base.Result.GuildSN = base["oidUser_group"].Parse(0);
			base.Result.AlreadyJoinGroupSN = base["oidUser_alreadyJoinedGroup"].Parse(0);
		}

		protected override void InitializeParameters()
		{
			base.AddParameter("maskGameCode_group", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("strLocalID", SqlDbType.VarChar, 24, ParameterDirection.Input);
			base.AddParameter("strName", SqlDbType.NVarChar, 50, ParameterDirection.Input);
			base.AddParameter("maskGameCode", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("maskGameCode_master", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("oidUser_master", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("strLocalID_master", SqlDbType.VarChar, 24, ParameterDirection.Input);
			base.AddParameter("strNameInGroup_master", SqlDbType.NVarChar, 50, ParameterDirection.Input);
			base.AddParameter("n4MasterServerCode", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("n8MasterCharacterSN", SqlDbType.BigInt, ParameterDirection.Input);
			base.AddParameter("strName_master", SqlDbType.NVarChar, 50, ParameterDirection.Input);
			base.AddParameter("codeSex_master", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("dateBirthday_master", SqlDbType.DateTime, ParameterDirection.Input);
			base.AddParameter("codeAdmissionType", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("IsUseNote", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("strQuiz_Question", SqlDbType.NVarChar, 255, ParameterDirection.Input);
			base.AddParameter("strQuiz_Answer", SqlDbType.NVarChar, 255, ParameterDirection.Input);
			base.AddParameter("IsRequiredName", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("IsRequiredAge", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("IsRequiredSchool", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("IsRequiredArea", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("IsRequiredTel", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("IsRequiredBirthday", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("strIntro", SqlDbType.NVarChar, 200, ParameterDirection.Input);
			base.AddParameter("IsAutoPromoteUser", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("n1PointOfArticleCreate", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("n1PointOfArticleView", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("n1PointOfCommentCreate", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("n1PointOfVisit", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("n1PointOfAbsence", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("oidItemTemplate2_skin", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("oidCoverImage", SqlDbType.BigInt, ParameterDirection.Input);
			base.AddParameter("strCoverIntro", SqlDbType.NVarChar, 500, ParameterDirection.Input);
			base.AddParameter("IsVisibleCoverStory", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("IsVisibleRecentArticles", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("IsVisibleMyRecentArticles", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("IsVisibleRecentImage", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("IsVisibleOnlineUser", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("IsVisibleCalendar", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("IsVisibleSchedule", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("IsVisiblePoll", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("IsVisibleGameBBS", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("IsDefinedGroupSkin", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("IsFullGroupSkin", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("strGroupSkin_BGImage", SqlDbType.NVarChar, 255, ParameterDirection.Input);
			base.AddParameter("strGroupSkin_BGColor", SqlDbType.NVarChar, 24, ParameterDirection.Input);
			base.AddParameter("strGroupSkin_UpperImage", SqlDbType.NVarChar, 255, ParameterDirection.Input);
			base.AddParameter("strGroupSkin_UpperColor", SqlDbType.NVarChar, 24, ParameterDirection.Input);
			base.AddParameter("strGroupSkin_TitleColor", SqlDbType.NVarChar, 24, ParameterDirection.Input);
			base.AddParameter("strGroupSkin_LoginBoxColor", SqlDbType.NVarChar, 24, ParameterDirection.Input);
			base.AddParameter("strGroupSkin_LoginBoxUpperColor", SqlDbType.NVarChar, 24, ParameterDirection.Input);
			base.AddParameter("strGroupSkin_LoginBottomColor", SqlDbType.NVarChar, 24, ParameterDirection.Input);
			base.AddParameter("strGroupSkin_LeftMenuColor", SqlDbType.NVarChar, 24, ParameterDirection.Input);
			base.AddParameter("strGroupSkin_BBSTitleColor", SqlDbType.NVarChar, 24, ParameterDirection.Input);
			base.AddParameter("strGroupSkin_MidBarColor", SqlDbType.NVarChar, 24, ParameterDirection.Input);
			base.AddParameter("codeGroupSkinStyle", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("strUserTypeName_master", SqlDbType.NVarChar, 50, ParameterDirection.Input);
			base.AddParameter("strUserTypeName_gwleader", SqlDbType.NVarChar, 50, ParameterDirection.Input);
			base.AddParameter("strUserTypeName_manager", SqlDbType.NVarChar, 50, ParameterDirection.Input);
			base.AddParameter("strUserTypeName_gwmember", SqlDbType.NVarChar, 50, ParameterDirection.Input);
			base.AddParameter("strUserTypeName_Level5", SqlDbType.NVarChar, 50, ParameterDirection.Input);
			base.AddParameter("strUserTypeName_Level4", SqlDbType.NVarChar, 50, ParameterDirection.Input);
			base.AddParameter("strUserTypeName_Level3", SqlDbType.NVarChar, 50, ParameterDirection.Input);
			base.AddParameter("strUserTypeName_Level2", SqlDbType.NVarChar, 50, ParameterDirection.Input);
			base.AddParameter("strUserTypeName_Level1", SqlDbType.NVarChar, 50, ParameterDirection.Input);
			base.AddParameter("strMenuName_ForGameNotice", SqlDbType.NVarChar, 50, ParameterDirection.Input);
			base.AddParameter("strMenuName_ForGameUpdate", SqlDbType.NVarChar, 50, ParameterDirection.Input);
			base.AddParameter("strMenuName_ForGameEvent", SqlDbType.NVarChar, 50, ParameterDirection.Input);
			base.AddParameter("oidBoard_ForGameNotice", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("oidBoard_ForGameUpdate", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("oidBoard_ForGameEvent", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("strMenuFontColor", SqlDbType.NVarChar, 50, ParameterDirection.Input);
			base.AddParameter("codeAdmissionType_web", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("IsAutoPromoteUser_web", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("IsUseNote_web", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("strQuiz_Question_web", SqlDbType.NVarChar, 255, ParameterDirection.Input);
			base.AddParameter("strQuiz_Answer_web", SqlDbType.NVarChar, 255, ParameterDirection.Input);
			base.AddParameter("IsRequiredName_web", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("IsRequiredAge_web", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("IsRequiredSchool_web", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("IsRequiredArea_web", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("IsRequiredTel_web", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("IsRequiredBirthday_web", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("oidUser_group", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("oidUser_alreadyJoinedGroup", SqlDbType.Int, ParameterDirection.Output);
		}

		internal void SetMasterInfo(GroupCreateMasterInfo MasterInfo)
		{
			this.maskGameCode = MasterInfo.MaskGameCode;
			this.maskGameCode_master = 65536;
			this.oidUser_master = MasterInfo.NexonSN;
			this.strLocalID_master = MasterInfo.NexonID;
			this.strName_master = MasterInfo.Name;
			this.codeSex_master = MasterInfo.Sex;
		}

        internal void SetMemberJoinType(GroupCreateMemberJoin JoinType)
        {
            IsRequiredName = (byte)(JoinType.IsRequiredName ? 1 : 0);
            IsRequiredAge = (byte)(JoinType.IsRequiredAge ? 1 : 0);
            IsRequiredSchool = (byte)(JoinType.IsRequiredSchool ? 1 : 0);
            IsRequiredArea = (byte)(JoinType.IsRequiredArea ? 1 : 0);
            IsRequiredTel = (byte)(JoinType.IsRequiredTel ? 1 : 0);
            IsRequiredBirthday = (byte)(JoinType.IsRequiredBirthday ? 1 : 0);
            codeAdmissionType = JoinType.codeAdmission;
            if ((JoinType.Question ?? string.Empty) != string.Empty)
            {
                strQuiz_Question = JoinType.Question;
            }
            if ((JoinType.Answer ?? string.Empty) != string.Empty)
            {
                strQuiz_Answer = JoinType.Answer;
            }
            IsUseNote = (byte)(JoinType.UseNote ? 1 : 0);
        }

        internal void SetMemberType(GroupCreateMemberType MemberType)
		{
			if ((MemberType.Master ?? string.Empty) != string.Empty)
			{
				this.strUserTypeName_master = MemberType.Master;
			}
			if ((MemberType.Manager ?? string.Empty) != string.Empty)
			{
				this.strUserTypeName_manager = MemberType.Manager;
			}
			if ((MemberType.Level1 ?? string.Empty) != string.Empty)
			{
				this.strUserTypeName_Level1 = MemberType.Level1;
			}
			if ((MemberType.Level2 ?? string.Empty) != string.Empty)
			{
				this.strUserTypeName_Level2 = MemberType.Level2;
			}
			if ((MemberType.Level3 ?? string.Empty) != string.Empty)
			{
				this.strUserTypeName_Level3 = MemberType.Level3;
			}
			if ((MemberType.Level4 ?? string.Empty) != string.Empty)
			{
				this.strUserTypeName_Level4 = MemberType.Level4;
			}
			if ((MemberType.Level5 ?? string.Empty) != string.Empty)
			{
				this.strUserTypeName_Level5 = MemberType.Level5;
			}
		}

        internal void SetSkin(GroupCreateSkin Skin)
        {
            codeGroupSkinStyle = Skin.GroupSkinSyle;
            IsFullGroupSkin = (byte)(Skin.IsFullGroupSkin ? 1 : 0);
            IsDefinedGroupSkin = (byte)(Skin.IsDefinedGroupSkin ? 1 : 0);
            strGroupSkin_BBSTitleColor = Skin.BBSTitleColor;
            strGroupSkin_BGColor = Skin.BGColor;
            strGroupSkin_BGImage = Skin.BGImage;
            strGroupSkin_LeftMenuColor = Skin.LeftMenuColor;
            strGroupSkin_LoginBottomColor = Skin.LoginBottomColor;
            strGroupSkin_LoginBoxColor = Skin.LoginBoxColor;
            strGroupSkin_LoginBoxUpperColor = Skin.LoginBoxUpperColor;
            strGroupSkin_MidBarColor = Skin.MidBarColor;
            strGroupSkin_TitleColor = Skin.TitleColor;
            strGroupSkin_UpperColor = Skin.UpperColor;
            strGroupSkin_UpperImage = Skin.UpperImage;
            strMenuFontColor = Skin.MenuFontColor;
        }

        internal void SetWebMemberJoinType(GroupCreateMemberJoin JoinType)
        {
            IsRequiredName_web = (byte)(JoinType.IsRequiredName ? 1 : 0);
            IsRequiredAge_web = (byte)(JoinType.IsRequiredAge ? 1 : 0);
            IsRequiredSchool_web = (byte)(JoinType.IsRequiredSchool ? 1 : 0);
            IsRequiredArea_web = (byte)(JoinType.IsRequiredArea ? 1 : 0);
            IsRequiredTel_web = (byte)(JoinType.IsRequiredTel ? 1 : 0);
            IsRequiredBirthday_web = (byte)(JoinType.IsRequiredBirthday ? 1 : 0);
            codeAdmissionType_web = JoinType.codeAdmission;
            strQuiz_Question_web = JoinType.Question;
            strQuiz_Answer_web = JoinType.Answer;
            IsUseNote_web = (byte)(JoinType.UseNote ? 1 : 0);
        }

        internal byte codeAdmissionType
		{
			set
			{
				base["codeAdmissionType"] = value;
			}
		}

		internal byte codeAdmissionType_web
		{
			set
			{
				base["codeAdmissionType_web"] = value;
			}
		}

		internal byte codeGroupSkinStyle
		{
			set
			{
				base["codeGroupSkinStyle"] = value;
			}
		}

		internal byte codeSex_master
		{
			set
			{
				base["codeSex_master"] = value;
			}
		}

		internal DateTime dateBirthday_master
		{
			set
			{
				base["dateBirthday_master"] = value;
			}
		}

		internal byte IsAutoPromoteUser_web
		{
			set
			{
				base["IsAutoPromoteUser_web"] = value;
			}
		}

		internal byte IsDefinedGroupSkin
		{
			set
			{
				base["IsDefinedGroupSkin"] = value;
			}
		}

		internal byte IsFullGroupSkin
		{
			set
			{
				base["IsFullGroupSkin"] = value;
			}
		}

		internal byte IsRequiredAge
		{
			set
			{
				base["IsRequiredAge"] = value;
			}
		}

		internal byte IsRequiredAge_web
		{
			set
			{
				base["IsRequiredAge_web"] = value;
			}
		}

		internal byte IsRequiredArea
		{
			set
			{
				base["IsRequiredArea"] = value;
			}
		}

		internal byte IsRequiredArea_web
		{
			set
			{
				base["IsRequiredArea_web"] = value;
			}
		}

		internal byte IsRequiredBirthday
		{
			set
			{
				base["IsRequiredBirthday"] = value;
			}
		}

		internal byte IsRequiredBirthday_web
		{
			set
			{
				base["IsRequiredBirthday_web"] = value;
			}
		}

		internal byte IsRequiredName
		{
			set
			{
				base["IsRequiredName"] = value;
			}
		}

		internal byte IsRequiredName_web
		{
			set
			{
				base["IsRequiredName_web"] = value;
			}
		}

		internal byte IsRequiredSchool
		{
			set
			{
				base["IsRequiredSchool"] = value;
			}
		}

		internal byte IsRequiredSchool_web
		{
			set
			{
				base["IsRequiredSchool_web"] = value;
			}
		}

		internal byte IsRequiredTel
		{
			set
			{
				base["IsRequiredTel"] = value;
			}
		}

		internal byte IsRequiredTel_web
		{
			set
			{
				base["IsRequiredTel_web"] = value;
			}
		}

		internal byte IsUseNote
		{
			set
			{
				base["IsUseNote"] = value;
			}
		}

		internal byte IsUseNote_web
		{
			set
			{
				base["IsUseNote_web"] = value;
			}
		}

		internal byte IsVisibleCalendar
		{
			set
			{
				base["IsVisibleCalendar"] = value;
			}
		}

		internal byte IsVisibleCoverStory
		{
			set
			{
				base["IsVisibleCoverStory"] = value;
			}
		}

		internal byte IsVisibleGameBBS
		{
			set
			{
				base["IsVisibleGameBBS"] = value;
			}
		}

		internal byte IsVisibleMyRecentArticles
		{
			set
			{
				base["IsVisibleMyRecentArticles"] = value;
			}
		}

		internal byte IsVisibleOnlineUser
		{
			set
			{
				base["IsVisibleOnlineUser"] = value;
			}
		}

		internal byte IsVisiblePoll
		{
			set
			{
				base["IsVisiblePoll"] = value;
			}
		}

		internal byte IsVisibleRecentArticles
		{
			set
			{
				base["IsVisibleRecentArticles"] = value;
			}
		}

		internal byte IsVisibleRecentImage
		{
			set
			{
				base["IsVisibleRecentImage"] = value;
			}
		}

		internal byte IsVisibleSchedule
		{
			set
			{
				base["IsVisibleSchedule"] = value;
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

		internal int maskGameCode_master
		{
			set
			{
				base["maskGameCode_master"] = value;
			}
		}

		internal int n4MasterServerCode
		{
			set
			{
				base["n4MasterServerCode"] = value;
			}
		}

		internal long n8MasterCharacterSN
		{
			set
			{
				base["n8MasterCharacterSN"] = value;
			}
		}

		internal string oidBoard_ForGameEvent
		{
			set
			{
				base["oidBoard_ForGameEvent"] = value;
			}
		}

		internal string oidBoard_ForGameNotice
		{
			set
			{
				base["oidBoard_ForGameNotice"] = value;
			}
		}

		internal string oidBoard_ForGameUpdate
		{
			set
			{
				base["oidBoard_ForGameUpdate"] = value;
			}
		}

		internal int oidUser_master
		{
			set
			{
				base["oidUser_master"] = value;
			}
		}

		internal string strCoverIntro
		{
			set
			{
				base["strCoverIntro"] = value;
			}
		}

		internal string strGroupSkin_BBSTitleColor
		{
			set
			{
				base["strGroupSkin_BBSTitleColor"] = value;
			}
		}

		internal string strGroupSkin_BGColor
		{
			set
			{
				base["strGroupSkin_BGColor"] = value;
			}
		}

		internal string strGroupSkin_BGImage
		{
			set
			{
				base["strGroupSkin_BGImage"] = value;
			}
		}

		internal string strGroupSkin_LeftMenuColor
		{
			set
			{
				base["strGroupSkin_LeftMenuColor"] = value;
			}
		}

		internal string strGroupSkin_LoginBottomColor
		{
			set
			{
				base["strGroupSkin_LoginBottomColor"] = value;
			}
		}

		internal string strGroupSkin_LoginBoxColor
		{
			set
			{
				base["strGroupSkin_LoginBoxColor"] = value;
			}
		}

		internal string strGroupSkin_LoginBoxUpperColor
		{
			set
			{
				base["strGroupSkin_LoginBoxUpperColor"] = value;
			}
		}

		internal string strGroupSkin_MidBarColor
		{
			set
			{
				base["strGroupSkin_MidBarColor"] = value;
			}
		}

		internal string strGroupSkin_TitleColor
		{
			set
			{
				base["strGroupSkin_TitleColor"] = value;
			}
		}

		internal string strGroupSkin_UpperColor
		{
			set
			{
				base["strGroupSkin_UpperColor"] = value;
			}
		}

		internal string strGroupSkin_UpperImage
		{
			set
			{
				base["strGroupSkin_UpperImage"] = value;
			}
		}

		internal string strIntro
		{
			set
			{
				base["strIntro"] = value;
			}
		}

		internal string strLocalID
		{
			set
			{
				base["strLocalID"] = value;
			}
		}

		internal string strLocalID_master
		{
			set
			{
				base["strLocalID_master"] = value;
			}
		}

		internal string strMenuFontColor
		{
			set
			{
				base["strMenuFontColor"] = value;
			}
		}

		internal string strMenuName_ForGameEvent
		{
			set
			{
				base["strMenuName_ForGameEvent"] = value;
			}
		}

		internal string strMenuName_ForGameNotice
		{
			set
			{
				base["strMenuName_ForGameNotice"] = value;
			}
		}

		internal string strMenuName_ForGameUpdate
		{
			set
			{
				base["strMenuName_ForGameUpdate"] = value;
			}
		}

		internal string strName
		{
			set
			{
				base["strName"] = value;
			}
		}

		internal string strName_master
		{
			set
			{
				base["strName_master"] = value;
			}
		}

		internal string strNameInGroup_master
		{
			set
			{
				base["strNameInGroup_master"] = value;
			}
		}

		internal string strQuiz_Answer
		{
			set
			{
				base["strQuiz_Answer"] = value;
			}
		}

		internal string strQuiz_Answer_web
		{
			set
			{
				base["strQuiz_Answer_web"] = value;
			}
		}

		internal string strQuiz_Question
		{
			set
			{
				base["strQuiz_Question"] = value;
			}
		}

		internal string strQuiz_Question_web
		{
			set
			{
				base["strQuiz_Question_web"] = value;
			}
		}

		internal string strUserTypeName_gwleader
		{
			set
			{
				base["strUserTypeName_gwleader"] = value;
			}
		}

		internal string strUserTypeName_gwmember
		{
			set
			{
				base["strUserTypeName_gwmember"] = value;
			}
		}

		internal string strUserTypeName_Level1
		{
			set
			{
				base["strUserTypeName_Level1"] = value;
			}
		}

		internal string strUserTypeName_Level2
		{
			set
			{
				base["strUserTypeName_Level2"] = value;
			}
		}

		internal string strUserTypeName_Level3
		{
			set
			{
				base["strUserTypeName_Level3"] = value;
			}
		}

		internal string strUserTypeName_Level4
		{
			set
			{
				base["strUserTypeName_Level4"] = value;
			}
		}

		internal string strUserTypeName_Level5
		{
			set
			{
				base["strUserTypeName_Level5"] = value;
			}
		}

		internal string strUserTypeName_manager
		{
			set
			{
				base["strUserTypeName_manager"] = value;
			}
		}

		internal string strUserTypeName_master
		{
			set
			{
				base["strUserTypeName_master"] = value;
			}
		}
	}
}
