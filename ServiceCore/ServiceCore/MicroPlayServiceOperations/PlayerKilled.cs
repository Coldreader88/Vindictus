using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class PlayerKilled : Operation
	{
		public long HostCID
		{
			get
			{
				return this.hostCID;
			}
		}

		public long Tag
		{
			get
			{
				return this.tag;
			}
		}

		public PlayerKilled(long host, long tag)
		{
			this.hostCID = host;
			this.tag = tag;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new PlayerKilled.Request(this);
		}

		private long hostCID;

		private long tag;

		private class Request : OperationProcessor
		{
			public Request(PlayerKilled op) : base(op)
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
