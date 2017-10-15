using System;
using UnifiedNetwork.Entity;

namespace UnifiedNetwork.CacheSync
{
	public class ObserverFactory
	{
		public static IObserver MakeObserver(IEntityProxy conn, long id, string category)
		{
			return new Observer(conn as EntityConnection, new ObservableIdentifier(id, category));
		}
	}
}
