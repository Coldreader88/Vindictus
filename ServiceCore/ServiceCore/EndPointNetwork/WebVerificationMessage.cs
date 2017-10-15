using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class WebVerificationMessage : IMessage
	{
		public int SessionNo { get; set; }

		public bool IsSuccessfullyGenerated { get; private set; }

		public long Passcode { get; private set; }

		public WebVerificationMessage(int sessionNo, bool isSuccessfullyGenerated, long passcode)
		{
			this.SessionNo = sessionNo;
			this.IsSuccessfullyGenerated = isSuccessfullyGenerated;
			this.Passcode = passcode;
		}

		public override string ToString()
		{
			return string.Format("WebVerificationMessage [ {0} {1} {2} ]", this.SessionNo, this.IsSuccessfullyGenerated, this.Passcode);
		}
	}
}
