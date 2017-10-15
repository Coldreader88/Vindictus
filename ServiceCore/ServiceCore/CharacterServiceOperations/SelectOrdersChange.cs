using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class SelectOrdersChange : Operation
	{
		public int NexonSN { get; set; }

		public ICollection<byte> SelectOrders { get; set; }

		public SelectOrdersChange(int nexonSN, ICollection<byte> selectOrders)
		{
			this.NexonSN = nexonSN;
			this.SelectOrders = selectOrders;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new SelectOrdersChange.Request(this);
		}

		private class Request : OperationProcessor<SelectOrdersChange>
		{
			public Request(SelectOrdersChange op) : base(op)
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
