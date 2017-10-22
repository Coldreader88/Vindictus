using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using TradeService.Properties;

namespace TradeService
{
	[Database(Name = "HeroesMarketPlace")]
	public class TradeDBDataContext : DataContext
	{
		public TradeDBDataContext() : base(Settings.Default.HeroesTradeNewConnectionString, TradeDBDataContext.mappingSource)
		{
		}

		public TradeDBDataContext(string connection) : base(connection, TradeDBDataContext.mappingSource)
		{
		}

		public TradeDBDataContext(IDbConnection connection) : base(connection, TradeDBDataContext.mappingSource)
		{
		}

		public TradeDBDataContext(string connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		public TradeDBDataContext(IDbConnection connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		[Function(Name = "dbo.TradeItemAvgPriceList")]
		public ISingleResult<TradeItemAvgPriceListResult> TradeItemAvgPriceList()
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[0]);
			return (ISingleResult<TradeItemAvgPriceListResult>)executeResult.ReturnValue;
		}

		private static MappingSource mappingSource = new AttributeMappingSource();
	}
}
