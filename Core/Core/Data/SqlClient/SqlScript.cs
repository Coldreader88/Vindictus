using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Devcat.Core.Data.SqlClient
{
	public class SqlScript
	{
		public static void ExecuteFromScript(string connectionString, string scriptFileName)
		{
			string scriptSqlCommand = string.Empty;
			using (TextReader textReader = new StreamReader(scriptFileName))
			{
				scriptSqlCommand = textReader.ReadToEnd();
			}
			SqlScript.ExecuteFromStream(connectionString, scriptSqlCommand);
		}

		public static void ExecuteFromStream(string connectionString, string scriptSqlCommand)
		{
			string[] array = scriptSqlCommand.Split(new string[]
			{
				"GO"
			}, StringSplitOptions.None);
			SqlConnection sqlConnection = new SqlConnection(connectionString);
			sqlConnection.Open();
			try
			{
				foreach (string cmdText in array)
				{
					using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
					{
						sqlCommand.CommandType = CommandType.Text;
						sqlCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				throw;
			}
			sqlConnection.Close();
		}
	}
}
