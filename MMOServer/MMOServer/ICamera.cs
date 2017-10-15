using System;
using System.Collections.Generic;

namespace MMOServer
{
	public interface ICamera
	{
		long ID { get; }

		long WatchingPartition { get; set; }

		void Update(IMessage message);

		void Update(IEnumerable<IMessage> messages);

		void Flush();
	}
}
