using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using CashShopService.Properties;

namespace CashShopService
{
	[Database(Name = "heroesLOG")]
	public class CashShopProcessLogDataContext : DataContext
	{
		public CashShopProcessLogDataContext() : base(Settings.Default.heroesLogConnectionString, CashShopProcessLogDataContext.mappingSource)
		{
		}

		public CashShopProcessLogDataContext(string connection) : base(connection, CashShopProcessLogDataContext.mappingSource)
		{
		}

		public CashShopProcessLogDataContext(IDbConnection connection) : base(connection, CashShopProcessLogDataContext.mappingSource)
		{
		}

		public CashShopProcessLogDataContext(string connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		public CashShopProcessLogDataContext(IDbConnection connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		public Table<CashShopProcessLog> CashShopProcessLog
		{
			get
			{
				return base.GetTable<CashShopProcessLog>();
			}
		}

		[Function(Name = "dbo.AddCashShopProcessLog")]
		public int AddCashShopProcessLog([Parameter(DbType = "NVarChar(128)")] string OrderID, [Parameter(DbType = "NVarChar(30)")] string OrderNo, [Parameter(DbType = "Int")] int? ProductNo, [Parameter(DbType = "Int")] int? Quantity, [Parameter(DbType = "BigInt")] long? CID, [Parameter(DbType = "Int")] int? NexonSN, [Parameter(DbType = "NVarChar(10)")] string OrderType, [Parameter(DbType = "NVarChar(20)")] string Event)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				OrderID,
				OrderNo,
				ProductNo,
				Quantity,
				CID,
				NexonSN,
				OrderType,
				Event
			});
			return (int)executeResult.ReturnValue;
		}

		private static MappingSource mappingSource = new AttributeMappingSource();
	}
}
