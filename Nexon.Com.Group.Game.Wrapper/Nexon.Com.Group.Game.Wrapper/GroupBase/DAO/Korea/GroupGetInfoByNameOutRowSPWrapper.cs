using System;
using System.Data;
using System.Data.SqlClient;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase.DAO.Korea
{
	internal class GroupGetInfoByNameOutRowSPWrapper : GroupSPWrapperBase<GroupResultBase>
	{
		internal GroupGetInfoByNameOutRowSPWrapper()
		{
			this.SPName = "public_Group_GetInfoByNameOutRow";
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
			base.AddParameter("maskGameCode_Group", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("strName", SqlDbType.NVarChar, 50, ParameterDirection.Input);
		}

		internal int maskGameCode_Group
		{
			set
			{
				base["maskGameCode_Group"] = value;
			}
		}

		internal string strName
		{
			set
			{
				base["strName"] = value;
			}
		}
	}
}
