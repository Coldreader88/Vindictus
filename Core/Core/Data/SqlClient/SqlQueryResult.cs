using System;
using System.Data.SqlClient;

namespace Devcat.Core.Data.SqlClient
{
	public class SqlQueryResult
	{
		public static void Get<T>(SqlDataReader reader, int index, ref T value)
		{
			value = (T)((object)reader.GetValue(index));
		}
	}
}
