using System;

namespace Utility
{
	public interface IQueryPerformance
	{
		long GetEntityCount();

		long GetQueueLength();
	}
}
