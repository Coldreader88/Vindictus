using System;

namespace Devcat.Core.Collections
{
	public interface IComparer<T, U>
	{
		int Compare(T x, U y);
	}
}
