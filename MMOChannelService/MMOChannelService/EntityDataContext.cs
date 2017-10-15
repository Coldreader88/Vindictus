using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using MMOChannelService.Properties;

namespace MMOChannelService
{
	[Database(Name = "heroes")]
	public class EntityDataContext : DataContext
	{
		public EntityDataContext() : base(Settings.Default.heroesConnectionString, EntityDataContext.mappingSource)
		{
		}

		public EntityDataContext(string connection) : base(connection, EntityDataContext.mappingSource)
		{
		}

		public EntityDataContext(IDbConnection connection) : base(connection, EntityDataContext.mappingSource)
		{
		}

		public EntityDataContext(string connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		public EntityDataContext(IDbConnection connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		[Function(Name = "dbo.AcquireService")]
		public int AcquireService([Parameter(DbType = "BigInt")] long? entityID, [Parameter(DbType = "VarChar(32)")] string category, [Parameter(DbType = "Int")] int? value, [Parameter(DbType = "Int")] int? comparand)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				entityID,
				category,
				value,
				comparand
			});
			return (int)executeResult.ReturnValue;
		}

		private static MappingSource mappingSource = new AttributeMappingSource();
	}
}
