using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class PickErg : Operation
	{
		public long HostCID
		{
			get
			{
				return this.hostCID;
			}
		}

		public int EID
		{
			get
			{
				return this.ergID;
			}
		}

		public int PickedPlayerTag
		{
			get
			{
				return this.pickedErg.Winner;
			}
		}

		public ErgInfo PickedErg
		{
			get
			{
				return this.pickedErg;
			}
		}

		public PickErg(long cid, int eid)
		{
			this.hostCID = cid;
			this.ergID = eid;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new PickErg.Request(this);
		}

		private long hostCID;

		private int ergID;

		[NonSerialized]
		private ErgInfo pickedErg;

		private class Request : OperationProcessor<PickErg>
		{
			public Request(PickErg op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is ErgInfo)
				{
					base.Result = true;
					base.Operation.pickedErg = (base.Feedback as ErgInfo);
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
