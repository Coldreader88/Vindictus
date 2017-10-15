using System;

namespace Devcat.Core.Threading.Profiler
{
	public interface IProfilePolicy
	{
		void JobEnqueue(JobProfileElement element);

		void JobDone(JobProfileElement element);

		void Calculate();

		void Flush();

		ValueType LastValue { get; }

		event EventHandler<EventArgs<ValueType>> Calculated;
	}
}
