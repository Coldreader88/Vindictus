using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using ServiceCore.Properties;

namespace ServiceCore
{
	[Database(Name = "heroes")]
	public class EventLoaderDataContext : DataContext
	{
		public EventLoaderDataContext() : base(Settings.Default.heroesConnectionString, EventLoaderDataContext.mappingSource)
		{
		}

		public EventLoaderDataContext(string connection) : base(connection, EventLoaderDataContext.mappingSource)
		{
		}

		public EventLoaderDataContext(IDbConnection connection) : base(connection, EventLoaderDataContext.mappingSource)
		{
		}

		public EventLoaderDataContext(string connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		public EventLoaderDataContext(IDbConnection connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		[Function(Name = "dbo.EventGet")]
		public ISingleResult<EventGetResult> EventGet()
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[0]);
			return (ISingleResult<EventGetResult>)executeResult.ReturnValue;
		}

		private static MappingSource mappingSource = new AttributeMappingSource();
	}
}
