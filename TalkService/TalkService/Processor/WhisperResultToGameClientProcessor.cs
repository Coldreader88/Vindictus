using System;
using System.Collections.Generic;
using ServiceCore.TalkServiceOperations;
using UnifiedNetwork.Cooperation;

namespace TalkService.Processor
{
	internal class WhisperResultToGameClientProcessor : OperationProcessor<WhisperResultToGameClient>
	{
		private TalkService Service { get; set; }

		public WhisperResultToGameClientProcessor(TalkService service, WhisperResultToGameClient op) : base(op)
		{
			this.Service = service;
		}

		public override IEnumerable<object> Run()
		{
			base.Finished = true;
			if (this.Service.WhipserResultToGameClient(base.Operation.MyCID, base.Operation.ResultNo, base.Operation.ReceiverName))
			{
				yield return new OkMessage();
			}
			else
			{
				yield return new FailMessage("[WhisperResultToGameClientProcessor] Service.WhipserResultToGameClient");
			}
			yield break;
		}
	}
}
