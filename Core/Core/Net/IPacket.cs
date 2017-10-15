using System;

namespace Devcat.Core.Net
{
	public interface IPacket
	{
		ArraySegment<byte> Bytes { get; }

		void Encrypt(ICryptoTransform crypto);
	}
}
