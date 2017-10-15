using System;

namespace MMOServer
{
	internal interface IPartition
	{
		long ID { get; }

		Region Region { get; }
	}
}
