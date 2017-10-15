using System;
using System.Collections.Generic;

namespace UnifiedNetwork.Cooperation
{
	public class OkOrFailProcessor : OperationProcessor
	{
		public OkOrFailProcessor(Operation op) : base(op)
		{
		}

		public override IEnumerable<object> Run()
		{
			yield return null;
			base.Result = (base.Feedback is OkMessage);
			yield break;
		}
	}
}
