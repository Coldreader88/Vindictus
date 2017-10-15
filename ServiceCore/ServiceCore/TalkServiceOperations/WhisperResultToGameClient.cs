using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.TalkServiceOperations
{
	[Serializable]
	public class WhisperResultToGameClient : Operation
	{
		public long MyCID { get; set; }

		public int ResultNo { get; set; }

		public string ReceiverName { get; set; }

		public WhisperResultToGameClient(long myCid, int resultNo, string recieverName)
		{
			this.MyCID = myCid;
			this.ResultNo = resultNo;
			this.ReceiverName = recieverName;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
