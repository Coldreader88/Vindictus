using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PartyServiceOperations
{
	[Serializable]
	public sealed class RemoveUser : Operation
	{
		public long CharacterID
		{
			get
			{
				return this.cid;
			}
		}

		public RemoveUser(long cid)
		{
			this.cid = cid;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new RemoveUser.Request(this);
		}

		private long cid;

		private class Request : OperationProcessor
		{
			public Request(RemoveUser op) : base(op)
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
