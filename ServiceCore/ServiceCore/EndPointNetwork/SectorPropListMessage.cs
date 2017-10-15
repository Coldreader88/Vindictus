using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SectorPropListMessage : IMessage
	{
		public IDictionary<int, int> Props { get; set; }

		public override string ToString()
		{
			return string.Format("SectorPropListMessage[ prop x {0} ]", this.Props.Count);
		}
	}
}
