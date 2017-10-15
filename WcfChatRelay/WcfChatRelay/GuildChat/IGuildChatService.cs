using System;
using System.ServiceModel;

namespace WcfChatRelay.GuildChat
{
	[ServiceContract(CallbackContract = typeof(IServiceCallback), SessionMode = SessionMode.Required)]
	public interface IGuildChatService
	{
		[OperationContract]
		void SubscribeService(string name);

		[OperationContract(IsOneWay = true)]
		void MemberInfo(long guildKey, string sender, bool isOnline);

		[OperationContract(IsOneWay = true)]
		void ChatMessage(long guildKey, string sender, string message);

		[OperationContract(IsOneWay = true)]
		void KickMember(long guildKey, long cid);

		[OperationContract(IsOneWay = true)]
		void Ping();

		[OperationContract(IsOneWay = true)]
		void RequestMemberInfos(long guildKey);
	}
}
