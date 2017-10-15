using System;
using System.Collections.Generic;
using DSService.WaitingQueue;
using ServiceCore.DSServiceOperations;
using ServiceCore.EndPointNetwork.DS;
using UnifiedNetwork.Cooperation;
using Utility;

namespace DSService.Processor
{
	internal class UpdateDSPlayerInfoProcessor : OperationProcessor<UpdateDSPlayerInfo>
	{
		public UpdateDSPlayerInfoProcessor(DSService service, UpdateDSPlayerInfo op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			if (this.service.DSWaitingSystem != null)
			{
				DSPlayer player = this.service.DSWaitingSystem.DSPlayerDict.TryGetValue(base.Operation.CID);
				if (player != null && base.Operation.Status == DSPlayerStatus.InShip)
				{
					player.EnterShip();
					base.Finished = true;
					yield return new OkMessage();
					goto IL_E5;
				}
			}
			base.Finished = true;
			yield return new FailMessage("[UpdateDSPlayerInfoProcessor] service.DSWaitingSystem");
			IL_E5:
			yield break;
		}

		private DSService service;
	}
}
