using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GuildService.API;
using ServiceCore;
using ServiceCore.CommonOperations;
using ServiceCore.EndPointNetwork.GuildService;
using ServiceCore.FrontendServiceOperations;
using ServiceCore.GuildServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using Utility;

namespace GuildService.Processor
{
	internal class _CheatCommandProcessor : EntityProcessor<_CheatCommand, GuildEntity>
	{
		private static Dictionary<string, MethodInfo> MethodDict { get; set; } = new Dictionary<string, MethodInfo>();

		static _CheatCommandProcessor()
		{
			Type typeFromHandle = typeof(_CheatCommandProcessor);
			foreach (MethodInfo methodInfo in typeFromHandle.GetMethods())
			{
				CheatFunction cheatFunction = methodInfo.GetCustomAttributes(typeof(CheatFunction), false).FirstOrDefault<object>() as CheatFunction;
				if (cheatFunction != null)
				{
					_CheatCommandProcessor.MethodDict.Add(cheatFunction.Command, methodInfo);
				}
			}
		}

		public _CheatCommandProcessor(GuildService service, _CheatCommand op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			MethodInfo method = _CheatCommandProcessor.MethodDict.TryGetValue(base.Operation.Command);
			if (method == null)
			{
				Log<_CheatCommandProcessor>.Logger.ErrorFormat("No Cheat Method [{0}]", base.Operation.Command);
				base.Finished = true;
				yield return new FailMessage("[_CheatCommandProcessor] method");
			}
			else
			{
				Log<_CheatCommandProcessor>.Logger.WarnFormat("Process CheatCommand [{0} {1} {2}]", this.service.GetType().Name, base.Operation.Command, base.Operation.Arg);
				bool processed = false;
				try
				{
					processed = (bool)method.Invoke(this, null);
				}
				catch (Exception ex)
				{
					Log<_CheatCommandProcessor>.Logger.Error("Exception while cheating", ex);
				}
				if (processed)
				{
					base.Finished = true;
					yield return new OkMessage();
				}
				else
				{
					base.Finished = true;
					yield return new FailMessage("[_CheatCommandProcessor] processed");
				}
			}
			yield break;
		}

		[CheatFunction("guild_debug")]
		public bool Debug()
		{
			string argString = base.Operation.GetArgString(0, "");
			string a;
			if ((a = argString) != null)
			{
				if (!(a == "clear"))
				{
					if (a == "trace")
					{
						foreach (OnlineGuildMember onlineGuildMember in base.Entity.OnlineMembers.Values)
						{
							onlineGuildMember.RequestFrontendOperation(new GuildKicked(GuildResultEnum.ConnectFail));
							onlineGuildMember.Disconnect();
						}
						base.Entity.Entity.Close(true);
					}
				}
				else
				{
					foreach (OnlineGuildMember onlineGuildMember2 in base.Entity.OnlineMembers.Values)
					{
						onlineGuildMember2.RequestFrontendOperation(new GuildKicked(GuildResultEnum.ConnectFail));
						onlineGuildMember2.Disconnect();
					}
					base.Entity.Entity.Close(true);
				}
			}
			return true;
		}

		[CheatFunction("give_gp")]
		public bool GiveGP()
		{
			int argInt = base.Operation.GetArgInt(0, 0);
			int argInt2 = base.Operation.GetArgInt(1, 255);
			bool flag = base.Operation.GetArgInt(2, 0) != 0;
			if (!GPGainTypeUtil.IsValidGPGainTypeValue(argInt2, true))
			{
				Log<_CheatCommandProcessor>.Logger.ErrorFormat("Invalid gainType value is retrieved from DB: GPGainType: {0}, GuildSN: {1}", argInt2, base.Entity.GuildSN);
				return false;
			}
			if (!flag)
			{
				int num = base.Entity.GiveGP(argInt, (GPGainType)argInt2, 0L);
				if (num != 0)
				{
					using (HeroesDataContext heroesDataContext = new HeroesDataContext())
					{
						heroesDataContext.UpdateGuildInfo(new int?(FeatureMatrix.GameCode), new int?(GuildAPI.ServerCode), new int?(base.Entity.GuildSN), null, null, new int?(base.Entity.GuildInfo.GuildLevel), new long?(base.Entity.GuildInfo.GuildPoint), null, new int?(FeatureMatrix.GetInteger("InGameGuild_MaxMember")), null);
					}
					base.Entity.ReportGuildInfoChanged();
					base.Entity.Sync();
				}
				return true;
			}
			long num2 = 0L;
			foreach (KeyValuePair<string, OnlineGuildMember> keyValuePair in base.Entity.OnlineMembers)
			{
				if (keyValuePair.Value.FID == base.Connection.RemoteID)
				{
					num2 = keyValuePair.Value.CID;
					break;
				}
			}
			if (num2 == 0L)
			{
				Log<_CheatCommandProcessor>.Logger.WarnFormat("Could not find _CheatCommand requester by FID. FID: {0}", base.Connection.RemoteID);
				return false;
			}
			GiveGuildGP giveGuildGP = new GiveGuildGP();
			giveGuildGP.GainType = (GPGainType)argInt2;
			giveGuildGP.GiveDict.Add(num2, argInt);
			this.service.RequestOperation(base.Entity.Entity, new Location(base.Entity.GuildID, "GuildService.GuildService"), giveGuildGP);
			return true;
		}

		[CheatFunction("set_daily_gp_limit")]
		public bool SetDailyGPLimit()
		{
			string argString = base.Operation.GetArgString(0, "");
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("GuildLevel_DailyGPLimit", argString);
			FeatureMatrix.OverrideFeature(dictionary);
			GuildContents.LoadGuildDailyGPLimit();
			GuildService guildService = base.Entity.Service;
			if (guildService != null)
			{
				UpdateFeatureMatrix op = new UpdateFeatureMatrix(dictionary);
				foreach (IEntity entity in guildService.Entities)
				{
					GuildEntity guildEntity = entity.Tag as GuildEntity;
					foreach (OnlineGuildMember onlineGuildMember in guildEntity.OnlineMembers.Values)
					{
						onlineGuildMember.FrontendConn.RequestOperation(op);
					}
				}
			}
			return true;
		}

		[CheatFunction("reset_daily_gp_all_guild")]
		public bool ResetDailyGPAllGuild()
		{
			this.service.ResetOnlineGuildDailyGPScheduleFunc();
			return true;
		}

		[CheatFunction("set_guild_level")]
		public bool set_guild_level()
		{
			int argInt = base.Operation.GetArgInt(0, 1);
			int argInt2 = base.Operation.GetArgInt(1, 0);
			base.Entity.SetGuildLevel(argInt, argInt2);
			using (HeroesDataContext heroesDataContext = new HeroesDataContext())
			{
				heroesDataContext.UpdateGuildInfo(new int?(FeatureMatrix.GameCode), new int?(GuildAPI.ServerCode), new int?(base.Entity.GuildSN), null, null, new int?(base.Entity.GuildInfo.GuildLevel), new long?(base.Entity.GuildInfo.GuildPoint), null, new int?(FeatureMatrix.GetInteger("InGameGuild_MaxMember")), null);
			}
			base.Entity.ReportGuildInfoChanged();
			base.Entity.Sync();
			return true;
		}

		private GuildService service;
	}
}
