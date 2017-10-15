using System;
using System.Collections.Generic;
using Devcat.Core;
using Utility;

namespace MMOServer
{
	internal class ComponentSpace : IComponentSpace
	{
		public int Count(string category)
		{
			return this.categoryIndex[category].Count;
		}

		public IComponent Find(long id, string category)
		{
			Key key = new Key(id, category);
			IComponent result;
			this.components.TryGetValue(key, out result);
			return result;
		}

		public IEnumerable<IComponent> FindByID(long id)
		{
			return this.idIndex[id];
		}

		public IEnumerable<KeyValuePair<long, HashSet<IComponent>>> TraverseByID()
		{
			return this.idIndex.KeySets;
		}

		public IEnumerable<IComponent> FindByCategory(string category)
		{
			return this.categoryIndex[category];
		}

		public IComponent Set(IComponent component)
		{
			IComponent component2 = this.components.TryGetValue(component.GetKey());
			if (component2 != null)
			{
				this.idIndex.Remove(component.ID, component2);
				this.categoryIndex.Remove(component.Category, component2);
			}
			this.components[component.GetKey()] = component;
			this.idIndex.Add(component.ID, component);
			this.categoryIndex.Add(component.Category, component);
			return component2;
		}

		public bool Remove(IComponent component)
		{
			if (!this.components.Remove(component.GetKey()))
			{
				return false;
			}
			this.idIndex.Remove(component.ID, component);
			this.categoryIndex.Remove(component.Category, component);
			return true;
		}

		public void NotifyModified(IComponent component)
		{
			if (this.ComponentModified != null)
			{
				this.ComponentModified(this, new EventArgs<IComponent>(component));
			}
		}

		public event EventHandler<EventArgs<IComponent>> ComponentModified;

		private Dictionary<Key, IComponent> components = new Dictionary<Key, IComponent>();

		private MultiDictionary<long, IComponent> idIndex = new MultiDictionary<long, IComponent>();

		private MultiDictionary<string, IComponent> categoryIndex = new MultiDictionary<string, IComponent>();
	}
}
