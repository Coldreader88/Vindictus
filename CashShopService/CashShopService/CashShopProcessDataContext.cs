using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using CashShopService.Properties;

namespace CashShopService
{
	[Database(Name = "heroes")]
	public class CashShopProcessDataContext : DataContext
	{
		public CashShopProcessDataContext() : base(Settings.Default.heroesConnectionString, CashShopProcessDataContext.mappingSource)
		{
		}

		public CashShopProcessDataContext(string connection) : base(connection, CashShopProcessDataContext.mappingSource)
		{
		}

		public CashShopProcessDataContext(IDbConnection connection) : base(connection, CashShopProcessDataContext.mappingSource)
		{
		}

		public CashShopProcessDataContext(string connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		public CashShopProcessDataContext(IDbConnection connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		public Table<CashShopProcess> CashShopProcess
		{
			get
			{
				return base.GetTable<CashShopProcess>();
			}
		}

		[Function(Name = "dbo.WishListDelete")]
		public int WishListDelete([Parameter(Name = "CID", DbType = "BigInt")] long? cID, [Parameter(Name = "ProductNo", DbType = "Int")] int? productNo)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				cID,
				productNo
			});
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.WishListInsert")]
		public int WishListInsert([Parameter(Name = "CID", DbType = "BigInt")] long? cID, [Parameter(Name = "ProductNo", DbType = "Int")] int? productNo, [Parameter(Name = "ProductName", DbType = "NVarChar(64)")] string productName)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				cID,
				productNo,
				productName
			});
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.WishListGet")]
		public ISingleResult<WishListGetResult> WishListGet([Parameter(Name = "CID", DbType = "BigInt")] long? cID)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				cID
			});
			return (ISingleResult<WishListGetResult>)executeResult.ReturnValue;
		}

		private static MappingSource mappingSource = new AttributeMappingSource();
	}
}
