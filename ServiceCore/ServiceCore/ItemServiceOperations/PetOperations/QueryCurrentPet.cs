using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations.PetOperations
{
	[Serializable]
	public sealed class QueryCurrentPet : Operation
	{
		public PetStatusInfo CurrentPet
		{
			get
			{
				return this.currentPet;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryCurrentPet.Request(this);
		}

		[NonSerialized]
		private PetStatusInfo currentPet;

		private class Request : OperationProcessor<QueryCurrentPet>
		{
			public Request(QueryCurrentPet op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is PetStatusInfo)
				{
					base.Operation.currentPet = (base.Feedback as PetStatusInfo);
				}
				else
				{
					base.Result = false;
					base.Operation.currentPet = null;
				}
				yield break;
			}
		}
	}
}
