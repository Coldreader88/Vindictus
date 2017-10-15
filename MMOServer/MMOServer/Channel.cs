using System;
using System.Collections.Generic;
using System.Linq;
using Devcat.Core;
using Utility;

namespace MMOServer
{
	public class Channel : IChannel
	{
		public IComponentSpace Space
		{
			get
			{
				return this.space;
			}
		}

		public object Tag { get; set; }

		public object Entity { get; set; }

		public Channel()
		{
			this.space.ComponentModified += this.space_ComponentModified;
		}

		public IEnumerable<string> GetRegionStrings()
		{
			return from x in this.regions.Values
			select x.ToString();
		}

		private void space_ComponentModified(object sender, EventArgs<IComponent> e)
		{
			this.ComponentModified(e.Value);
		}

		public IEnumerable<long> GetNeighborPartitions(long partitionID)
		{
			Partition cur;
			if (this.partitions.TryGetValue(partitionID, out cur))
			{
				foreach (Partition partition in cur.CanSeePartitions(1))
				{
					yield return partition.ID;
				}
			}
			yield break;
		}

		public virtual IComponent MakeLocation(long id, int sight, long pid)
		{
			Partition partition = this.partitions.TryGetValue(pid);
			if (partition == null)
			{
				partition = Partition.Nowhere;
			}
			return new Location
			{
				CurPartition = partition,
				NextPartition = partition,
				ID = id,
				SightDegree = sight
			};
		}

		public IModifier MakeMove(long id, long pid)
		{
			Partition partition = this.partitions.TryGetValue(pid);
			if (partition == null)
			{
				return null;
			}
			return new MovePartition(id, partition);
		}

		public bool AddRegion(string bsp)
		{
			if (this.regions.ContainsKey(bsp))
			{
				return false;
			}
			Region value = new Region(bsp);
			this.regions.Add(bsp, value);
			return true;
		}

		public bool AddPartition(string bsp, long id)
		{
			if (this.partitions.ContainsKey(id))
			{
				return false;
			}
			Region region = this.regions.TryGetValue(bsp);
			if (region == null)
			{
				return false;
			}
			Partition partition = region.AddPartition(id);
			if (partition != null)
			{
				this.partitions[id] = partition;
				return true;
			}
			return false;
		}

		public bool AddLink(string bsp, long l, long r)
		{
			Region region = this.regions.TryGetValue(bsp);
			return region != null && region.Link(l, r);
		}

		private void BuildEmptyRegion()
		{
			this.AddRegion(this.emptyRegionName);
			this.AddPartition(this.emptyRegionName, 0L);
		}

		public void ConfirmRegions(int sightDegree)
		{
			this.BuildEmptyRegion();
			foreach (Region region in this.regions.Values)
			{
				region.BuildVisibilityTable(sightDegree);
			}
		}

		public bool AddObserver(long partitionID, ICamera observer)
		{
			this.observers.Add(observer);
			Partition partition = this.partitions.TryGetValue(partitionID);
			if (partition == null)
			{
				return false;
			}
			if (!partition.AddObserver(observer))
			{
				return false;
			}
			observer.WatchingPartition = partitionID;
			foreach (IComponent component in partition.GetSightComponents())
			{
				IMessage message = component.AppearMessage();
				if (message != null)
				{
					observer.Update(message);
				}
			}
			return true;
		}

		public bool MoveObserver(long to, ICamera observer)
		{
			long watchingPartition = observer.WatchingPartition;
			Partition partition = this.partitions.TryGetValue(watchingPartition);
			if (partition == null)
			{
				return this.AddObserver(to, observer);
			}
			Partition partition2 = this.partitions.TryGetValue(to);
			if (partition2 == null)
			{
				return false;
			}
			if (!partition.RemoveObserver(observer))
			{
				return false;
			}
			if (!partition2.AddObserver(observer))
			{
				return false;
			}
			observer.WatchingPartition = to;
			foreach (IComponent component in partition.GetSightComponents())
			{
				if (!partition2.IsInSight(component))
				{
					IMessage message = component.DisappearMessage();
					if (message != null)
					{
						observer.Update(message);
					}
				}
			}
			foreach (IComponent component2 in partition2.GetSightComponents())
			{
				if (!partition.IsInSight(component2))
				{
					IMessage message2 = component2.AppearMessage();
					if (message2 != null)
					{
						observer.Update(message2);
					}
				}
			}
			return true;
		}

		public bool RemoveObserver(ICamera observer)
		{
			this.observers.Remove(observer);
			long watchingPartition = observer.WatchingPartition;
			Partition partition = this.partitions.TryGetValue(watchingPartition);
			return partition != null && partition.RemoveObserver(observer);
		}

		public void Broadcast(IMessage message)
		{
			foreach (Partition partition in this.partitions.Values)
			{
				partition.Broadcast(message);
			}
		}

		private void Broadcast(Partition origin, int degree, IEnumerable<IMessage> messages)
		{
			foreach (Partition partition in origin.CanSeePartitions(degree))
			{
				partition.Broadcast(messages);
			}
		}

		private void Broadcast(Partition origin, int degree, IMessage message)
		{
			foreach (Partition partition in origin.CanSeePartitions(degree))
			{
				partition.Broadcast(message);
			}
		}

		public void BroadcastToColhenLobby(IMessage message)
		{
			foreach (Partition partition in this.partitions.Values)
			{
				if (partition.ID == 6479215318592913461L || partition.ID == 6479215318592913462L || partition.ID == 6479215318592913463L)
				{
					partition.Broadcast(message);
				}
			}
		}

		private void BroadcastAppear(IComponent component, Partition curPartition)
		{
			IMessage message = component.AppearMessage();
			if (message == null)
			{
				return;
			}
			this.Broadcast(curPartition, component.SightDegree, message);
		}

		private void BroadcastDisappear(IComponent component, Partition curPartition)
		{
			IMessage message = component.DisappearMessage();
			if (message == null)
			{
				return;
			}
			this.Broadcast(curPartition, component.SightDegree, message);
		}

		private void BroadcastMove(Location location)
		{
			Partition curPartition = location.CurPartition;
			Partition nextPartition = location.NextPartition;
			IMessage message = location.DifferenceMessage();
			if (curPartition == nextPartition)
			{
				this.Broadcast(curPartition, location.SightDegree, message);
				return;
			}
			if (curPartition.Region != nextPartition.Region)
			{
				foreach (IComponent component in this.space.FindByID(location.ID))
				{
					this.BroadcastDisappear(component, curPartition);
					this.BroadcastAppear(component, nextPartition);
				}
				return;
			}
			MultiDictionary<int, IMessage> multiDictionary = new MultiDictionary<int, IMessage>();
			MultiDictionary<int, IMessage> multiDictionary2 = new MultiDictionary<int, IMessage>();
			int num = 0;
			foreach (IComponent component2 in this.space.FindByID(location.ID))
			{
				multiDictionary.Add(component2.SightDegree, component2.AppearMessage());
				multiDictionary2.Add(component2.SightDegree, component2.DisappearMessage());
				if (component2.SightDegree > num)
				{
					num = component2.SightDegree;
				}
			}
			foreach (Partition partition in curPartition.CanSeePartitions(num))
			{
				for (int i = 0; i <= num; i++)
				{
					HashSet<IMessage> hashSet = multiDictionary2[i];
					if (partition.CanSee(curPartition, i))
					{
						if (partition.CanSee(nextPartition, i))
						{
							if (i == location.SightDegree)
							{
								partition.Broadcast(message);
							}
						}
						else if (hashSet.Count > 0)
						{
							partition.Broadcast(hashSet);
						}
					}
				}
			}
			foreach (Partition partition2 in nextPartition.CanSeePartitions(num))
			{
				for (int j = 0; j <= num; j++)
				{
					HashSet<IMessage> hashSet2 = multiDictionary[j];
					if (hashSet2.Count > 0 && nextPartition.CanSee(partition2, j) && !partition2.CanSee(curPartition, j))
					{
						partition2.Broadcast(hashSet2);
					}
				}
			}
		}

		public bool AddComponent(IComponent newcomponent)
		{
			if (this.space.Find(newcomponent.ID, newcomponent.Category) != null)
			{
				return false;
			}
			IComponent component = this.space.Set(newcomponent);
			if (newcomponent is Location)
			{
				Location location = newcomponent as Location;
				foreach (IComponent component2 in this.space.FindByID(newcomponent.ID))
				{
					this.BroadcastAppear(component2, location.CurPartition);
				}
				if (component is Location)
				{
					this.ProcessPartitionMove(newcomponent.ID, (component as Location).CurPartition, location.CurPartition);
				}
				else
				{
					this.ProcessPartitionAppear(newcomponent.ID, location.CurPartition);
				}
			}
			else
			{
				Location location2 = this.space.Find(newcomponent.ID, "Location") as Location;
				if (location2 != null)
				{
					this.BroadcastAppear(newcomponent, location2.CurPartition);
					if (!newcomponent.IsTemporal)
					{
						foreach (Partition partition in location2.NextPartition.CanSeePartitions(newcomponent.SightDegree))
						{
							partition.AddSightComponent(newcomponent);
						}
					}
				}
				if (newcomponent.IsTemporal)
				{
					this.space.Remove(newcomponent);
				}
			}
			return true;
		}

		public void SendToAllRegionMembers(IComponent component)
		{
			Location location = this.space.Find(component.ID, "Location") as Location;
			if (location != null)
			{
				location.CurPartition.Region.SendToAllRegionMembers(component);
			}
		}

		private void ProcessPartitionDisappear(long id, Partition partition)
		{
			IEnumerable<IComponent> source = this.space.FindByID(id);
			IEnumerable<IGrouping<int, IComponent>> enumerable = from x in source
			group x by x.SightDegree;
			foreach (IGrouping<int, IComponent> grouping in enumerable)
			{
				foreach (Partition partition2 in partition.CanSeePartitions(grouping.Key))
				{
					foreach (IComponent component in grouping)
					{
						partition2.RemoveSightComponent(component);
					}
				}
			}
		}

		private void ProcessPartitionAppear(long id, Partition partition)
		{
			IEnumerable<IComponent> source = this.space.FindByID(id);
			IEnumerable<IGrouping<int, IComponent>> enumerable = from x in source
			group x by x.SightDegree;
			foreach (IGrouping<int, IComponent> grouping in enumerable)
			{
				foreach (Partition partition2 in partition.CanSeePartitions(grouping.Key))
				{
					foreach (IComponent component in grouping)
					{
						partition2.AddSightComponent(component);
					}
				}
			}
		}

		private void ProcessPartitionMove(long id, Partition oldp, Partition newp)
		{
			if (oldp.Region != newp.Region)
			{
				this.ProcessPartitionDisappear(id, oldp);
				this.ProcessPartitionAppear(id, newp);
				return;
			}
			IEnumerable<IComponent> source = this.space.FindByID(id);
			IEnumerable<IGrouping<int, IComponent>> enumerable = from x in source
			group x by x.SightDegree;
			foreach (IGrouping<int, IComponent> grouping in enumerable)
			{
				foreach (Partition partition in oldp.CanSeePartitions(grouping.Key))
				{
					if (!newp.CanSeePartitions(grouping.Key).Contains(partition))
					{
						foreach (IComponent component in grouping)
						{
							partition.RemoveSightComponent(component);
						}
					}
				}
				foreach (Partition partition2 in newp.CanSeePartitions(grouping.Key))
				{
					if (!oldp.CanSeePartitions(grouping.Key).Contains(partition2))
					{
						foreach (IComponent component2 in grouping)
						{
							partition2.AddSightComponent(component2);
						}
					}
				}
			}
		}

		public bool ComponentModified(IComponent component)
		{
			if (component == null)
			{
				return false;
			}
			Location location = this.space.Find(component.ID, "Location") as Location;
			if (component == location)
			{
				this.BroadcastMove(location);
				this.ProcessPartitionMove(component.ID, location.CurPartition, location.NextPartition);
			}
			else if (location != null)
			{
				IMessage message = component.DifferenceMessage();
				if (message == null)
				{
					return true;
				}
				this.Broadcast(location.CurPartition, component.SightDegree, message);
			}
			component.Flatten();
			return true;
		}

		public bool RemoveComponent(IComponent component)
		{
			Location location = this.space.Find(component.ID, "Location") as Location;
			if (!this.space.Remove(component))
			{
				return false;
			}
			if (location == null)
			{
				return true;
			}
			this.BroadcastDisappear(component, location.CurPartition);
			if (component == location)
			{
				this.ProcessPartitionDisappear(location.ID, location.CurPartition);
			}
			else
			{
				foreach (Partition partition in location.CurPartition.CanSeePartitions(component.SightDegree))
				{
					partition.RemoveSightComponent(component);
				}
			}
			return true;
		}

		public bool ApplyModifier(IModifier modifier)
		{
			if (modifier == null)
			{
				return false;
			}
			modifier.Space = this.space;
			modifier.Apply();
			return true;
		}

		private ComponentSpace space = new ComponentSpace();

		private Dictionary<string, Region> regions = new Dictionary<string, Region>();

		private Dictionary<long, Partition> partitions = new Dictionary<long, Partition>();

		private HashSet<ICamera> observers = new HashSet<ICamera>();

		private readonly string emptyRegionName = "[Empty]";
	}
}
