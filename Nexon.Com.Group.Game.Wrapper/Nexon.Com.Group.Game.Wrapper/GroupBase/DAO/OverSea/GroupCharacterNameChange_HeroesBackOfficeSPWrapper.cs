using System;
using System.Data;
using System.Data.SqlClient;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase.DAO.OverSea
{
	internal class GroupCharacterNameChange_HeroesBackOfficeSPWrapper : GroupSPWrapperBase<GroupCharacterNameChange_HeroesBackOfficeResult>
	{
		public GroupCharacterNameChange_HeroesBackOfficeSPWrapper()
		{
			this.SPName = "gdp_CharacterNameChange_HeroesBackOffice";
			this.IsRetrieveRecordset = false;
		}

		protected override void GenerateDataEntity(int TableIndex, SqlDataReader dataReader)
		{
		}

		protected override void GenerateOutputParameter()
		{
		}

		protected override void InitializeParameters()
		{
			base.AddParameter("codeGame", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("codeServer", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("oidCharacter", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("oidUser", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("strCharacterName", SqlDbType.NVarChar, 50, ParameterDirection.Input);
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

		internal int CharacterSN
		{
			set
			{
				base["oidCharacter"] = value;
			}
		}

		internal int NexonSN
		{
			set
			{
				base["oidUser"] = value;
			}
		}

		internal string NewCharacterName
		{
			set
			{
				base["strCharacterName"] = value;
			}
		}
	}
}
