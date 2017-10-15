using System;

namespace Devcat.Core.Net.Transport
{
	internal enum TransrouterMessage
	{
		Invalid,
		ClientConnect,
		ClientDisconnect,
		SingleTransferMessage,
		GroupTransferMessage,
		CreateClientGroup,
		DestroyClientGroup,
		AddClientGroupMember,
		RemoveClientGroupMember,
		ConnectClient,
		DisconnectClient
	}
}
