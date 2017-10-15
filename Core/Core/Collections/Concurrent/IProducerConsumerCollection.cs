using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Devcat.Core.Collections.Concurrent
{
	[ComVisible(true)]
	public interface IProducerConsumerCollection<T> : IEnumerable<T>, ICollection, IEnumerable
	{
		void CopyTo(T[] array, int index);

		T[] ToArray();

		bool TryAdd(T item);

		bool TryTake(out T item);
	}
}
