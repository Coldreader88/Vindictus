using System;
using System.Collections.Generic;
using ServiceCore.DSServiceOperations;
using UnifiedNetwork.Cooperation;
using Utility;

namespace DSService.Processor
{
	internal class UnregisterDSPlayerProcessor : OperationProcessor<UnregisterDSPlayer>
	{
		public UnregisterDSPlayerProcessor(DSService sv, UnregisterDSPlayer op) : base(op)
		{
			this.service = sv;
		}

		public override IEnumerable<object> Run()
		{
			Log<UnregisterDSPlayerProcessor>.Logger.Info("UnregisterDSPlayer");
			if (this.service.DSWaitingSystem != null)
			{
				this.service.DSWaitingSystem.Unregister(base.Operation.CID, base.Operation.ByUser);
				base.Finished = true;
				yield return new OkMessage();
			}
			else
			{
				Log<RegisterDSPartyProcessor>.Logger.Fatal("DS WaitingSystem is null. invalid DSBossID");
				base.Finished = true;
				yield return new FailMessage("[UnregisterDSPlayerProcessor] service.DSWaitingSystem");
			}
			yield break;
		}

		private DSService service;
	}
}
