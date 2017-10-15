using System;
using System.Collections.Generic;
using System.Net;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PingServiceOperations
{
	[Serializable]
	public sealed class AddPeer : Operation
	{
		public long PeerCid { get; private set; }

		public int PeerUid { get; private set; }

		public IPEndPoint EndPoint
		{
			get
			{
				return this.endPoint;
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

		public long HostCid
		{
			get
			{
				return this.hostCid;
			}
		}

		public long GroupID
		{
			get
			{
				return this.groupID;
			}
		}

		public AddPeer(long hostCid, int hostUid)
		{
			this.PeerCid = hostCid;
			this.PeerUid = hostUid;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new AddPeer.Request(this);
		}

		[NonSerialized]
		private IPEndPoint endPoint;

		[NonSerialized]
		private int sn;

		[NonSerialized]
		private int key;

		[NonSerialized]
		private long hostCid;

		[NonSerialized]
		private long groupID;

		private class Request : OperationProcessor<AddPeer>
		{
			public Request(AddPeer op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is FailMessage)
				{
					base.Result = false;
				}
				else
				{
					base.Operation.endPoint = (IPEndPoint)base.Feedback;
					yield return null;
					base.Operation.sn = (int)base.Feedback;
					yield return null;
					base.Operation.key = (int)base.Feedback;
					yield return null;
					base.Operation.hostCid = (long)base.Feedback;
					yield return null;
					base.Operation.groupID = (long)base.Feedback;
				}
				yield break;
			}
		}
	}
}
