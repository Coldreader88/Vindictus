using System;

namespace Devcat.Core.Collections
{
	internal delegate bool TreeWalkAction<T>(TreeSet<T>.Node node);
}
