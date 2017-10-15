using System;
using System.ServiceModel;

namespace WcfChatRelay
{
	[ServiceContract(CallbackContract = typeof(IClientCallback), SessionMode = SessionMode.Required)]
	public interface IChatRelayClient
	{
		[OperationContract]
		void SubscribeClient(string name);

		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginRequestJoinChatRoom(long guildKey, long cid, string name, AsyncCallback callback, object asyncState);

		string EndRequestJoinChatRoom(IAsyncResult result);

		[OperationContract(IsOneWay = true)]
		void RequestLeaveChatRoom(long guildKey, long cid);

		[OperationContract(IsOneWay = true)]
		void RequestSendChat(long guildKey, long cid, string sender, string message);

		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginAsyncWhisperResultFromApp(long toCID, int resultNo, string receiverName, AsyncCallback callback, object asyncState);

		bool EndAsyncWhisperResultFromApp(IAsyncResult result);

		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginAsyncWhisperFromApp(string from, long toCID, string message, AsyncCallback callback, object asyncState);

		bool EndAsyncWhisperFromApp(IAsyncResult result);

		[OperationContract(IsOneWay = true)]
		void WhisperFromApp(string from, long toCID, string message);

		[OperationContract(IsOneWay = true)]
		void Ping();
	}
}
