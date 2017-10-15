using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.DSServiceOperations
{
	[Serializable]
	public sealed class RegisterDSServiceInfo : Operation
	{
		public int ServiceID { get; set; }

		public int CoreCount { get; set; }

		public RegisterDSServiceInfo(int serviceID, int coreCount)
		{
			this.ServiceID = serviceID;
			this.CoreCount = coreCount;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new RegisterDSServiceInfo.Request(this);
		}

		private class Request : OperationProcessor<RegisterDSServiceInfo>
		{
			public Request(RegisterDSServiceInfo op) : base(op)
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
