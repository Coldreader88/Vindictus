using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class MailSentMessage : IMessage
	{
		public int ErrorCode { get; set; }

		public MailSentMessage(MailSentMessage.ErrorCodeEnum ErrorCode)
		{
			this.ErrorCode = (int)ErrorCode;
		}

		public override string ToString()
		{
			return string.Format("MailSentMessage[ ErrorCode = {0} ]", this.ErrorCode);
		}

		public enum ErrorCodeEnum
		{
			ReturnSuccessful = -2,
			Successful,
			TransFerError_Unknown,
			TransFerError_NoInventoryInfo,
			TransFerError_NoItemInfo,
			TransFerError_ImproperTradeLevel,
			TransFerError_EquippedItem,
			TransFerError_TrasferError,
			TransFerError_FeePayError,
			TransFerError_DBSubmitError,
			CIDQueryFailed,
			InvalidMailType,
			SendFailed,
			ReturnFailed,
			SelfCIDFailed
		}
	}
}
