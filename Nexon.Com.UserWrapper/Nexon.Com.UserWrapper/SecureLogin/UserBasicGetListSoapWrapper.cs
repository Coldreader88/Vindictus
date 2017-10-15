using System;
using System.Data;
using System.Web;
using Nexon.Com.DAO;
using Nexon.Com.UserWrapper.UserAPI.SecureLogin;

namespace Nexon.Com.UserWrapper.SecureLogin
{
	internal class UserBasicGetListSoapWrapper : SoapWrapperBase<securelogin, UserBasicGetListSoapResult>
	{
		protected override ServiceCode serviceCode
		{
			get
			{
				return Nexon.Com.ServiceCode.member;
			}
		}

		public UserBasicGetListSoapWrapper()
		{
			this.WebServiceURLProvider = new UserAPIServerURLProvider();
		}

		protected override void ExecuteImpl(out int errorCode, out string errorMessage)
		{
			string nexonID = string.Empty;
			string ssn = string.Empty;
			string strClientIP = (HttpContext.Current != null) ? HttpContext.Current.Request.UserHostAddress : string.Empty;
			DataSet dataSet = null;
			int num = 0;
			bool adminUser = false;
			errorCode = base.Soap.GetUserBasicList(this._n4ServiceCode, this._strSsn, this._isTempUserInclude, strClientIP, out dataSet, out num, out adminUser);
			errorMessage = string.Empty;
			if (errorCode == 0 && dataSet != null && dataSet.Tables.Count > 0)
			{
				DataTable dataTable = null;
				if (dataSet.Tables.Count == 1)
				{
					dataTable = dataSet.Tables[0];
				}
				else
				{
					foreach (object obj in dataSet.Tables)
					{
						DataTable dataTable2 = (DataTable)obj;
						if (dataTable2.TableName.ToLower().IndexOf("_param") == -1)
						{
							dataTable = dataTable2;
							break;
						}
					}
				}
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					int value = dataTable.Rows[i]["oidUser"].Parse(0);
					nexonID = dataTable.Rows[i]["strNexonID"].Parse(string.Empty);
					ssn = dataTable.Rows[i]["strSsn"].Parse(string.Empty);
					base.Result.AddUserBasic(new int?(value), nexonID, null, null, null, null, ssn, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
				}
				base.Result.TotalRowCount = dataTable.Rows.Count;
				base.Result.AdminUser = adminUser;
			}
		}

		internal int ServiceCode
		{
			set
			{
				this._n4ServiceCode = value;
			}
		}

		internal string Ssn
		{
			set
			{
				this._strSsn = value;
			}
		}

		internal bool TempUserInclude
		{
			set
			{
				this._isTempUserInclude = value;
			}
		}

		private int _n4ServiceCode;

		private string _strSsn;

		private bool _isTempUserInclude;
	}
}
