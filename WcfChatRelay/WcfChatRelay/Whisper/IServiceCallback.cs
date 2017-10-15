using System;
using System.ServiceModel;

namespace WcfChatRelay.Whisper
{
	public interface IServiceCallback
	{
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginAsyncWhisperResultFromApp(long toCID, int resultNo, string receiverName, AsyncCallback callback, object asyncState);

		bool EndAsyncWhisperResultFromApp(IAsyncResult result);

		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginAsyncWhisperFromApp(string from, long toCID, string message, AsyncCallback callback, object asyncState);

		bool EndAsyncWhisperFromApp(IAsyncResult result);

		[OperationContract(IsOneWay = true)]
		void WhisperFromApp(string from, long toCID, string message);

		[OperationContract(IsOneWay = true)]
		void WebClose();
	}
}
