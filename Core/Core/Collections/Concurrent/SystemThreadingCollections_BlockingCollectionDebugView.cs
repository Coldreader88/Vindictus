using System;
using System.Diagnostics;

namespace Devcat.Core.Collections.Concurrent
{
	internal sealed class SystemThreadingCollections_BlockingCollectionDebugView<T>
	{
		public SystemThreadingCollections_BlockingCollectionDebugView(BlockingCollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.m_blockingCollection = collection;
		}

		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this.m_blockingCollection.ToArray();
			}
		}

		private BlockingCollection<T> m_blockingCollection;
	}
}
