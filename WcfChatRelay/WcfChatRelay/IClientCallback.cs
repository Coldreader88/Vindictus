using System;
using System.ServiceModel;

namespace WcfChatRelay
{
	public interface IClientCallback
	{
		[OperationContract(IsOneWay = true)]
		void GuildChatMessage(long guildKey, string sender, string message);

		[OperationContract(IsOneWay = true)]
		void GuildChatMemberInfo(long guildKey, string sender, bool isOnline);

		[OperationContract(IsOneWay = true)]
		void KickGuildChatMember(long guildKey, long cid);

		[OperationContract(IsOneWay = true)]
		void GuildServiceClose();

		[OperationContract(IsOneWay = true)]
		void Whisper(string from, long fromCID, string to, string message);

		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginAsyncWhisper(string from, long fromCID, string to, string message, AsyncCallback callback, object asyncState);

		bool EndAsyncWhisper(IAsyncResult result);
	}
}
