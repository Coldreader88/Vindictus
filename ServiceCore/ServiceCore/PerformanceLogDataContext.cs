using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using ServiceCore.Properties;

namespace ServiceCore
{
	[Database(Name = "heroesLog")]
	public class PerformanceLogDataContext : DataContext
	{
		public PerformanceLogDataContext() : base(Settings.Default.heroesLogConnectionString, PerformanceLogDataContext.mappingSource)
		{
		}

		public PerformanceLogDataContext(string connection) : base(connection, PerformanceLogDataContext.mappingSource)
		{
		}

		public PerformanceLogDataContext(IDbConnection connection) : base(connection, PerformanceLogDataContext.mappingSource)
		{
		}

		public PerformanceLogDataContext(string connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		public PerformanceLogDataContext(IDbConnection connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		[Function(Name = "dbo.LogOperationPerf")]
		public int LogOperationPerf([Parameter(Name = "ServiceID", DbType = "BigInt")] long? serviceID, [Parameter(Name = "ServiceName", DbType = "NVarChar(50)")] string serviceName, [Parameter(Name = "OperationName", DbType = "NVarChar(50)")] string operationName, [Parameter(Name = "OperationCount", DbType = "Int")] int? operationCount, [Parameter(Name = "TotalSpendTicks", DbType = "BigInt")] long? totalSpendTicks, [Parameter(Name = "TotalStepCount", DbType = "BigInt")] long? totalStepCount, [Parameter(Name = "TotalCoverageTicks", DbType = "BigInt")] long? totalCoverageTicks, [Parameter(Name = "Interval", DbType = "Int")] int? interval)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				serviceID,
				serviceName,
				operationName,
				operationCount,
				totalSpendTicks,
				totalStepCount,
				totalCoverageTicks,
				interval
			});
			return (int)executeResult.ReturnValue;
		}

		private static MappingSource mappingSource = new AttributeMappingSource();
	}
}
