using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;
using Utility;

namespace UnifiedNetwork.LocationService.Test
{
	internal class Plus1Processor : OperationProcessor<Plus1>
	{
		public Plus1Processor(BService service, Plus1 op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			base.Finished = true;
			yield return base.Operation.Input + this.service.ID;
			Log<Plus1Processor>.Logger.DebugFormat("{0} + {1} = ?", base.Operation.Input, this.service.ID);
			yield break;
		}

		private BService service;
	}
}
