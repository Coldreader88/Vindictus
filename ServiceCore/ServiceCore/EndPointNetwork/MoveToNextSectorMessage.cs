using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class MoveToNextSectorMessage : IMessage
	{
		public string TriggerName { get; set; }

		public int TargetGroup { get; set; }

		public List<int> HolyProps { get; set; }

		public override string ToString()
		{
			return string.Format("MoveToNextSectorMessage [ {0} {1} {2} ea holyprops]", this.TriggerName, this.TargetGroup, this.HolyProps.Count);
		}
	}
}
