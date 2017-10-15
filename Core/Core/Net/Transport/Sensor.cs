using System;
using System.Collections.Generic;
using System.Threading;

namespace Devcat.Core.Net.Transport
{
	public class Sensor
	{
		public int ID
		{
			get
			{
				return this.id;
			}
		}

		[Obsolete("Renamed. Use Transmitter instead.")]
		public IPacketTransmitter Adapter
		{
			get
			{
				return this.transmitter;
			}
		}

		public IPacketTransmitter Transmitter
		{
			get
			{
				return this.transmitter;
			}
		}

		public Sensor(IPacketTransmitter transmitter)
		{
			this.transmitter = transmitter;
			this.versionList = new SortedDictionary<int, long>();
			this.id = Interlocked.Increment(ref Sensor.globalID);
		}

		internal void SetVersion(int stationID, long version)
		{
			if (this.versionList.ContainsKey(stationID))
			{
				this.versionList[stationID] = version;
				return;
			}
			lock (this.versionList)
			{
				this.versionList[stationID] = version;
			}
		}

		internal long GetVersion(int stationID)
		{
			long result;
			this.versionList.TryGetValue(stationID, out result);
			return result;
		}

		private static int globalID;

		private int id;

		private IPacketTransmitter transmitter;

		private SortedDictionary<int, long> versionList;
	}
}
