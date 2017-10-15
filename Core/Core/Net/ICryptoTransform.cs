using System;

namespace Devcat.Core.Net
{
	public interface ICryptoTransform
	{
		bool Encrypt(ArraySegment<byte> buffer, out long salt);

		void Decrypt(ArraySegment<byte> buffer);

		bool Decrypt(ArraySegment<byte> buffer, long salt);
	}
}
