using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class AddGuildStorageSlots : Operation
	{
		public int SlotCount { get; set; }

		public int ResultSlotCount
		{
			get
			{
				return this.count;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new AddGuildStorageSlots.Request(this);
		}

		[NonSerialized]
		private int count;

		private class Request : OperationProcessor<AddGuildStorageSlots>
		{
			public Request(AddGuildStorageSlots op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is int)
				{
					base.Operation.count = (int)base.Feedback;
				}
				else
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
