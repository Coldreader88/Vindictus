using System;
using System.Data;
using System.Data.SqlClient;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase.DAO.Korea
{
	internal class GroupRemoveSPWrapper : GroupSPWrapperBase<GroupResultBase>
	{
		public GroupRemoveSPWrapper()
		{
			this.SPName = "public_Group_Remove";
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
			base.AddParameter("maskGameCode_group", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("oidUser_group", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("isPermanentRemove", SqlDbType.TinyInt, ParameterDirection.Input);
		}

		internal byte isPermanentRemove
		{
			set
			{
				base["isPermanentRemove"] = value;
			}
		}

		internal int maskGameCode_Group
		{
			set
			{
				base["maskGameCode_Group"] = value;
			}
		}

		internal int oidUser_group
		{
			set
			{
				base["oidUser_group"] = value;
			}
		}
	}
}
