using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.DSServiceOperations
{
	[Serializable]
	public sealed class RequestMakeDSEntity : Operation
	{
		public long ID { get; set; }

		public DSType DSType { get; set; }

		public int PVPServiceID { get; set; }

		public RequestMakeDSEntity(long id, DSType dsType)
		{
			this.ID = id;
			this.DSType = dsType;
		}

		public RequestMakeDSEntity(long id, DSType dsType, int pvpServiceID)
		{
			this.ID = id;
			this.DSType = dsType;
			this.PVPServiceID = pvpServiceID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new RequestMakeDSEntity.Request(this);
		}

		private class Request : OperationProcessor<RequestMakeDSEntity>
		{
			public Request(RequestMakeDSEntity op) : base(op)
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
