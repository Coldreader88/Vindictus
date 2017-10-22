using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using TalkService.Properties;

namespace TalkService
{
	[Database(Name = "heroes")]
	public class HeroesDataContext : DataContext
	{
		public HeroesDataContext() : base(Settings.Default.heroesConnectionString, HeroesDataContext.mappingSource)
		{
		}

		public HeroesDataContext(string connection) : base(connection, HeroesDataContext.mappingSource)
		{
		}

		public HeroesDataContext(IDbConnection connection) : base(connection, HeroesDataContext.mappingSource)
		{
		}

		public HeroesDataContext(string connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		public HeroesDataContext(IDbConnection connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		[Function(Name = "dbo.GetCIDByName")]
		public int GetCIDByName([Parameter(Name = "Name", DbType = "NVarChar(50)")] string name, [Parameter(Name = "CID", DbType = "BigInt")] ref long? cID)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				name,
				cID
			});
			cID = (long?)executeResult.GetParameterValue(1);
			return (int)executeResult.ReturnValue;
		}

		private static MappingSource mappingSource = new AttributeMappingSource();
	}
}
