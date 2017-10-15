using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using CafeAuthServiceCore.Properties;

namespace CafeAuthServiceCore
{
	[Database(Name = "heroesLOG")]
	public class LogDBDataContext : DataContext
	{
		public LogDBDataContext() : base(Settings.Default.heroesLogConnectionString, LogDBDataContext.mappingSource)
		{
		}

		public LogDBDataContext(string connection) : base(connection, LogDBDataContext.mappingSource)
		{
		}

		public LogDBDataContext(IDbConnection connection) : base(connection, LogDBDataContext.mappingSource)
		{
		}

		public LogDBDataContext(string connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		public LogDBDataContext(IDbConnection connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		[Function(Name = "dbo.AddLoginJournal")]
		public int AddLoginJournal([Parameter(Name = "JournalID", DbType = "BigInt")] ref long? journalID)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				journalID
			});
			journalID = (long?)executeResult.GetParameterValue(0);
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.AddLoginLedger")]
		public int AddLoginLedger([Parameter(Name = "JournalID", DbType = "BigInt")] long? journalID, [Parameter(Name = "NexonID", DbType = "NVarChar(30)")] string nexonID, [Parameter(Name = "LoginIP", DbType = "NVarChar(20)")] string loginIP, [Parameter(Name = "Result", DbType = "NVarChar(30)")] string result)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				journalID,
				nexonID,
				loginIP,
				result
			});
			return (int)executeResult.ReturnValue;
		}

		private static MappingSource mappingSource = new AttributeMappingSource();
	}
}
