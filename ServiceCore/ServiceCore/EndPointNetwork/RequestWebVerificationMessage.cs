using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RequestWebVerificationMessage : IMessage
	{
		public int SessionNo { get; set; }

		public RequestWebVerificationMessage(int sessionNo)
		{
			this.SessionNo = sessionNo;
		}

		public override string ToString()
		{
			return string.Format("RequestWebVerificationMessage[ {0} ]", this.SessionNo);
		}
	}
}
