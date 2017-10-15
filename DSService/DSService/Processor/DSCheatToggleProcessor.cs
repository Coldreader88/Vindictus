using System;
using System.Collections.Generic;
using ServiceCore.DSServiceOperations;
using ServiceCore.EndPointNetwork.DS;
using UnifiedNetwork.Cooperation;
using Utility;

namespace DSService.Processor
{
	internal class DSCheatToggleProcessor : OperationProcessor<DSCheatToggle>
	{
		public DSCheatToggleProcessor(DSService service, DSCheatToggle op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			bool result = false;
			try
			{
				string command = string.Format("sv_cheats {0};developer {0}", base.Operation.On);
				DSCommandMessage message = new DSCommandMessage(DSCommandType.ClientCmd, command);
				foreach (DSEntity dsentity in this.service.DSEntities.Values)
				{
					if (dsentity != null)
					{
						dsentity.SendMessage<DSCommandMessage>(message);
					}
				}
				result = true;
			}
			catch (Exception ex)
			{
				Log<DSCheatToggleProcessor>.Logger.Error("exception occurred!!!", ex);
			}
			if (result)
			{
				base.Finished = true;
				yield return new OkMessage();
			}
			else
			{
				base.Finished = true;
				yield return new FailMessage("[DSCheatToggleProcessor] result");
			}
			yield break;
		}

		private DSService service;
	}
}
