using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class NpcListMessage : IMessage
	{
		public ICollection<BuildingInfo> Buildings { get; private set; }

		public NpcListMessage(ICollection<BuildingInfo> locations)
		{
			this.Buildings = locations;
		}

		public override string ToString()
		{
			return string.Format("NpcListMessage[ location x {0} ]", this.Buildings.Count);
		}
	}
}
