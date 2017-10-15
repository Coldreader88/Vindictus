using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Nexon.Com.Log;

namespace Nexon.Com.DAO
{
	public abstract class SPWrapperBase<TResult> : DAOBase where TResult : SPResultBase, new()
	{
		protected abstract ServiceCode serviceCode { get; }

		public SPWrapperBase()
		{
		}

		private void Initialize()
		{
			this._cmd = new SqlCommand();
			this._cmd.CommandType = CommandType.StoredProcedure;
			this._cmd.CommandText = this.SPName;
			this._cmd.CommandTimeout = this.CommandTimeout;
			this.InitializeReturnParameters();
			this.InitializeInterfaceFrameworkParameters();
			this.InitializeParameters();
			this.ProcessSPFramework();
			this._isInitialized = true;
		}

		protected abstract void InitializeParameters();

		private void InitializeReturnParameters()
		{
			this._cmd.Parameters.Add("RETURN_VALUE", SqlDbType.Int, 4).Direction = ParameterDirection.ReturnValue;
		}

		protected virtual void InitializeInterfaceFrameworkParameters()
		{
			this._cmd.Parameters.Add("@frk_n4ErrorCode", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
			this._cmd.Parameters.Add("@frk_strErrorText", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
			this._cmd.Parameters.Add("@frk_isRequiresNewTransaction", SqlDbType.TinyInt, 1);
		}

		protected void ProcessSPFramework()
		{
			if (this._cmd.Parameters.Contains("@frk_isRequiresNewTransaction"))
			{
				this._cmd.Parameters["@frk_isRequiresNewTransaction"].Value = 1;
			}
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
            if (!this._isInitialized)
            {
                this.Initialize();
            }
            Exception exception = null;
            try
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(this.SQLConnectionStringProvider.GetConnectionString()))
                    {
                        this._cmd.Connection = sqlConnection;
                        sqlConnection.Open();
                        if (!this.IsRetrieveRecordset)
                        {
                            this._cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            using (SqlDataReader sqlDataReader = this._cmd.ExecuteReader())
                            {
                                int num = -1;
                                do
                                {
                                    num++;
                                    while (sqlDataReader != null && sqlDataReader.Read())
                                    {
                                        try
                                        {
                                            this.GenerateDataEntity(num, sqlDataReader);
                                        }
                                        catch (Exception exception2)
                                        {
                                            Exception exception1 = exception2;
                                            throw new GenerateDataEntityExecption(exception1.Message, exception1);
                                        }
                                    }
                                }
                                while (sqlDataReader != null && sqlDataReader.NextResult());
                            }
                        }
                    }
                }
                catch (Exception exception3)
                {
                    exception = exception3;
                }
            }
            finally
            {
                this._cmd.Connection = null;
            }
            if (exception is GenerateDataEntityExecption)
            {
                if (!this.HandleDBError(exception))
                {
                    throw exception;
                }
            }
            else if (this._cmd.Parameters.Contains("@frk_n4ErrorCode") && this._cmd.Parameters.Contains("@frk_strErrorText"))
            {
                try
                {
                    this.Result.SPErrorCode = (int)this["frk_n4ErrorCode"];
                    this.Result.SPErrorMessage = (string)this["frk_strErrorText"];
                }
                catch (Exception)
                {
                    if (exception == null)
                    {
                        this.Result.SPErrorCode = 0;
                        this.Result.SPErrorMessage = string.Empty;
                    }
                    else if (!this.HandleDBError(exception))
                    {
                        throw exception;
                    }
                }
            }
            else if (exception != null && !this.HandleDBError(exception))
            {
                throw exception;
            }
            exception = null;
            if (this._cmd.Parameters.Contains("RETURN_VALUE"))
            {
                TResult result = this.Result;
                result.SPReturnValue = this["RETURN_VALUE"].Parse<int>(0);
            }
            this.GenerateOutputParameter();
            this.HandleSPExecuteError();
            return this.Result;
        }

        protected abstract void GenerateDataEntity(int TableIndex, SqlDataReader dataReader);

		protected abstract void GenerateOutputParameter();

		protected virtual void HandleSPExecuteError()
		{
			TResult result = this.Result;
			if (result.SPErrorCode < 0)
			{
				TResult result2 = this.Result;
				int sperrorCode = result2.SPErrorCode;
				TResult result3 = this.Result;
				throw new SPFatalException(sperrorCode, result3.SPErrorMessage, this.Dump());
			}
			TResult result4 = this.Result;
			if (result4.SPErrorCode > 0)
			{
				TResult result5 = this.Result;
				int sperrorCode2 = result5.SPErrorCode;
				TResult result6 = this.Result;
				throw new SPLogicalException(sperrorCode2, result6.SPErrorMessage, this.Dump());
			}
		}

		protected object this[string ParameterName]
		{
			get
			{
				if (!this._isInitialized)
				{
					this.Initialize();
				}
				string text;
				if (ParameterName == "RETURN_VALUE")
				{
					text = ParameterName;
				}
				else if (ParameterName.IndexOf("@") != 0)
				{
					text = "@" + ParameterName;
				}
				else
				{
					text = ParameterName;
				}
				if (!this._cmd.Parameters.Contains(text))
				{
					throw new Exception("Parameter Not Found. name : " + text, null);
				}
				object value = this._cmd.Parameters[text].Value;
				if (value == null)
				{
					return DBNull.Value;
				}
				return value;
			}
			set
			{
				if (!this._isInitialized)
				{
					this.Initialize();
				}
				string text;
				if (ParameterName == "RETURN_VALUE")
				{
					text = ParameterName;
				}
				else if (ParameterName.IndexOf("@") != 0)
				{
					text = "@" + ParameterName;
				}
				else
				{
					text = ParameterName;
				}
				if (!this._cmd.Parameters.Contains(text))
				{
					throw new Exception("Parameter Not Found. name : " + text, null);
				}
				if (value == null)
				{
					this._cmd.Parameters[text].Value = DBNull.Value;
					return;
				}
				this._cmd.Parameters[text].Value = value;
			}
		}

		protected SqlParameter AddParameter(string parameterName, SqlDbType type)
		{
			return this._cmd.Parameters.Add("@" + parameterName, type);
		}

		protected SqlParameter AddParameter(string parameterName, SqlDbType type, ParameterDirection direction)
		{
			SqlParameter sqlParameter = this.AddParameter(parameterName, type);
			sqlParameter.Direction = direction;
			return sqlParameter;
		}

		protected SqlParameter AddParameter(string parameterName, SqlDbType type, int size, ParameterDirection direction)
		{
			SqlParameter sqlParameter = this.AddParameter(parameterName, type, direction);
			sqlParameter.Size = size;
			return sqlParameter;
		}

		protected virtual bool HandleDBError(Exception e)
		{
			try
			{
				if (!(this.SQLConnectionStringProvider is ErrorLogConnectionStringProvider))
				{
					string strErrorMessage = string.Format("{0} : {1}", this._cmd.CommandText, e.Message);
					string text = string.Format("Message:{0}", e.Message);
					text += string.Format("\r\nDump:{0}", this.Dump());
					int num;
					DateTime dateTime;
					ErrorLog.CreateErrorLog(this.serviceCode, 30000, null, strErrorMessage, text, out num, out dateTime);
				}
			}
			catch (Exception)
			{
			}
			return false;
		}

		public string Dump()
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			StringBuilder stringBuilder3 = new StringBuilder();
			stringBuilder.Append("declare @return int \r\n");
			stringBuilder2.AppendFormat("exec @return = dbo.{0} \r\n", this._cmd.CommandText);
			stringBuilder3.Append("select @return as returnvalue \r\n");
			for (int i = 0; i < this._cmd.Parameters.Count; i++)
			{
				SqlParameter sqlParameter = this._cmd.Parameters[i];
				if (sqlParameter.Direction != ParameterDirection.ReturnValue)
				{
					if (sqlParameter.Direction == ParameterDirection.InputOutput || sqlParameter.Direction == ParameterDirection.Output)
					{
						if (this.IsString(sqlParameter.SqlDbType))
						{
							stringBuilder.AppendFormat("declare {0} {1}({2}) \r\n", sqlParameter.ParameterName, sqlParameter.SqlDbType, (sqlParameter.Size == -1) ? "max" : sqlParameter.Size.ToString());
						}
						else
						{
							stringBuilder.AppendFormat("declare {0} {1} \r\n", sqlParameter.ParameterName, sqlParameter.SqlDbType);
						}
					}
					if (sqlParameter.Direction == ParameterDirection.Input)
					{
						stringBuilder2.AppendFormat("{2} {0} = {3}{1}{3} \r\n", new object[]
						{
							sqlParameter.ParameterName,
							sqlParameter.Value,
							(i == 1) ? "" : ",",
							this.IsString(sqlParameter.SqlDbType) ? "'" : ""
						});
					}
					else if (sqlParameter.Direction == ParameterDirection.InputOutput)
					{
						stringBuilder2.AppendFormat("{2} {0} = {3}{1}{3} OUTPUT \r\n", new object[]
						{
							sqlParameter.ParameterName,
							sqlParameter.Value,
							(i == 1) ? "" : ",",
							this.IsString(sqlParameter.SqlDbType) ? "'" : ""
						});
					}
					else if (sqlParameter.Direction == ParameterDirection.Output)
					{
						stringBuilder2.AppendFormat("{2} {0} = {0} OUTPUT \r\n", new object[]
						{
							sqlParameter.ParameterName,
							sqlParameter.Value,
							(i == 1) ? "" : ",",
							this.IsString(sqlParameter.SqlDbType) ? "'" : ""
						});
					}
					if (sqlParameter.Direction == ParameterDirection.InputOutput || sqlParameter.Direction == ParameterDirection.Output)
					{
						stringBuilder3.AppendFormat(", {0} as {1}\r\n", sqlParameter.ParameterName, sqlParameter.ParameterName.Replace("@", ""));
					}
				}
			}
			return stringBuilder.ToString() + stringBuilder2.ToString() + stringBuilder3.ToString();
		}

		private bool IsString(SqlDbType type)
		{
			if (type <= SqlDbType.NVarChar)
			{
				switch (type)
				{
				case SqlDbType.Char:
					return true;
				case SqlDbType.DateTime:
					return true;
				default:
					switch (type)
					{
					case SqlDbType.NChar:
						return true;
					case SqlDbType.NText:
						return true;
					case SqlDbType.NVarChar:
						return true;
					}
					break;
				}
			}
			else
			{
				if (type == SqlDbType.Text)
				{
					return true;
				}
				switch (type)
				{
				case SqlDbType.VarChar:
					return true;
				case SqlDbType.Variant:
					return true;
				case (SqlDbType)24:
					break;
				case SqlDbType.Xml:
					return true;
				default:
					switch (type)
					{
					case SqlDbType.Date:
						return true;
					case SqlDbType.DateTime2:
						return true;
					}
					break;
				}
			}
			return false;
		}

		private bool _isInitialized;

		private SqlCommand _cmd;

		protected string SPName;

		protected bool IsRetrieveRecordset;

		protected ISQLConnectionStringProvider SQLConnectionStringProvider;

		protected int CommandTimeout = 3;

		private TResult _result = default(TResult);
	}
}
