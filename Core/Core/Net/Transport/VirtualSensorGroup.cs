using System;

namespace Devcat.Core.Net.Transport
{
	public class VirtualSensorGroup
	{
		public VirtualSensorGroup(VirtualServer server, Station station)
		{
			this.vcGroup = server.CreateVirtualClientGroup();
			this.station = station;
			this.sensor = new Sensor(this.vcGroup);
			station.Add(this.sensor);
		}

		public void Destroy()
		{
			this.station.Remove(this.sensor);
			this.station = null;
			this.sensor = null;
			this.vcGroup.Destroy();
			this.vcGroup = null;
		}

		public void Add(VirtualSensor virtualSensor)
		{
			VirtualClient virtualClient = virtualSensor.VirtualClient;
			this.vcGroup.Add(virtualClient);
			this.station.TransmitPermanentAppearance(virtualClient, virtualSensor.GetVersion(this.station.ID));
			this.station.TransmitInstantAppearance(virtualClient);
		}

		public void Remove(VirtualSensor virtualSensor)
		{
			VirtualClient virtualClient = virtualSensor.VirtualClient;
			this.vcGroup.Remove(virtualClient);
			this.station.TransmitInstantDisappearance(virtualClient);
			virtualSensor.SetVersion(this.station.ID, this.station.PermanentAppearanceVersion);
		}

		private Sensor sensor;

		private Station station;

		private VirtualClientGroup vcGroup;
	}
}
