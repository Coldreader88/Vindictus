using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace NPL.SSO
{
	public class ErrorLogDAO
	{
		private string m_strConnectionStr
		{
			get
			{
				return string.Format("{0}", ConfigurationManager.ConnectionStrings["LogConStr"]);
			}
		}

		public ErrorLogDAO(int n4ErrorCode, string strErrorMessage, string strServerIP, string strRequestUrl, string strHostName, string strStackTrace, string strClientIP, string strUrlReferrer, string strLoginID, string strRequestInfo, int n4ServiceCode, byte n1PlatformCode, string strServerName)
		{
			this.m_cmd = new SqlCommand();
			this.m_cmd.CommandTimeout = 3;
			this.m_cmd.CommandType = CommandType.StoredProcedure;
			this.m_cmd.CommandText = "lgp_ErrorLog_Create";
			this.m_cmd.Parameters.Add("@frk_n4ErrorCode", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
			this.m_cmd.Parameters.Add("@frk_strErrorText", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
			this.m_cmd.Parameters.Add("@frk_isRequiresNewTransaction", SqlDbType.TinyInt, 1);
			this.m_cmd.Parameters.Add("@n2ErrorCode", SqlDbType.SmallInt, 2).Direction = ParameterDirection.Input;
			this.m_cmd.Parameters.Add("@strErrorMessage", SqlDbType.VarChar, 100).Direction = ParameterDirection.Input;
			this.m_cmd.Parameters.Add("@strServerIP", SqlDbType.VarChar, 16).Direction = ParameterDirection.Input;
			this.m_cmd.Parameters.Add("@strRequestUrl", SqlDbType.VarChar, 1024).Direction = ParameterDirection.Input;
			this.m_cmd.Parameters.Add("@strHostName", SqlDbType.VarChar, 40).Direction = ParameterDirection.Input;
			this.m_cmd.Parameters.Add("@strStackTrace", SqlDbType.Text, int.MaxValue).Direction = ParameterDirection.Input;
			this.m_cmd.Parameters.Add("@strClientIP", SqlDbType.VarChar, 16).Direction = ParameterDirection.Input;
			this.m_cmd.Parameters.Add("@strLoginID", SqlDbType.VarChar, 24).Direction = ParameterDirection.Input;
			this.m_cmd.Parameters.Add("@strUrlReferrer", SqlDbType.VarChar, 1024).Direction = ParameterDirection.Input;
			this.m_cmd.Parameters.Add("@strRequestInfo", SqlDbType.Text, int.MaxValue).Direction = ParameterDirection.Input;
			this.m_cmd.Parameters.Add("@n4ErrorCode", SqlDbType.Int, 4).Direction = ParameterDirection.Input;
			this.m_cmd.Parameters.Add("@n4ServiceCode", SqlDbType.Int, 4).Direction = ParameterDirection.Input;
			this.m_cmd.Parameters.Add("@n1PlatformCode", SqlDbType.TinyInt, 1).Direction = ParameterDirection.Input;
			this.m_cmd.Parameters.Add("@strServerName", SqlDbType.VarChar, 20).Direction = ParameterDirection.Input;
			this.m_cmd.Parameters.Add("@oidErrorLog", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
			this.m_cmd.Parameters.Add("@dateCreate", SqlDbType.DateTime, 8).Direction = ParameterDirection.Output;
			this.m_cmd.Parameters["@frk_isRequiresNewTransaction"].Value = 1;
			this.m_cmd.Parameters["@strErrorMessage"].Value = strErrorMessage;
			this.m_cmd.Parameters["@strServerIP"].Value = strServerIP;
			this.m_cmd.Parameters["@strRequestUrl"].Value = strRequestUrl;
			this.m_cmd.Parameters["@strHostName"].Value = strHostName;
			this.m_cmd.Parameters["@strStackTrace"].Value = strStackTrace;
			this.m_cmd.Parameters["@strClientIP"].Value = strClientIP;
			this.m_cmd.Parameters["@strLoginID"].Value = strLoginID;
			this.m_cmd.Parameters["@strUrlReferrer"].Value = strUrlReferrer;
			this.m_cmd.Parameters["@strRequestInfo"].Value = strRequestInfo;
			this.m_cmd.Parameters["@n4ErrorCode"].Value = n4ErrorCode;
			this.m_cmd.Parameters["@n4ServiceCode"].Value = n4ServiceCode;
			this.m_cmd.Parameters["@n1PlatformCode"].Value = n1PlatformCode;
			this.m_cmd.Parameters["@strServerName"].Value = strServerName;
		}

		public void Execute()
		{
			try
			{
				this.m_conn = new SqlConnection(this.m_strConnectionStr);
				this.m_conn.Open();
				this.m_cmd.Connection = this.m_conn;
				this.m_cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message.ToString(), ex);
			}
			finally
			{
				try
				{
					this.m_conn.Close();
				}
				catch (Exception)
				{
				}
				this.m_conn = null;
				this.m_cmd.Connection = null;
			}
		}

		private SqlCommand m_cmd;

		private SqlConnection m_conn;
	}
}
