using System;
using System.Collections.Generic;
using System.Net;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PingServiceOperations
{
	[Serializable]
	public sealed class MakeGroup : Operation
	{
		public long MicroPlayId { get; set; }

		public long HostCid { get; set; }

		public int HostUid { get; set; }

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

		public long GroupID
		{
			get
			{
				return this.groupID;
			}
		}

		public MakeGroup(long microPlayID, long hostCid, int hostUid)
		{
			this.MicroPlayId = microPlayID;
			this.HostCid = hostCid;
			this.HostUid = hostUid;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new MakeGroup.Request(this);
		}

		[NonSerialized]
		private IPEndPoint endPoint;

		[NonSerialized]
		private int sn;

		[NonSerialized]
		private int key;

		[NonSerialized]
		private long groupID;

		private class Request : OperationProcessor<MakeGroup>
		{
			public Request(MakeGroup op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is IPEndPoint)
				{
					base.Operation.endPoint = (IPEndPoint)base.Feedback;
					yield return null;
					base.Operation.sn = (int)base.Feedback;
					yield return null;
					base.Operation.key = (int)base.Feedback;
					yield return null;
					base.Operation.groupID = (long)base.Feedback;
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
