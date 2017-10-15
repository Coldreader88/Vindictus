using System;
using System.Collections.Generic;
using Devcat.Core;

namespace MMOServer
{
	public interface IComponentSpace
	{
		int Count(string category);

		IComponent Find(long id, string category);

		IEnumerable<IComponent> FindByID(long id);

		IEnumerable<KeyValuePair<long, HashSet<IComponent>>> TraverseByID();

		IEnumerable<IComponent> FindByCategory(string category);

		IComponent Set(IComponent component);

		bool Remove(IComponent component);

		void NotifyModified(IComponent component);

		event EventHandler<EventArgs<IComponent>> ComponentModified;
	}
}
