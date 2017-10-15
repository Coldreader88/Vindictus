using System;
using Devcat.Core.Net.Message;

namespace Devcat.Core.Net.Transport
{
	public class Substance
	{
		public ISubstanceAdapter Adapter
		{
			get
			{
				return this.adapter;
			}
		}

		public Station Station
		{
			get
			{
				return this.station;
			}
			internal set
			{
				this.station = value;
			}
		}

		public bool UseInstantAppearance
		{
			get
			{
				return this.useInstantAppearance;
			}
		}

		internal SubstanceListElement SubstanceListElement
		{
			get
			{
				return this.substanceListElement;
			}
			set
			{
				this.substanceListElement = value;
			}
		}

		public Substance(ISubstanceAdapter substance) : this(substance, true)
		{
		}

		public Substance(ISubstanceAdapter adapter, bool useInstantAppearance)
		{
			this.adapter = adapter;
			this.useInstantAppearance = useInstantAppearance;
		}

		public void Notify(Packet instantMessage)
		{
			if (this.station != null)
			{
				this.station.Broadcast(instantMessage);
			}
		}

		public void Mutate(Packet permanentMessage)
		{
			if (this.station != null)
			{
				this.station.Mutate(permanentMessage);
			}
		}

		private ISubstanceAdapter adapter;

		private Station station;

		private SubstanceListElement substanceListElement;

		private bool useInstantAppearance;
	}
}
