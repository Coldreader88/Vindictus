using System;
using MMOServer;

namespace MMOServerTest
{
	internal class Position2DUpdated : IMessage
	{
		public long ID { get; set; }

		public int X { get; set; }

		public int Y { get; set; }
	}
}
