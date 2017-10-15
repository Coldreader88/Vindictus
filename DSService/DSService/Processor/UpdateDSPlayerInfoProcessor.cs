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
                DSPlayer player = this.service.DSWaitingSystem.DSPlayerDict.TryGetValue<long, DSPlayer>(this.Operation.CID);
                if (player != null && this.Operation.Status == DSPlayerStatus.InShip)
                {
                    player.EnterShip();
                    this.Finished = true;
                    yield return (object)new OkMessage();
                    yield break;
                }
            }
            this.Finished = true;
            yield return (object)new FailMessage("[UpdateDSPlayerInfoProcessor] service.DSWaitingSystem");
        }

        private DSService service;
	}
}
