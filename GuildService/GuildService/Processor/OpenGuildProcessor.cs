using System;
using System.Collections.Generic;
using GuildService.API;
using ServiceCore;
using ServiceCore.GuildServiceOperations;
using UnifiedNetwork.Cooperation;
using Utility;

namespace GuildService.Processor
{
	internal class OpenGuildProcessor : OperationProcessor<OpenGuild>
	{
		public OpenGuildProcessor(GuildService service, OpenGuild op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			Log<OpenGuildProcessor>.Logger.Warn("Starting OpenGuild...");
			Func<int> func = delegate
			{
				GroupIDCheckResult groupIDCheckResult = GuildAPI.GetAPI().CheckGroupID(base.Operation.GuildNameID);
				if (groupIDCheckResult != GroupIDCheckResult.Succeed)
				{
					int result;
					switch (groupIDCheckResult)
					{
					case GroupIDCheckResult.IDNotSupplied:
						result = -8;
						break;
					case GroupIDCheckResult.DuplicatedID:
						result = -9;
						break;
					case GroupIDCheckResult.InvalidCharacter:
						result = -1;
						break;
					default:
						Log<OpenGuildProcessor>.Logger.ErrorFormat("CheckGroupID result is {0}, but it's not processed by OpenGuildProcessor.Run().", groupIDCheckResult.ToString());
						result = -7;
						break;
					}
					return result;
				}
				GroupNameCheckResult groupNameCheckResult = GuildAPI.GetAPI().CheckGroupName(base.Operation.GuildName);
				if (groupNameCheckResult != GroupNameCheckResult.Succeed)
				{
					int result2;
					switch (groupNameCheckResult)
					{
					case GroupNameCheckResult.NotMatchedNamingRule:
						result2 = -2;
						break;
					case GroupNameCheckResult.NotMatchedNamingRuleMaxBytes:
						result2 = -5;
						break;
					case GroupNameCheckResult.RepeatedCharacters:
						result2 = -6;
						break;
					case GroupNameCheckResult.DuplicatedName:
						result2 = -4;
						break;
					default:
						result2 = -7;
						break;
					}
					return result2;
				}
				return GuildAPI.GetAPI().OpenGuild(base.Operation.GuildMemberKey, base.Operation.GuildName, base.Operation.GuildNameID, base.Operation.GuildIntro);
			};
			bool openResult = false;
			int openFuncResult = -99;
			if (FeatureMatrix.IsEnable("GuildProcessorNotUseAsync"))
			{
				openResult = true;
				openFuncResult = func();
			}
			else
			{
				AsyncFuncSync<int> sync = new AsyncFuncSync<int>(func);
				yield return sync;
				openResult = sync.Result;
				openFuncResult = sync.FuncResult;
			}
			Log<OpenGuildProcessor>.Logger.Warn("OpenGuild Finished");
			base.Finished = true;
			if (openResult)
			{
				Log<OpenGuildProcessor>.Logger.WarnFormat("OpenGuild Result : {0}", openResult);
				if (openFuncResult > 0)
				{
					using (HeroesDataContext heroesDataContext = new HeroesDataContext())
					{
						heroesDataContext.UpdateGuildCharacterInfo(new long?(base.Operation.GuildMemberKey.CID), new long?(0L));
						GuildLog.AddGuildLedger(new LogData((long)openFuncResult, base.Operation.GuildMemberKey.CID, OperationType.GainGuildPoint, GuildLedgerEventType.UpdateGuildUserPoint, "0", "OpenGuild"));
					}
					yield return (long)openFuncResult;
				}
				else if (openFuncResult == -1)
				{
					yield return "GuildID";
				}
				else if (openFuncResult == -2)
				{
					yield return "GuildNamingRule";
				}
				else if (openFuncResult == -3)
				{
					yield return "GuildInvalidName";
				}
				else if (openFuncResult == -4)
				{
					yield return "GuildDuplicatedName";
				}
				else if (openFuncResult == -5)
				{
					yield return "GuildNamingRuleMaxBytes";
				}
				else if (openFuncResult == -6)
				{
					yield return "GuildRepeatedCharacters";
				}
				else if (openFuncResult == -7)
				{
					yield return "GuildEtc";
				}
				else if (openFuncResult == -8)
				{
					yield return "GuildEmptyID";
				}
				else if (openFuncResult == -9)
				{
					yield return "GuildDuplicatedID";
				}
				else
				{
					yield return new FailMessage("[OpenGuildProcessor] openFuncResult");
				}
			}
			else
			{
				yield return new FailMessage("[OpenGuildProcessor] openResult");
			}
			yield break;
		}

		private GuildService service;
	}
}
