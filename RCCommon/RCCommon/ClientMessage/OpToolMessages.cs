using System;
using System.Collections.Generic;

namespace RemoteControlSystem.ClientMessage
{
	public static class OpToolMessages
	{
		public static IEnumerable<Type> Types
		{
			get
			{
				yield return typeof(PingMessage);
				yield return typeof(LoginMessage);
				yield return typeof(LoginReply);
				yield return typeof(ClientAddedMessage);
				yield return typeof(ClientRemovedMessage);
				yield return typeof(ControlRequestMessage);
				yield return typeof(ControlReplyMessage);
				yield return typeof(ClientInfoMessage);
				yield return typeof(GetUserAuthMesssage);
				yield return typeof(GetUserAuthReply);
				yield return typeof(GetUserListMessage);
				yield return typeof(GetUserListReply);
				yield return typeof(AddUserMessage);
				yield return typeof(RemoveUserMessage);
				yield return typeof(ChangeMyPasswordMessage);
				yield return typeof(ChangePasswordMessage);
				yield return typeof(ChangeAuthorityMessage);
				yield return typeof(NotifyMessage);
				yield return typeof(ControlEnterMessage);
				yield return typeof(ControlEnterReply);
				yield return typeof(ControlFinishMessage);
				yield return typeof(WorkGroupChangeMessage);
				yield return typeof(ServerGroupChangeMessage);
				yield return typeof(TemplateChangeMessage);
				yield return typeof(EmergencyCallMessage);
				yield break;
			}
		}

		public static IDictionary<Type, int> TypeConverters
		{
			get
			{
				return OpToolMessages.Types.GetConverter(4096);
			}
		}
	}
}
