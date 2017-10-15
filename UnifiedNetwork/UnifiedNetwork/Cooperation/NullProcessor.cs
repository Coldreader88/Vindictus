using System;
using System.Collections.Generic;

namespace UnifiedNetwork.Cooperation
{
	public class NullProcessor : OperationProcessor
	{
		public NullProcessor(Operation op) : base(op)
		{
		}

		public override IEnumerable<object> Run()
		{
			yield break;
		}
	}
}
