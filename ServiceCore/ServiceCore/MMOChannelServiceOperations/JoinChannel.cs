using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MMOChannelServiceOperations
{
	[Serializable]
	public class JoinChannel : Operation
	{
		public long CID { get; private set; }

		public long PartitionID { get; private set; }

		public ActionSync Action { get; private set; }

		public ChannelServerAddress AddressMessage
		{
			get
			{
				return this.addressMessage;
			}
		}

		public JoinChannel.FailReasonEnum FailReason
		{
			get
			{
				return this.failReason;
			}
			set
			{
				this.failReason = value;
			}
		}

		public JoinChannel(long cid, long pid, ActionSync action)
		{
			this.CID = cid;
			this.PartitionID = pid;
			this.Action = action;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new JoinChannel.Request(this);
		}

		[NonSerialized]
		private ChannelServerAddress addressMessage;

		[NonSerialized]
		private JoinChannel.FailReasonEnum failReason;

		public enum FailReasonEnum
		{
			Unknown,
			NotFrontEnd,
			MaxCapacityOver,
			HasWaitingMemeber
		}

		private class Request : OperationProcessor<JoinChannel>
		{
			public Request(JoinChannel op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is JoinChannel.FailReasonEnum)
				{
					base.Result = false;
					base.Operation.FailReason = (JoinChannel.FailReasonEnum)base.Feedback;
				}
				else if (base.Feedback.GetType() != typeof(string))
				{
					base.Result = false;
				}
				else
				{
					string address = base.Feedback as string;
					yield return null;
					int port = (int)base.Feedback;
					yield return null;
					long channelID = (long)base.Feedback;
					yield return null;
					int key = (int)base.Feedback;
					base.Operation.addressMessage = new ChannelServerAddress
					{
						Address = address,
						Port = port,
						ChannelID = channelID,
						Key = key
					};
				}
				yield break;
			}
		}
	}
}
