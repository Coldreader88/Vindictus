using System;
using System.Collections.Generic;
using System.Net;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ChannelServiceOperations
{
	[Serializable]
	public sealed class JoinChannel : Operation
	{
		public long CID { get; set; }

		public int NexonSN { get; set; }

		public bool IsNewConnection
		{
			get
			{
				return this.newConnection;
			}
		}

		public IPEndPoint EndPoint
		{
			get
			{
				return this.endpoint;
			}
		}

		public int SN
		{
			get
			{
				return this.sn;
			}
		}

		public int Key
		{
			get
			{
				return this.key;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new JoinChannel.Request(this);
		}

		[NonSerialized]
		private bool newConnection;

		[NonSerialized]
		private IPEndPoint endpoint;

		[NonSerialized]
		private int sn;

		[NonSerialized]
		private int key;

		private class Request : OperationProcessor<JoinChannel>
		{
			public Request(JoinChannel op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.newConnection = (bool)base.Feedback;
				if (base.Operation.newConnection)
				{
					yield return null;
					base.Operation.endpoint = (IPEndPoint)base.Feedback;
					yield return null;
					base.Operation.sn = (int)base.Feedback;
					yield return null;
					base.Operation.key = (int)base.Feedback;
				}
				yield break;
			}
		}
	}
}
