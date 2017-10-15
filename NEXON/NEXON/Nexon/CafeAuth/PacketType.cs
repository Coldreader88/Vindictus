using System;

namespace Nexon.CafeAuth
{
	internal enum PacketType : byte
	{
		Initialize = 41,
		Login,
		Logout,
		Terminate,
		Message,
		Synchronize,
		Alive = 100
	}
}
