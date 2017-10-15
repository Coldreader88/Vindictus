using System;

namespace Devcat.Core.Net.Transport
{
	internal class SubstanceListElement
	{
		public SubstanceListElement(Substance substance)
		{
			this.Substance = substance;
		}

		public SubstanceListElement Previous;

		public SubstanceListElement Next;

		public Substance Substance;
	}
}
