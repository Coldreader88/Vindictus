using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ExchangeMileageResultMessage : IMessage
	{
		private bool IsSuccess { get; set; }

		public ExchangeMileageResultMessage(bool isSuccess)
		{
			this.IsSuccess = isSuccess;
		}

		public override string ToString()
		{
			return string.Format("ExchangeMileageResultMessage IsSuccess {0}", this.IsSuccess);
		}
	}
}
