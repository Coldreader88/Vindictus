using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.DSServiceOperations
{
	[Serializable]
	public sealed class MakeDSEntity : Operation
	{
		public MakeDSEntity(int dsID)
		{
			this.DSID = dsID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new MakeDSEntity.Request(this);
		}

		public int DSID;

		private class Request : OperationProcessor<MakeDSEntity>
		{
			public Request(MakeDSEntity op) : base(op)
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
