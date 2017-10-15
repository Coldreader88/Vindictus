using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.DSServiceOperations
{
	[Serializable]
	public sealed class CancelMakeDSEntity : Operation
	{
		public long ID { get; set; }

		public DSType DSType { get; set; }

		public CancelMakeDSEntity(long id, DSType dsType)
		{
			this.ID = id;
			this.DSType = dsType;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
