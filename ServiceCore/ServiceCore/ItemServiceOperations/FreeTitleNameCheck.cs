using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class FreeTitleNameCheck : Operation
	{
		public string FreeTitleName { get; set; }

		public FreeTitleNameCheck(string freeTitleName)
		{
			this.FreeTitleName = freeTitleName;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new FreeTitleNameCheck.Request(this);
		}

		[NonSerialized]
		public bool isSuccess;

		[NonSerialized]
		public bool hasFreeTitle;

		private class Request : OperationProcessor<FreeTitleNameCheck>
		{
			public Request(FreeTitleNameCheck op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.isSuccess = (bool)base.Feedback;
				yield return null;
				base.Operation.hasFreeTitle = (bool)base.Feedback;
				yield break;
			}
		}
	}
}
