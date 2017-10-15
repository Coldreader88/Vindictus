using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using AdminClient.Properties;

namespace AdminClient
{
	[Database(Name = "heroes")]
	public class GMAccountDBDataContext : DataContext
	{
		private void OnCreated()
		{
			base.CommandTimeout = 3600;
		}

		public GMAccountDBDataContext() : base(Settings.Default.heroesConnectionString, GMAccountDBDataContext.mappingSource)
		{
			this.OnCreated();
		}

		public GMAccountDBDataContext(string connection) : base(connection, GMAccountDBDataContext.mappingSource)
		{
			this.OnCreated();
		}

		public GMAccountDBDataContext(IDbConnection connection) : base(connection, GMAccountDBDataContext.mappingSource)
		{
			this.OnCreated();
		}

		public GMAccountDBDataContext(string connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
			this.OnCreated();
		}

		public GMAccountDBDataContext(IDbConnection connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
			this.OnCreated();
		}

		public Table<GMAccounts> GMAccounts
		{
			get
			{
				return base.GetTable<GMAccounts>();
			}
		}

		[Function(Name = "dbo.ExtendCashItems")]
		public int ExtendCashItems([Parameter(DbType = "DateTime2")] DateTime? fromDate, [Parameter(DbType = "Int")] int? minutes)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				fromDate,
				minutes
			});
			return (int)executeResult.ReturnValue;
		}

		private static MappingSource mappingSource = new AttributeMappingSource();
	}
}
