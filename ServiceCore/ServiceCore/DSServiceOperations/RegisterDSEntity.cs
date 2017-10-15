using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.DSServiceOperations
{
	[Serializable]
	public sealed class RegisterDSEntity : Operation
	{
		public int ServiceID { get; set; }

		public int CoreCount { get; set; }

		public bool GiantRaidMachine { get; set; }

		public RegisterDSEntity(int serviceID, int coreCount, bool giantRaidMachine)
		{
			this.ServiceID = serviceID;
			this.CoreCount = coreCount;
			this.GiantRaidMachine = giantRaidMachine;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new RegisterDSEntity.Request(this);
		}

		[NonSerialized]
		public int IDStart;

		[NonSerialized]
		public int ProcessCount;

		[NonSerialized]
		public DSType DSType;

		private class Request : OperationProcessor<RegisterDSEntity>
		{
			public Request(RegisterDSEntity op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is int)
				{
					base.Operation.IDStart = (int)base.Feedback;
					yield return null;
					base.Operation.ProcessCount = (int)base.Feedback;
					yield return null;
					base.Operation.DSType = (DSType)base.Feedback;
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
