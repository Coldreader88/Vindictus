using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using DSService.Properties;

namespace DSService
{
	[Database(Name = "heroesLog")]
	public class DSLogDataContext : DataContext
	{
		public DSLogDataContext() : base(Settings.Default.heroesLogConnectionString, DSLogDataContext.mappingSource)
		{
		}

		public DSLogDataContext(string connection) : base(connection, DSLogDataContext.mappingSource)
		{
		}

		public DSLogDataContext(IDbConnection connection) : base(connection, DSLogDataContext.mappingSource)
		{
		}

		public DSLogDataContext(string connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		public DSLogDataContext(IDbConnection connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		[Function(Name = "dbo.AddDSLog")]
		public int AddDSLog([Parameter(Name = "ServiceID", DbType = "Int")] int? serviceID, [Parameter(Name = "DSID", DbType = "Int")] int? dSID, [Parameter(Name = "QuestID", DbType = "NVarChar(128)")] string questID, [Parameter(Name = "PartyID", DbType = "BigInt")] long? partyID, [Parameter(Name = "MicroPlayID", DbType = "BigInt")] long? microPlayID, [Parameter(Name = "State", DbType = "Int")] int? state, [Parameter(Name = "Action", DbType = "NVarChar(128)")] string action, [Parameter(Name = "Argument", DbType = "NVarChar(128)")] string argument)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				serviceID,
				dSID,
				questID,
				partyID,
				microPlayID,
				state,
				action,
				argument
			});
			return (int)executeResult.ReturnValue;
		}

		private static MappingSource mappingSource = new AttributeMappingSource();
	}
}
