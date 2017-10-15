using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using GuildService.Properties;

namespace GuildService
{
	[Database(Name = "heroesLog")]
	public class HeroesLogDataContext : DataContext
	{
		public HeroesLogDataContext() : base(Settings.Default.heroesLogConnectionString, HeroesLogDataContext.mappingSource)
		{
		}

		public HeroesLogDataContext(string connection) : base(connection, HeroesLogDataContext.mappingSource)
		{
		}

		public HeroesLogDataContext(IDbConnection connection) : base(connection, HeroesLogDataContext.mappingSource)
		{
		}

		public HeroesLogDataContext(string connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		public HeroesLogDataContext(IDbConnection connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		[Function(Name = "dbo.AddGuildLedger")]
		public int AddGuildLedger([Parameter(Name = "GuildSN", DbType = "BigInt")] long? guildSN, [Parameter(Name = "CharacterID", DbType = "BigInt")] long? characterID, [Parameter(Name = "Operation", DbType = "SmallInt")] short? operation, [Parameter(Name = "Event", DbType = "SmallInt")] short? @event, [Parameter(Name = "Arg1", DbType = "NVarChar(50)")] string arg1, [Parameter(Name = "Arg2", DbType = "NVarChar(50)")] string arg2)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				guildSN,
				characterID,
				operation,
				@event,
				arg1,
				arg2
			});
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.AddGuildStorageLedger")]
		public int AddGuildStorageLedger([Parameter(Name = "GuildSN", DbType = "BigInt")] long? guildSN, [Parameter(Name = "CharacterID", DbType = "BigInt")] long? characterID, [Parameter(Name = "OperationType", DbType = "SmallInt")] short? operationType, [Parameter(Name = "EventType", DbType = "SmallInt")] short? eventType, [Parameter(Name = "ItemClassEX", DbType = "NVarChar(1024)")] string itemClassEX, [Parameter(Name = "Amount", DbType = "Int")] int? amount, [Parameter(Name = "Color1", DbType = "Int")] int? color1, [Parameter(Name = "Color2", DbType = "Int")] int? color2, [Parameter(Name = "Color3", DbType = "Int")] int? color3, [Parameter(Name = "ReduceDurability", DbType = "Int")] int? reduceDurability, [Parameter(Name = "MaxDurabilityBonus", DbType = "Int")] int? maxDurabilityBonus)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				guildSN,
				characterID,
				operationType,
				eventType,
				itemClassEX,
				amount,
				color1,
				color2,
				color3,
				reduceDurability,
				maxDurabilityBonus
			});
			return (int)executeResult.ReturnValue;
		}

		private static MappingSource mappingSource = new AttributeMappingSource();
	}
}
