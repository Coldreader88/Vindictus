using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace UnifiedNetwork.ProfileService
{
	[Database(Name = "heroesLog")]
	public class ProfileDataContext : DataContext
	{
		public bool AddProfileRecord(LogProfile profile)
		{
			bool result;
			try
			{
				this.LogProfiles.InsertOnSubmit(profile);
				result = true;
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
				result = false;
			}
			return result;
		}

		public bool AddMachineStatus(LogMachine status)
		{
			bool result;
			try
			{
				this.LogMachines.InsertOnSubmit(status);
				result = true;
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
				result = false;
			}
			return result;
		}

		public ProfileDataContext(string connection) : base(connection, ProfileDataContext.mappingSource)
		{
		}

		public ProfileDataContext(IDbConnection connection) : base(connection, ProfileDataContext.mappingSource)
		{
		}

		public ProfileDataContext(string connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		public ProfileDataContext(IDbConnection connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		public Table<DailyChart> DailyCharts
		{
			get
			{
				return base.GetTable<DailyChart>();
			}
		}

		public Table<LogMachine> LogMachines
		{
			get
			{
				return base.GetTable<LogMachine>();
			}
		}

		public Table<LogProfile> LogProfiles
		{
			get
			{
				return base.GetTable<LogProfile>();
			}
		}

		private static MappingSource mappingSource = new AttributeMappingSource();
	}
}
