using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using AdminClientServiceCore.Properties;

namespace AdminClientServiceCore
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

		public Table<AdminClientAccessLog> AdminClientAccessLog
		{
			get
			{
				return base.GetTable<AdminClientAccessLog>();
			}
		}

		public Table<UserCountLog> UserCountLog
		{
			get
			{
				return base.GetTable<UserCountLog>();
			}
		}

		public Table<UserCountLogChanneling> UserCountLogChanneling
		{
			get
			{
				return base.GetTable<UserCountLogChanneling>();
			}
		}

		[Function(Name = "dbo.AddAdminClientAccessLog")]
		public int AddAdminClientAccessLog([Parameter(DbType = "VarChar(20)")] string IP, [Parameter(DbType = "VarChar(80)")] string REQUEST)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				IP,
				REQUEST
			});
			return (int)executeResult.ReturnValue;
		}

		private static MappingSource mappingSource = new AttributeMappingSource();
	}
}
