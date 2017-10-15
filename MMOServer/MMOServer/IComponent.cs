using System;

namespace MMOServer
{
	public interface IComponent
	{
		long ID { get; }

		string Category { get; }

		bool IsTemporal { get; }

		int SightDegree { get; }

		IMessage AppearMessage();

		IMessage DifferenceMessage();

		IMessage DisappearMessage();

		void Flatten();
	}
}
