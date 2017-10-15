using System;
using System.Collections.Generic;
using Utility;

namespace MMOServer
{
	internal class Partition : IPartition
	{
		public Region Region { get; private set; }

		public long ID { get; private set; }

		public int SN { get; private set; }

		public Partition(Region region, long id, int sn)
		{
			this.Region = region;
			this.ID = id;
			this.SN = sn;
			this.linkedPartitions.Add(this);
		}

		public bool AddObserver(ICamera observer)
		{
			if (this.observers.Contains(observer))
			{
				return false;
			}
			this.observers.Add(observer);
			return true;
		}

		public bool RemoveObserver(ICamera observer)
		{
			return this.observers.Remove(observer);
		}

		public bool AddLink(Partition other)
		{
			if (this.linkedPartitions.Contains(other))
			{
				return false;
			}
			this.linkedPartitions.Add(other);
			if (!other.linkedPartitions.Contains(this))
			{
				other.linkedPartitions.Add(this);
			}
			return true;
		}

		public bool CanSee(Partition rhs)
		{
			return this.Equals(rhs) || this.linkedPartitions.Contains(rhs) || rhs.linkedPartitions.Contains(this);
		}

		public bool CanSee(Partition rhs, int degree)
		{
			return this.Region == rhs.Region && this.Region.CanSee(this.SN, rhs.SN, degree);
		}

		public IEnumerable<Partition> CanSeePartitions()
		{
			return this.linkedPartitions;
		}

		public IEnumerable<Partition> CanSeePartitions(int degree)
		{
			return this.Region.GetVisiblePartitions(this, degree);
		}

		public bool AddSightComponent(IComponent component)
		{
			Key key = component.GetKey();
			if (this.componentsInSight.ContainsKey(key))
			{
				return false;
			}
			this.componentsInSight.Add(key, component);
			return true;
		}

		public bool RemoveSightComponent(IComponent component)
		{
			Key key = component.GetKey();
			IComponent component2 = this.componentsInSight.TryGetValue(key);
			if (component2 == null || component2 != component)
			{
				return false;
			}
			this.componentsInSight.Remove(key);
			return true;
		}

		public IEnumerable<IComponent> GetSightComponents()
		{
			return this.componentsInSight.Values;
		}

		public bool IsInSight(IComponent component)
		{
			return this.componentsInSight.ContainsKey(component.GetKey());
		}

		public void Broadcast(IMessage msg)
		{
			if (msg == null)
			{
				return;
			}
			foreach (ICamera camera in this.observers)
			{
				camera.Update(msg);
			}
		}

		public void Broadcast(IEnumerable<IMessage> msgs)
		{
			foreach (ICamera camera in this.observers)
			{
				camera.Update(msgs);
			}
		}

		public override string ToString()
		{
			return string.Format("Partition {0}", this.ID);
		}

		private List<Partition> linkedPartitions = new List<Partition>();

		private HashSet<ICamera> observers = new HashSet<ICamera>();

		private Dictionary<Key, IComponent> componentsInSight = new Dictionary<Key, IComponent>();

		public static readonly Partition Nowhere = new Partition(Region.Nowhere, -1L, -1);
	}
}
