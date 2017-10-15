using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;
using GuildService.API;
using GuildService.Properties;
using ServiceCore;
using ServiceCore.EndPointNetwork.GuildService;
using Utility;

namespace GuildService
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

		public Table<GuildCharacterInfo> GuildCharacterInfo
		{
			get
			{
				return base.GetTable<GuildCharacterInfo>();
			}
		}

		public Table<Guild> Guild
		{
			get
			{
				return base.GetTable<Guild>();
			}
		}

		[Function(Name = "dbo.AddGuildStorageBriefLog")]
		public int AddGuildStorageBriefLog([Parameter(Name = "GuildID", DbType = "Int")] int? guildID, [Parameter(Name = "CharacterName", DbType = "NVarChar(32)")] string characterName, [Parameter(Name = "OperationType", DbType = "TinyInt")] byte? operationType, [Parameter(Name = "AddCount", DbType = "Int")] int? addCount, [Parameter(Name = "PickCount", DbType = "Int")] int? pickCount)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				guildID,
				characterName,
				operationType,
				addCount,
				pickCount
			});
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GetGuildStorageBriefLogs")]
		public ISingleResult<GetGuildStorageBriefLogs> GetGuildStorageBriefLogs([Parameter(Name = "GuildID", DbType = "Int")] int? guildID)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				guildID
			});
			return (ISingleResult<GetGuildStorageBriefLogs>)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GetGuildStorageSetting")]
		public ISingleResult<GetGuildStorageSetting> GetGuildStorageSetting([Parameter(Name = "GuildID", DbType = "Int")] int? guildID)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				guildID
			});
			return (ISingleResult<GetGuildStorageSetting>)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.InitGuildStorageSetting")]
		public int InitGuildStorageSetting([Parameter(Name = "GuildID", DbType = "Int")] int? guildID)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				guildID
			});
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.SetGuildStorageSetting")]
		public int SetGuildStorageSetting([Parameter(Name = "GuildID", DbType = "Int")] int? guildID, [Parameter(Name = "GoldLimit", DbType = "Int")] int? goldLimit, [Parameter(Name = "AccessLimitTag", DbType = "BigInt")] long? accessLimitTag)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				guildID,
				goldLimit,
				accessLimitTag
			});
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GetGuildStorageBriefLogsToday")]
		public ISingleResult<GetGuildStorageBriefLogsToday> GetGuildStorageBriefLogsToday([Parameter(Name = "GuildID", DbType = "Int")] int? guildID)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				guildID
			});
			return (ISingleResult<GetGuildStorageBriefLogsToday>)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.RemoveOldGuildStorageLog")]
		public int RemoveOldGuildStorageLog([Parameter(Name = "GuildID", DbType = "Int")] int? guildID)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				guildID
			});
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GetLatestConnectedDateByName")]
		public int GetLatestConnectedDateByName([Parameter(DbType = "NVarChar(50)")] string name, [Parameter(DbType = "DateTime")] ref DateTime? date)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				name,
				date
			});
			date = (DateTime?)executeResult.GetParameterValue(1);
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.AddGuildStorageItemLog")]
		public int AddGuildStorageItemLog([Parameter(Name = "GuildID", DbType = "Int")] int? guildID, [Parameter(Name = "CharacterName", DbType = "NVarChar(32)")] string characterName, [Parameter(Name = "OperationType", DbType = "TinyInt")] byte? operationType, [Parameter(Name = "ItemClass", DbType = "NVarChar(128)")] string itemClass, [Parameter(Name = "Count", DbType = "Int")] int? count, [Parameter(Name = "Color1", DbType = "Int")] int? color1, [Parameter(Name = "Color2", DbType = "Int")] int? color2, [Parameter(Name = "Color3", DbType = "Int")] int? color3)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				guildID,
				characterName,
				operationType,
				itemClass,
				count,
				color1,
				color2,
				color3
			});
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GetGuildStorageItemLogs")]
		public ISingleResult<GetGuildStorageItemLogs> GetGuildStorageItemLogs([Parameter(Name = "GuildID", DbType = "Int")] int? guildID)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				guildID
			});
			return (ISingleResult<GetGuildStorageItemLogs>)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GetGuildStorageItemLogsToday")]
		public ISingleResult<GetGuildStorageItemLogsToday> GetGuildStorageItemLogsToday([Parameter(Name = "GuildID", DbType = "Int")] int? guildID)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				guildID
			});
			return (ISingleResult<GetGuildStorageItemLogsToday>)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GetInGameGuildInfo")]
		public ISingleResult<GetInGameGuildInfo> GetInGameGuildInfo([Parameter(DbType = "Int")] int? codeGame, [Parameter(DbType = "Int")] int? codeServer, [Parameter(DbType = "Int")] int? oidGuild)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				codeGame,
				codeServer,
				oidGuild
			});
			return (ISingleResult<GetInGameGuildInfo>)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GetInGameGuildCharacterInfo")]
		public ISingleResult<GetInGameGuildCharacterInfo> GetInGameGuildCharacterInfo([Parameter(Name = "CID", DbType = "BigInt")] long? cID)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				cID
			});
			return (ISingleResult<GetInGameGuildCharacterInfo>)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.UpdateGuildCharacterInfo")]
		public int UpdateGuildCharacterInfo([Parameter(Name = "CID", DbType = "BigInt")] long? cID, [Parameter(Name = "Point", DbType = "BigInt")] long? point)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				cID,
				point
			});
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GetInGameGuildDailyGainGP")]
		public ISingleResult<GetInGameGuildDailyGainGP> GetInGameGuildDailyGainGP([Parameter(DbType = "Int")] int? codeGame, [Parameter(DbType = "Int")] int? codeServer, [Parameter(DbType = "Int")] int? oidGuild)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				codeGame,
				codeServer,
				oidGuild
			});
			return (ISingleResult<GetInGameGuildDailyGainGP>)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GetInGameGuildLastDailyGPReset")]
		public ISingleResult<GetInGameGuildLastDailyGPReset> GetInGameGuildLastDailyGPReset([Parameter(DbType = "Int")] int? codeGame, [Parameter(DbType = "Int")] int? codeServer, [Parameter(DbType = "Int")] int? oidGuild)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				codeGame,
				codeServer,
				oidGuild
			});
			return (ISingleResult<GetInGameGuildLastDailyGPReset>)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.ResetInGameGuildDailyGainGP")]
		public int ResetInGameGuildDailyGainGP([Parameter(DbType = "Int")] int? codeGame, [Parameter(DbType = "Int")] int? codeServer, [Parameter(DbType = "Int")] int? oidGuild)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				codeGame,
				codeServer,
				oidGuild
			});
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.UpdateInGameGuildDailyGainGP")]
		public int UpdateInGameGuildDailyGainGP([Parameter(DbType = "Int")] int? codeGame, [Parameter(DbType = "Int")] int? codeServer, [Parameter(DbType = "Int")] int? oidGuild, [Parameter(DbType = "TinyInt")] byte? gpGainType, [Parameter(DbType = "Int")] int? point)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				codeGame,
				codeServer,
				oidGuild,
				gpGainType,
				point
			});
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GetGuildCharacterMaxLevel")]
		public ISingleResult<GetGuildCharacterMaxLevel> GetGuildCharacterMaxLevel([Parameter(Name = "CID", DbType = "BigInt")] long? cID)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				cID
			});
			return (ISingleResult<GetGuildCharacterMaxLevel>)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.UpdateGuildCharacterMaxLevel")]
		public int UpdateGuildCharacterMaxLevel([Parameter(Name = "CID", DbType = "BigInt")] long? cID, [Parameter(Name = "GuildLevel", DbType = "Int")] int? guildLevel)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				cID,
				guildLevel
			});
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GetCharacterInfoByName")]
		public ISingleResult<GetCharacterInfoByName> GetCharacterInfoByName([Parameter(Name = "Name", DbType = "NVarChar(32)")] string name)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				name
			});
			return (ISingleResult<GetCharacterInfoByName>)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.UpdateGuildInfo")]
		public int UpdateGuildInfo([Parameter(DbType = "Int")] int? codeGame, [Parameter(DbType = "Int")] int? codeServer, [Parameter(DbType = "Int")] int? oidGuild, [Parameter(DbType = "Int")] int? maxMemberLimit, [Parameter(Name = "NewbieRecommend", DbType = "Bit")] bool? newbieRecommend, [Parameter(Name = "Level", DbType = "Int")] int? level, [Parameter(Name = "Exp", DbType = "BigInt")] long? exp, [Parameter(Name = "Notice", DbType = "NVarChar(256)")] string notice, [Parameter(Name = "DefaultMaxMemberLimit", DbType = "Int")] int? defaultMaxMemberLimit, [Parameter(Name = "TotalGP", DbType = "BigInt")] long? totalGP)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				codeGame,
				codeServer,
				oidGuild,
				maxMemberLimit,
				newbieRecommend,
				level,
				exp,
				notice,
				defaultMaxMemberLimit,
				totalGP
			});
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GuildInfoUpdate")]
		public int GuildInfoUpdate([Parameter(Name = "GuildSN", DbType = "Int")] int? guildSN, [Parameter(Name = "GuildID", DbType = "VarChar(24)")] string guildID, [Parameter(Name = "GuildName", DbType = "NVarChar(50)")] string guildName)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				guildSN,
				guildID,
				guildName
			});
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GuildTransSelect")]
		public int GuildTransSelect([Parameter(Name = "CID", DbType = "BigInt")] long? cID, [Parameter(Name = "GuildSN", DbType = "Int")] ref int? guildSN)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				cID,
				guildSN
			});
			guildSN = (int?)executeResult.GetParameterValue(1);
			return (int)executeResult.ReturnValue;
		}

		public int GetMaxMemberLimit(int guildSN)
		{
			if (FeatureMatrix.IsEnable("InGameGuild"))
			{
				if (FeatureMatrix.GetInteger("InGameGuild_MaxMemberLimit") > FeatureMatrix.GetInteger("InGameGuild_MaxMember"))
				{
					GetInGameGuildInfo getInGameGuildInfo = this.GetInGameGuildInfo(new int?(FeatureMatrix.GameCode), new int?(GuildAPI.ServerCode), new int?(guildSN)).FirstOrDefault<GetInGameGuildInfo>();
					if (getInGameGuildInfo != null && getInGameGuildInfo.MaxMemberLimit > FeatureMatrix.GetInteger("InGameGuild_MaxMember"))
					{
						return getInGameGuildInfo.MaxMemberLimit;
					}
				}
				return FeatureMatrix.GetInteger("InGameGuild_MaxMember");
			}
			return 0;
		}

		public bool IsRecommendedGuild(int guildSN)
		{
			if (FeatureMatrix.IsEnable("NewbieGuildRecommend"))
			{
				GetInGameGuildInfo getInGameGuildInfo = this.GetInGameGuildInfo(new int?(FeatureMatrix.GameCode), new int?(ServiceCoreSettings.ServerCode), new int?(guildSN)).FirstOrDefault<GetInGameGuildInfo>();
				return getInGameGuildInfo != null && getInGameGuildInfo.NewbieRecommend;
			}
			return false;
		}

		public bool UpdateMaxMemberLimit(int guildSN, int maxMemberLimit)
		{
			if (FeatureMatrix.IsEnable("InGameGuild") && FeatureMatrix.GetInteger("InGameGuild_MaxMemberLimit") >= maxMemberLimit)
			{
				int num = this.UpdateGuildInfo(new int?(FeatureMatrix.GameCode), new int?(GuildAPI.ServerCode), new int?(guildSN), new int?(maxMemberLimit), null, null, null, null, new int?(FeatureMatrix.GetInteger("InGameGuild_MaxMember")), null);
				if (num == 1)
				{
					return true;
				}
			}
			return false;
		}

		public bool UpdateNewbieRecommend(int guildSN, bool recommend)
		{
			if (FeatureMatrix.IsEnable("NewbieGuildRecommend"))
			{
				int num = this.UpdateGuildInfo(new int?(FeatureMatrix.GameCode), new int?(GuildAPI.ServerCode), new int?(guildSN), null, new bool?(recommend), null, null, null, new int?(FeatureMatrix.GetInteger("InGameGuild_MaxMember")), null);
				if (num == 1)
				{
					return true;
				}
			}
			return false;
		}

		public GetInGameGuildInfo GetGuildInfo(int guildSN)
		{
			GetInGameGuildInfo getInGameGuildInfo = this.GetInGameGuildInfo(new int?(FeatureMatrix.GameCode), new int?(GuildAPI.ServerCode), new int?(guildSN)).FirstOrDefault<GetInGameGuildInfo>();
			if (getInGameGuildInfo != null)
			{
				return getInGameGuildInfo;
			}
			return new GetInGameGuildInfo
			{
				Level = 1,
				Exp = 0L,
				NewbieRecommend = false,
				Notice = ""
			};
		}

		public Dictionary<byte, int> GetGuildDailyGainGP(int guildSN)
		{
			ISingleResult<GetInGameGuildDailyGainGP> inGameGuildDailyGainGP = this.GetInGameGuildDailyGainGP(new int?(FeatureMatrix.GameCode), new int?(GuildAPI.ServerCode), new int?(guildSN));
			Dictionary<byte, int> dictionary = new Dictionary<byte, int>();
			foreach (GetInGameGuildDailyGainGP getInGameGuildDailyGainGP in inGameGuildDailyGainGP)
			{
				if (!GPGainTypeUtil.IsValidGPGainTypeValue((int)getInGameGuildDailyGainGP.GPGainType, false))
				{
					Log<GuildAPI>.Logger.ErrorFormat("Invalid GPGainType value is retrieved from DB: GPGainType: {0}, GuildSN: {1}", getInGameGuildDailyGainGP.GPGainType, guildSN);
				}
				else
				{
					dictionary.Add(getInGameGuildDailyGainGP.GPGainType, getInGameGuildDailyGainGP.Point);
				}
			}
			return dictionary;
		}

		public DateTime GetGuildLastDailyGPReset(int guildSN)
		{
			if (FeatureMatrix.IsEnable("GuildLevel"))
			{
				GetInGameGuildLastDailyGPReset getInGameGuildLastDailyGPReset = this.GetInGameGuildLastDailyGPReset(new int?(FeatureMatrix.GameCode), new int?(GuildAPI.ServerCode), new int?(guildSN)).FirstOrDefault<GetInGameGuildLastDailyGPReset>();
				if (getInGameGuildLastDailyGPReset != null)
				{
					return getInGameGuildLastDailyGPReset.LastDailyGPReset;
				}
			}
			return DateTime.Now;
		}

		public bool UpdateInGameGuildDailyGainGP(int guildSN, GPGainType gainType, int point)
		{
			if (FeatureMatrix.IsEnable("GuildLevel"))
			{
				int num = this.UpdateInGameGuildDailyGainGP(new int?(FeatureMatrix.GameCode), new int?(GuildAPI.ServerCode), new int?(guildSN), new byte?((byte)gainType), new int?(point));
				if (num == 1)
				{
					return true;
				}
			}
			return false;
		}

		public bool ResetInGameGuildDailyGainGP(int guildSN)
		{
			if (FeatureMatrix.IsEnable("GuildLevel"))
			{
				int num = this.ResetInGameGuildDailyGainGP(new int?(FeatureMatrix.GameCode), new int?(GuildAPI.ServerCode), new int?(guildSN));
				if (num == 1)
				{
					return true;
				}
			}
			return false;
		}

		public long GetGuildCharacterPoint(long cid)
		{
			if (FeatureMatrix.IsEnable("GuildLevel"))
			{
				GetInGameGuildCharacterInfo getInGameGuildCharacterInfo = this.GetInGameGuildCharacterInfo(new long?(cid)).FirstOrDefault<GetInGameGuildCharacterInfo>();
				if (getInGameGuildCharacterInfo != null)
				{
					return getInGameGuildCharacterInfo.Point;
				}
			}
			return 0L;
		}

		private static MappingSource mappingSource = new AttributeMappingSource();
	}
}
