using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace WcfChatRelay.GuildChat
{
	public interface IServiceCallback
	{
		[OperationContract(IsOneWay = true)]
		void RefreshWebMember(long guildKey, IDictionary<long, string> members);

		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginJoinChatRoom(long guildKey, long cid, string sender, AsyncCallback callback, object asyncState);

		string EndJoinChatRoom(IAsyncResult result);

		[OperationContract(IsOneWay = true)]
		void LeaveChatRoom(long guildKey, long cid);

		[OperationContract(IsOneWay = true)]
		void SendChat(long guildKey, long cid, string sender, string message);

		[OperationContract(IsOneWay = true)]
		void WebClose();
	}
}
