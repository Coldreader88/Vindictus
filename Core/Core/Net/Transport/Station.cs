using System;
using System.Collections;
using System.Collections.Generic;
using Devcat.Core.Collections;
using Devcat.Core.Math;
using Devcat.Core.Net.Message;

namespace Devcat.Core.Net.Transport
{
	public class Station
	{
		public int ID
		{
			get
			{
				return this.id;
			}
		}

		public long PermanentAppearanceVersion
		{
			get
			{
				return this.paVersion;
			}
		}

		public Station(int id)
		{
			this.id = id;
			this.sensorList = new SortedDictionary<int, Sensor>();
			this.reservedSensors = new WriteFreeQueue2<KeyValuePair<Sensor, bool>>();
			this.paHistory = new Station.PAHistory();
			this.paHistory.PacketList = new List<Packet>();
		}

		public void Add(Substance substance)
		{
			if (substance.Station != null)
			{
				throw new InvalidOperationException("Substance can be registered to one station at the same time.");
			}
			substance.Station = this;
			if (substance.UseInstantAppearance)
			{
				this.ProcessReservedRequest();
				SubstanceListElement substanceListElement = new SubstanceListElement(substance);
				substanceListElement.Next = this.firstElement;
				if (this.firstElement != null)
				{
					this.firstElement.Previous = substanceListElement;
				}
				substance.SubstanceListElement = substanceListElement;
				this.firstElement = substanceListElement;
				foreach (Sensor sensor in this.sensorList.Values)
				{
					sensor.Transmitter.Transmit(substance.Adapter.InstantAppearMessage);
				}
			}
		}

		public void Remove(Substance substance)
		{
			if (substance.Station != this)
			{
				if (substance.Station != null)
				{
					throw new InvalidOperationException("Substance is not registered on this station.");
				}
			}
			else
			{
				substance.Station = null;
				if (substance.UseInstantAppearance)
				{
					this.ProcessReservedRequest();
					if (this.firstElement == substance.SubstanceListElement)
					{
						this.firstElement = substance.SubstanceListElement.Next;
						if (this.firstElement != null)
						{
							this.firstElement.Previous = null;
						}
					}
					else
					{
						if (substance.SubstanceListElement.Previous != null)
						{
							substance.SubstanceListElement.Previous.Next = substance.SubstanceListElement.Next;
						}
						if (substance.SubstanceListElement.Next != null)
						{
							substance.SubstanceListElement.Next.Previous = substance.SubstanceListElement.Previous;
						}
					}
					substance.SubstanceListElement = null;
					foreach (Sensor sensor in this.sensorList.Values)
					{
						sensor.Transmitter.Transmit(substance.Adapter.InstantDisappearMessage);
					}
				}
			}
		}

		public void Add(Sensor sensor)
		{
			this.reservedSensors.Enqueue(new KeyValuePair<Sensor, bool>(sensor, true));
			this.TransmitPermanentAppearance(sensor.Transmitter, sensor.GetVersion(this.id));
			this.TransmitInstantAppearance(sensor.Transmitter);
		}

		public void Remove(Sensor sensor)
		{
			this.reservedSensors.Enqueue(new KeyValuePair<Sensor, bool>(sensor, false));
			this.TransmitInstantDisappearance(sensor.Transmitter);
		}

		public void SetAppearanceHistory(long startVersion, IEnumerable<Packet> history)
		{
			Station.PAHistory pahistory = new Station.PAHistory();
			pahistory.StartVersion = startVersion;
			pahistory.PacketList = new List<Packet>();
			pahistory.PacketList.AddRange(history);
			this.paHistory = pahistory;
			this.paVersion = startVersion + (long)pahistory.PacketList.Count;
		}

		public void RemoveAppearanceHistory(long newStartVersion)
		{
			int x = (int)(this.paVersion - newStartVersion);
			Station.PAHistory pahistory = new Station.PAHistory();
			pahistory.StartVersion = newStartVersion;
			pahistory.PacketList = new List<Packet>((int)BitOperation.SmallestPow2((uint)x));
			for (int i = (int)(newStartVersion - this.paHistory.StartVersion); i < this.paHistory.PacketList.Count; i++)
			{
				pahistory.PacketList.Add(this.paHistory[(long)i]);
			}
		}

		internal void Broadcast(Packet packet)
		{
			this.ProcessReservedRequest();
			foreach (Sensor sensor in this.sensorList.Values)
			{
				sensor.Transmitter.Transmit(packet);
			}
		}

		internal void Mutate(Packet permanentAppearance)
		{
			this.paHistory.PacketList.Add(permanentAppearance);
			this.Broadcast(permanentAppearance);
			this.paVersion += 1L;
		}

		internal void TransmitInstantAppearance(ITransmitter<IEnumerable<Packet>> transmitter)
		{
			if (Station.paListEnumerable == null)
			{
				Station.paListEnumerable = new Station.PAListEnumerable();
			}
			Station.paListEnumerable.Reset(this.firstElement, true);
			transmitter.Transmit(Station.paListEnumerable);
		}

		internal void TransmitInstantDisappearance(ITransmitter<IEnumerable<Packet>> transmitter)
		{
			if (Station.paListEnumerable == null)
			{
				Station.paListEnumerable = new Station.PAListEnumerable();
			}
			Station.paListEnumerable.Reset(this.firstElement, false);
			transmitter.Transmit(Station.paListEnumerable);
		}

		internal void TransmitPermanentAppearance(ITransmitter<IEnumerable<Packet>> transmitter, long oldPAVersion)
		{
			if (Station.paEnumerable == null)
			{
				Station.paEnumerable = new Station.PAEnumerable();
			}
			Station.paEnumerable.Reset(this.paHistory, oldPAVersion);
			transmitter.Transmit(Station.paEnumerable);
		}

		private void ProcessReservedRequest()
		{
			KeyValuePair<Sensor, bool> keyValuePair;
			while (this.reservedSensors.TryDequeue(out keyValuePair))
			{
				if (keyValuePair.Value)
				{
					this.sensorList.Add(keyValuePair.Key.ID, keyValuePair.Key);
				}
				else
				{
					keyValuePair.Key.SetVersion(this.id, this.paVersion);
					this.sensorList.Remove(keyValuePair.Key.ID);
				}
			}
		}

		public void Clear()
		{
			this.reservedSensors.Clear();
			this.sensorList.Clear();
			for (SubstanceListElement next = this.firstElement; next != null; next = next.Next)
			{
				next.Substance.Station = null;
				if (next.Previous != null)
				{
					next.Previous.Next = null;
					next.Previous = null;
				}
			}
			this.firstElement = null;
		}

		private int id;

		private SortedDictionary<int, Sensor> sensorList;

		private WriteFreeQueue2<KeyValuePair<Sensor, bool>> reservedSensors;

		private SubstanceListElement firstElement;

		private long paVersion;

		private Station.PAHistory paHistory;

		[ThreadStatic]
		private static Station.PAEnumerable paEnumerable;

		[ThreadStatic]
		private static Station.PAListEnumerable paListEnumerable;

		private class PAHistory
		{
			public Packet this[long version]
			{
				get
				{
					return this.packetList[(int)(version - this.startVersion)];
				}
			}

			public long StartVersion
			{
				get
				{
					return this.startVersion;
				}
				set
				{
					this.startVersion = value;
				}
			}

			public List<Packet> PacketList
			{
				get
				{
					return this.packetList;
				}
				set
				{
					this.packetList = value;
				}
			}

			private long startVersion;

			private List<Packet> packetList;
		}

		private class PAEnumerable : IEnumerable<Packet>, IEnumerable, IEnumerator<Packet>, IDisposable, IEnumerator
		{
			public IEnumerator<Packet> GetEnumerator()
			{
				return this;
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return this;
			}

			public Packet Current
			{
				get
				{
					return this.paHistory.PacketList[this.index];
				}
			}

			public void Dispose()
			{
			}

			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			public bool MoveNext()
			{
				return this.index < this.paHistory.PacketList.Count && ++this.index < this.paHistory.PacketList.Count;
			}

			public void Reset()
			{
			}

			public void Reset(Station.PAHistory paHistory, long startVersion)
			{
				this.paHistory = paHistory;
				this.index = (int)(startVersion - paHistory.StartVersion);
				if (this.index < 0)
				{
					this.index = 0;
				}
				this.index--;
			}

			private int index;

			private Station.PAHistory paHistory;
		}

		private class PAListEnumerable : IEnumerable<Packet>, IEnumerable, IEnumerator<Packet>, IDisposable, IEnumerator
		{
			public IEnumerator<Packet> GetEnumerator()
			{
				return this;
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return this;
			}

			public Packet Current
			{
				get
				{
					return this.message.Current;
				}
			}

			public void Dispose()
			{
			}

			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			public bool MoveNext()
			{
				while (this.message == null || !this.message.MoveNext())
				{
					if (this.element == null)
					{
						return false;
					}
					if (this.appear)
					{
						this.message = this.element.Substance.Adapter.InstantAppearMessage.GetEnumerator();
					}
					else
					{
						this.message = this.element.Substance.Adapter.InstantDisappearMessage.GetEnumerator();
					}
					this.element = this.element.Next;
				}
				return true;
			}

			public void Reset()
			{
			}

			public void Reset(SubstanceListElement firstElement, bool appear)
			{
				this.element = firstElement;
				this.message = null;
				this.appear = appear;
			}

			private SubstanceListElement element;

			private IEnumerator<Packet> message;

			private bool appear;
		}
	}
}
