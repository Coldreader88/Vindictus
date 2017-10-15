using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using ExecutionSupporter.Properties;

namespace ExecutionSupporter
{
	[Database(Name = "heroesSupport")]
	public class HeroesSupportDataContext : DataContext
	{
		public void Clear()
		{
			base.ExecuteCommand("TRUNCATE TABLE MachineInfo", new object[0]);
			base.ExecuteCommand("TRUNCATE TABLE UserCount", new object[0]);
		}

		public HeroesSupportDataContext() : base(Settings.Default.heroesSupportConnectionString, HeroesSupportDataContext.mappingSource)
		{
		}

		public HeroesSupportDataContext(string connection) : base(connection, HeroesSupportDataContext.mappingSource)
		{
		}

		public HeroesSupportDataContext(IDbConnection connection) : base(connection, HeroesSupportDataContext.mappingSource)
		{
		}

		public HeroesSupportDataContext(string connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		public HeroesSupportDataContext(IDbConnection connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		public Table<Input> Input
		{
			get
			{
				return base.GetTable<Input>();
			}
		}

		public Table<UserCount> UserCount
		{
			get
			{
				return base.GetTable<UserCount>();
			}
		}

		public Table<Output> Output
		{
			get
			{
				return base.GetTable<Output>();
			}
		}

		public Table<MachineInfo> MachineInfo
		{
			get
			{
				return base.GetTable<MachineInfo>();
			}
		}

		private static MappingSource mappingSource = new AttributeMappingSource();
	}
}
