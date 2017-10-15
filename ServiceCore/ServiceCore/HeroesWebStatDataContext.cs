using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using ServiceCore.Properties;

namespace ServiceCore
{
	[Database(Name = "HeroesWebStat")]
	public class HeroesWebStatDataContext : DataContext
	{
		public HeroesWebStatDataContext() : base(Settings.Default.HeroesWebStatConnectionString, HeroesWebStatDataContext.mappingSource)
		{
		}

		public HeroesWebStatDataContext(string connection) : base(connection, HeroesWebStatDataContext.mappingSource)
		{
		}

		public HeroesWebStatDataContext(IDbConnection connection) : base(connection, HeroesWebStatDataContext.mappingSource)
		{
		}

		public HeroesWebStatDataContext(string connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		public HeroesWebStatDataContext(IDbConnection connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		[Function(Name = "dbo.AddServiceReporterSession")]
		public int AddServiceReporterSession([Parameter(Name = "GameCode", DbType = "Int")] int? gameCode, [Parameter(Name = "ServerCode", DbType = "Int")] int? serverCode, [Parameter(Name = "MachineIP", DbType = "NVarChar(64)")] string machineIP, [Parameter(Name = "Category", DbType = "NVarChar(64)")] string category, [Parameter(Name = "SessionID", DbType = "NVarChar(64)")] string sessionID)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				gameCode,
				serverCode,
				machineIP,
				category,
				sessionID
			});
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.AddServiceReporterIndicator")]
		public int AddServiceReporterIndicator([Parameter(Name = "SessionID", DbType = "NVarChar(64)")] string sessionID, [Parameter(Name = "ID", DbType = "Int")] int? iD, [Parameter(Name = "Time", DbType = "DateTime")] DateTime? time, [Parameter(Name = "Subject", DbType = "NVarChar(64)")] string subject, [Parameter(Name = "Key", DbType = "NVarChar(MAX)")] string key, [Parameter(Name = "Value", DbType = "NVarChar(MAX)")] string value)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				sessionID,
				iD,
				time,
				subject,
				key,
				value
			});
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.AddServiceReporterLog")]
		public int AddServiceReporterLog([Parameter(Name = "SessionID", DbType = "NVarChar(64)")] string sessionID, [Parameter(Name = "ID", DbType = "Int")] int? iD, [Parameter(Name = "Time", DbType = "DateTime")] DateTime? time, [Parameter(Name = "Subject", DbType = "NVarChar(64)")] string subject, [Parameter(Name = "Key", DbType = "NVarChar(MAX)")] string key, [Parameter(Name = "Value", DbType = "NVarChar(MAX)")] string value)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				sessionID,
				iD,
				time,
				subject,
				key,
				value
			});
			return (int)executeResult.ReturnValue;
		}

		private static MappingSource mappingSource = new AttributeMappingSource();
	}
}
