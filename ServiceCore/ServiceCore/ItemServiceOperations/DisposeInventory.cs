using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class DisposeInventory : Operation
	{
		public override OperationProcessor RequestProcessor()
		{
			return new DisposeInventory.Request(this);
		}

		private class Request : OperationProcessor
		{
			public Request(DisposeInventory op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				yield break;
			}
		}
	}
}
