using System;
using Devcat.Core.Threading;
using ServiceCore;
using Utility;

namespace GuildService
{
	public static class GuildLog
	{
		private static void _AddGuildLedger(LogData logData)
		{
			try
			{
				HeroesLogDataContext heroesLogDataContext = new HeroesLogDataContext();
				heroesLogDataContext.AddGuildLedger(new long?(logData.GuildSN), new long?(logData.CID), new short?((short)logData.Operation), new short?(logData.Event), logData.Arg1, logData.Arg2);
			}
			catch (Exception ex)
			{
				Log<GuildEntity>.Logger.Error(string.Format("Error while Saving Logs.]", new object[0]), ex);
			}
		}

		public static void AddGuildLedger(LogData logData)
		{
			if (FeatureMatrix.IsEnable("DisableLogging"))
			{
				return;
			}
			if (FeatureMatrix.IsEnable("AsynchronousLogging"))
			{
				Scheduler.Schedule(GuildService.Instance.LogThread, Job.Create(delegate
				{
					GuildLog._AddGuildLedger(logData);
				}), 1);
				return;
			}
			GuildLog._AddGuildLedger(logData);
		}
	}
}
