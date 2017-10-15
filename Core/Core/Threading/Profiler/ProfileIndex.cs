using System;

namespace Devcat.Core.Threading.Profiler
{
	public struct ProfileIndex<IndexType>
	{
		public IndexType WaitTimeIndex { get; set; }

		public IndexType ProcessTimeIndex { get; set; }

		public IndexType DoneTimeIndex { get; set; }

		public int SampleCount { get; set; }
	}
}
