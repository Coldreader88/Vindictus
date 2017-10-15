using System;
using System.Data;
using System.Data.SqlClient;
using Nexon.Com.DAO;

namespace Nexon.Com.Log
{
	internal class LightErrorLogCreateSPWrapper : SPWrapperBase<ErrorLogCreateSPResult>
	{
		protected override ServiceCode serviceCode
		{
			get
			{
				return ServiceCode.framework;
			}
		}

		internal LightErrorLogCreateSPWrapper()
		{
			this.SPName = "lgp_LightErrorLog_Create";
			this.SQLConnectionStringProvider = new ErrorLogConnectionStringProvider();
			this.IsRetrieveRecordset = false;
		}

		protected override void InitializeParameters()
		{
			base.AddParameter("n4ServiceCode", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("n4ErrorCode", SqlDbType.Int, ParameterDirection.Input);
			base.AddParameter("strErrorMessage", SqlDbType.VarChar, 100, ParameterDirection.Input);
			base.AddParameter("strServerIP", SqlDbType.VarChar, 16, ParameterDirection.Input);
			base.AddParameter("strHostName", SqlDbType.VarChar, 40, ParameterDirection.Input);
			base.AddParameter("strRequestUrl", SqlDbType.VarChar, 1024, ParameterDirection.Input);
			base.AddParameter("strClientIP", SqlDbType.VarChar, 16, ParameterDirection.Input);
			base.AddParameter("strLoginID", SqlDbType.VarChar, 24, ParameterDirection.Input);
			base.AddParameter("strUrlReferrer", SqlDbType.VarChar, 1024, ParameterDirection.Input);
			base.AddParameter("strRequestInfo", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input);
			base.AddParameter("strStackTrace", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input);
			base.AddParameter("n1PlatformCode", SqlDbType.TinyInt, ParameterDirection.Input);
			base.AddParameter("strServerName", SqlDbType.VarChar, 20, ParameterDirection.Input);
			base.AddParameter("oidErrorLog", SqlDbType.Int, ParameterDirection.Output);
			base.AddParameter("dateCreate", SqlDbType.DateTime, ParameterDirection.Output);
		}

		internal int n4ServiceCode
		{
			set
			{
				base["n4ServiceCode"] = value;
			}
		}

		internal int n4ErrorCode
		{
			set
			{
				base["n4ErrorCode"] = value;
			}
		}

		internal string strErrorMessage
		{
			set
			{
				base["strErrorMessage"] = value;
			}
		}

		internal string strServerIP
		{
			set
			{
				base["strServerIP"] = value;
			}
		}

		internal string strDomainName
		{
			set
			{
				base["strHostName"] = value;
			}
		}

		internal string strRequestUrl
		{
			set
			{
				base["strRequestUrl"] = value;
			}
		}

		internal string strClientIP
		{
			set
			{
				base["strClientIP"] = value;
			}
		}

		internal string strLoginID
		{
			set
			{
				base["strLoginID"] = value;
			}
		}

		internal string strUrlReferrer
		{
			set
			{
				base["strUrlReferrer"] = value;
			}
		}

		internal string strRequestInfo
		{
			set
			{
				base["strRequestInfo"] = value;
			}
		}

		internal string strStackTrace
		{
			set
			{
				base["strStackTrace"] = value;
			}
		}

		internal byte n1PlatformCode
		{
			set
			{
				base["n1PlatformCode"] = value;
			}
		}

		internal string strServerName
		{
			set
			{
				base["strServerName"] = value;
			}
		}

		protected override void GenerateDataEntity(int TableIndex, SqlDataReader dataReader)
		{
		}

		protected override void GenerateOutputParameter()
		{
			base.Result.n4ErrorLogSN = base["oidErrorLog"].Parse(0);
			base.Result.dtCreateDate = base["dateCreate"].Parse(DateTime.MinValue);
		}

		protected override void HandleSPExecuteError()
		{
		}
	}
}
