using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using GRCService.Properties;

namespace GRCService
{
	[Database(Name = "heroesLog")]
	public class GRCServiceErrorLogDataContext : DataContext
	{
		public GRCServiceErrorLogDataContext() : base(Settings.Default.heroesLogConnectionString, GRCServiceErrorLogDataContext.mappingSource)
		{
		}

		public GRCServiceErrorLogDataContext(string connection) : base(connection, GRCServiceErrorLogDataContext.mappingSource)
		{
		}

		public GRCServiceErrorLogDataContext(IDbConnection connection) : base(connection, GRCServiceErrorLogDataContext.mappingSource)
		{
		}

		public GRCServiceErrorLogDataContext(string connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		public GRCServiceErrorLogDataContext(IDbConnection connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		[Function(Name = "dbo.AddGRCServiceError")]
		public int AddGRCServiceError([Parameter(Name = "CharacterId", DbType = "BigInt")] long? characterId, [Parameter(Name = "Type", DbType = "Int")] int? type, [Parameter(Name = "Value", DbType = "NVarChar(50)")] string value)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				characterId,
				type,
				value
			});
			return (int)executeResult.ReturnValue;
		}

		private static MappingSource mappingSource = new AttributeMappingSource();
	}
}
