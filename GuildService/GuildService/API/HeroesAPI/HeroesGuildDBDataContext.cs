using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using GuildService.Properties;

namespace GuildService.API.HeroesAPI
{
	[Database(Name = "heroes")]
	public class HeroesGuildDBDataContext : DataContext
	{
		public HeroesGuildDBDataContext() : base(Settings.Default.heroesConnectionString, HeroesGuildDBDataContext.mappingSource)
		{
		}

		public HeroesGuildDBDataContext(string connection) : base(connection, HeroesGuildDBDataContext.mappingSource)
		{
		}

		public HeroesGuildDBDataContext(IDbConnection connection) : base(connection, HeroesGuildDBDataContext.mappingSource)
		{
		}

		public HeroesGuildDBDataContext(string connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		public HeroesGuildDBDataContext(IDbConnection connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		[Function(Name = "dbo.GuildCheckGroupID")]
		public int GuildCheckGroupID([Parameter(DbType = "Int")] int? serverCode, [Parameter(DbType = "NVarChar(128)")] string guildId)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				serverCode,
				guildId
			});
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GuildCheckGroupName")]
		public int GuildCheckGroupName([Parameter(DbType = "Int")] int? serverCode, [Parameter(DbType = "NVarChar(128)")] string guildName)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				serverCode,
				guildName
			});
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GuildRemove")]
		public int GuildRemove([Parameter(DbType = "Int")] int? serverCode, [Parameter(DbType = "Int")] int? nexonSn, [Parameter(DbType = "Int")] int? guildSn)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				serverCode,
				nexonSn,
				guildSn
			});
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GuildUserJoinApply")]
		public int GuildUserJoinApply([Parameter(DbType = "Int")] int? serverCode, [Parameter(DbType = "Int")] int? guildSn, [Parameter(DbType = "Int")] int? nexonSn, [Parameter(DbType = "BigInt")] long? characterSn, [Parameter(DbType = "NVarChar(128)")] string characterName)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				serverCode,
				guildSn,
				nexonSn,
				characterSn,
				characterName
			});
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GuildAllMemberGetList")]
		public ISingleResult<GuildAllMemberGetListResult> GuildAllMemberGetList([Parameter(DbType = "Int")] int? serverCode, [Parameter(DbType = "Int")] int? guildSn, [Parameter(DbType = "Int")] int? pageNo, [Parameter(DbType = "TinyInt")] byte? pageSize, [Parameter(DbType = "TinyInt")] byte? isAscending, [Parameter(DbType = "Int")] ref int? rowCount, [Parameter(DbType = "Int")] ref int? totalRowCount)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				serverCode,
				guildSn,
				pageNo,
				pageSize,
				isAscending,
				rowCount,
				totalRowCount
			});
			rowCount = (int?)executeResult.GetParameterValue(5);
			totalRowCount = (int?)executeResult.GetParameterValue(6);
			return (ISingleResult<GuildAllMemberGetListResult>)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GuildGetInfoBySn")]
		public ISingleResult<GuildGetInfoBySnResult> GuildGetInfoBySn([Parameter(DbType = "Int")] int? serverCode, [Parameter(DbType = "Int")] int? guildSn)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				serverCode,
				guildSn
			});
			return (ISingleResult<GuildGetInfoBySnResult>)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GuildGroupChangeMaster")]
		public int GuildGroupChangeMaster([Parameter(DbType = "Int")] int? serverCode, [Parameter(DbType = "Int")] int? guildSn, [Parameter(DbType = "Int")] int? newNexonSn, [Parameter(DbType = "BigInt")] long? newCharacterSn, [Parameter(DbType = "NVarChar(50)")] string newCharacterName, [Parameter(DbType = "Int")] int? oldMasterRank)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				serverCode,
				guildSn,
				newNexonSn,
				newCharacterSn,
				newCharacterName,
				oldMasterRank
			});
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GuildGroupGetListByGuildMaster")]
		public ISingleResult<GuildGroupGetListByGuildMasterResult> GuildGroupGetListByGuildMaster([Parameter(DbType = "Int")] int? serverCode, [Parameter(DbType = "TinyInt")] byte? orderType, [Parameter(DbType = "Int")] int? pageNo, [Parameter(DbType = "TinyInt")] byte? pageSize, [Parameter(DbType = "NVarChar(50)")] string nameInGroup_master_search, [Parameter(DbType = "Int")] ref int? totalRowCount)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				serverCode,
				orderType,
				pageNo,
				pageSize,
				nameInGroup_master_search,
				totalRowCount
			});
			totalRowCount = (int?)executeResult.GetParameterValue(5);
			return (ISingleResult<GuildGroupGetListByGuildMasterResult>)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GuildGroupGetListByGuildName")]
		public ISingleResult<GuildGroupGetListByGuildNameResult> GuildGroupGetListByGuildName([Parameter(DbType = "Int")] int? serverCode, [Parameter(DbType = "TinyInt")] byte? orderType, [Parameter(DbType = "Int")] int? pageNo, [Parameter(DbType = "TinyInt")] byte? pageSize, [Parameter(DbType = "NVarChar(50)")] string guildName, [Parameter(DbType = "Int")] ref int? totalRowCount)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				serverCode,
				orderType,
				pageNo,
				pageSize,
				guildName,
				totalRowCount
			});
			totalRowCount = (int?)executeResult.GetParameterValue(5);
			return (ISingleResult<GuildGroupGetListByGuildNameResult>)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GuildGroupMemberGetInfo")]
		public ISingleResult<GuildGroupMemberGetInfoResult> GuildGroupMemberGetInfo([Parameter(DbType = "Int")] int? serverCode, [Parameter(DbType = "Int")] int? guildSn, [Parameter(DbType = "Int")] int? characterSn, [Parameter(DbType = "NVarChar(50)")] string characterName)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				serverCode,
				guildSn,
				characterSn,
				characterName
			});
			return (ISingleResult<GuildGroupMemberGetInfoResult>)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GuildGroupUserGetInfo")]
		public ISingleResult<GuildGroupUserGetInfoResult> GuildGroupUserGetInfo([Parameter(DbType = "Int")] int? serverCode, [Parameter(DbType = "Int")] int? nexonSn)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				serverCode,
				nexonSn
			});
			return (ISingleResult<GuildGroupUserGetInfoResult>)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GuildGroupUserTryJoin")]
		public int GuildGroupUserTryJoin([Parameter(DbType = "Int")] int? serverCode, [Parameter(DbType = "BigInt")] long? characterSn, [Parameter(DbType = "Int")] int? nexonSn, [Parameter(DbType = "NVarChar(50)")] string characterName)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				serverCode,
				characterSn,
				nexonSn,
				characterName
			});
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GuildUserGroupGetList")]
		public ISingleResult<GuildUserGroupGetListResult> GuildUserGroupGetList([Parameter(DbType = "Int")] int? serverCode, [Parameter(DbType = "Int")] int? nexonSn, [Parameter(DbType = "Int")] int? pageNo, [Parameter(DbType = "TinyInt")] byte? pageSize, [Parameter(DbType = "Int")] ref int? rowCount, [Parameter(DbType = "Int")] ref int? totalRowCount)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				serverCode,
				nexonSn,
				pageNo,
				pageSize,
				rowCount,
				totalRowCount
			});
			rowCount = (int?)executeResult.GetParameterValue(4);
			totalRowCount = (int?)executeResult.GetParameterValue(5);
			return (ISingleResult<GuildUserGroupGetListResult>)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GuildUserJoin")]
		public int GuildUserJoin([Parameter(DbType = "Int")] int? serverCode, [Parameter(DbType = "Int")] int? guildSn, [Parameter(DbType = "Int")] int? nexonSn, [Parameter(DbType = "BigInt")] long? characterSn, [Parameter(DbType = "NVarChar(50)")] string characterName, [Parameter(DbType = "NVarChar(200)")] string intro)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				serverCode,
				guildSn,
				nexonSn,
				characterSn,
				characterName,
				intro
			});
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GuildUserJoinReject")]
		public int GuildUserJoinReject([Parameter(DbType = "Int")] int? serverCode, [Parameter(DbType = "Int")] int? guildSn, [Parameter(DbType = "Int")] int? nexonSn, [Parameter(DbType = "BigInt")] long? characterSn, [Parameter(DbType = "NVarChar(50)")] string characterName)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				serverCode,
				guildSn,
				nexonSn,
				characterSn,
				characterName
			});
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GuildUserNameModify")]
		public int GuildUserNameModify([Parameter(DbType = "Int")] int? serverCode, [Parameter(DbType = "Int")] int? nexonSn, [Parameter(DbType = "BigInt")] long? characterSn, [Parameter(DbType = "NVarChar(50)")] string newName)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				serverCode,
				nexonSn,
				characterSn,
				newName
			});
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GuildUserSecede")]
		public int GuildUserSecede([Parameter(DbType = "Int")] int? serverCode, [Parameter(DbType = "Int")] int? guildSn, [Parameter(DbType = "Int")] int? nexonSn, [Parameter(DbType = "BigInt")] long? characterSn, [Parameter(DbType = "NVarChar(50)")] string characterName)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				serverCode,
				guildSn,
				nexonSn,
				characterSn,
				characterName
			});
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GuildUserTypeModify")]
		public int GuildUserTypeModify([Parameter(DbType = "Int")] int? serverCode, [Parameter(DbType = "Int")] int? guildSn, [Parameter(DbType = "Int")] int? nexonSn, [Parameter(DbType = "Int")] int? characterSn, [Parameter(DbType = "NVarChar(50)")] string characterName, [Parameter(DbType = "Int")] int? toRank)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				serverCode,
				guildSn,
				nexonSn,
				characterSn,
				characterName,
				toRank
			});
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GuildGetInfoByName")]
		public ISingleResult<GuildGetInfoByNameResult> GuildGetInfoByName([Parameter(DbType = "Int")] int? serverCode, [Parameter(DbType = "NVarChar(50)")] string guildName)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				serverCode,
				guildName
			});
			return (ISingleResult<GuildGetInfoByNameResult>)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GuildCreate")]
		public int GuildCreate([Parameter(DbType = "Int")] int? serverCode, [Parameter(DbType = "Int")] int? nexonSn, [Parameter(DbType = "BigInt")] long? characterSn, [Parameter(DbType = "NVarChar(50)")] string characterName, [Parameter(DbType = "NVarChar(50)")] string guildName, [Parameter(DbType = "NVarChar(24)")] string guildId, [Parameter(DbType = "NVarChar(200)")] string guildIntro)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				serverCode,
				nexonSn,
				characterSn,
				characterName,
				guildName,
				guildId,
				guildIntro
			});
			return (int)executeResult.ReturnValue;
		}

		private static MappingSource mappingSource = new AttributeMappingSource();
	}
}
