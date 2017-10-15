using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PartyServiceOperations
{
	[Serializable]
	public sealed class ShipLaunched : Operation
	{
		public long HostCID { get; set; }

		public long MicroPlayID { get; set; }

		public int QuestLevel { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new ShipLaunched.Request(this);
		}

		private class Request : OperationProcessor
		{
			public Request(ShipLaunched op) : base(op)
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
