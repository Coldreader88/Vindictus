using System;

namespace ServiceCore.EndPointNetwork.MicroPlay
{
	[Serializable]
	public sealed class AskFinishQuestMessage : IMessage
	{
		public bool ShowPendingDialog { get; set; }

		public string Message { get; set; }

		public int TimeOut { get; set; }

		public AskFinishQuestMessage(bool pending, string message, int timeOut)
		{
			this.ShowPendingDialog = pending;
			this.Message = message;
			this.TimeOut = timeOut;
		}

		public override string ToString()
		{
			return string.Format("AskFinishQuestMessage[ {0} ]", this.Message);
		}
	}
}
