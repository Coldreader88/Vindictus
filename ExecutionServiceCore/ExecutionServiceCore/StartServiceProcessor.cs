using System;
using System.Collections.Generic;
using ServiceCore.ExecutionServiceOperations;
using UnifiedNetwork.Cooperation;
using Utility;

namespace ExecutionServiceCore
{
	internal class StartServiceProcessor : OperationProcessor<StartService>
	{
		internal StartServiceProcessor(ExecutionService service, StartService op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			base.Finished = true;
			string message;
			if (this.service.StartService(base.Operation.ServiceName, out message))
			{
				yield return new OkMessage();
			}
			else
			{
				Log<StartServiceProcessor>.Logger.Error(message);
				yield return new FailMessage("[StartServiceProcessor] service.StartService");
			}
			yield break;
		}

		private ExecutionService service;
	}
}
