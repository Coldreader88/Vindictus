using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using HackShieldServiceCore.Properties;

namespace HackShieldServiceCore
{
	[Database(Name = "heroesLog")]
	public class HackShieldErrorLogDataContext : DataContext
	{
		public HackShieldErrorLogDataContext() : base(Settings.Default.heroesLogConnectionString, HackShieldErrorLogDataContext.mappingSource)
		{
		}

		public HackShieldErrorLogDataContext(string connection) : base(connection, HackShieldErrorLogDataContext.mappingSource)
		{
		}

		public HackShieldErrorLogDataContext(IDbConnection connection) : base(connection, HackShieldErrorLogDataContext.mappingSource)
		{
		}

		public HackShieldErrorLogDataContext(string connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		public HackShieldErrorLogDataContext(IDbConnection connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		[Function(Name = "dbo.AddHackShieldError")]
		public int AddHackShieldError([Parameter(Name = "CharacterId", DbType = "BigInt")] long? characterId, [Parameter(Name = "Type", DbType = "NVarChar(50)")] string type, [Parameter(Name = "Value", DbType = "NVarChar(50)")] string value)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				characterId,
				type,
				value
			});
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.AddTcProtectError")]
		public int AddTcProtectError([Parameter(Name = "CharacterId", DbType = "BigInt")] long? characterId, [Parameter(Name = "Type", DbType = "NVarChar(50)")] string type, [Parameter(Name = "Value", DbType = "NVarChar(50)")] string value)
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
