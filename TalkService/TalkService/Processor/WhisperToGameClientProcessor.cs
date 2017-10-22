using System;
using System.Collections.Generic;
using ServiceCore.TalkServiceOperations;
using UnifiedNetwork.Cooperation;

namespace TalkService.Processor
{
	internal class WhisperToGameClientProcessor : OperationProcessor<WhisperToGameClient>
	{
		private TalkService Service { get; set; }

		public WhisperToGameClientProcessor(TalkService service, WhisperToGameClient op) : base(op)
		{
			this.Service = service;
		}

		public override IEnumerable<object> Run()
		{
			base.Finished = true;
			if (this.Service.WhisperToGameClient(base.Operation.From, base.Operation.ToCID, base.Operation.Message))
			{
				yield return new OkMessage();
			}
			else
			{
				yield return new FailMessage("WhisperToGameClientProcessor[] Service.WhisperToGameClient");
			}
			yield break;
		}
	}
}
