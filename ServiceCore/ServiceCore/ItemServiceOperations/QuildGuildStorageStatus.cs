using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class QuildGuildStorageStatus : Operation
	{
		public GuildInventoryStatus Status
		{
			get
			{
				return this.status;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QuildGuildStorageStatus.Request(this);
		}

		[NonSerialized]
		private GuildInventoryStatus status;

		private class Request : OperationProcessor<QuildGuildStorageStatus>
		{
			public Request(QuildGuildStorageStatus op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is GuildInventoryStatus)
				{
					base.Operation.status = (base.Feedback as GuildInventoryStatus);
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
