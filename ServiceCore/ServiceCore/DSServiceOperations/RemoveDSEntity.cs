using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.DSServiceOperations
{
	[Serializable]
	public sealed class RemoveDSEntity : Operation
	{
		public int DSID { get; set; }

		public RemoveDSEntity(int dsID)
		{
			this.DSID = dsID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new RemoveDSEntity.Request(this);
		}

		private class Request : OperationProcessor<RemoveDSEntity>
		{
			public Request(RemoveDSEntity op) : base(op)
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
