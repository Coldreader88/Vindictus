using System;
using System.Data;
using System.Data.SqlClient;
using Nexon.Com.DAO;

namespace Nexon.Com.Log
{
	internal class ErrorLogCreateSPWrapper : SPWrapperBase<ErrorLogCreateSPResult>
	{
		protected override ServiceCode serviceCode
		{
			get
			{
				return ServiceCode.framework;
			}
		}

		internal ErrorLogCreateSPWrapper()
		{
			this.SPName = "lgp_ErrorLog_Create";
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
				this._n4ServiceCode = value;
			}
		}

		internal int n4ErrorCode
		{
			set
			{
				base["n4ErrorCode"] = value;
				this._n4ErrorCode = value;
			}
		}

		internal string strErrorMessage
		{
			set
			{
				base["strErrorMessage"] = value;
				this._strErrorMessage = value;
			}
		}

		internal string strServerIP
		{
			set
			{
				base["strServerIP"] = value;
				this._strServerIP = value;
			}
		}

		internal string strDomainName
		{
			set
			{
				base["strHostName"] = value;
				this._strHostName = value;
			}
		}

		internal string strRequestUrl
		{
			set
			{
				base["strRequestUrl"] = value;
				this._strRequestUrl = value;
			}
		}

		internal string strClientIP
		{
			set
			{
				base["strClientIP"] = value;
				this._strClientIP = value;
			}
		}

		internal string strLoginID
		{
			set
			{
				base["strLoginID"] = value;
				this._strLoginID = value;
			}
		}

		internal string strUrlReferrer
		{
			set
			{
				base["strUrlReferrer"] = value;
				this._strUrlReferrer = value;
			}
		}

		internal string strRequestInfo
		{
			set
			{
				base["strRequestInfo"] = value;
				this._strRequestInfo = value;
			}
		}

		internal string strStackTrace
		{
			set
			{
				base["strStackTrace"] = value;
				this._strStackTrace = value;
			}
		}

		internal byte n1PlatformCode
		{
			set
			{
				base["n1PlatformCode"] = value;
				this._n1PlatformCode = value;
			}
		}

		internal string strServerName
		{
			set
			{
				base["strServerName"] = value;
				this._strServerName = value;
			}
		}

		protected override void GenerateDataEntity(int TableIndex, SqlDataReader dataReader)
		{
		}

		protected override void GenerateOutputParameter()
		{
			int num = base["oidErrorLog"].Parse(0);
			DateTime dateTime = base["dateCreate"].Parse(DateTime.MinValue);
			base.Result.errorLogInfo = new ErrorLogInfo(num, dateTime, this._strErrorMessage, this._strServerIP, this._strRequestUrl, this._strStackTrace, this._strHostName, this._strClientIP, this._strLoginID, this._strUrlReferrer, this._n4ErrorCode, this._n4ServiceCode, this._strRequestInfo, this._n1PlatformCode, this._strServerName);
			base.Result.n4ErrorLogSN = num;
			base.Result.dtCreateDate = dateTime;
		}

		protected override void HandleSPExecuteError()
		{
		}

		private int _n4ServiceCode;

		private int _n4ErrorCode;

		private string _strErrorMessage;

		private string _strServerIP;

		private string _strHostName;

		private string _strRequestUrl;

		private string _strClientIP;

		private string _strLoginID;

		private string _strUrlReferrer;

		private string _strRequestInfo;

		private string _strStackTrace;

		private byte _n1PlatformCode;

		private string _strServerName;
	}
}
