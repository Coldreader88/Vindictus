using System;
using System.Collections.Generic;
using ServiceCore.TalkServiceOperations;
using UnifiedNetwork.Entity;

namespace TalkService.Processor
{
	internal class WhisperProcessor : EntityProcessor<Whisper, TalkClient>
	{
		private TalkService Service { get; set; }

		public WhisperProcessor(TalkService service, Whisper op) : base(op)
		{
			this.Service = service;
		}

		public override IEnumerable<object> Run()
		{
			base.Finished = true;
			Whisper.WhisperResult result = this.Service.WhisperToAll(base.Operation.From, base.Entity.Entity.ID, base.Operation.To, base.Operation.Message);
			yield return result;
			yield break;
		}
	}
}
