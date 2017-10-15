using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Threading;
using Nexon.Com.Log;

namespace Nexon.Com.DAO
{
	public abstract class AsyncSPWrapperBase<TResult> : DAOBase where TResult : AsyncSPResultBase, new()
	{
		protected abstract ServiceCode serviceCode { get; }

		public AsyncSPWrapperBase()
		{
		}

		protected abstract void InitializeParameters();

		private void InitializeReturnParameters()
		{
			this.AddParameter("RETURN_VALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue);
		}

		protected virtual void InitializeInterfaceFrameworkParameters()
		{
			this.AddParameter("frk_n4ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output);
			this.AddParameter("frk_strErrorText", SqlDbType.NVarChar, 100, ParameterDirection.Output);
			this.AddParameter("frk_isRequiresNewTransaction", SqlDbType.TinyInt, 1);
		}

		protected TResult Result
		{
			get
			{
				if (this._result == null)
				{
					this._result = Activator.CreateInstance<TResult>();
				}
				return this._result;
			}
		}

		public TResult Execute()
		{
			WaitHandle[] array = new WaitHandle[this.SQLConnectionStringProvider.GetConnectionStrings().Length];
			this.InitializeReturnParameters();
			this.InitializeInterfaceFrameworkParameters();
			this.InitializeParameters();
			try
			{
				for (int i = 0; i < this.SQLConnectionStringProvider.GetConnectionStrings().Length; i++)
				{
					string connectionString = this.SQLConnectionStringProvider.GetConnectionStrings()[i];
					SqlCommand sqlCommand = new SqlCommand();
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.CommandText = this.SPName;
					sqlCommand.CommandTimeout = this.CommandTimeout;
					foreach (SqlParameter sqlParameter in this.Parameters)
					{
						SqlParameter sqlParameter2 = new SqlParameter(sqlParameter.ParameterName, sqlParameter.SqlDbType, sqlParameter.Size);
						sqlParameter2.Direction = sqlParameter.Direction;
						sqlCommand.Parameters.Add(sqlParameter2);
					}
					if (sqlCommand.Parameters.Contains("@frk_isRequiresNewTransaction"))
					{
						sqlCommand.Parameters["@frk_isRequiresNewTransaction"].Value = 1;
					}
					foreach (KeyValuePair<string, object> keyValuePair in this.ParameterValues)
					{
						if (keyValuePair.Value == null)
						{
							sqlCommand.Parameters[keyValuePair.Key].Value = DBNull.Value;
						}
						else
						{
							sqlCommand.Parameters[keyValuePair.Key].Value = keyValuePair.Value;
						}
					}
					SqlConnection sqlConnection = new SqlConnection(connectionString);
					sqlCommand.Connection = sqlConnection;
					sqlConnection.Open();
					AutoResetEvent autoResetEvent = new AutoResetEvent(false);
					array[i] = autoResetEvent;
					if (this.IsRetrieveRecordset)
					{
						sqlCommand.BeginExecuteReader(new AsyncCallback(this.EndExecute), new AsyncSPParam(i, sqlCommand, autoResetEvent));
					}
					else
					{
						sqlCommand.BeginExecuteNonQuery(new AsyncCallback(this.EndExecute), new AsyncSPParam(i, sqlCommand, autoResetEvent));
					}
				}
				WaitHandle.WaitAll(array, this.CommandTimeout * 1000);
				this.HandleSPExecuteError();
				this.HandleAsyncError();
			}
			catch (SqlException ex)
			{
				this.HandleDBError(ex);
				throw ex;
			}
			catch (Exception ex2)
			{
				if (!this.HandleDBError(ex2))
				{
					throw ex2;
				}
			}
			return this.Result;
		}

        private void EndExecute(IAsyncResult ar)
        {
            try
            {
                AsyncSPParam asyncState = ar.AsyncState as AsyncSPParam;
                if (!this.IsRetrieveRecordset)
                {
                    asyncState.cmd.EndExecuteNonQuery(ar);
                }
                else
                {
                    using (SqlDataReader sqlDataReader = asyncState.cmd.EndExecuteReader(ar))
                    {
                        int num = -1;
                        do
                        {
                            num++;
                            while (sqlDataReader != null && sqlDataReader.Read())
                            {
                                this.GenerateDataEntity(asyncState.index, num, sqlDataReader);
                            }
                        }
                        while (sqlDataReader != null && sqlDataReader.NextResult());
                    }
                }
                asyncState.cmd.Connection.Close();
                asyncState.cmd.Connection = null;
                int num1 = 0;
                int num2 = 0;
                string str = null;
                if (asyncState.cmd.Parameters.Contains("RETURN_VALUE"))
                {
                    num1 = asyncState.cmd.Parameters["RETURN_VALUE"].Value.Parse<int>(0);
                }
                if (asyncState.cmd.Parameters.Contains("@frk_n4ErrorCode"))
                {
                    num2 = asyncState.cmd.Parameters["@frk_n4ErrorCode"].Value.Parse<int>(0);
                }
                if (asyncState.cmd.Parameters.Contains("@frk_strErrorText"))
                {
                    str = asyncState.cmd.Parameters["@frk_strErrorText"].Value.Parse<string>(string.Empty);
                }
                TResult result = this.Result;
                result.AddSPFrameworkParameter(new SPFrameworkParameters(asyncState.index, num1, num2, str));
                this.GenerateOutputParameter(asyncState.index, asyncState.cmd);
                asyncState.handel.Set();
            }
            catch (Exception exception)
            {
                this.Result.AddException(exception);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
		protected abstract void GenerateDataEntity(int ConnectionIndex, int TableIndex, SqlDataReader dataReader);

		[MethodImpl(MethodImplOptions.Synchronized)]
		protected abstract void GenerateOutputParameter(int ConnectionIndex, SqlCommand cmd);

		protected virtual void HandleSPExecuteError()
		{
			int num = 0;
			SPFrameworkParameters spframeworkParameters;
			for (;;)
			{
				int num2 = num;
				TResult result = this.Result;
				if (num2 >= result.SPFrameworkParameters.Count)
				{
					return;
				}
				TResult result2 = this.Result;
				spframeworkParameters = result2.SPFrameworkParameters[num];
				if (spframeworkParameters.SPErrorCode != 0)
				{
					break;
				}
				num++;
			}
			throw new SPFatalException(num, spframeworkParameters.SPErrorCode, spframeworkParameters.SPErrorMessage, null);
		}

		protected object this[string ParameterName]
		{
			set
			{
				string key;
				if (ParameterName == "RETURN_VALUE")
				{
					key = ParameterName;
				}
				else if (ParameterName.IndexOf("@") != 0)
				{
					key = "@" + ParameterName;
				}
				else
				{
					key = ParameterName;
				}
				this.ParameterValues.Add(key, value);
			}
		}

		protected void AddParameter(string parameterName, SqlDbType type)
		{
			SqlParameter item = new SqlParameter("@" + parameterName, type);
			this.Parameters.Add(item);
		}

		protected void AddParameter(string parameterName, SqlDbType type, int size)
		{
			SqlParameter item = new SqlParameter("@" + parameterName, type, size);
			this.Parameters.Add(item);
		}

		protected void AddParameter(string parameterName, SqlDbType type, ParameterDirection direction)
		{
			SqlParameter sqlParameter = new SqlParameter("@" + parameterName, type);
			sqlParameter.Direction = direction;
			this.Parameters.Add(sqlParameter);
		}

		protected void AddParameter(string parameterName, SqlDbType type, int size, ParameterDirection direction)
		{
			SqlParameter sqlParameter = new SqlParameter("@" + parameterName, type, size);
			sqlParameter.Direction = direction;
			this.Parameters.Add(sqlParameter);
		}

		protected virtual void HandleDBError(SqlException ex)
		{
			try
			{
				if (!(this.SQLConnectionStringProvider is ErrorLogConnectionStringProvider))
				{
					string strErrorMessage = string.Format("{0},{1}.{2},{3}", new object[]
					{
						this.SPName,
						ex.Server,
						ex.Procedure,
						ex.Message
					});
					string strStackTrace = string.Format("Message:{0}", ex.StackTrace);
					int num;
					DateTime dateTime;
					ErrorLog.CreateErrorLog(this.serviceCode, 30000, null, strErrorMessage, strStackTrace, out num, out dateTime);
				}
			}
			catch (Exception)
			{
			}
		}

		protected virtual bool HandleDBError(Exception e)
		{
			try
			{
				if (!(this.SQLConnectionStringProvider is ErrorLogConnectionStringProvider))
				{
					string strErrorMessage = string.Format("{0} : {1}", this.SPName, e.Message);
					string strStackTrace = string.Format("Message:{0}", e.Message);
					int num;
					DateTime dateTime;
					ErrorLog.CreateErrorLog(this.serviceCode, 30000, null, strErrorMessage, strStackTrace, out num, out dateTime);
				}
			}
			catch (Exception)
			{
			}
			return false;
		}

		protected virtual void HandleAsyncError()
		{
			int num = 0;
			for (;;)
			{
				int num2 = num;
				TResult result = this.Result;
				if (num2 >= result.ExceptionList.Count)
				{
					break;
				}
				TResult result2 = this.Result;
				Exception e = result2.ExceptionList[num];
				this.HandleDBError(e);
				num++;
			}
			TResult result3 = this.Result;
			if (result3.ExceptionList.Count > 0)
			{
				TResult result4 = this.Result;
				throw result4.ExceptionList[0];
			}
		}

		protected List<SqlParameter> Parameters = new List<SqlParameter>();

		protected Dictionary<string, object> ParameterValues = new Dictionary<string, object>();

		protected string SPName;

		protected bool IsRetrieveRecordset;

		protected ISQLConnectionStringsProvider SQLConnectionStringProvider;

		protected int CommandTimeout = 3;

		private TResult _result = default(TResult);
	}
}
