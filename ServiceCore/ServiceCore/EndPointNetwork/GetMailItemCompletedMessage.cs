using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class GetMailItemCompletedMessage : IMessage
	{
		public long MailID { get; set; }

		public byte Result { get; set; }

		public GetMailItemCompletedMessage(long MailID, GetMailItemCompletedMessage.ResultEnum Result)
		{
			this.MailID = MailID;
			this.Result = (byte)Result;
		}

		public override string ToString()
		{
			return string.Format("GetMailItemCompletedMessage[ ]", new object[0]);
		}

		public enum ResultEnum
		{
			Success,
			NoItem,
			NoEmptySlot,
			UniqueViolation,
			FeePayError,
			GetItemNotAllowed,
			GiveItemFail,
			ItemTypeMixed,
			QueryMailFail,
			Unknown
		}
	}
}
