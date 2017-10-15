using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PartyServiceOperations
{
	[Serializable]
	public sealed class PartyOption : Operation
	{
		public ShipOptionInfo Option
		{
			get
			{
				return this.option;
			}
		}

		public PartyOption(ShipOptionInfo option)
		{
			this.option = option;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new PartyOption.Request(this);
		}

		private ShipOptionInfo option;

		private class Request : OperationProcessor
		{
			public Request(PartyOption op) : base(op)
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
