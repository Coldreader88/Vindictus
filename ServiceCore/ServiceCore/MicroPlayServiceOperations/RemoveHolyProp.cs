using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class RemoveHolyProp : Operation
	{
		public int Type { get; set; }

		public RemoveHolyProp(int Type)
		{
			this.Type = Type;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new RemoveHolyProp.Request(this);
		}

		private class Request : OperationProcessor
		{
			public Request(RemoveHolyProp op) : base(op)
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
}
