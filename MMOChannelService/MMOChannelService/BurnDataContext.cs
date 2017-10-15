using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using MMOChannelService.Properties;

namespace MMOChannelService
{
	[Database(Name = "heroesShare")]
	public class BurnDataContext : DataContext
	{
		public BurnDataContext() : base(Settings.Default.heroesShareConnectionString, BurnDataContext.mappingSource)
		{
		}

		public BurnDataContext(string connection) : base(connection, BurnDataContext.mappingSource)
		{
		}

		public BurnDataContext(IDbConnection connection) : base(connection, BurnDataContext.mappingSource)
		{
		}

		public BurnDataContext(string connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		public BurnDataContext(IDbConnection connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		[Function(Name = "dbo.AddAllUserEventCount")]
		public int AddAllUserEventCount([Parameter(DbType = "Int")] int? slotID, [Parameter(DbType = "Int")] int? increment, [Parameter(DbType = "DateTime")] DateTime? endTime)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				slotID,
				increment,
				endTime
			});
			return (int)executeResult.ReturnValue;
		}

		private static MappingSource mappingSource = new AttributeMappingSource();
	}
}
