using System;
using System.ServiceModel;

namespace WcfChatRelay.Whisper
{
	[ServiceContract(CallbackContract = typeof(IServiceCallback), SessionMode = SessionMode.Required)]
	public interface ITalkService
	{
		[OperationContract]
		void SubscribeService(string name);

		[OperationContract(IsOneWay = true)]
		void Whisper(string from, long fromCID, string to, string message);

		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginAsyncWhisper(string from, long fromCID, string to, string message, AsyncCallback callback, object asyncState);

		bool EndAsyncWhisper(IAsyncResult result);

		[OperationContract(IsOneWay = true)]
		void Ping();
	}
}
