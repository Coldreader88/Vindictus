using System;
using System.Collections.Generic;
using ServiceCore.CommonOperations;
using UnifiedNetwork.Cooperation;
using Utility;

namespace GuildService.Processor
{
	public class AdminCommandProcessor : OperationProcessor<AdminCommand>
	{
		public AdminCommandProcessor(GuildService sv, AdminCommand op) : base(op)
		{
			this.service = sv;
		}

		public override IEnumerable<object> Run()
		{
			base.Finished = true;
			try
			{
				if (base.Operation.Command == "guildwebchat_activate")
				{
					if (base.Operation.Argument != "on")
					{
						this.service.ChatRelay.Close();
					}
					else
					{
						this.service.ConnectToRelayServer();
					}
					yield break;
				}
				if (string.Compare(base.Operation.Command, "query_service_id", true) == 0)
				{
					Log<AdminCommandProcessor>.Logger.ErrorFormat("query_service_id: {0} category: {1}", this.service.ID, this.service.Category);
					yield break;
				}
				if (string.Compare(base.Operation.Command, "clr_profiler", true) == 0 && string.Compare(base.Operation.Argument, "disable_log", true) == 0)
				{
					if (CLRProfilerController.TrySetEnableLogging(false))
					{
						Log<AdminCommandProcessor>.Logger.InfoFormat("clr_profiler command is succeeded. arg: {0}", base.Operation.Argument);
					}
					else
					{
						Log<AdminCommandProcessor>.Logger.ErrorFormat("clr_profiler command is failed. arg: {0}", base.Operation.Argument);
					}
					yield break;
				}
			}
			catch (Exception ex)
			{
				Log<AdminCommandProcessor>.Logger.ErrorFormat("Error while execute AdminCommand : [{0} {1}]", base.Operation.Command, ex.Message);
				FileLog.Log("AdminClientProcess.log", ex.Message);
				yield break;
			}
			Log<AdminCommandProcessor>.Logger.ErrorFormat("Wrong AdminCommand. [{0} {1}]", base.Operation.Command, base.Operation.Argument);
			yield break;
		}

		private GuildService service;
	}
}
