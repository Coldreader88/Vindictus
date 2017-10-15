using System;

namespace Devcat.Core.Net
{
	public interface ITransmitter<T>
	{
		void Transmit(T data);
	}
}
