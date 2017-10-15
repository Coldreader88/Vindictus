using System;

namespace MMOServer
{
	public interface IChannel
	{
		object Tag { get; set; }

		bool AddPartition(string bsp, long id);

		bool AddRegion(string bsp);

		bool AddLink(string bsp, long l, long r);

		void ConfirmRegions(int sightDegree);

		bool AddComponent(IComponent newcomponent);

		bool ComponentModified(IComponent component);

		bool RemoveComponent(IComponent component);

		void SendToAllRegionMembers(IComponent component);

		bool AddObserver(long partitionID, ICamera observer);

		bool MoveObserver(long partitionID, ICamera observer);

		bool RemoveObserver(ICamera observer);

		bool ApplyModifier(IModifier modifier);
	}
}
